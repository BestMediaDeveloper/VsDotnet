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
using System.Threading.Tasks;
using System.Runtime.InteropServices;


/// <summary>
/// This file is a version of vs_connect_api.h(VS_SDK) converted for C #.
/// See vs_connect_api.h or vs_sdk for details
/// </summary>
namespace BestMedia.VsDotnet.vs_connect_api_v3
{
       
    using VscSerialNum_t = System.UInt32;
    using VscContractId_t = System.UInt32;    

    using P3mHost = System.IntPtr;
    using P3mPeer = System.IntPtr;


    /// <summary>
    /// VscNode In VS_SDK, this is just a pointer, but we redefine it in a struct to distinguish the names
    /// </summary>
    public struct VscNode { public IntPtr ptr; public VscNode(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscNode _) => _.ptr; public static implicit operator VscNode(IntPtr _) => new VscNode(_);   }
    public struct VscLink { public IntPtr ptr; public VscLink(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscLink _) => _.ptr; public static implicit operator VscLink(IntPtr _) => new VscLink(_); }
    public struct VscSchema { public IntPtr ptr; public VscSchema(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscSchema _) => _.ptr; public static implicit operator VscSchema(IntPtr _) => new VscSchema(_); }
    public struct VscField { public IntPtr ptr; public VscField(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscField _) => _.ptr; public static implicit operator VscField(IntPtr _) => new VscField(_); }
    public struct VscContract { public IntPtr ptr; public VscContract(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscContract _) => _.ptr; public static implicit operator VscContract(IntPtr _) => new VscContract(_); }
    public struct VscUpdateDefinition { public IntPtr ptr; public VscUpdateDefinition(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscUpdateDefinition _) => _.ptr; public static implicit operator VscUpdateDefinition(IntPtr _) => new VscUpdateDefinition(_); }
    public struct VscUpdateData { public IntPtr ptr; public VscUpdateData(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VscUpdateData _) => _.ptr; public static implicit operator VscUpdateData(IntPtr _) => new VscUpdateData(_); }

    public enum VscResult : sbyte
    {
        UNDEFINED = -128,
        ERROR = -127,   ///< Generic error.
        //--------------------------
        ERROR_INTERNAL = -11,    ///< Indicates an unexpected internal failure in the library (primarily used for debugging).
        ERROR_UNINITIALIZED = -10,    ///< P3M is not initialized.
        ERROR_CANCELED = -9,     ///< The operation was actively canceled (e.g. by the user).
        ERROR_INVALID_DATA = -8,     ///< The data provided is not valid.
        ERROR_INVALID_PARAM = -7,     ///< One or more of the parameters is invalid, or the parameters are incompatible with each other.
        ERROR_RESOURCE = -6,     ///< A resource needed to complete the request is not available.
        ERROR_MEMORY = -5,     ///< Memory could not be allocated to complete the request (out of memory).
        ERROR_UNSUPPORTED = -4,     ///< The request is not supported by this version.
        ERROR_UNAVAILABLE = -3,     ///< The request is not available at this time.
        ERROR_TIMEOUT = -2,     ///< A timeout expired before the request could be completed.
        ERROR_VERSION = -1,     ///< The specified version is not supported.
        OK = 0,      ///< Success! No error!
        OK_ASYNC = 1,      ///< Successfully initiated asynchronous operation.
        WARNING = 2,      ///< Generic warning (non-fatal).
        WARNING_NOOP = 3,      ///< No-op (non-fatal).
    };

    public static partial class VscResultExtend
    {
        public static bool IsError(this VscResult param)
        {
            return param < VscResult.OK;
        }
    }

    public enum VscLogLevel : sbyte
    {
        DISABLED = -1, ///< Log setting that disables all log messages.
        DEFAULT = 0,  ///< Log setting that instructs the library to use its default log level.
        ALWAYS = 1,  ///< Log level for messages that are always logged (when logging is enabled), regardless of log level setting.
        ERROR = 2,  ///< Something unexpected has occurred that prevents proper functioning.
        WARNING = 3,  ///< Something unusual has occurred which could cause unexpected behavior.
        INFO = 4,  ///< General informational message.
        HELPFUL = 5,  ///< Helpful details that may assist with troubleshooting unexpected behavior.
        VERBOSE = 6,  ///< Excessive text for those that want to see all available information.
        DEBUG = 7,  ///< Includes log messages only available in _DEBUG builds of this library.
    };


    public enum VscConnectionStatus : sbyte
    {
        UNDEFINED = 0,  ///< The connection state is undefined. This indicates an error (use of an unitialized object).
        //==========================
        UNCONNECTED = 1,  ///< We are not connected, and we are not attempting to connect. This is the initial/default connection state of a Peer.
        CONNECTING = 2,  ///< We are not connected, but we are currently attempting to connect.
        CONNECTED = 3,  ///< We are currently connected.
        ERROR = 4,  ///< We are not connected and not attempting to connect because An unexpected error occurred.
        DISCONNECTING = 5,  ///< We are in the process of disconnecting from the Peer.
    };

    public enum VscParty : sbyte
    {
        /// !!! IMPORTAINT: Values of elements must be representable by uint8_t (unsigned char).
        UNDEFINED = 0,
        LOCAL = 1,  ///< "Us"
        REMOTE = 2,  ///< "Them"
    };



    public enum VscDataType : byte
    {
        UNKNOWN = 0,
        FLOAT = 1,
    };


    public enum VscInterp : byte
    {
        DEFAULT = 0,  ///< Use the default interpolation method for the data type.
        LINEAR = 1,
        FLOOR = 2,
        NEAREST = 3,
        CEIL = 4,
        STEP = 5,
        //=========================
        VSC_INTERP_COUNT,
    };


    public enum VscExtrap : byte
    {
        DEFAULT = 0,  ///< Use the default extrapolation method for the data type.
        NONE = 1,
        FLAT = 2,
        STRAIGHT = 3,
        //=========================
        COUNT,
    };


    public enum VscRounding : byte
    {
        DEFAULT = 0,  ///< Use the default rounding method for the data type.
        FLOOR = 1,
        NEAREST = 2,
        CEIL = 3,
        TRUNCATE = 4,
        //==========================
        COUNT,
    };


    public enum VscScheduleType : byte
    {
        UNDEFINED = 0,
        PERIODIC_SIMTIME_MILLISECONDS = 1,  ///< Periodic updates w/ a period specified in milliseconds of simulation time.
        SCHED_TRIGGERED = 2,                 ///< Updates are only sent when The Application triggers them.
    };


    public enum VscRole : byte
    {
        UNDEFINED = 0,
        SENDER = 1,
        RECEIVER = 2,
    };



    
    public enum TimeMomentAttr : byte
    {   
        SYSTEM_TIME = 0,  
        SIMTIME,                 
        SIMTIME_SYSTEM_TIME,     
        SIMTIME_DILATION,                
        COUNT,
    }
    ;


// A moment in the ping Request-Response-Result process timeline.
    public  enum PingMilestone : byte
    {
        REQUESTER_REQUEST_INITIATED = 0,  
        REQUESTER_REQUEST_QUEUED,                  
        RESPONDER_REQUEST_RECEIVED,        
        RESPONDER_REQUEST_PROCESSED,       
        RESPONDER_RESPONSE_QUEUED,                 
        REQUESTER_RESPONSE_RECEIVED,               
        COUNT,
    }


    //Time Sync (TS) mode.
    public enum VscTimeSyncMode : byte
    {
        NONE = 0,  ///< Time Sync disabled.
        ABSOLUTE = 1,  ///< Synchronize simulation time of Peers absolutely.
        //==============================
        COUNT,          ///< Number of TS Modes.
    }
    


    public delegate int VscLogFunc_t(VscLogLevel logLevel
    , VscNode node
    , VscLink link
    , VscContract contract
    , String format
    , IntPtr argptr);

    public delegate VscResult VscLinkConnectedFunc_t(VscLink connectingLink);
    public delegate void VscLinkDisconnectedFunc_t(VscLink disconnectedLink);
    public delegate VscResult VscProcessContractRequestFunc_t(VscLink link, VscContract contract);
    public delegate void VscContractCanceledFunc_t(VscLink link, VscContract contract);
    public delegate VscResult VscSendUpdateFunc_t(VscLink link, VscContract contract, VscUpdateData out_data);
    public delegate VscResult VscReceiveUpdateFunc_t(VscLink link, VscContract contract, VscUpdateData data);
    public delegate void      VscPingResultsFunc_t( VscNode vscNode, VscLink link );

    public delegate void      VscNodePreDestroyFunc_t(VscNode objectToBeDestroyed);
	public delegate void      VscLinkPreDestroyFunc_t(VscLink objectToBeDestroyed);
	public delegate void      VscSchemaPreDestroyFunc_t(VscSchema objectToBeDestroyed);
	public delegate void      VscFieldPreDestroyFunc_t(VscField objectToBeDestroyed);
	public delegate void      VscContractPreDestroyFunc_t(VscContract objectToBeDestroyed);
	public delegate void      VscUpdateDefinitionPreDestroyFunc_t(VscUpdateDefinition objectToBeDestroyed);
	public delegate void      VscUpdateDataPreDestroyFunc_t(VscUpdateData objectToBeDestroyed);




    unsafe static public class vsc_Api_V3_t
    {
        public class delegates
        {
            #region Init_Other

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void SetLogFunc(VscLogFunc_t logFunc);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void SetLogLevel(VscLogLevel logLevel);


            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Init();

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool IsInitialized();

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Shutdown();

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate String DescribeResult(VscResult resultCode);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool IsValidDouble(double value);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double GetInvalidDouble();

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void InvalidateDouble(double* varToInvalidate);


            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool IsValidSimtime(double time);

            

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double GetInvalidSimtime();
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void InvalidateTime(double* varToInvalidate);
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool IsValidSerialNumber(VscSerialNum_t serialNumber);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSerialNum_t GetInvalidSerialNumber();
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSerialNum_t GetFirstSerialNumber();
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSerialNum_t GetLastSerialNumber();
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate ushort GetDefaultListenPort();
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            internal delegate IntPtr DescribeTsModePtr(VscTimeSyncMode tsMode);
            #endregion


            #region Node
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscNode Node_Create_UDPIP(String listenAddress
                , ushort listenPort
                , int maxConnections
                , VscTimeSyncMode requiredTsMode
                , VscLinkConnectedFunc_t linkConnectCallback
                , VscLinkDisconnectedFunc_t linkdisconnectCallback
                , VscProcessContractRequestFunc_t contractRequestCallback
                , VscContractCanceledFunc_t contractCanceledCallback
                , VscSendUpdateFunc_t sendUpdateCallback
                , VscReceiveUpdateFunc_t receiveUpdateCallback
                , VscPingResultsFunc_t pingResultsCallback
                , ref VscResult out_result
                );

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Node_SetPreDestroyCallback(VscNode node, VscNodePreDestroyFunc_t func);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Node_HandleCopy(VscNode source, out VscNode out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Node_HandleRelease(out VscNode inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Node_SetAppData(VscNode vscNode, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr Node_GetAppData(VscNode vscNode);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double Node_GetTimeInfo(VscNode vscNode, TimeMomentAttr timeAttr);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate int Node_GetNumLinks(VscNode vscNode);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscLink Node_GetLink(VscNode vscNode, int linkIndex);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Node_Service(VscNode vscNode, double simtime, double timeDilation, bool processEvents);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Node_IgnoreConnectionRequests(VscNode node, bool ignoreIncomingConnectionRequests);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Node_ConnectionRequestsIgnored(VscNode node);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Node_CanBlock(VscNode node);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Node_IsBlocked(VscNode node);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscTimeSyncMode Node_GetTsMode(VscNode node);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscContract Node_GetTsContract(VscNode node);


            #endregion


            #region Link

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscLink Link_Create_UDPIP(String serverAddress, ushort serverPort, ref VscResult out_result);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Link_SetPreDestroyCallback(VscLink link, VscLinkPreDestroyFunc_t func);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Link_HandleCopy(VscLink source, out VscLink out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Link_HandleRelease(out VscLink inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Link_SetAppData(VscLink link, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr Link_GetAppData(VscLink link);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscNode Link_GetNode(VscLink vscLink);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Link_Connect_Async(VscNode vscNode, VscLink vscLink);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Link_Disconnect(VscLink vscLink);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscConnectionStatus Link_GetConnectionStatus(VscLink vscLink);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscParty Link_GetInitiator(VscLink vscLink);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Link_CreateIncomingContract(VscLink vscLink, VscUpdateDefinition updateDef, VscReceiveUpdateFunc_t receiveUpdateCallback, VscTimeSyncMode tsMode, double overdueDataTimeoutSec, out VscContract out_contractHandle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Link_CreateOutgoingContract(VscLink vscLink, VscUpdateDefinition updateDef, VscSendUpdateFunc_t sendUpdateCallback, VscTimeSyncMode tsMode, double overdueDataTimeoutSec, out VscContract out_contractHandle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscContract Link_GetTsContract(VscLink vscLink);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Link_PingInitiate(VscLink vscLink, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Link_PingSetPeriodSec(VscLink vscLink, double period, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double Link_PingGetStat(VscLink vscLink, PingMilestone pingMilestone, TimeMomentAttr momentAttribute);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr Link_PingGetAppData(VscLink vscLink);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double Link_GetRttMean(VscLink vscLink);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Link_CanBlock(VscLink vscLink);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Link_IsBlocked(VscLink vscLink);
            #endregion


            #region Schema


            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSchema Schema_Create(int numFields);
           

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Schema_SetPreDestroyCallback(VscSchema schema, VscSchemaPreDestroyFunc_t func);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Schema_HandleCopy(VscSchema source, out VscSchema out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Schema_HandleRelease(out VscSchema inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Schema_SetAppData(VscSchema schema, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr Schema_GetAppData(VscSchema schema);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Schema_InitField(VscSchema schema
               , int fieldIndex
               , VscDataType dataType
               , int dataElementSizeInBits
               , int numElements
               , String objectName
               , String propertyName
               , String propertyParams
               , VscInterp interpMethod
               , VscExtrap extrapMethod
               , VscRounding roundingMethod
               );

            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate int Schema_GetNumFields(VscSchema schema);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscField Schema_GetField(VscSchema schema, int fieldIndex);




            #endregion

            #region Field
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Field_HandleCopy(VscField source, out VscField out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Field_HandleRelease(out VscField inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Field_SetAppData(VscField field, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr Field_GetAppData(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]            
            internal delegate IntPtr Field_GetObjectNamePtr(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            internal delegate IntPtr Field_GetPropertyNamePtr(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            internal delegate IntPtr Field_GetParamsPtr(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscDataType Field_GetDataType(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate int Field_GetElementSizeInBits(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate int Field_GetNumElements(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscInterp Field_GetInterpMethod(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscExtrap Field_GetExtrapMethod(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscRounding Field_GetRoundingMethod(VscField field);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Field_SetPreDestroyCallback(VscField field, VscFieldPreDestroyFunc_t func);
            
            
            
            
            
            
            
            
            #endregion

            #region Update
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscUpdateData UpdateData_CreateFromSchema(VscSchema schema, out VscResult out_result);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateData_SetPreDestroyCallback(VscUpdateData updateData, VscUpdateDataPreDestroyFunc_t func);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateData_HandleCopy(VscUpdateData source, out VscUpdateData out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateData_HandleRelease(out VscUpdateData inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSchema UpdateData_GetSchema(VscUpdateData data);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult UpdateData_CopyData(VscUpdateData source, VscUpdateData dest);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSerialNum_t UpdateData_GetSerialNumber(VscUpdateData data);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double UpdateData_GetSimtime(VscUpdateData data);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double UpdateData_GetTimeDilation(VscUpdateData data);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr UpdateData_GetFieldValue(VscUpdateData data, int fieldIndex);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateData_SetSimtime(VscUpdateData data, double simtime);

           
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscUpdateDefinition UpdateDefinition_Create(VscSchema schema, VscScheduleType scheduleType, double scheduleValue);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateDefinition_SetPreDestroyCallback(VscUpdateDefinition updateDef, VscUpdateDefinitionPreDestroyFunc_t func);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateDefinition_HandleCopy(VscUpdateDefinition source, out VscUpdateDefinition out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void UpdateDefinition_HandleRelease(out VscUpdateDefinition inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]            
            public delegate VscSchema UpdateDefinition_GetSchema(VscUpdateDefinition updateDef);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscScheduleType UpdateDefinition_GetScheduleType(VscUpdateDefinition updateDef);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double UpdateDefinition_GetScheduleValue(VscUpdateDefinition updateDef);
            #endregion

            #region Contract


            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Contract_SetPreDestroyCallback(VscContract contract, VscContractPreDestroyFunc_t func);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Contract_HandleCopy(VscContract source, out VscContract out_destination);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Contract_HandleRelease(out VscContract inout_handle);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate void Contract_SetAppData(VscContract contract, IntPtr appData);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate IntPtr Contract_GetAppData(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscRole Contract_GetLocalRole(VscContract contract);            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscLink Contract_GetLink(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscSchema Contract_GetSchema(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscUpdateDefinition Contract_GetUpdateDefinition(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double Contract_GetReceiveTimeOffset(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscUpdateData Contract_GetIncomingData(VscContract contract,ref double out_receiveTime);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscTimeSyncMode Contract_GetTsMode(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Contract_CanBlock(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate double Contract_GetBlockTime(VscContract contract, double simtime);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate bool Contract_IsBlocked(VscContract contract);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            public delegate VscResult Contract_Trigger(VscContract contract, bool immediateSend, double simtime, double timeDilation);
            #endregion
        }


        //Init_Other
        static public delegates.SetLogFunc SetLogFunc = null;
        static public delegates.SetLogLevel SetLogLevel = null;
        static public delegates.Init Init=null;
        static public delegates.IsInitialized IsInitialized=null;
        static public delegates.Shutdown Shutdown=null;
        static public delegates.DescribeResult DescribeResult=null;
        static public delegates.IsValidDouble IsValidDouble=null;
        static public delegates.GetInvalidDouble GetInvalidDouble=null;
        static public delegates.InvalidateDouble InvalidateDouble = null;
        static public delegates.IsValidSimtime IsValidSimtime=null;
        static public delegates.GetInvalidSimtime GetInvalidSimtime=null;
        static public delegates.InvalidateTime InvalidateTime = null;
        
        static public delegates.IsValidSerialNumber IsValidSerialNumber=null;
        static public delegates.GetInvalidSerialNumber GetInvalidSerialNumber=null;
        static public delegates.GetFirstSerialNumber GetFirstSerialNumber=null;
        static public delegates.GetLastSerialNumber GetLastSerialNumber=null;
        static public delegates.GetDefaultListenPort GetDefaultListenPort=null;
        static private delegates.DescribeTsModePtr DescribeTsModePtr = null;
        static public string DescribeTsMode(VscTimeSyncMode tsMode) => Marshal.PtrToStringAnsi(DescribeTsModePtr(tsMode));
      

        //Node
        static public delegates.Node_Create_UDPIP Node_Create_UDPIP=null;
        static public delegates.Node_SetPreDestroyCallback Node_SetPreDestroyCallback=null;
        static public delegates.Node_SetAppData Node_SetAppData=null;
        static public delegates.Node_GetAppData Node_GetAppData=null;
        static public delegates.Node_GetTimeInfo Node_GetTimeInfo = null;
        static public delegates.Node_GetNumLinks Node_GetNumLinks=null;
        static public delegates.Node_GetLink Node_GetLink=null;
        static public delegates.Node_Service Node_Service=null;
        static public delegates.Node_IgnoreConnectionRequests Node_IgnoreConnectionRequests = null;
        static public delegates.Node_HandleCopy Node_HandleCopy=null;
        static public delegates.Node_HandleRelease Node_HandleRelease=null;
        static public delegates.Node_ConnectionRequestsIgnored Node_ConnectionRequestsIgnored = null;
        static public delegates.Node_CanBlock Node_CanBlock = null;
        static public delegates.Node_IsBlocked Node_IsBlocked = null;
        static public delegates.Node_GetTsMode Node_GetTsMode = null;
        static public delegates.Node_GetTsContract Node_GetTsContract = null;


        //Link
        static public delegates.Link_Create_UDPIP Link_Create_UDPIP=null;
        static public delegates.Link_SetPreDestroyCallback Link_SetPreDestroyCallback=null;
        static public delegates.Link_GetNode Link_GetNode=null;
        static public delegates.Link_SetAppData Link_SetAppData=null;
        static public delegates.Link_GetAppData Link_GetAppData=null;
        static public delegates.Link_Connect_Async Link_Connect_Async = null;
        static public delegates.Link_Disconnect Link_Disconnect = null;
        static public delegates.Link_GetConnectionStatus Link_GetConnectionStatus = null;
        static public delegates.Link_GetInitiator Link_GetInitiator = null;
        static public delegates.Link_PingInitiate Link_PingInitiate = null;
        static public delegates.Link_PingSetPeriodSec Link_PingSetPeriodSec = null;
        static public delegates.Link_PingGetStat Link_PingGetStat = null;
        static public delegates.Link_PingGetAppData Link_PingGetAppData = null;
        static public delegates.Link_GetRttMean Link_GetRttMean = null;
        static public delegates.Link_CanBlock Link_CanBlock = null;
        static public delegates.Link_IsBlocked Link_IsBlocked = null;
        static public delegates.Link_HandleCopy Link_HandleCopy = null;
        static public delegates.Link_HandleRelease Link_HandleRelease = null;
        static public delegates.Link_CreateIncomingContract Link_CreateIncomingContract = null;
        static public delegates.Link_CreateOutgoingContract Link_CreateOutgoingContract = null;
        static public delegates.Link_GetTsContract Link_GetTsContract = null;


        //Schema
        static public delegates.Schema_Create Schema_Create=null;
        static public delegates.Schema_InitField Schema_InitField=null;
        static public delegates.Schema_SetPreDestroyCallback Schema_SetPreDestroyCallback=null;
        static public delegates.Schema_GetNumFields Schema_GetNumFields=null;
        static public delegates.Schema_GetField Schema_GetField=null;
        static public delegates.Schema_SetAppData Schema_SetAppData=null;
        static public delegates.Schema_GetAppData Schema_GetAppData=null;
        static public delegates.Schema_HandleCopy Schema_HandleCopy=null;
        static public delegates.Schema_HandleRelease Schema_HandleRelease=null;

        //Field
        static public delegates.Field_SetPreDestroyCallback Field_SetPreDestroyCallback=null;
        static private delegates.Field_GetObjectNamePtr Field_GetObjectNamePtr=null;
        static private delegates.Field_GetPropertyNamePtr Field_GetPropertyNamePtr =null;
        static private delegates.Field_GetParamsPtr Field_GetParamsPtr = null;

        static public string Field_GetObjectName(VscField field) => Marshal.PtrToStringAnsi(Field_GetObjectNamePtr(field));
        static public string Field_GetPropertyName(VscField field) => Marshal.PtrToStringAnsi(Field_GetPropertyNamePtr(field));
        static public string Field_GetParams(VscField field) => Marshal.PtrToStringAnsi(Field_GetParamsPtr(field));



        static public delegates.Field_GetDataType Field_GetDataType=null;
        static public delegates.Field_GetElementSizeInBits Field_GetElementSizeInBits=null;
        static public delegates.Field_GetNumElements Field_GetNumElements=null;
        static public delegates.Field_GetInterpMethod Field_GetInterpMethod=null;
        static public delegates.Field_GetExtrapMethod Field_GetExtrapMethod=null;
        static public delegates.Field_GetRoundingMethod Field_GetRoundingMethod=null;
        static public delegates.Field_SetAppData Field_SetAppData=null;
        static public delegates.Field_GetAppData Field_GetAppData=null;
        static public delegates.Field_HandleCopy Field_HandleCopy=null;
        static public delegates.Field_HandleRelease Field_HandleRelease=null;

        //Update
        static public delegates.UpdateData_CreateFromSchema UpdateData_CreateFromSchema=null;
        static public delegates.UpdateData_SetPreDestroyCallback UpdateData_SetPreDestroyCallback=null;
        static public delegates.UpdateData_HandleCopy UpdateData_HandleCopy=null;
        static public delegates.UpdateData_HandleRelease UpdateData_HandleRelease=null;
        static public delegates.UpdateData_GetSchema UpdateData_GetSchema=null;
        static public delegates.UpdateData_CopyData UpdateData_CopyData=null;
        static public delegates.UpdateData_GetSerialNumber UpdateData_GetSerialNumber=null;
        static public delegates.UpdateData_GetSimtime UpdateData_GetSimtime=null;
        static public delegates.UpdateData_GetTimeDilation UpdateData_GetTimeDilation = null;
        static public delegates.UpdateData_GetFieldValue UpdateData_GetFieldValue=null;
        static public delegates.UpdateData_SetSimtime UpdateData_SetSimtime=null;        
        static public delegates.UpdateDefinition_Create UpdateDefinition_Create=null;
        static public delegates.UpdateDefinition_SetPreDestroyCallback UpdateDefinition_SetPreDestroyCallback=null;
        static public delegates.UpdateDefinition_HandleCopy UpdateDefinition_HandleCopy=null;
        static public delegates.UpdateDefinition_HandleRelease UpdateDefinition_HandleRelease=null;
        static public delegates.UpdateDefinition_GetSchema UpdateDefinition_GetSchema = null;
        static public delegates.UpdateDefinition_GetScheduleType UpdateDefinition_GetScheduleType = null;
        static public delegates.UpdateDefinition_GetScheduleValue UpdateDefinition_GetScheduleValue = null;
        


        //Contract
        static public delegates.Contract_SetPreDestroyCallback Contract_SetPreDestroyCallback=null;
        static public delegates.Contract_HandleCopy Contract_HandleCopy = null;
        static public delegates.Contract_HandleRelease Contract_HandleRelease = null;
        static public delegates.Contract_SetAppData Contract_SetAppData = null;
        static public delegates.Contract_GetAppData Contract_GetAppData = null;        
        static public delegates.Contract_GetLocalRole Contract_GetLocalRole=null;
        static public delegates.Contract_GetLink Contract_GetLink = null;
        static public delegates.Contract_GetSchema Contract_GetSchema = null;
        static public delegates.Contract_GetUpdateDefinition Contract_GetUpdateDefinition = null;
        static public delegates.Contract_GetReceiveTimeOffset Contract_GetReceiveTimeOffset = null;
        static public delegates.Contract_GetIncomingData Contract_GetIncomingData = null;
        static public delegates.Contract_GetTsMode Contract_GetTsMode = null;
        static public delegates.Contract_CanBlock Contract_CanBlock = null;
        static public delegates.Contract_GetBlockTime Contract_GetBlockTime = null;
        static public delegates.Contract_IsBlocked Contract_IsBlocked = null;
        static public delegates.Contract_Trigger Contract_Trigger = null;



        #region  DLL
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate vsc_Api_V3_tPtr* VsConnectApi_Get(int apiVersion);
        const int VsConnectApi_GetDefaultVersion = 3;

        [StructLayout(LayoutKind.Sequential)]
        struct vsc_Api_V3_tPtr
        {
            //
            public IntPtr SetLogFunc;
            public IntPtr SetLogLevel;
            public IntPtr Init;
            public IntPtr IsInitialized;
            public IntPtr Shutdown;
            public IntPtr DescribeResult;
            public IntPtr IsValidDouble;
            public IntPtr GetInvalidDouble;
            public IntPtr InvalidateDouble;
            public IntPtr IsValidSimtime;
            public IntPtr GetInvalidSimtime;
            public IntPtr InvalidateTime;
            public IntPtr IsValidSerialNumber;
            public IntPtr GetInvalidSerialNumber;
            public IntPtr GetFirstSerialNumber;
            public IntPtr GetLastSerialNumber;
            public IntPtr GetDefaultListenPort;
            public IntPtr DescribeTsMode;
            //Node
            public IntPtr Node_Create_UDPIP;
            public IntPtr Node_SetPreDestroyCallback;
            public IntPtr Node_HandleCopy;
            public IntPtr Node_HandleRelease;
            public IntPtr Node_SetAppData;
            public IntPtr Node_GetAppData;
            public IntPtr Node_GetTimeInfo;
            public IntPtr Node_GetNumLinks;
            public IntPtr Node_GetLink;
            public IntPtr Node_Service;
            public IntPtr Node_IgnoreConnectionRequests;
            public IntPtr Node_ConnectionRequestsIgnored;
            public IntPtr Node_CanBlock;
            public IntPtr Node_IsBlocked;
            public IntPtr Node_GetTsMode;
            public IntPtr Node_GetTsContract;

            //Link
            public IntPtr Link_Create_UDPIP;
            public IntPtr Link_SetPreDestroyCallback;
            public IntPtr Link_HandleCopy;
            public IntPtr Link_HandleRelease;
            public IntPtr Link_SetAppData;
            public IntPtr Link_GetAppData;
            public IntPtr Link_GetNode;
            public IntPtr Link_Connect_Async;
            public IntPtr Link_Disconnect;
            public IntPtr Link_GetConnectionStatus;
            public IntPtr Link_GetInitiator;
            public IntPtr Link_CreateIncomingContract;
            public IntPtr Link_CreateOutgoingContract;
            public IntPtr Link_GetTsContract;
            public IntPtr Link_PingInitiate;
            public IntPtr Link_PingSetPeriodSec;
            public IntPtr Link_PingGetStat;
            public IntPtr Link_PingGetAppData;
            public IntPtr Link_GetRttMean;
            public IntPtr Link_CanBlock;
            public IntPtr Link_IsBlocked;
            //Schema
            public IntPtr Schema_Create;
            public IntPtr Schema_SetPreDestroyCallback;
            public IntPtr Schema_HandleCopy;
            public IntPtr Schema_HandleRelease;
            public IntPtr Schema_SetAppData;
            public IntPtr Schema_GetAppData;
            public IntPtr Schema_InitField;
            public IntPtr Schema_GetNumFields;
            public IntPtr Schema_GetField;
            //Field
            public IntPtr Field_HandleCopy;
            public IntPtr Field_HandleRelease;
            public IntPtr Field_SetPreDestroyCallback;
            public IntPtr Field_SetAppData;
            public IntPtr Field_GetAppData;
            public IntPtr Field_GetObjectName;
            public IntPtr Field_GetPropertyName;
            public IntPtr Field_GetParams;
            public IntPtr Field_GetDataType;
            public IntPtr Field_GetElementSizeInBits;
            public IntPtr Field_GetNumElements;
            public IntPtr Field_GetInterpMethod;
            public IntPtr Field_GetExtrapMethod;
            public IntPtr Field_GetRoundingMethod;
            //Update
            public IntPtr UpdateData_CreateFromSchema;
            public IntPtr UpdateData_SetPreDestroyCallback;
            public IntPtr UpdateData_HandleCopy;
            public IntPtr UpdateData_HandleRelease;
            public IntPtr UpdateData_GetSchema;
            public IntPtr UpdateData_CopyData;
            public IntPtr UpdateData_GetSerialNumber;
            public IntPtr UpdateData_GetSimtime;
            public IntPtr UpdateData_GetTimeDilation;
            public IntPtr UpdateData_GetFieldValue;
            public IntPtr UpdateData_SetSimtime;
            public IntPtr UpdateDefinition_Create;
            public IntPtr UpdateDefinition_SetPreDestroyCallback;
            public IntPtr UpdateDefinition_HandleCopy;
            public IntPtr UpdateDefinition_HandleRelease;
            public IntPtr UpdateDefinition_GetSchema;
            public IntPtr UpdateDefinition_GetScheduleType;
            public IntPtr UpdateDefinition_GetScheduleValue;
            //Contract
            public IntPtr Contract_SetPreDestroyCallback;
            public IntPtr Contract_HandleCopy;
            public IntPtr Contract_HandleRelease;
            public IntPtr Contract_SetAppData;
            public IntPtr Contract_GetAppData;            
            public IntPtr Contract_GetLocalRole;
            public IntPtr Contract_GetLink;
            public IntPtr Contract_GetSchema;
            public IntPtr Contract_GetUpdateDefinition;
            public IntPtr Contract_GetReceiveTimeOffset;
            public IntPtr Contract_GetIncomingData;
            public IntPtr Contract_GetTsMode;
            public IntPtr Contract_CanBlock;
            public IntPtr Contract_GetBlockTime;
            public IntPtr Contract_IsBlocked;
            public IntPtr Contract_Trigger;
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        static IntPtr dllLibrary = IntPtr.Zero;

        static public void DllInit(string Path)
        {
            var dllPath= System.IO.Path.Combine(Path, IntPtr.Size == 8 ? "vs_connect_64.dll" : "vs_connect_32.dll");
            dllLibrary = LoadLibrary(dllPath);
            if (dllLibrary == IntPtr.Zero) return;

            var VsConnectApi_GetPtr = GetProcAddress(dllLibrary, "VsConnectApi_Get");
            if (VsConnectApi_GetPtr == IntPtr.Zero) return;
            var VsConnectApi_Get = Marshal.GetDelegateForFunctionPointer(VsConnectApi_GetPtr, typeof(VsConnectApi_Get)) as VsConnectApi_Get;


            vsc_Api_V3_tPtr* api = VsConnectApi_Get(VsConnectApi_GetDefaultVersion);

            SetLogFunc = Marshal.GetDelegateForFunctionPointer(api->SetLogFunc, typeof(delegates.SetLogFunc)) as delegates.SetLogFunc;
            SetLogLevel = Marshal.GetDelegateForFunctionPointer(api->SetLogLevel, typeof(delegates.SetLogLevel)) as delegates.SetLogLevel;
            Init = Marshal.GetDelegateForFunctionPointer(api->Init, typeof(delegates.Init)) as delegates.Init;
            IsInitialized = Marshal.GetDelegateForFunctionPointer(api->IsInitialized, typeof(delegates.IsInitialized)) as delegates.IsInitialized;
            Shutdown = Marshal.GetDelegateForFunctionPointer(api->Shutdown, typeof(delegates.Shutdown)) as delegates.Shutdown;
            DescribeResult = Marshal.GetDelegateForFunctionPointer(api->DescribeResult, typeof(delegates.DescribeResult)) as delegates.DescribeResult;
            IsValidDouble = Marshal.GetDelegateForFunctionPointer(api->IsValidDouble, typeof(delegates.IsValidDouble)) as delegates.IsValidDouble;
            GetInvalidDouble = Marshal.GetDelegateForFunctionPointer(api->GetInvalidDouble, typeof(delegates.GetInvalidDouble)) as delegates.GetInvalidDouble;
            InvalidateDouble = Marshal.GetDelegateForFunctionPointer(api->GetInvalidDouble, typeof(delegates.InvalidateDouble)) as delegates.InvalidateDouble;
            IsValidSimtime = Marshal.GetDelegateForFunctionPointer(api->IsValidSimtime, typeof(delegates.IsValidSimtime)) as delegates.IsValidSimtime;
            GetInvalidSimtime = Marshal.GetDelegateForFunctionPointer(api->GetInvalidSimtime, typeof(delegates.GetInvalidSimtime)) as delegates.GetInvalidSimtime;
            InvalidateTime = Marshal.GetDelegateForFunctionPointer(api->InvalidateTime, typeof(delegates.InvalidateTime)) as delegates.InvalidateTime;
            IsValidSerialNumber = Marshal.GetDelegateForFunctionPointer(api->IsValidSerialNumber, typeof(delegates.IsValidSerialNumber)) as delegates.IsValidSerialNumber;
            GetInvalidSerialNumber = Marshal.GetDelegateForFunctionPointer(api->GetInvalidSerialNumber, typeof(delegates.GetInvalidSerialNumber)) as delegates.GetInvalidSerialNumber;
            GetFirstSerialNumber = Marshal.GetDelegateForFunctionPointer(api->GetFirstSerialNumber, typeof(delegates.GetFirstSerialNumber)) as delegates.GetFirstSerialNumber;
            GetLastSerialNumber = Marshal.GetDelegateForFunctionPointer(api->GetLastSerialNumber, typeof(delegates.GetLastSerialNumber)) as delegates.GetLastSerialNumber;
            GetDefaultListenPort = Marshal.GetDelegateForFunctionPointer(api->GetDefaultListenPort, typeof(delegates.GetDefaultListenPort)) as delegates.GetDefaultListenPort;
            DescribeTsModePtr = Marshal.GetDelegateForFunctionPointer(api->DescribeTsMode, typeof(delegates.DescribeTsModePtr)) as delegates.DescribeTsModePtr;

            
            Node_Create_UDPIP = Marshal.GetDelegateForFunctionPointer(api->Node_Create_UDPIP, typeof(delegates.Node_Create_UDPIP)) as delegates.Node_Create_UDPIP;
            Node_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->Node_SetPreDestroyCallback, typeof(delegates.Node_SetPreDestroyCallback)) as delegates.Node_SetPreDestroyCallback;
            Node_SetAppData = Marshal.GetDelegateForFunctionPointer(api->Node_SetAppData, typeof(delegates.Node_SetAppData)) as delegates.Node_SetAppData;
            Node_GetAppData = Marshal.GetDelegateForFunctionPointer(api->Node_GetAppData, typeof(delegates.Node_GetAppData)) as delegates.Node_GetAppData;
            Node_GetNumLinks = Marshal.GetDelegateForFunctionPointer(api->Node_GetNumLinks, typeof(delegates.Node_GetNumLinks)) as delegates.Node_GetNumLinks;
            Node_GetLink = Marshal.GetDelegateForFunctionPointer(api->Node_GetLink, typeof(delegates.Node_GetLink)) as delegates.Node_GetLink;
            Node_Service = Marshal.GetDelegateForFunctionPointer(api->Node_Service, typeof(delegates.Node_Service)) as delegates.Node_Service;
            Node_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->Node_HandleCopy, typeof(delegates.Node_HandleCopy)) as delegates.Node_HandleCopy;
            Node_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->Node_HandleRelease, typeof(delegates.Node_HandleRelease)) as delegates.Node_HandleRelease;
            Node_ConnectionRequestsIgnored = Marshal.GetDelegateForFunctionPointer(api->Node_ConnectionRequestsIgnored, typeof(delegates.Node_ConnectionRequestsIgnored)) as delegates.Node_ConnectionRequestsIgnored;
            Node_IgnoreConnectionRequests = Marshal.GetDelegateForFunctionPointer(api->Node_IgnoreConnectionRequests, typeof(delegates.Node_IgnoreConnectionRequests)) as delegates.Node_IgnoreConnectionRequests;
            Node_GetTimeInfo = Marshal.GetDelegateForFunctionPointer(api->Node_GetTimeInfo, typeof(delegates.Node_GetTimeInfo)) as delegates.Node_GetTimeInfo;
            Node_CanBlock = Marshal.GetDelegateForFunctionPointer(api->Node_CanBlock, typeof(delegates.Node_CanBlock)) as delegates.Node_CanBlock;
            Node_IsBlocked = Marshal.GetDelegateForFunctionPointer(api->Node_IsBlocked, typeof(delegates.Node_IsBlocked)) as delegates.Node_IsBlocked;
            Node_GetTsMode = Marshal.GetDelegateForFunctionPointer(api->Node_GetTsMode, typeof(delegates.Node_GetTsMode)) as delegates.Node_GetTsMode;
            Node_GetTsContract = Marshal.GetDelegateForFunctionPointer(api->Node_GetTsContract, typeof(delegates.Node_GetTsContract)) as delegates.Node_GetTsContract;

            Link_Create_UDPIP = Marshal.GetDelegateForFunctionPointer(api->Link_Create_UDPIP, typeof(delegates.Link_Create_UDPIP)) as delegates.Link_Create_UDPIP;
            Link_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->Link_SetPreDestroyCallback, typeof(delegates.Link_SetPreDestroyCallback)) as delegates.Link_SetPreDestroyCallback;
            Link_GetNode = Marshal.GetDelegateForFunctionPointer(api->Link_GetNode, typeof(delegates.Link_GetNode)) as delegates.Link_GetNode;
            Link_SetAppData = Marshal.GetDelegateForFunctionPointer(api->Link_SetAppData, typeof(delegates.Link_SetAppData)) as delegates.Link_SetAppData;
            Link_GetAppData = Marshal.GetDelegateForFunctionPointer(api->Link_GetAppData, typeof(delegates.Link_GetAppData)) as delegates.Link_GetAppData;
            Link_CreateIncomingContract = Marshal.GetDelegateForFunctionPointer(api->Link_CreateIncomingContract, typeof(delegates.Link_CreateIncomingContract)) as delegates.Link_CreateIncomingContract;
            Link_CreateOutgoingContract = Marshal.GetDelegateForFunctionPointer(api->Link_CreateOutgoingContract, typeof(delegates.Link_CreateOutgoingContract)) as delegates.Link_CreateOutgoingContract;
            Link_GetTsContract = Marshal.GetDelegateForFunctionPointer(api->Link_GetTsContract, typeof(delegates.Link_GetTsContract)) as delegates.Link_GetTsContract;
            Link_Connect_Async = Marshal.GetDelegateForFunctionPointer(api->Link_Connect_Async, typeof(delegates.Link_Connect_Async)) as delegates.Link_Connect_Async;
            Link_Disconnect = Marshal.GetDelegateForFunctionPointer(api->Link_Disconnect, typeof(delegates.Link_Disconnect)) as delegates.Link_Disconnect;
            Link_GetConnectionStatus = Marshal.GetDelegateForFunctionPointer(api->Link_GetConnectionStatus, typeof(delegates.Link_GetConnectionStatus)) as delegates.Link_GetConnectionStatus;
            Link_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->Link_HandleCopy, typeof(delegates.Link_HandleCopy)) as delegates.Link_HandleCopy;
            Link_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->Link_HandleRelease, typeof(delegates.Link_HandleRelease)) as delegates.Link_HandleRelease;
            Link_GetInitiator = Marshal.GetDelegateForFunctionPointer(api->Link_GetInitiator, typeof(delegates.Link_GetInitiator)) as delegates.Link_GetInitiator;
            Link_PingInitiate = Marshal.GetDelegateForFunctionPointer(api->Link_PingInitiate, typeof(delegates.Link_PingInitiate)) as delegates.Link_PingInitiate;
            Link_PingSetPeriodSec = Marshal.GetDelegateForFunctionPointer(api->Link_PingSetPeriodSec, typeof(delegates.Link_PingSetPeriodSec)) as delegates.Link_PingSetPeriodSec;
            Link_PingGetStat = Marshal.GetDelegateForFunctionPointer(api->Link_PingGetStat, typeof(delegates.Link_PingGetStat)) as delegates.Link_PingGetStat;
            Link_PingGetAppData = Marshal.GetDelegateForFunctionPointer(api->Link_PingGetAppData, typeof(delegates.Link_PingGetAppData)) as delegates.Link_PingGetAppData;
            Link_GetRttMean = Marshal.GetDelegateForFunctionPointer(api->Link_GetRttMean, typeof(delegates.Link_GetRttMean)) as delegates.Link_GetRttMean;
            Link_CanBlock = Marshal.GetDelegateForFunctionPointer(api->Link_CanBlock, typeof(delegates.Link_CanBlock)) as delegates.Link_CanBlock;
            Link_IsBlocked = Marshal.GetDelegateForFunctionPointer(api->Link_IsBlocked, typeof(delegates.Link_IsBlocked)) as delegates.Link_IsBlocked;


            Schema_Create = Marshal.GetDelegateForFunctionPointer(api->Schema_Create, typeof(delegates.Schema_Create)) as delegates.Schema_Create;
            Schema_InitField = Marshal.GetDelegateForFunctionPointer(api->Schema_InitField, typeof(delegates.Schema_InitField)) as delegates.Schema_InitField;
            Schema_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->Schema_SetPreDestroyCallback, typeof(delegates.Schema_SetPreDestroyCallback)) as delegates.Schema_SetPreDestroyCallback;
            Schema_GetNumFields = Marshal.GetDelegateForFunctionPointer(api->Schema_GetNumFields, typeof(delegates.Schema_GetNumFields)) as delegates.Schema_GetNumFields;
            Schema_GetField = Marshal.GetDelegateForFunctionPointer(api->Schema_GetField, typeof(delegates.Schema_GetField)) as delegates.Schema_GetField;
            Schema_SetAppData = Marshal.GetDelegateForFunctionPointer(api->Schema_SetAppData, typeof(delegates.Schema_SetAppData)) as delegates.Schema_SetAppData;
            Schema_GetAppData = Marshal.GetDelegateForFunctionPointer(api->Schema_GetAppData, typeof(delegates.Schema_GetAppData)) as delegates.Schema_GetAppData;
            Schema_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->Schema_HandleCopy, typeof(delegates.Schema_HandleCopy)) as delegates.Schema_HandleCopy;
            Schema_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->Schema_HandleRelease, typeof(delegates.Schema_HandleRelease)) as delegates.Schema_HandleRelease;


            Field_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->Field_SetPreDestroyCallback, typeof(delegates.Field_SetPreDestroyCallback)) as delegates.Field_SetPreDestroyCallback;
            Field_GetObjectNamePtr = Marshal.GetDelegateForFunctionPointer(api->Field_GetObjectName, typeof(delegates.Field_GetObjectNamePtr)) as delegates.Field_GetObjectNamePtr;
            Field_GetPropertyNamePtr = Marshal.GetDelegateForFunctionPointer(api->Field_GetPropertyName, typeof(delegates.Field_GetPropertyNamePtr)) as delegates.Field_GetPropertyNamePtr;
            Field_GetParamsPtr = Marshal.GetDelegateForFunctionPointer(api->Field_GetParams, typeof(delegates.Field_GetParamsPtr)) as delegates.Field_GetParamsPtr;
            Field_GetDataType = Marshal.GetDelegateForFunctionPointer(api->Field_GetDataType, typeof(delegates.Field_GetDataType)) as delegates.Field_GetDataType;
            Field_GetElementSizeInBits = Marshal.GetDelegateForFunctionPointer(api->Field_GetElementSizeInBits, typeof(delegates.Field_GetElementSizeInBits)) as delegates.Field_GetElementSizeInBits;
            Field_GetNumElements = Marshal.GetDelegateForFunctionPointer(api->Field_GetNumElements, typeof(delegates.Field_GetNumElements)) as delegates.Field_GetNumElements;
            Field_GetInterpMethod = Marshal.GetDelegateForFunctionPointer(api->Field_GetInterpMethod, typeof(delegates.Field_GetInterpMethod)) as delegates.Field_GetInterpMethod;
            Field_GetExtrapMethod = Marshal.GetDelegateForFunctionPointer(api->Field_GetExtrapMethod, typeof(delegates.Field_GetExtrapMethod)) as delegates.Field_GetExtrapMethod;
            Field_GetRoundingMethod = Marshal.GetDelegateForFunctionPointer(api->Field_GetRoundingMethod, typeof(delegates.Field_GetRoundingMethod)) as delegates.Field_GetRoundingMethod;
            Field_SetAppData = Marshal.GetDelegateForFunctionPointer(api->Field_SetAppData, typeof(delegates.Field_SetAppData)) as delegates.Field_SetAppData;
            Field_GetAppData = Marshal.GetDelegateForFunctionPointer(api->Field_GetAppData, typeof(delegates.Field_GetAppData)) as delegates.Field_GetAppData;
            Field_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->Field_HandleCopy, typeof(delegates.Field_HandleCopy)) as delegates.Field_HandleCopy;
            Field_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->Field_HandleRelease, typeof(delegates.Field_HandleRelease)) as delegates.Field_HandleRelease;


            UpdateData_CreateFromSchema = Marshal.GetDelegateForFunctionPointer(api->UpdateData_CreateFromSchema, typeof(delegates.UpdateData_CreateFromSchema)) as delegates.UpdateData_CreateFromSchema;
            UpdateData_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->UpdateData_SetPreDestroyCallback, typeof(delegates.UpdateData_SetPreDestroyCallback)) as delegates.UpdateData_SetPreDestroyCallback;
            UpdateData_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->UpdateData_HandleCopy, typeof(delegates.UpdateData_HandleCopy)) as delegates.UpdateData_HandleCopy;
            UpdateData_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->UpdateData_HandleRelease, typeof(delegates.UpdateData_HandleRelease)) as delegates.UpdateData_HandleRelease;
            UpdateData_GetSchema = Marshal.GetDelegateForFunctionPointer(api->UpdateData_GetSchema, typeof(delegates.UpdateData_GetSchema)) as delegates.UpdateData_GetSchema;
            UpdateData_CopyData = Marshal.GetDelegateForFunctionPointer(api->UpdateData_CopyData, typeof(delegates.UpdateData_CopyData)) as delegates.UpdateData_CopyData;
            UpdateData_GetSerialNumber = Marshal.GetDelegateForFunctionPointer(api->UpdateData_GetSerialNumber, typeof(delegates.UpdateData_GetSerialNumber)) as delegates.UpdateData_GetSerialNumber;
            UpdateData_GetSimtime = Marshal.GetDelegateForFunctionPointer(api->UpdateData_GetSimtime, typeof(delegates.UpdateData_GetSimtime)) as delegates.UpdateData_GetSimtime;
            UpdateData_GetTimeDilation = Marshal.GetDelegateForFunctionPointer(api->UpdateData_GetTimeDilation, typeof(delegates.UpdateData_GetTimeDilation)) as delegates.UpdateData_GetTimeDilation;
            UpdateData_GetFieldValue = Marshal.GetDelegateForFunctionPointer(api->UpdateData_GetFieldValue, typeof(delegates.UpdateData_GetFieldValue)) as delegates.UpdateData_GetFieldValue;
            UpdateData_SetSimtime = Marshal.GetDelegateForFunctionPointer(api->UpdateData_SetSimtime, typeof(delegates.UpdateData_SetSimtime)) as delegates.UpdateData_SetSimtime;
            UpdateDefinition_Create = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_Create, typeof(delegates.UpdateDefinition_Create)) as delegates.UpdateDefinition_Create;
            UpdateDefinition_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_SetPreDestroyCallback, typeof(delegates.UpdateDefinition_SetPreDestroyCallback)) as delegates.UpdateDefinition_SetPreDestroyCallback;
            UpdateDefinition_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_HandleCopy, typeof(delegates.UpdateDefinition_HandleCopy)) as delegates.UpdateDefinition_HandleCopy;
            UpdateDefinition_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_HandleRelease, typeof(delegates.UpdateDefinition_HandleRelease)) as delegates.UpdateDefinition_HandleRelease;
            UpdateDefinition_GetSchema = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_GetSchema, typeof(delegates.UpdateDefinition_GetSchema)) as delegates.UpdateDefinition_GetSchema;
            UpdateDefinition_GetScheduleType = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_GetScheduleType, typeof(delegates.UpdateDefinition_GetScheduleType)) as delegates.UpdateDefinition_GetScheduleType;
            UpdateDefinition_GetScheduleValue = Marshal.GetDelegateForFunctionPointer(api->UpdateDefinition_GetScheduleValue, typeof(delegates.UpdateDefinition_GetScheduleValue)) as delegates.UpdateDefinition_GetScheduleValue;



            Contract_SetPreDestroyCallback = Marshal.GetDelegateForFunctionPointer(api->Contract_SetPreDestroyCallback, typeof(delegates.Contract_SetPreDestroyCallback)) as delegates.Contract_SetPreDestroyCallback;
            Contract_GetLocalRole = Marshal.GetDelegateForFunctionPointer(api->Contract_GetLocalRole, typeof(delegates.Contract_GetLocalRole)) as delegates.Contract_GetLocalRole;
            Contract_SetAppData = Marshal.GetDelegateForFunctionPointer(api->Contract_SetAppData, typeof(delegates.Contract_SetAppData)) as delegates.Contract_SetAppData;
            Contract_GetAppData = Marshal.GetDelegateForFunctionPointer(api->Contract_GetAppData, typeof(delegates.Contract_GetAppData)) as delegates.Contract_GetAppData;
            Contract_GetLink = Marshal.GetDelegateForFunctionPointer(api->Contract_GetLink, typeof(delegates.Contract_GetLink)) as delegates.Contract_GetLink;
            Contract_HandleCopy = Marshal.GetDelegateForFunctionPointer(api->Contract_HandleCopy, typeof(delegates.Contract_HandleCopy)) as delegates.Contract_HandleCopy;
            Contract_HandleRelease = Marshal.GetDelegateForFunctionPointer(api->Contract_HandleRelease, typeof(delegates.Contract_HandleRelease)) as delegates.Contract_HandleRelease;
            Contract_GetSchema = Marshal.GetDelegateForFunctionPointer(api->Contract_GetSchema, typeof(delegates.Contract_GetSchema)) as delegates.Contract_GetSchema;
            Contract_GetUpdateDefinition = Marshal.GetDelegateForFunctionPointer(api->Contract_GetUpdateDefinition, typeof(delegates.Contract_GetUpdateDefinition)) as delegates.Contract_GetUpdateDefinition;           
            Contract_GetReceiveTimeOffset = Marshal.GetDelegateForFunctionPointer(api->Contract_GetReceiveTimeOffset, typeof(delegates.Contract_GetReceiveTimeOffset)) as delegates.Contract_GetReceiveTimeOffset;
            Contract_GetIncomingData = Marshal.GetDelegateForFunctionPointer(api->Contract_GetIncomingData, typeof(delegates.Contract_GetIncomingData)) as delegates.Contract_GetIncomingData;
            Contract_GetTsMode = Marshal.GetDelegateForFunctionPointer(api->Contract_GetTsMode, typeof(delegates.Contract_GetTsMode)) as delegates.Contract_GetTsMode;
            Contract_GetBlockTime = Marshal.GetDelegateForFunctionPointer(api->Contract_GetBlockTime, typeof(delegates.Contract_GetBlockTime)) as delegates.Contract_GetBlockTime;
            Contract_Trigger = Marshal.GetDelegateForFunctionPointer(api->Contract_Trigger, typeof(delegates.Contract_Trigger)) as delegates.Contract_Trigger;
            Contract_CanBlock = Marshal.GetDelegateForFunctionPointer(api->Contract_CanBlock, typeof(delegates.Contract_CanBlock)) as delegates.Contract_CanBlock;
            Contract_IsBlocked = Marshal.GetDelegateForFunctionPointer(api->Contract_IsBlocked, typeof(delegates.Contract_IsBlocked)) as delegates.Contract_IsBlocked;


        }

        static public void DLLShutDown()
        {
            if (dllLibrary == IntPtr.Zero) return;
            FreeLibrary(dllLibrary);
        }

        #endregion
    }



}
