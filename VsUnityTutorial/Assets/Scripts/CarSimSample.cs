//
// Copyright (C) 2019-2020 BestMedia.  All rights reserved.
//
// The information and source code contained herein is the exclusive property of BestMedia and may not be disclosed, 
// examined or reproduced in whole or in part without explicit written authorization from the company.
//
//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using BestMedia.VsDotnet;
using BestMedia.VsUnity;

/// <summary>
/// Get the accelerator, brake, and steering values ​​from Unity's input and set the values to CarSimMovmentComponet
/// And change Tire Transform
/// </summary>
[RequireComponent(typeof(CarSimMovementComponent))]
[System.Serializable]
public class CarSimSample : MonoBehaviour
{

    public CarSimMovementComponent CarSimMovement { get; private set; }

    
    //Tire transforms
    public Transform FrontLeftTireTransform;
    public Transform FrontRightTireTransform;
    public Transform RearLeftTireTransform;
    public Transform RearRightTireTransform;

    //Vx(Speed) Variable
    VSVehicleVarDouble VarVx;
    //AvEng(For RPM) Variable
    VSVehicleVarDouble VarAvEng;


    void Start()
    {
        CarSimMovement = this.GetComponent<CarSimMovementComponent>();
        //Get Vx Variable
        VarVx = new VSVehicleVarDouble("Vx", VsVarDirection.Output, CarSimMovement.VehicleSolver);
        VarAvEng = new VSVehicleVarDouble("AV_Eng", VsVarDirection.Output, CarSimMovement.VehicleSolver);

    }

    // Update is called once per frame
    void Update()
    {
        //horizon -> steering input 
        var horizon = Input.GetAxis("Horizontal");
        this.Steering = horizon;

        //vertical -> equals Plus accelerator, Minus Brake
        var vertical = Input.GetAxis("Vertical");
            
        if (vertical >= 0)
        {
            this.ThrottlePedal = vertical;
            this.BrakePedal = 0;
        }
        else
        {
            this.ThrottlePedal = 0;
            this.BrakePedal = -vertical;
        }
            
        //Set to CarSimMovmentComponet
        CarSimMovement.ThrottleInput = this.ThrottlePedal;
        CarSimMovement.BrakeInput = this.BrakePedal;
        CarSimMovement.ClutchInput = this.ClutchPedal;
        CarSimMovement.SteeringInput = this.Steering;

        //Vx speed m/s change to  km/h and set speed.
        if (VarVx != null) Speed = (float)VarVx.Value * 3.6f;

        if (VarAvEng != null) RPM = (float)(VarAvEng.Value * 0.5 / Mathf.PI * 60.0f);
        

        //Set Tire transform
        if (FrontLeftTireTransform!=null) CarSimMovement.SetTireTransform(FrontLeftTireTransform, 0, 0, 0);
        if (FrontRightTireTransform != null) CarSimMovement.SetTireTransform(FrontRightTireTransform, 0, 0, 1);

        if (RearLeftTireTransform != null) CarSimMovement.SetTireTransform(RearLeftTireTransform, 0, 1, 0);
        if (RearRightTireTransform != null) CarSimMovement.SetTireTransform(RearRightTireTransform, 0, 1, 1);

        
    }

    [Range(0, 1.0f)]
    public float ThrottlePedal = 0;
    [Range(0, 1.0f)]
    public float BrakePedal = 0;

    [Range(0, 1.0f)]
    public float ClutchPedal = 0;

    [Range(-1.0f, 1.0f)]
    public float Steering = 0;

    public float Speed;
    public float RPM;

}
