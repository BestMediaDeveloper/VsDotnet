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
using UnityEngine;
using UnityEngine.Assertions;
using BestMedia.VsDotnet;
using BestMedia.VsDotnet.vs_vehicle;
using static BestMedia.VsDotnet.VsUtility;




namespace BestMedia.VsUnity
{



    /// <summary>
    /// This file is a version of CarSimMovementComponent.h(Unreal) converted for C #.
    /// See CarSimMovementComponent.h for details
    /// </summary>
    [DefaultExecutionOrder(-1)]
    [Serializable]
    unsafe public class CarSimMovementComponent : MonoBehaviour
    {

        /// <summary>
        /// Is integrate cars sim on fixed update
        /// </summary>
        public bool IsFixedUpdate = false;
        [SerializeField]
        public VsDotnet.VsVehilceData VehicleData = new VsDotnet.VsVehilceData();

        public VsVehicleSolver VehicleSolver { get; } = new VsVehicleSolver();

        
        IntPtr VsVehicleHandle => VehicleSolver.VsVehicleHandle;

	
		public float BodyOffset=0;
		public LayerMask RayLayerMask=Physics.DefaultRaycastLayers;
        bool Destroied = false;
        
        public bool DebugShowRay=false;
         

        #region Initialize_Vehicle_Create_SolverSet


        Vector3 InitialLocaion;          
        Quaternion InitialOrientation;   



        float UserThrottle = 0.0f;
        float UserBrake = 0.0f;
        float UserClutch = 0.0f;
        float UserSteer = 0.0f;

       
     

        bool RoadInfoCallback(IntPtr callbackData
                                    , double carSimX0
                                    , double carSimY0
                                    , double carSimZ0
                                    , double carSimX1
                                    , double carSimY1
                                    , double carSimZ1
                                    , ref BestMedia.VsDotnet.vs_vehicle.VsRoadInfo out_roadInfo)
        {   

            bool retSuccess = false;

            var MovementComponent = VsUtility.ToObject(callbackData) as CarSimMovementComponent;
            
            

            Vector3 StartVect = VsUnityLib.VSToUnityVector(carSimX0, carSimY0, carSimZ0);
            Vector3 EndVect = VsUnityLib.VSToUnityVector(carSimX1, carSimY1, carSimZ1);
            Vector3 DirVect = EndVect - StartVect;  DirVect.Normalize();


            Ray ray = new Ray(StartVect, DirVect);

            RaycastHit HitResult;
            bool roadPointFound = Physics.Raycast(ray, out HitResult, (StartVect - EndVect).magnitude, RayLayerMask, QueryTriggerInteraction.Ignore);
            if(DebugShowRay) Debug.DrawRay(ray.origin, ray.direction* (StartVect - EndVect).magnitude, Color.red,0.1f );

            if (!roadPointFound)
            {
                Check(retSuccess == false);
            }
            else
            {   
                if (HitResult.normal.y == 0)
                {   
                    Check(retSuccess == false);
                }
                else
                {


                    HitResult.point.UnityToVSVector(ref out_roadInfo.mCarSimLocX, ref out_roadInfo.mCarSimLocY, ref out_roadInfo.mCarSimLocZ);
                    double nx=0, ny = 0, nz = 0;
                    HitResult.normal.UnityToVSVector(ref nx, ref ny, ref nz);
                    out_roadInfo.mCarSimDzDx = nz / nx;
                    out_roadInfo.mCarSimDzDy = nz / ny;
                    
                    if(HitResult.collider!=null && HitResult.collider.material!=null)
                    {
                        out_roadInfo.mCarSimMu = HitResult.collider.material.dynamicFriction;
                    }else
                    {
                        out_roadInfo.mCarSimMu = 0.85f;
                    }

                    out_roadInfo.mCollisionInfo = IntPtr.Zero;

                    retSuccess = true;
                }
            }

            return retSuccess;
        }

       

        void ResetVehicleEvent(object sender,EventArgs e)
        {
            EngineRadPerSec = 0;
            EnginePowerNorm = 0;
            ForwardSpeed = 0;
            CurrentGear = 0;

            
        }

        void CreateVehicleEvent(object sender, EventArgs e)
        {

            if (!VehicleData.UseVehicleSimStartPosition)
            {
                this.gameObject.transform.position = ResetLocaion;
                this.gameObject.transform.rotation = ResetOrientation;

                SyncVsVehicleLocOri();
            }


        }

        void DestroyVehicleEvent(object sender, EventArgs e)
        {
           
        }



        public bool IsModuleLoadedAndActive
        {
            get
            {
                return VS_Vehicle.DllIsLoaded && VsVehicleHandle != IntPtr.Zero;
            }

        }

        internal Vector3 ResetLocaion { get; set; }

        internal Quaternion ResetOrientation { get; set; }


        public void SyncVsVehicleLocOri()
        {
            var wpos = this.gameObject.transform.position;
            var wrot = this.gameObject.transform.rotation;

            double forward = 0, left = 0, up = 0;
            VsUnityLib.UnityToVSVector(wpos, ref forward, ref left, ref up);
            double roll = 0, pitch = 0, yaw = 0;
            wrot.UnityToVSRotation(ref roll, ref pitch, ref yaw);
            VS_Vehicle.SetWorldPosition(VsVehicleHandle, forward, left, up);
            VS_Vehicle.SetWorldOrientation(VsVehicleHandle, roll, pitch, yaw);


            
        }
        #endregion

        #region UnityEvents

        private void Awake()
        {
           
            VehicleSolver.CreateVehicleEvent += CreateVehicleEvent;
            VehicleSolver.ResetVehicleEvent += ResetVehicleEvent;
            VehicleSolver.DestroyVehicleEvent += DestroyVehicleEvent;

            Log(("CarSimMovementComponent: BeginPlay."));

            VehicleSolver.Data = VehicleData;
            VehicleSolver.Data.RoadInfoDelegate = new GetRoadInfo(RoadInfoCallback);
            VehicleSolver.Data.RoadInfoData = this;


            VehicleSolver.SetSimFile();

            InitialLocaion = this.gameObject.transform.position;
            InitialOrientation = this.gameObject.transform.rotation;
            ResetLocaion = InitialLocaion;
            ResetOrientation = InitialOrientation;

            this.VehicleSolver.ResetVsVehicle();            


        }

        void Start()
        {
            

        }

        private void FixedUpdate()
        {
            if (IsFixedUpdate==false) return;
            Integrate(Time.fixedDeltaTime);


        }

        private void Update()
        {
            if (IsFixedUpdate) return;
            Integrate(Time.deltaTime);
        }

        private void Integrate(float delta)
        {
            

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    double throttle = UserThrottle * VehicleData.MaxThrottle;
                    double brakePedalForceNewtons = UserBrake * VehicleData.MaxBrakePedalForceNewtons;
                    double steerLeftDeg = SteeringWheelAngleLeftDeg;

                    VS_Vehicle.SetDriverThrottle(VsVehicleHandle, throttle);
                    VS_Vehicle.SetDriverBrakePedalNewtons(VsVehicleHandle, brakePedalForceNewtons);
                    VS_Vehicle.SetDriverSteerLeftDegrees(VsVehicleHandle, steerLeftDeg);

                    int integrateResult = VS_Vehicle.Integrate(VsVehicleHandle, delta);
                    

                    if (!VS_Vehicle.IsOk(VsVehicleHandle))
                    {
                        if (this.VehicleData.AutoResetSolver)
                        {

                            VehicleSolver.ResetVsVehicle();
                        }
                    }
                    else
                    {
                        
                        double forward = 0;
                        double left = 0;
                        double up = 0;

                        double rollRightRad = 0;
                        double pitchDownRad = 0;
                        double yawLeftRad = 0;

                        VS_Vehicle.GetWorldPosition(VsVehicleHandle, 0, ref forward, ref left, ref up);
                        VS_Vehicle.GetWorldOrientation(VsVehicleHandle, 0, ref rollRightRad, ref pitchDownRad, ref yawLeftRad);

                        //Log((180/Math.PI*rollRightRad) + "," + (180 / Math.PI * pitchDownRad) + "," + (180 / Math.PI * yawLeftRad));
                        //Log(forward + "," + left + "," + up);

                        VsUnityLib.SetUnityTransformFromSolverValues(this.gameObject, (float)forward, (float)left, (float)up, (float)rollRightRad, (float)pitchDownRad, (float)yawLeftRad);

                        //Todo temporary change. more precise
                        this.transform.position += this.transform.forward * BodyOffset;



                    }
                }
            }

        }

        void Destory()
        {
            if (Destroied) return;

            Destroied = true;
            this.VehicleSolver.DestroyVsVehicle();

            VehicleSolver.CreateVehicleEvent -= CreateVehicleEvent;
            VehicleSolver.ResetVehicleEvent -= ResetVehicleEvent;
            VehicleSolver.DestroyVehicleEvent -= DestroyVehicleEvent;
        }

        private void OnApplicationQuit()
        {
            Destory();
        }

        private void OnDestroy()
        {
            Destory();
            
        }

        #endregion


        #region VSVehicleMovement




        public float EngineRadPerSec { get; protected set; } = 0.0f;
        [SerializeField]
        public float EnginePowerNorm { get; protected set; } = 0.0f;
        public int CurrentGear { get; protected set; } = 0;

        public float ForwardSpeed { get; protected set; }

        [ SerializeField]
        public float temp { get { return ForwardSpeed; } }

        public float ThrottleInput
        {
            get
            {
                return UserThrottle;
            }

            set
            {
                UserThrottle = Mathf.Clamp(value, 0, 1);
            }
        }


        public float BrakeInput
        {
            get
            {
                return UserBrake;
            }

            set
            {
                UserBrake = Mathf.Clamp(value, 0, 1);
            }
        }


        public float ClutchInput
        {
            get
            {
                return UserClutch;
            }

            set
            {
                UserClutch = Mathf.Clamp(value, 0, 1);
            }
        }


        public float SteeringInput
        {
            get
            {
                return UserSteer;
            }

            set
            {
                UserSteer = Mathf.Clamp(value, -1, 1);
            }
        }

        public int NumUnits
        {
            get
            {
                int retNumUnits = 0;
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    retNumUnits = (int)VS_Vehicle.GetNumUnits(VsVehicleHandle);
                }
                return retNumUnits;
            }
        }


        public int GetNumAxles(int unitIndex)
        {
            int retNumAxles = 0;

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    retNumAxles = (int)VS_Vehicle.GetNumAxles(VsVehicleHandle, unitIndex);
                }
            }

            return retNumAxles;
        }

        


        public void GetWheelTransform(int unitIndex, int axleIndex, int axleSide ,ref Vector3 position,ref Quaternion rotation)
        {
            

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    double forward = 0;
                    double left = 0;
                    double up = 0;

                    double rollRightRad = 0;
                    double pitchDownRad = 0;
                    double yawLeftRad = 0;

                    

                    VS_Vehicle.GetTireWorldPosition(VsVehicleHandle, unitIndex, axleIndex, axleSide, ref forward, ref left, ref up);
                    VS_Vehicle.GetTireWorldOrientation(VsVehicleHandle, unitIndex, axleIndex, axleSide, ref rollRightRad, ref pitchDownRad, ref yawLeftRad);

                    position = VsUnityLib.VSToUnityVector(forward, left, up);
                    rotation = VsUnityLib.VSToUnityRotation(rollRightRad, pitchDownRad, yawLeftRad);

                    //Log(rollRightRad + "," + pitchDownRad + "," + yawLeftRad);
                    //CarSim_UnityLib.SetUnityTransformFromSolverValues(wheelGameObject, (float)forward, (float)left, (float)up, (float)rollRightRad, (float)pitchDownRad, (float)yawLeftRad);

                }





            }

           
        }


        public void GetTireTransform(int unitIndex, int axleIndex, int axleSide, int wheelSide, ref Vector3 position, ref Quaternion rotation)
        {
            
            GetWheelTransform(unitIndex, axleIndex, axleSide,ref position,ref rotation);
            if (wheelSide >= 0)
            {


                //Todo
                /*
                 * throw new System.NotImplementedException();
                Matrix4x4 adjustment=Matrix4x4.identity;
                float dualSpacingFromCenter = GetDualTireWidth(unitIndex, axleIndex) / 2;
                

                
                Vector3 axisOffset = wt.up(EAxis::Y);
                if (wheelSide == 0)
                    axisOffset *= -dualSpacingFromCenter;
                else
                    axisOffset *= dualSpacingFromCenter;
                adjustment*= adjustment.transposeSetTranslation(axisOffset);

                
                wt *= adjustment;
                */
            }



        }


        public float GetWheelRotation(int unitIndex, int axleIndex, int axleSide)
        {
            float rotation = 0;

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    double rotPitchForward=0;

                    VS_Vehicle.GetTireSolverRotation(VsVehicleHandle, unitIndex, axleIndex, axleSide, ref rotPitchForward);
                    rotation = (float)rotPitchForward*180/Mathf.PI;
                }
            }
            return rotation;
        }


        public float GetTireRotation(int unitIndex, int axleIndex, int axleSide, int wheelSide)
        {
            return GetWheelRotation(unitIndex, axleIndex, axleSide);
        }


        public Vector3 GetUnitWorldPosition(int unitIndex)
        {
            double forward=0, left = 0, up = 0;
            VS_Vehicle.GetWorldPosition(VsVehicleHandle, unitIndex, ref forward, ref left, ref up);
            Vector3 worldLocation = VsUnityLib.VSToUnityVector(forward, left, up);
            return worldLocation;
        }

        public Quaternion GetUnitWorldOrientation(int unitIndex)
        {
            double rollRightRad=0, pitchDownRad = 0, yawLeftRad = 0;
            VS_Vehicle.GetWorldOrientation(VsVehicleHandle, unitIndex, ref rollRightRad, ref pitchDownRad, ref yawLeftRad);

            Quaternion worldRotation = VsUnityLib.VSToUnityRotation(rollRightRad, pitchDownRad, yawLeftRad);
            return worldRotation;
        }


        public float GetAxleOffsetForward(int unitIndex, int axleIndex)
        {
            float retOffset = 0;  // The offset of the axle in centimeters.

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    retOffset = (float) VS_Vehicle.GetAxleOffsetForward(VsVehicleHandle, unitIndex, (ulong)axleIndex) ;
                }
            }
            return retOffset;
        }


        public float GetAxleTrack(int unitIndex, int axleIndex)
        {
            float retTrack = 0; // The track width of the axle in centimeters.

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    retTrack = (float)VS_Vehicle.GetAxleTrack(VsVehicleHandle, unitIndex, (ulong)axleIndex) ;
                }
            }

            return retTrack;
        }

        public float GetDualTireWidth(int unitIndex, int axleIndex)
        {
            float retDualWidth = 0;

            if (IntPtr.Zero != VsVehicleHandle)
            {
                Check(VS_Vehicle.IsValidVehicle(VsVehicleHandle));

                if (VS_Vehicle.IsOk(VsVehicleHandle))
                {
                    retDualWidth = (float)VS_Vehicle.GetDualWidth(VsVehicleHandle, unitIndex, (ulong)axleIndex) ;
                }
            }

            return retDualWidth;
        }

       

        public float SteeringWheelAngleLeftDeg
        {
            get
            {
                return -UserSteer * VehicleData.MaxHandWheelAngleDegrees;
            }
        }

      


        public int NumForwardGears { get; protected set; } = 0;

        public void SetTireTransform(Transform target, int unitIndex, int axleIndex, int axleSide)
        {
            Vector3 position = new Vector3();
            Quaternion rotation = new Quaternion();

            //Get Tire Transform ,but rotaion is not include wheel rotation
            //First set transform ,next rotate wheel rotate
            GetTireTransform(unitIndex, axleIndex, axleSide, 0, ref position, ref rotation);
            var wheelRotation = GetWheelRotation(unitIndex, axleIndex, axleSide);
            target.position = position;
            target.rotation = rotation;
            target.Rotate(wheelRotation, 0, 0);

        }



        #endregion



    }

}
