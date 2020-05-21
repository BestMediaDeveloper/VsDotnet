//
// Copyright (C) 2020 BestMedia.  All rights reserved.
//
// The information and source code contained herein is the exclusive property of BestMedia and may not be disclosed, 
// examined or reproduced in whole or in part without explicit written authorization from the company.
//
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BestMedia.VsDotnet.VsUtility;
using BestMedia.VsDotnet.vs_connect_api_v3;


namespace BestMedia.VsDotnet
{

    /// <summary>
    /// Base class to implement the shared part between server and client in VSConnect
    /// </summary>
    public class VsConnectBase
    {
        protected VscLinkConnectedFunc_t linkConnectCallback = null;
        protected VscLinkDisconnectedFunc_t linkdisconnectCallback = null;
        protected VscProcessContractRequestFunc_t contractRequestCallback = null;
        protected VscContractCanceledFunc_t contractCanceledCallback = null;
        protected VscSendUpdateFunc_t sendUpdateCallback = null;
        protected VscReceiveUpdateFunc_t receiveUpdateCallback = null;
        //VscPingResultsFunc_t pingResultsCallback = null;


        [System.Runtime.InteropServices.DllImport("msvcrt")]
        unsafe static extern int vsprintf(byte[] str, string format, IntPtr args);

        /// <summary>
        /// Callback used by VS Connect to report log messages to this application.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="vscNode"></param>
        /// <param name="vscLink"></param>
        /// <param name="vscContract"></param>
        /// <param name="format"></param>
        /// <param name="argptr"></param>
        /// <returns></returns>
        protected unsafe int _VscLogCallback(VscLogLevel logLevel
                            , VscNode vscNode
                            , VscLink vscLink
                            , VscContract vscContract
                            , string format
                            , IntPtr argptr)
        {


            string logMessage = $"VS Connect ";
            logMessage += logLevel == VscLogLevel.ERROR ? "ERROR" : (logLevel == VscLogLevel.WARNING ? "WARNING" : "");
            logMessage += string.Format("N:{0:x} L:{1:x} C:{2:x}", vscNode.ptr.ToInt64(), vscLink.ptr.ToInt64(), vscContract.ptr.ToInt64());


            int BUFF_SIZE = 1024;
            byte[] tempBuff = new byte[BUFF_SIZE];


            int charsOutput2 = vsprintf(tempBuff, format, argptr);

            //check(charsOutput2 > 0 );

            logMessage += System.Text.Encoding.UTF8.GetString(tempBuff, 0, charsOutput2);

            if (VscLogLevel.ERROR == logLevel)
            {
                LogError(logMessage);
            }
            else if (VscLogLevel.WARNING == logLevel)
            {
                LogWarning(logMessage);
            }
            else
            {
                Log(logMessage);
            }

            return logMessage.Length;
        }



        virtual protected VscResult ShouldProcessCallback(VsConnectNode node, VscLink link)
        {
            VscResult retRes;

            if (node == null || link == IntPtr.Zero)
            {
                retRes = VscResult.ERROR_INVALID_PARAM;
            }
            else if (node.ShuttingDown)
            {
                retRes = VscResult.ERROR_UNAVAILABLE;
            }
            else
            {
                retRes = VscResult.OK;
            }

            return retRes;
        }



        /// <summary>
        /// Callback that informs us when a Link is connected.
        /// </summary>
        /// <param name="connectingLink"></param>
        /// <returns></returns>
        virtual protected VscResult LinkConnectCallback(VscLink connectingLink)
        {

            VscResult retRes = VscResult.OK;

            Log($"VS Connect Link connecting: {connectingLink.ptr.ToInt64():x}.\n");

            VsConnectNode node = VsConnectNode.GetInstanceFromLink(connectingLink);

            retRes = ShouldProcessCallback(node, connectingLink);

            return retRes;
        }


        /// <summary>
        /// Callback that informs us when a Link becomes disconnected.
        /// </summary>
        /// <param name="disconnectedLink"></param>
        virtual protected void LinkDisconnectCallback(VscLink link)
        {
            Log($"VS Connect Link disconnected: {link.ptr.ToInt64():x}.\n");

            var contract = vsc_Api_V3_t.Link_GetTsContract(link);

            if (contract != IntPtr.Zero)
            {
                var localRole = vsc_Api_V3_t.Contract_GetLocalRole(contract);

                if (localRole == VscRole.RECEIVER)
                {
                    // Our Incoming TS Contract is being canceled. Ensure
                    // time dilation is set back to its original value:
                    //var unode = UVsConnectNode::GetInstanceFromLink(link);
                    //var uVsConnect = Cast<UVsConnect>(unode->GetOuter());
                    //var worldSettings = uVsConnect->mWorld->GetWorldSettings();
                    //if (worldSettings) uVsConnect->RestoreOriginalTimeDilation(worldSettings);

                }
            }
        }


        /// <summary>
        /// Called by VS Connect when it needs new data to send.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="contract"></param>
        /// <param name="out_data"></param>
        /// <returns></returns>
        unsafe virtual protected VscResult SendCallback(VscLink link, VscContract contract, VscUpdateData out_data)
        {
            VscResult retRes;

            var node = VsConnectNode.GetInstanceFromLink(link);

            retRes = ShouldProcessCallback(node, link);

            if (!retRes.IsError())
            {
                VscSchema schema = vsc_Api_V3_t.Contract_GetSchema(contract);
                var numSchemaFields = vsc_Api_V3_t.Schema_GetNumFields(schema);
                var localRole = vsc_Api_V3_t.Contract_GetLocalRole(contract);

                Check(localRole == VscRole.SENDER);
                Check(schema != IntPtr.Zero);
                Check((IntPtr)vsc_Api_V3_t.UpdateData_GetSchema(out_data) == (IntPtr)schema);

                retRes = VscResult.OK;
                for (int fieldIndex = 0; VscResult.OK == retRes && fieldIndex < numSchemaFields; ++fieldIndex)
                {
                    var field = vsc_Api_V3_t.Schema_GetField(schema, fieldIndex);
                    double* fieldValue = (double*)vsc_Api_V3_t.UpdateData_GetFieldValue(out_data, fieldIndex);

                    var vsVar = ToObject(vsc_Api_V3_t.Field_GetAppData(field)) as VSVarBase;
                    

                    Check(fieldValue != null);

                    if (vsVar == null)
                    {
                        vsc_Api_V3_t.InvalidateDouble(fieldValue);
                    }
                    else
                    {

                        var arraySize = vsc_Api_V3_t.Field_GetNumElements(field);
                        var vscDataType = vsc_Api_V3_t.Field_GetDataType(field);
                        var dataSize = vsc_Api_V3_t.Field_GetElementSizeInBits(field);

                        Check(vscDataType == VscDataType.FLOAT);
                        Check(64 == dataSize);
                        Check(arraySize > 0);

                        if (vsVar.VariableType == typeof(double)) *fieldValue = ((VsConnectVar<double>)vsVar).Value;
                        if (vsVar.VariableType == typeof(double[]))
                        {
                            int index = 0;
                            foreach (var value in ((VsConnectVar<double[]>)vsVar).Value)
                            {
                                fieldValue[index] = value;
                                index++;
                            }
                        }
                    }
                }
            }

            return retRes;
        }



        /// <summary>
        /// Called when new data has arrived from a peer via the specified Link.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="contract"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        unsafe virtual protected VscResult ReceiveCallback(VscLink link, VscContract contract, VscUpdateData data)
        {
            {
                VscResult retRes;
                var node = VsConnectNode.GetInstanceFromLink(link);

                retRes = ShouldProcessCallback(node, link);

                if (!retRes.IsError())
                {
                    VscSchema schema = vsc_Api_V3_t.Contract_GetSchema(contract);
                    var numSchemaFields = vsc_Api_V3_t.Schema_GetNumFields(schema);
                    var localRole = vsc_Api_V3_t.Contract_GetLocalRole(contract);


                    Check(localRole == VscRole.SENDER);
                    Check(localRole == VscRole.SENDER);
                    Check(schema != IntPtr.Zero);
                    Check((IntPtr)vsc_Api_V3_t.UpdateData_GetSchema(data) == (IntPtr)schema);


                    for (int fieldIndex = 0; fieldIndex < numSchemaFields; ++fieldIndex)
                    {
                        var field = vsc_Api_V3_t.Schema_GetField(schema, fieldIndex);
                        var arraySize = vsc_Api_V3_t.Field_GetNumElements(field);
                        Check(arraySize > 0);
                        var vsVar = ToObject(vsc_Api_V3_t.Field_GetAppData(field)) as VSVarBase;

                        if (vsVar != null && vsVar.Direction == VsVarDirection.Input)
                        {
                            double* fieldValue = (double*)vsc_Api_V3_t.UpdateData_GetFieldValue(data, fieldIndex);
                            Check(fieldValue!=null);

                            if (vsVar.VariableType == typeof(double)) ((VsConnectVar<double>)vsVar).Value = *fieldValue;
                            if (vsVar.VariableType == typeof(double[]))
                            {
                                double[] values = new double[arraySize];
                                for (int index = 0; index < arraySize; index++) values[index] = fieldValue[index];
                                ((VsConnectVar<double[]>)vsVar).Value = values;
                            }
                        }
                    }
                    retRes = VscResult.OK;
                }

                return retRes;
            }

        }

    }
}
