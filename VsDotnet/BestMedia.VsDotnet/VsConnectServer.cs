//
// Copyright (C) 2020 BestMedia.  All rights reserved.
//
// The information and source code contained herein is the exclusive property of BestMedia and may not be disclosed, 
// examined or reproduced in whole or in part without explicit written authorization from the company.
//
//


using System;
using System.Collections;
using System.Collections.Generic;

using static BestMedia.VsDotnet.VsUtility;
using BestMedia.VsDotnet.vs_connect_api_v3;



namespace BestMedia.VsDotnet
{
    
    
    /// <summary>
    /// This class is based of VsConnect.h(Unreal) and rund as VS_Connecct_Server.    
    /// </summary>    
    public class VsConnectServer : VsConnectBase
    {

        const int VS_TS_REQUIRE_FIXED_FRAME_RATE = 1;

        /// <summary>
        ///VSConnectServer singleton.
        /// </summary>
        static public VsConnectServer Singleton { get; protected set; } = null;


        public IEnumerable<VsConnectNode> VscNodes => mVscNodes;

        List<VsConnectNode> mVscNodes = new List<VsConnectNode>();

        Dictionary<string, VsConnectSolver> mVscObjects = new Dictionary<string, VsConnectSolver>();
        float mUnblockedTimeDilation = -1;


       

        

        public VsConnectServer()
        {
            linkConnectCallback = LinkConnectCallback;
            linkdisconnectCallback = LinkDisconnectCallback;
            sendUpdateCallback = this.SendCallback;
            receiveUpdateCallback = this.ReceiveCallback;
            contractRequestCallback = ProcessContractRequest;
            contractCanceledCallback = ContractCanceledCallback;
        }



        
        static public void StartupModule()
        {
            Check(Singleton == null);
            Check(VsModule.CreateVsConnectServerFunc != null);
            //Create VSConnectServer;
            Singleton = VsModule.CreateVsConnectServerFunc();

            Singleton.InitializeServer();
        }
        public static void ShutdownModule()
        {
            Check(Singleton != null);
            Singleton.DestroyServer();
            Singleton = null;
        }




        /// <summary>
        /// Exec Node_Service operation
        /// </summary>
        /// <param name="worldTime"></param>
        /// <param name="timeDilation"></param>
        public virtual void Tick(double worldTime, double timeDilation)
        {

            {

                Check(vsc_Api_V3_t.Node_Service!=null);
                


                bool block = true;
                block = false;
                foreach (var node in VscNodes)
                {
                    Check(node.VscNode!=IntPtr.Zero);
                    if (node.VscNode != IntPtr.Zero)
                    {
                        vsc_Api_V3_t.Node_Service(node.VscNode, worldTime, timeDilation, true);
                        block = block || vsc_Api_V3_t.Node_IsBlocked(node.VscNode);
                    }
                }


                if (!block && IsBlocking)
                {

                    // Stop blocking:
                    //Time.timeScale=mUnblockedTimeDilation;
                    //check(ActualTimeDilation == vsConnect->mUnblockedTimeDilation);
                    //mUnblockedTimeDilation = -1;
                }
                else if (block && !IsBlocking)
                {
                    //mUnblockedTimeDilation = timeDilation;
                    //check(vsConnect->mUnblockedTimeDilation >= 0);                    

                    //const float ActualTimeDilation = WorldSettings->SetTimeDilation(blockingTimeDilation);
                    //check(ActualTimeDilation == blockingTimeDilation);
                }


            }

        }
        



        #region CallBacks

      
        /// <summary>
        /// Clean up references/etc. when a Contract is destroyed.
        /// </summary>
        /// <param name="contract"></param>
        void SPreDestroyContract_(VscContract contract)
        {
            VsConnectServer vsConnect = ToObject(vsc_Api_V3_t.Contract_GetAppData(contract)) as VsConnectServer;

            // Loop through all Fields and remove the corresponding UObject* from vsConnect->mVsVars.
            var schema = vsc_Api_V3_t.Contract_GetSchema(contract);
            var numFields = vsc_Api_V3_t.Schema_GetNumFields(schema);

            for (int i = 0; i < numFields; ++i)
            {
                var field = vsc_Api_V3_t.Schema_GetField(schema, i);
                var vsvar = ToObject(vsc_Api_V3_t.Field_GetAppData(field)) as VSVarBase;
                if (vsvar!=null) vsc_Api_V3_t.Field_SetAppData(field, IntPtr.Zero);
            }
        }



        virtual protected VscResult ProcessContractRequest(VscLink link, VscContract contract)
        {
            VscResult retRes;
            var node = VsConnectNode.GetInstanceFromLink(link);

            retRes = ShouldProcessCallback(node, link);

            if (retRes.IsError())
            {
                LogWarning("VS Connect - Ignoring Contract Request.");
            }
            else
            {
                var  tsMode = vsc_Api_V3_t.Contract_GetTsMode(contract);

                if (vsc_Api_V3_t.Contract_GetLocalRole(contract)== VscRole.RECEIVER
                      && VS_TS_REQUIRE_FIXED_FRAME_RATE==1
                      && tsMode!= VscTimeSyncMode.NONE
                      //&& !GEngine->bUseFixedFrameRate                      

                      )
                {
                    LogError("VS Connect - Rejecting Incoming Contract with Time Sync (TS) enabled. Unity must be operating in fixed framerate mode to use TS.");
                    retRes = VscResult.ERROR_UNSUPPORTED;
                }
                else
                {

                    VscSchema schema = vsc_Api_V3_t.Contract_GetSchema(contract);
                    var numSchemaFields = vsc_Api_V3_t.Schema_GetNumFields(schema);
                    var localRole = vsc_Api_V3_t.Contract_GetLocalRole(contract);
                    Check(schema != IntPtr.Zero);


                    if (localRole == VscRole.SENDER)
                    {
                        Log($"VS Connect outgoing Contract request received: {contract.ptr.ToInt64():x}.\n");
                    }
                    else if (localRole == VscRole.RECEIVER)
                    {
                        Log($"VS Connect outgoing Contract request received: {contract.ptr.ToInt64():x}.\n");
                    }
                    else
                    {
                        LogWarning($"VS Connect outgoing Contract request received: {contract.ptr.ToInt64():x}.\n");
                    }

                    for (int fieldIndex = 0; fieldIndex < numSchemaFields; ++fieldIndex)
                    {
                        var field = vsc_Api_V3_t.Schema_GetField(schema, fieldIndex);
                        string objectName = "";
                        objectName = (vsc_Api_V3_t.Field_GetObjectName(field)).ToString();



                        var solver = GetObject(objectName);
                        if (solver == null)
                        {
                            LogWarning($"Unable to locate VS Connect Object {objectName} for Contract request {contract.ptr.ToInt64():x}.");
                        }
                        else
                        {
                            string fieldName = vsc_Api_V3_t.Field_GetPropertyName(field);
                            string fieldParams = vsc_Api_V3_t.Field_GetParams(field);

                            var arraySize = vsc_Api_V3_t.Field_GetNumElements(field);
                            var vscDataType = vsc_Api_V3_t.Field_GetDataType(field);
                            var dataSize = vsc_Api_V3_t.Field_GetElementSizeInBits(field);

                            fieldName.TrimAndUnquoteInPlace();
                            fieldParams.TrimAndUnquoteInPlace();

                            VSVarBase vsVar = null;

                            if (localRole == VscRole.SENDER)
                            {
                                if (vscDataType == VscDataType.FLOAT && dataSize == 64 && arraySize == 1)
                                {
                                    vsVar = new VsConnectVar<double>(solver, fieldName, solver.GetDoubleFuncs[fieldName]);
                                }
                                if (vscDataType == VscDataType.FLOAT && dataSize == 64 && arraySize > 1)
                                {
                                    vsVar = new VsConnectVar<double[]>(solver, fieldName, solver.GetDoubleArrayFuncs[fieldName]);
                                }

                                if (vsVar == null)
                                {
                                    LogWarning($"VS Connect - Contract request %llx, Object {objectName}: No output Property {fieldName} with params {fieldParams}) for .");
                                }
                            }
                            else if (localRole == VscRole.RECEIVER)
                            {
                                if (vscDataType == VscDataType.FLOAT && dataSize == 64 && arraySize == 1)
                                {
                                    vsVar = new VsConnectVar<double>(solver, fieldName, solver.SetDoubleActions[fieldName]);
                                }
                                if (vscDataType == VscDataType.FLOAT && dataSize == 64 && arraySize > 1)
                                {
                                    vsVar = new VsConnectVar<double[]>(solver, fieldName, solver.SetDoubleArrayActions[fieldName]);
                                }


                                if (vsVar == null)
                                {
                                    LogWarning($"VS Connect - Contract request {contract.ptr.ToInt64():x}, Object {objectName}: No input Property {fieldName} with params {fieldParams}) for .");

                                }
                            }
                            else
                            {
                                vsVar = null;
                                LogError(("VS Connect - Unhandled VS Connect Role.\n"));
                            }


                            Check(vsc_Api_V3_t.Field_GetAppData(field) == IntPtr.Zero);

                            if (vsVar != null)
                            {
                                // Store the handle in the schema:
                                vsc_Api_V3_t.Field_SetAppData(field, ToPtr(vsVar));
                            }

                        }

                    }


                    Check(vsc_Api_V3_t.Contract_GetAppData(contract)==IntPtr.Zero);                    
                    vsc_Api_V3_t.Contract_SetAppData(contract, ToPtr(Singleton));
                    vsc_Api_V3_t.Contract_SetPreDestroyCallback(contract, SPreDestroyContract_);


                    retRes = VscResult.OK;
                }
            }

            return retRes;

        }

        void ContractCanceledCallback( VscLink link, VscContract contract )
        {
            var tsMode = vsc_Api_V3_t.Contract_GetTsMode(contract);
            var localRole = vsc_Api_V3_t.Contract_GetLocalRole(contract);

            if (tsMode !=  VscTimeSyncMode.NONE  &&  localRole== VscRole.RECEIVER )
            {
                // Our Incoming TS Contract is being canceled. Ensure
                // time dilation is set back to its original value:
                //auto unode = UVsConnectNode::GetInstanceFromLink(link);
                //auto uVsConnect = Cast<UVsConnect>(unode->GetOuter());
                //auto worldSettings = uVsConnect->mWorld->GetWorldSettings();
                //if ( worldSettings ) uVsConnect->RestoreOriginalTimeDilation(worldSettings);
                
            }
        }

       

        

        #endregion




        

        

        virtual public bool RegisterObject(VsConnectSolver vscObject)
        {
            bool retSuccess = false;            

            //check(vscObject);

            if (vscObject!=null)
            {
                var name = vscObject.VscObjectName;
                name.Trim();

                if (string.IsNullOrEmpty(name)==false)
                {
                    

                    var numElementsRemoved = mVscObjects.Remove(name);
                    if (numElementsRemoved)
                    {
                        LogWarning(($"Registering VS Connect Object with duplicate name \"{name}\" (not case sensitive). Newly registered object will replace previously registered object."));
                    }

                    mVscObjects.Add(name, vscObject);

                    retSuccess = true;
                }
            }

            return retSuccess;

        }
        virtual public bool DeregisterObject(VsConnectSolver vscObject)
        {
            bool retSuccess = false;
            //check(object);
            foreach (var key in mVscObjects.Keys )
            {
                if (mVscObjects[key] == vscObject)
                {
                    mVscObjects.Remove(key);
                    retSuccess = true;
                    break;
                }
            }

            return retSuccess;
        }
        virtual public VsConnectSolver GetObject(string vsConnectObjectName)
        {
            vsConnectObjectName.TrimAndUnquoteInPlace();

            return mVscObjects.ContainsKey(vsConnectObjectName) ? mVscObjects[vsConnectObjectName] : null;
            
        }


        

        public VsConnectNode Listen_IP(string ListenAddress
                                    , int port = 0
                                    , int maxConnections = 10
                                    , bool requireTimeSync = false)
        {



            ListenAddress.TrimAndUnquoteInPlace();

            VscResult createRes = VscResult.UNDEFINED;
            var newNodePtr = vsc_Api_V3_t.Node_Create_UDPIP(ListenAddress
                                                                        , (ushort)port
                                                                        , maxConnections
                                                                        , requireTimeSync ?   VscTimeSyncMode.ABSOLUTE : VscTimeSyncMode.NONE
                                                                        , linkConnectCallback
                                                                        , linkdisconnectCallback
                                                                        , contractRequestCallback
                                                                        , contractCanceledCallback
                                                                        , sendUpdateCallback
                                                                        , receiveUpdateCallback
                                                                        , null
                                                                        , ref createRes
                                                                      );
            

            if (newNodePtr!=IntPtr.Zero)
            {
                var newNode = new VsConnectNode(newNodePtr);
                vsc_Api_V3_t.Node_SetAppData(newNode.VscNode, ToPtr(newNode));

                Log(("Successfully created VS Connect Server Node."));
                mVscNodes.Add(newNode);
                return newNode;
            }
            else
            {   
                LogError($"Failed to create VS Connect Server Node: {ListenAddress}:{port}");
                return null;
            }
            
        }



        virtual protected void DisconnectAllLinks()
        {
            foreach (var node in mVscNodes)
            {
                Check(node != null);
                if (node.VscNode != IntPtr.Zero)
                {
                    int numLinks = vsc_Api_V3_t.Node_GetNumLinks(node.VscNode);

                    for (int i = 0; i < numLinks; ++i)
                    {
                        VscLink link = vsc_Api_V3_t.Node_GetLink(node.VscNode, i);
                        Check(link != IntPtr.Zero);                        
                        Check(vsc_Api_V3_t.Link_GetConnectionStatus(link)!= VscConnectionStatus.UNCONNECTED);
                        VscResult disconnectRes = vsc_Api_V3_t.Link_Disconnect(link);
                        //check(!vsc_IsError(disconnectRes));
                    }
                }
            }

            bool done = false;
            float timeout = 5.0f;

            while (!done && timeout > 0)
            {
                done = true;  // We're done unless we find a Node with Links that are not disconnected.

                foreach (var node in mVscNodes)
                {
                    Check(node != null);

                    if (node.VscNode != IntPtr.Zero)
                    {
                        vsc_Api_V3_t.Node_Service(node.VscNode, vsc_Api_V3_t.GetInvalidSimtime(), vsc_Api_V3_t.GetInvalidSimtime(), true);
                    }
                }

                foreach (var node in mVscNodes)
                {
                    Check(node != null);

                    if (node.VscNode != IntPtr.Zero)
                    {
                        if (0 == vsc_Api_V3_t.Node_GetNumLinks(node.VscNode))
                        {
                        }
                        else
                        {
                            done = false;
                            vsc_Api_V3_t.Node_Service(node.VscNode, vsc_Api_V3_t.GetInvalidSimtime(), vsc_Api_V3_t.GetInvalidSimtime(), true);
                        }
                    }
                }

                if (!done)
                {
                    const float sleepTime = 0.100f;  ///< Seconds to sleep during each iteration while disconnecting.
                    System.Threading.Thread.Sleep((int)(1000 * sleepTime));
                    timeout -= sleepTime;
                }
            }
        }
        virtual public void DestroyAllNodes()
        {
            foreach (var node in mVscNodes )
            {
                Check(node != null);
                node.ShuttingDown = true;
                vsc_Api_V3_t.Node_IgnoreConnectionRequests(node.VscNode, true);
            }

            DisconnectAllLinks();

            foreach (var node in mVscNodes)
            {
                node.Shutdown();
            }

            mVscNodes.Clear();
        }




        public bool IsBlocking { get { return (mUnblockedTimeDilation >= 0); } }



        virtual protected void InitializeServer()
        {   

            Check(vsc_Api_V3_t.IsInitialized != null);


            if (!vsc_Api_V3_t.IsInitialized())
            {
                vsc_Api_V3_t.SetLogFunc(_VscLogCallback);
                vsc_Api_V3_t.SetLogLevel(VscLogLevel.DEFAULT);


                vsc_Api_V3_t.Init();

                if (!vsc_Api_V3_t.IsInitialized())
                {
                    LogError("Failed to initialize VS Connect.");
                }
                else
                {
                    Log("VS Connect successfully initialized.");
                }
            }

            Listen_IP(VsModule.VsConnectIP);
        }

        
        
        virtual protected void DestroyServer()
        {   

            DestroyAllNodes();
            mVscObjects.Clear();
        }

    }
}
