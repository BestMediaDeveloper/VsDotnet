//
// Copyright (C) 2020 BestMedia.  All rights reserved.
//
// The information and source code contained herein is the exclusive property of BestMedia and may not be disclosed, 
// examined or reproduced in whole or in part without explicit written authorization from the company.
//
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using static BestMedia.VsDotnet.VsUtility;
using BestMedia.VsDotnet;


namespace BestMedia.VsUnity
{


    /// <summary>
    /// Class that specifies, initializes, and ends the path associated with the connection with CarSim.
    /// </summary>
    public class VsUnityModule
    {
        

        /// <summary>
        ///Processing at the start of Unity        
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void StartupModule()
        {
            

            //Add Shutodown ent
            Application.quitting += ShutdownModule;

            /// Current surpport windows and 64bit
            Check((Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) && System.IntPtr.Size == 8);


            //set unity log function
            VsUtility.Log = Debug.Log;
            VsUtility.LogError = Debug.LogError;
            VsUtility.LogWarning = Debug.LogWarning;


            //Set Unity Data and dll path
            VsModule.VsPath = System.IO.Path.Combine(Application.streamingAssetsPath, "CarSim");
            VsModule.VsEngineUserDir = System.IO.Path.Combine(Application.persistentDataPath, "CarSim_plugin");            
            VsModule.EnableVsVehicle = true;

            //In unity VS Connect Server is halfway
            VsModule.EnableVsConnectServer = false;
            //Reset CreateVsConnectServerFunc for Unity
            //VsModule.CreateVsConnectServerFunc = () => new VsConnectServerUnity();

            Log(("CarSim Module starting..."));

            VsModule.StartupModule();
          
        }

        /// <summary>
        /// Processing at the end of Unity
        /// </summary>
        static void ShutdownModule()
        {
            VsModule.ShutdownModule();
        }
    }
}
