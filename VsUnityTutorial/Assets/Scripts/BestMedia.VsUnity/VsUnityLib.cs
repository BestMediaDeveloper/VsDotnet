//
// Copyright (C) 2019-2020 BestMedia.  All rights reserved.
//
// The information and source code contained herein is the exclusive property of BestMedia and may not be disclosed, 
// examined or reproduced in whole or in part without explicit written authorization from the company.
//
//


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BestMedia.VsUnity
{

    /// <summary>
    /// Utility class for carsim and unity conversion
    /// </summary>
    public static class VsUnityLib
    {


        
        static public float VsFoward(this Vector3 v) => v.z;
        static public float VsLeft(this Vector3 v) => v.x;
        static public float VsUp(this Vector3 v) => v.y;



        /// <summary>
        /// Carsim position to Unity Vector3
        /// </summary>
        /// <param name="forward">forward position. unity z</param>
        /// <param name="left">left position. unity x</param>
        /// <param name="up">up position. unity y</param>
        /// <returns>unity position</returns>
        public static Vector3 VSToUnityVector( double forward, double left, double up)
        {

            return new Vector3((float)left, (float)up, (float)forward);

        }

        
        /// <summary>
        /// Unity Vector3 to Carsim position
        /// </summary>
        /// <param name="v"></param>
        /// <param name="forward"></param>
        /// <param name="left"></param>
        /// <param name="up"></param>
        public static void UnityToVSVector(this Vector3 v, ref double forward, ref double left, ref double up)
        {
            forward = v.z;
            left = v.x;
            up = v.y;


        }

        static public double VsRoll(this Quaternion q) => q.eulerAngles.z * Math.PI / 180;
        static public double VsPitch(this Quaternion q) => q.eulerAngles.x * Mathf.PI / 180;
        static public double VsYaw(this Quaternion q) => q.eulerAngles.y * Mathf.PI / 180;

        /// <summary>
        /// Carsim rotation(rad) to Unity Quaternion(deg)
        /// </summary>
        /// <param name="Roll"></param>
        /// <param name="Pitch"></param>
        /// <param name="Yaw"></param>
        /// <returns></returns>
        public static Quaternion VSToUnityRotation(double Roll, double Pitch, double Yaw)
        {
            return Quaternion.Euler((float)(Pitch *  180/ Math.PI), (float)(Yaw * 180 / Math.PI), (float)(Roll * 180 / Math.PI));
        }



        /// <summary>
        /// Unity Quaternion(deg) to Carsim rotation(rad)
        /// </summary>
        /// <param name="unity"></param>
        /// <param name="Roll"></param>
        /// <param name="Pitch"></param>
        /// <param name="Yaw"></param>
        public static void UnityToVSRotation(this Quaternion unity, ref double Roll, ref double Pitch, ref double Yaw)
        {
            var euler = unity.eulerAngles;
            Pitch = euler.x * Math.PI / 180;
            Yaw = euler.y * Math.PI / 180;
            Roll = euler.z * Math.PI / 180;


        }

        /// <summary>
        /// set unity transform from vs vehicle infomation.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="forward"></param>
        /// <param name="left"></param>
        /// <param name="up"></param>
        /// <param name="roll"></param>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        static public void SetUnityTransformFromSolverValues(GameObject gameObject, float forward, float left, float up, float roll, float pitch, float yaw)
        {
            
            gameObject.transform.position = VsUnityLib.VSToUnityVector(forward, left, up);
            gameObject.transform.rotation = VsUnityLib.VSToUnityRotation(roll, pitch, yaw);


        }

       

       
    }

}
