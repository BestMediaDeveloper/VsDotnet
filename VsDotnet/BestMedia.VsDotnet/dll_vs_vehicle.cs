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
using static BestMedia.VsDotnet.VsUtility;



/// <summary>
/// This file is a version of vs_vehicle.h(VS_SDK) converted for C #.
/// See vs_vehicle.h or vs_sdk for details
/// </summary>
namespace BestMedia.VsDotnet.vs_vehicle
{

    /// <summary>
    /// VSEW_VEHICLE_HANDLE In VS_SDK, this is just a pointer, but we redefine it in a struct to distinguish the names
    /// </summary>
    public struct VSEW_VEHICLE_HANDLE { public IntPtr ptr; public VSEW_VEHICLE_HANDLE(IntPtr setPtr) { ptr = setPtr; } public static implicit operator IntPtr(VSEW_VEHICLE_HANDLE _) => _.ptr; public static implicit operator VSEW_VEHICLE_HANDLE(IntPtr _) => new VSEW_VEHICLE_HANDLE(_); }

    public enum AxleSide : byte
    {
        Left = 0, Right = 1,
    };


    public enum WheelSide : byte
    {
        Inner = 0, Outer = 1,
    };



  

    [StructLayout(LayoutKind.Sequential)]
    public struct VsRoadInfo
    {
        public double mCarSimLocX;  ///< X (CarSim), Z (Unity)
        public double mCarSimLocY;  ///< Y (CarSim), X (Unity)
        public double mCarSimLocZ;  ///< Z (CarSim), Y (Unity)
        public double mCarSimDzDx;  ///< Slope in global X direction.
        public double mCarSimDzDy;  ///< Slope in global Y direction.
        public double mCarSimMu;    ///< Surface Mu.

        public IntPtr mCollisionInfo;

    };

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool GetRoadInfo(IntPtr callbackData
        , double carSimX0
        , double carSimY0
        , double carSimZ0
        , double carSimX1
        , double carSimY1
        , double carSimZ1
        , ref VsRoadInfo out_roadInfo);




    unsafe static public class VS_Vehicle
    {
        static bool Initialized = false;
        static IntPtr DllHandle = IntPtr.Zero;


        
        static IntPtr CreateVehiclePtr = IntPtr.Zero;
        static IntPtr ReinitializeVehiclePtr = IntPtr.Zero;
        static IntPtr IsValidVehiclePtr = IntPtr.Zero;
        static IntPtr IsOkPtr = IntPtr.Zero;
        static IntPtr GetVarPtrPtr = IntPtr.Zero;
        static IntPtr GetVarPtrIntPtr = IntPtr.Zero;
        static IntPtr ExecuteVsCommandPtr = IntPtr.Zero;
        static IntPtr GetNumUnitsPtr = IntPtr.Zero;
        static IntPtr GetNumAxlesPtr = IntPtr.Zero;
        static IntPtr GetAxleOffsetForwardPtr = IntPtr.Zero;
        static IntPtr GetAxleTrackPtr = IntPtr.Zero;
        static IntPtr GetDualWidthPtr = IntPtr.Zero;
        static IntPtr GetWorldPositionPtr = IntPtr.Zero;
        static IntPtr GetWorldOrientationPtr = IntPtr.Zero;
        static IntPtr GetTireWorldPositionPtr = IntPtr.Zero;
        static IntPtr GetTireWorldOrientationPtr = IntPtr.Zero;
        static IntPtr GetTireSolverRotationPtr = IntPtr.Zero;
        static IntPtr SetWorldPositionPtr = IntPtr.Zero;
        static IntPtr SetWorldOrientationPtr = IntPtr.Zero;
        static IntPtr SetDriverThrottlePtr = IntPtr.Zero;
        static IntPtr SetDriverBrakePedalNewtonsPtr = IntPtr.Zero;
        static IntPtr SetDriverSteerLeftDegreesPtr = IntPtr.Zero;
        static IntPtr IntegratePtr = IntPtr.Zero;
        static IntPtr DestroyVehiclePtr = IntPtr.Zero;
        static IntPtr GetVarPtr_NCPtr = IntPtr.Zero;
        static IntPtr GetVarPtrInt_NCPtr = IntPtr.Zero;
        static IntPtr Import_CountPtr = IntPtr.Zero;
        static IntPtr Import_AddPtr = IntPtr.Zero;
        static IntPtr Import_GetIdPtr = IntPtr.Zero;
        static IntPtr Import_GetOrAddPtr = IntPtr.Zero;
        static IntPtr Import_GetNamePtr = IntPtr.Zero;
        static IntPtr Import_SetValuePtr = IntPtr.Zero;
        static IntPtr Import_GetLerpPtr = IntPtr.Zero;

        public class delegates
        {
            
            //CreateVehicle
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool Reinitializee(VSEW_VEHICLE_HANDLE vehicle);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool IsValidVehicle(VSEW_VEHICLE_HANDLE vehicle);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool IsOk(VSEW_VEHICLE_HANDLE vehicle);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate ulong Import_Count(VSEW_VEHICLE_HANDLE vehicle);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int Import_Add(VSEW_VEHICLE_HANDLE vehicle, String name, double initialValue, bool lerp);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int Import_GetId(VSEW_VEHICLE_HANDLE vehicle, String name);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int Import_GetOrAdd(VSEW_VEHICLE_HANDLE vehicle, String name, double initialValue, bool lerp);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate String Import_GetName(VSEW_VEHICLE_HANDLE vehicle, int id);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool Import_GetLerp(VSEW_VEHICLE_HANDLE vehicle, int id);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void Import_SetValue(VSEW_VEHICLE_HANDLE vehicle, int id, double val);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int ExecuteVsCommand(VSEW_VEHICLE_HANDLE vehicleHandle, String key, String buffer);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate ulong GetNumUnits(VSEW_VEHICLE_HANDLE vehicle);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate ulong GetNumAxles(VSEW_VEHICLE_HANDLE vehicle, int unitIndex);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate double GetAxleOffsetForward(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, ulong axleIndex);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate double GetAxleTrack(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, ulong axleIndex);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate double GetDualWidth(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, ulong axleIndex);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool GetWorldPosition(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, ref double out_posForward, ref double out_posLeft, ref double out_posUp);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool GetWorldOrientation(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, ref double out_rollRight, ref double out_pitchDown, ref double out_yawLeft);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool GetTireWorldPosition(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, int axleIndex, int wheelSide, ref double out_posForward, ref double out_posLeft, ref double out_posUp);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool GetTireWorldOrientation(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, int axleIndex, int wheelSide, ref double out_rollRight, ref double out_pitchDown, ref double out_yawLeft);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate bool GetTireSolverRotation(VSEW_VEHICLE_HANDLE vehicle, int unitIndex, int axleIndex, int wheelSide, ref double out_rotPitchRad);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetWorldPosition(VSEW_VEHICLE_HANDLE vehicle, double posForward, double posLeft, double posUp);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetWorldOrientation(VSEW_VEHICLE_HANDLE vehicle, double rollRightRad, double pitchDownRad, double yawLeftRad);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetDriverThrottle(VSEW_VEHICLE_HANDLE vehicle, double throttleNorm);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetDriverBrakePedalNewtons(VSEW_VEHICLE_HANDLE vehicle, double brakePedalNewtons);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetDriverSteerLeftDegrees(VSEW_VEHICLE_HANDLE vehicle, double steerDeg);
            
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetDriverTransGear(VSEW_VEHICLE_HANDLE vehicle, int gearSelection);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void SetDriverClutchPedalDepressNorm(VSEW_VEHICLE_HANDLE vehicle, double clutchPedalDepressNorm);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int Integrate(VSEW_VEHICLE_HANDLE vehicle, double deltaTimeSec);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate void DestroyVehicle(VSEW_VEHICLE_HANDLE vehicle);


            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate double* GetVarPtr(VSEW_VEHICLE_HANDLE vehicle, String varName);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int* GetVarPtrInt(VSEW_VEHICLE_HANDLE vehicle, String varName);

            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate double* GetVarPtr_NC(VSEW_VEHICLE_HANDLE vehicle,String varName);
            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate int* GetVarPtrInt_NC(VSEW_VEHICLE_HANDLE vehicle, String varName);

            [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
            public delegate VSEW_VEHICLE_HANDLE
                            CreateVehicle( String simfile
                                              //, string[][] simfileMacros
                                              , IntPtr simfileMacros
                                              , ulong numSimfileMacros
                                              , bool useExternalDriverInput
                                              , GetRoadInfo roadInfoCallback
                                              , IntPtr roadInfoCallbackData
                                              , byte[] out_errorMsg 
                                              , ulong      errMsgSize
                                              );

        }



        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


        public static bool IsInitialized { get { return Initialized; } }
        public static bool DllIsLoaded { get { return DllHandle != IntPtr.Zero; } }

        static public void Initialize(String dllPath)
        {
            
            if (Initialized)
            {
                LogWarning(("Already initialized."));
                throw new InvalidOperationException("VsVehicleWrapper::Initialize called twice!");
            }
            else
            {
                DllHandle = LoadLibrary(dllPath);

                if (DllHandle == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Unable to load VS Vehicle DLL");
                }
                else
                {
                    
                    CreateVehiclePtr = GetProcAddress(DllHandle, "vsew_CreateVehicle");
                    ReinitializeVehiclePtr = GetProcAddress(DllHandle, "vsew_ReinitializeVehicle");
                    IsValidVehiclePtr = GetProcAddress(DllHandle, "vsew_IsValidVehicle");
                    IsOkPtr = GetProcAddress(DllHandle, "vsew_IsOk");
                    GetVarPtrPtr = GetProcAddress(DllHandle, "vsew_GetVarPtr");
                    GetVarPtrIntPtr = GetProcAddress(DllHandle, "vsew_GetVarPtrInt");
                    ExecuteVsCommandPtr = GetProcAddress(DllHandle, "vsew_ExecuteVsCommand");
                    GetNumUnitsPtr = GetProcAddress(DllHandle, "vsew_GetNumUnits");
                    GetNumAxlesPtr = GetProcAddress(DllHandle, "vsew_GetNumAxles");
                    GetAxleOffsetForwardPtr = GetProcAddress(DllHandle, "vsew_GetAxleOffsetForward");
                    GetAxleTrackPtr = GetProcAddress(DllHandle, "vsew_GetAxleTrack");
                    GetDualWidthPtr = GetProcAddress(DllHandle, "vsew_GetDualWidth");
                    GetWorldPositionPtr = GetProcAddress(DllHandle, "vsew_GetWorldPosition");
                    GetWorldOrientationPtr = GetProcAddress(DllHandle, "vsew_GetWorldOrientation");
                    GetTireWorldPositionPtr = GetProcAddress(DllHandle, "vsew_GetTireWorldPosition");
                    GetTireWorldOrientationPtr = GetProcAddress(DllHandle, "vsew_GetTireWorldOrientation");
                    GetTireSolverRotationPtr = GetProcAddress(DllHandle, "vsew_GetTireSolverRotation");
                    SetWorldPositionPtr = GetProcAddress(DllHandle, "vsew_SetWorldPosition");
                    SetWorldOrientationPtr = GetProcAddress(DllHandle, "vsew_SetWorldOrientation");
                    SetDriverThrottlePtr = GetProcAddress(DllHandle, "vsew_SetDriverThrottle");
                    SetDriverBrakePedalNewtonsPtr = GetProcAddress(DllHandle, "vsew_SetDriverBrakePedalNewtons");
                    SetDriverSteerLeftDegreesPtr = GetProcAddress(DllHandle, "vsew_SetDriverSteerLeftDegrees");                    
                    IntegratePtr = GetProcAddress(DllHandle, "vsew_Integrate");
                    DestroyVehiclePtr = GetProcAddress(DllHandle, "vsew_DestroyVehicle");
                    GetVarPtr_NCPtr = GetProcAddress(DllHandle, "vsew_GetVarPtr_NC");
                    GetVarPtrInt_NCPtr = GetProcAddress(DllHandle, "vsew_GetVarPtrInt_NC");
                    Import_CountPtr = GetProcAddress(DllHandle, "vsew_Import_Count");
                    Import_AddPtr = GetProcAddress(DllHandle, "vsew_Import_Add");
                    Import_GetIdPtr = GetProcAddress(DllHandle, "vsew_Import_GetId");
                    Import_GetOrAddPtr = GetProcAddress(DllHandle, "vsew_Import_GetOrAdd");
                    Import_GetNamePtr = GetProcAddress(DllHandle, "vsew_Import_GetName");
                    Import_SetValuePtr = GetProcAddress(DllHandle, "vsew_Import_SetValue");
                    Import_GetLerpPtr = GetProcAddress(DllHandle, "vsew_Import_GetLerp");

                    if (
                        IntPtr.Zero == CreateVehiclePtr
                        || IntPtr.Zero == ReinitializeVehiclePtr
                        || IntPtr.Zero == IsValidVehiclePtr
                        || IntPtr.Zero == IsOkPtr
                        || IntPtr.Zero == GetVarPtrPtr
                        || IntPtr.Zero == GetVarPtrIntPtr
                        || IntPtr.Zero == ExecuteVsCommandPtr
                        || IntPtr.Zero == GetNumUnitsPtr
                        || IntPtr.Zero == GetNumAxlesPtr
                        || IntPtr.Zero == GetAxleOffsetForwardPtr
                        || IntPtr.Zero == GetAxleTrackPtr
                        || IntPtr.Zero == GetDualWidthPtr
                        || IntPtr.Zero == GetWorldPositionPtr
                        || IntPtr.Zero == GetWorldOrientationPtr
                        || IntPtr.Zero == SetWorldPositionPtr
                        || IntPtr.Zero == GetTireWorldPositionPtr
                        || IntPtr.Zero == GetTireWorldOrientationPtr
                        || IntPtr.Zero == GetTireSolverRotationPtr
                        || IntPtr.Zero == SetWorldOrientationPtr
                        || IntPtr.Zero == SetDriverThrottlePtr
                        || IntPtr.Zero == SetDriverBrakePedalNewtonsPtr
                        || IntPtr.Zero == SetDriverSteerLeftDegreesPtr                        
                        || IntPtr.Zero == IntegratePtr
                        || IntPtr.Zero == DestroyVehiclePtr
                        || IntPtr.Zero == GetVarPtr_NCPtr
                        || IntPtr.Zero == GetVarPtrInt_NCPtr
                        || IntPtr.Zero == Import_CountPtr
                        || IntPtr.Zero == Import_AddPtr
                        || IntPtr.Zero == Import_GetIdPtr
                        || IntPtr.Zero == Import_GetOrAddPtr
                        || IntPtr.Zero == Import_GetNamePtr
                        || IntPtr.Zero == Import_SetValuePtr
                        || IntPtr.Zero == Import_GetLerpPtr
                        )
                    {
                        ShutDown();

                        throw new InvalidOperationException("Unable to map required functions in DLL file");

                    }
                    else
                    {
                        
                        
                        CreateVehicle = Marshal.GetDelegateForFunctionPointer(CreateVehiclePtr, typeof(delegates.CreateVehicle)) as delegates.CreateVehicle;
                        ReinitializeVehicle = Marshal.GetDelegateForFunctionPointer(IsValidVehiclePtr, typeof(delegates.Reinitializee)) as delegates.Reinitializee;
                        IsValidVehicle = Marshal.GetDelegateForFunctionPointer(IsValidVehiclePtr, typeof(delegates.IsValidVehicle)) as delegates.IsValidVehicle;
                        IsOk = Marshal.GetDelegateForFunctionPointer(IsOkPtr, typeof(delegates.IsOk)) as delegates.IsOk;
                        Import_Count = Marshal.GetDelegateForFunctionPointer(Import_CountPtr, typeof(delegates.Import_Count)) as delegates.Import_Count;
                        Import_Add = Marshal.GetDelegateForFunctionPointer(Import_AddPtr, typeof(delegates.Import_Add)) as delegates.Import_Add;
                        Import_GetId = Marshal.GetDelegateForFunctionPointer(Import_GetIdPtr, typeof(delegates.Import_GetId)) as delegates.Import_GetId;
                        Import_GetOrAdd = Marshal.GetDelegateForFunctionPointer(Import_GetOrAddPtr, typeof(delegates.Import_GetOrAdd)) as delegates.Import_GetOrAdd;
                        Import_GetName = Marshal.GetDelegateForFunctionPointer(Import_GetNamePtr, typeof(delegates.Import_GetName)) as delegates.Import_GetName;
                        Import_GetLerp = Marshal.GetDelegateForFunctionPointer(Import_GetLerpPtr, typeof(delegates.Import_GetLerp)) as delegates.Import_GetLerp;
                        Import_SetValue = Marshal.GetDelegateForFunctionPointer(Import_SetValuePtr, typeof(delegates.Import_SetValue)) as delegates.Import_SetValue;
                        GetVarPtr = Marshal.GetDelegateForFunctionPointer(GetVarPtrPtr, typeof(delegates.GetVarPtr)) as delegates.GetVarPtr;
                        GetVarPtrInt = Marshal.GetDelegateForFunctionPointer(GetVarPtrIntPtr, typeof(delegates.GetVarPtrInt)) as delegates.GetVarPtrInt;
                        ExecuteVsCommand = Marshal.GetDelegateForFunctionPointer(ExecuteVsCommandPtr, typeof(delegates.ExecuteVsCommand)) as delegates.ExecuteVsCommand;
                        GetNumUnits = Marshal.GetDelegateForFunctionPointer(GetNumUnitsPtr, typeof(delegates.GetNumUnits)) as delegates.GetNumUnits;
                        GetNumAxles = Marshal.GetDelegateForFunctionPointer(GetNumAxlesPtr, typeof(delegates.GetNumAxles)) as delegates.GetNumAxles;
                        GetAxleOffsetForward = Marshal.GetDelegateForFunctionPointer(GetAxleOffsetForwardPtr, typeof(delegates.GetAxleOffsetForward)) as delegates.GetAxleOffsetForward;
                        GetAxleTrack = Marshal.GetDelegateForFunctionPointer(GetAxleTrackPtr, typeof(delegates.GetAxleTrack)) as delegates.GetAxleTrack;
                        GetDualWidth = Marshal.GetDelegateForFunctionPointer(GetDualWidthPtr, typeof(delegates.GetDualWidth)) as delegates.GetDualWidth;
                        GetWorldPosition = Marshal.GetDelegateForFunctionPointer(GetWorldPositionPtr, typeof(delegates.GetWorldPosition)) as delegates.GetWorldPosition;
                        GetWorldOrientation = Marshal.GetDelegateForFunctionPointer(GetWorldOrientationPtr, typeof(delegates.GetWorldOrientation)) as delegates.GetWorldOrientation;
                        GetTireWorldPosition = Marshal.GetDelegateForFunctionPointer(GetTireWorldPositionPtr, typeof(delegates.GetTireWorldPosition)) as delegates.GetTireWorldPosition;
                        GetTireWorldOrientation = Marshal.GetDelegateForFunctionPointer(GetTireWorldOrientationPtr, typeof(delegates.GetTireWorldOrientation)) as delegates.GetTireWorldOrientation;
                        GetTireSolverRotation = Marshal.GetDelegateForFunctionPointer(GetTireSolverRotationPtr, typeof(delegates.GetTireSolverRotation)) as delegates.GetTireSolverRotation;
                        SetWorldPosition = Marshal.GetDelegateForFunctionPointer(SetWorldPositionPtr, typeof(delegates.SetWorldPosition)) as delegates.SetWorldPosition;
                        SetWorldOrientation = Marshal.GetDelegateForFunctionPointer(SetWorldOrientationPtr, typeof(delegates.SetWorldOrientation)) as delegates.SetWorldOrientation;
                        SetDriverThrottle = Marshal.GetDelegateForFunctionPointer(SetDriverThrottlePtr, typeof(delegates.SetDriverThrottle)) as delegates.SetDriverThrottle;
                        SetDriverBrakePedalNewtons = Marshal.GetDelegateForFunctionPointer(SetDriverBrakePedalNewtonsPtr, typeof(delegates.SetDriverBrakePedalNewtons)) as delegates.SetDriverBrakePedalNewtons;
                        SetDriverSteerLeftDegrees = Marshal.GetDelegateForFunctionPointer(SetDriverSteerLeftDegreesPtr, typeof(delegates.SetDriverSteerLeftDegrees)) as delegates.SetDriverSteerLeftDegrees;                        
                        
                        
                        Integrate = Marshal.GetDelegateForFunctionPointer(IntegratePtr, typeof(delegates.Integrate)) as delegates.Integrate;
                        DestroyVehicle = Marshal.GetDelegateForFunctionPointer(DestroyVehiclePtr, typeof(delegates.DestroyVehicle)) as delegates.DestroyVehicle;
                        GetVarPtr_NC = Marshal.GetDelegateForFunctionPointer(GetVarPtr_NCPtr, typeof(delegates.GetVarPtr_NC)) as delegates.GetVarPtr_NC;
                        GetVarPtrInt_NC = Marshal.GetDelegateForFunctionPointer(GetVarPtrInt_NCPtr, typeof(delegates.GetVarPtrInt_NC)) as delegates.GetVarPtrInt_NC;

                        Initialized = true;
                    }
                }
            }



        }
        public static void ShutDown()
        {
            DllHandle = IntPtr.Zero;
            
            CreateVehiclePtr = IntPtr.Zero;
            ReinitializeVehiclePtr = IntPtr.Zero;
            IsValidVehiclePtr = IntPtr.Zero;
            IsOkPtr = IntPtr.Zero;
            GetVarPtrPtr = IntPtr.Zero;
            GetVarPtrIntPtr = IntPtr.Zero;
            ExecuteVsCommandPtr = IntPtr.Zero;
            GetNumUnitsPtr = IntPtr.Zero;
            GetNumAxlesPtr = IntPtr.Zero;
            GetAxleOffsetForwardPtr = IntPtr.Zero;
            GetAxleTrackPtr = IntPtr.Zero;
            GetDualWidthPtr = IntPtr.Zero;
            GetWorldPositionPtr = IntPtr.Zero;
            GetWorldOrientationPtr = IntPtr.Zero;
            GetTireWorldPositionPtr = IntPtr.Zero;
            GetTireWorldOrientationPtr = IntPtr.Zero;
            GetTireSolverRotationPtr = IntPtr.Zero;
            SetWorldPositionPtr = IntPtr.Zero;
            SetWorldOrientationPtr = IntPtr.Zero;
            SetDriverThrottlePtr = IntPtr.Zero;
            SetDriverBrakePedalNewtonsPtr = IntPtr.Zero;
            SetDriverSteerLeftDegreesPtr = IntPtr.Zero;
            IntegratePtr = IntPtr.Zero;
            DestroyVehiclePtr = IntPtr.Zero;
            GetVarPtr_NCPtr = IntPtr.Zero;
            GetVarPtrInt_NCPtr = IntPtr.Zero;
            Import_CountPtr = IntPtr.Zero;
            Import_AddPtr = IntPtr.Zero;
            Import_GetIdPtr = IntPtr.Zero;
            Import_GetOrAddPtr = IntPtr.Zero;
            Import_GetNamePtr = IntPtr.Zero;
            Import_SetValuePtr = IntPtr.Zero;
            if (DllHandle != IntPtr.Zero)
            {
                FreeLibrary(DllHandle);
                DllHandle = IntPtr.Zero;
            }

            Initialized = false;
        }


        
        static public delegates.CreateVehicle CreateVehicle;
        static public delegates.Reinitializee ReinitializeVehicle;
        static public delegates.IsValidVehicle IsValidVehicle;
        static public delegates.IsOk IsOk;
        static public delegates.Import_Count Import_Count;
        static public delegates.Import_Add Import_Add;
        static public delegates.Import_GetId Import_GetId;
        static public delegates.Import_GetOrAdd Import_GetOrAdd;
        static public delegates.Import_GetName Import_GetName;
        static public delegates.Import_GetLerp Import_GetLerp;
        static public delegates.Import_SetValue Import_SetValue;
        static public delegates.GetVarPtr GetVarPtr;
        static public delegates.GetVarPtrInt GetVarPtrInt;
        static public delegates.ExecuteVsCommand ExecuteVsCommand;
        static public delegates.GetNumUnits GetNumUnits;
        static public delegates.GetNumAxles GetNumAxles;
        static public delegates.GetAxleOffsetForward GetAxleOffsetForward;
        static public delegates.GetAxleTrack GetAxleTrack;
        static public delegates.GetDualWidth GetDualWidth;
        static public delegates.GetWorldPosition GetWorldPosition;
        static public delegates.GetWorldOrientation GetWorldOrientation;
        static public delegates.GetTireWorldPosition GetTireWorldPosition;
        static public delegates.GetTireWorldOrientation GetTireWorldOrientation;
        static public delegates.GetTireSolverRotation GetTireSolverRotation;
        static public delegates.SetWorldPosition SetWorldPosition;
        static public delegates.SetWorldOrientation SetWorldOrientation;
        static public delegates.SetDriverThrottle SetDriverThrottle;
        static public delegates.SetDriverBrakePedalNewtons SetDriverBrakePedalNewtons;
        static public delegates.SetDriverSteerLeftDegrees SetDriverSteerLeftDegrees;        
        
        static public delegates.Integrate Integrate;
        static public delegates.DestroyVehicle DestroyVehicle;
        static public delegates.GetVarPtr_NC GetVarPtr_NC;
        static public delegates.GetVarPtrInt_NC GetVarPtrInt_NC;






    }

}
