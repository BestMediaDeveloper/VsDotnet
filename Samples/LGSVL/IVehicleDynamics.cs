/**
 * Copyright (c) 2019 LG Electronics, Inc.
 *
 * This software contains code licensed as described in LICENSE.
 *
 */

using UnityEngine;

public enum IgnitionStatus { Off, On };

public interface IVehicleDynamics
{
    
    //Rigidbody RB { get; }
    float Speed { get; }
    Vector3 Velocity { get; }
    Vector3 AngularVelocity { get; }


    float AccellInput { get; }
    float SteerInput { get; }

    bool HandBrake { get; }
    float CurrentRPM { get; }
    float CurrentGear { get; }
    bool Reverse { get; }
    float WheelAngle { get; }

    IgnitionStatus CurrentIgnitionStatus { get; set; }

    bool GearboxShiftUp();

    bool GearboxShiftDown();

    bool ShiftFirstGear();

    bool ShiftReverse();

    bool ToggleReverse();

    bool ShiftReverseAutoGearBox();

    bool ToggleIgnition();

    bool ToggleHandBrake();

    bool SetHandBrake(bool state);

    bool ForceReset(Vector3 pos, Quaternion rot);
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider Left;
    public WheelCollider Right;
    public GameObject LeftVisual;
    public GameObject RightVisual;
    public bool Motor;
    public bool Steering;
    public float BrakeBias = 0.5f;

    [System.NonSerialized]
    public WheelHit HitLeft;
    [System.NonSerialized]
    public WheelHit HitRight;
    [System.NonSerialized]
    public bool GroundedLeft = false;
    [System.NonSerialized]
    public bool GroundedRight = false;
}
