using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using BestMedia.VsDotnet;
using BestMedia.VsUnity;

[RequireComponent(typeof(CarSimMovementComponent))]
[RequireComponent(typeof(VehicleController))]
public class CarSimSMI : MonoBehaviour,IVehicleDynamics
{
    #region IVehicleDynamics
    public float Speed { get; private set; } = 0f;
    public Vector3 Velocity { get; private set; } = new Vector3();
    public Vector3 AngularVelocity { get; private set; } = new Vector3();

    public float AccellInput { get; private set; } = 0f;
    public float SteerInput { get; private set; } = 0f;

    public bool HandBrake { get; private set; } = false;
    public float CurrentRPM { get; private set; } = 0f;
    public float CurrentGear { get; private set; } = 1f;
    public bool Reverse { get; private set; } = false;
    public float WheelAngle
    {
        get
        {
            if(this.FrontLeftTireTransform!=null && this.FrontRightTireTransform!=null)
            {
                var fldir = this.transform.InverseTransformDirection(this.FrontLeftTireTransform.forward);
                var frdir = this.transform.InverseTransformDirection(this.FrontRightTireTransform.forward);

                //Mathf.Atan2(fldir.x, fldir.y);

            }
            
           
            return 0.0f;
        }
    }
    public IgnitionStatus CurrentIgnitionStatus { get; set; } = IgnitionStatus.On;

    public bool GearboxShiftUp()
    {
        //Currently AT Vehicle.Todo
        return true;
    }

    public bool GearboxShiftDown()
    {
        //Currently AT Vehicle.Todo
        return true;
    }

    public bool ShiftFirstGear()
    {
        //Currently AT Vehicle.Todo
        VarTrans.Value = 6;
        return true;
    }

    public bool ShiftReverse()
    {
        VarTrans.Value = -1;
        Reverse = true;
        return true;
    }

    public bool ToggleReverse()
    {
        if (Reverse)
        {
            ShiftFirstGear();
        }
        else
        {
            ShiftReverse();
        }
        return true;
    }

    public bool ShiftReverseAutoGearBox()
    {
        //Todo
        /*
        if (Time.time - LastShift > ShiftDelay)
        {
            if (CurrentRPM / MaxRPM < ShiftDownCurve.Evaluate(AccellInput) && Mathf.RoundToInt(CurrentGear) > 1)
            {
                GearboxShiftDown();
            }
        }
        */

        if (CurrentGear == 1)
        {
            Reverse = true;
        }
        return true;
    }

    public bool ToggleIgnition()
    {
        CurrentIgnitionStatus = CurrentIgnitionStatus == IgnitionStatus.On ? IgnitionStatus.Off : IgnitionStatus.On;
        return true;
    }

    public bool ToggleHandBrake()
    {
        HandBrake = !HandBrake;
        return true;
    }

    public bool SetHandBrake(bool state)
    {
        HandBrake = state;
        return true;
    }


    public bool ForceReset(Vector3 pos, Quaternion rot)
    {
        Debug.Log(pos);
        CarSimMovement.ResetLocaion = pos;
        CarSimMovement.ResetOrientation = rot;
        
        CurrentGear = 1;
        CurrentRPM = 0f;
        AccellInput = 0f;
        SteerInput = 0f;

        CarSimMovement.VehicleSolver.ResetVsVehicle();

        return true;
    }


    #endregion

    private CarSimMovementComponent CarSimMovement;
    private VehicleController VehicleController;

    public Transform FrontLeftTireTransform;
    public Transform FrontRightTireTransform;
    public Transform RearLeftTireTransform;
    public Transform RearRightTireTransform;

    //Vx(Speed) Variable
    VSVehicleVarDouble VarVx;
    VSVehicleVarDouble VarVy;
    VSVehicleVarDouble VarVz;

    VSVehicleVarDouble VarAVx;
    VSVehicleVarDouble VarAVy;
    VSVehicleVarDouble VarAVz;
    //AvEng(For RPM) Variable
    VSVehicleVarDouble VarAvEng;

    VSVehicleVarDouble VarGearStat;

    VSVehicleVarDouble VarTrans;

    // Start is called before the first frame update
    void Start()
    {
        VehicleController = GetComponent<VehicleController>();
        CarSimMovement = GetComponent<CarSimMovementComponent>();

        VarVx = new VSVehicleVarDouble("Vx", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarVy = new VSVehicleVarDouble("Vy", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarVz = new VSVehicleVarDouble("Vz", VsVarDirection.Output, CarSimMovement.VehicleSolver);

        VarAVx = new VSVehicleVarDouble("AVx", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarAVy = new VSVehicleVarDouble("AVy", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarAVz = new VSVehicleVarDouble("AVz", VsVarDirection.Output, CarSimMovement.VehicleSolver);


        VarAvEng = new VSVehicleVarDouble("AV_Eng", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarGearStat = new VSVehicleVarDouble("GearStat", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarTrans = new VSVehicleVarDouble("IMP_MODE_TRANS", VsVarDirection.Input, CarSimMovement.VehicleSolver);
        VarTrans.Value = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CarSimMovement.IsFixedUpdate)
        {
            CommonUpdate();

        }
    }

    void FixedUpdate()
    {
        if(CarSimMovement.IsFixedUpdate)
        {
            CommonUpdate();
            
        }

    }

    void CommonUpdate()
    {
        GetInput();


        CarSimMovement.ThrottleInput = AccellInput >= 0 ? AccellInput : 0;
        CarSimMovement.BrakeInput = AccellInput < 0 ? -AccellInput : 0;
        CarSimMovement.SteeringInput = this.SteerInput;

        if (VarVx != null) Speed = (float)VarVx.Value * 3.6f;

        if (VarAvEng != null) CurrentRPM = (float)(VarAvEng.Value * 0.5 / Mathf.PI * 60.0f);

        if (VarGearStat != null) CurrentGear = Mathf.RoundToInt((float)VarGearStat.Value);

        if(VarVx!=null && VarVy!=null && VarVz!=null)
        {
            Velocity = VsUnityLib.VSToUnityVector(VarVx.Value, VarVy.Value, VarVz.Value);
        }

        if (VarVx != null && VarVy != null && VarVz != null)
        {
            AngularVelocity = VsUnityLib.VSToUnityRotation(VarAVx.Value, VarAVy.Value, VarAVz.Value).eulerAngles;
        }
        

        if (FrontLeftTireTransform != null) CarSimMovement.SetTireTransform(FrontLeftTireTransform, 0, 0, 0);
        if (FrontRightTireTransform != null) CarSimMovement.SetTireTransform(FrontRightTireTransform, 0, 0, 1);

        if (RearLeftTireTransform != null) CarSimMovement.SetTireTransform(RearLeftTireTransform, 0, 1, 0);
        if (RearRightTireTransform != null) CarSimMovement.SetTireTransform(RearRightTireTransform, 0, 1, 1);


    }



    private void GetInput()
    {
        if (VehicleController != null)
        {
            SteerInput = VehicleController.SteerInput;
            AccellInput = VehicleController.AccelInput;
        }

        if (HandBrake)
        {
            AccellInput = -1.0f; // TODO better way using Accel and Brake
        }
    }
}
