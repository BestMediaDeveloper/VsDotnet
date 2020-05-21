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

namespace BestMedia.VsDotnet
{
    /// <summary>
    /// VsDotnet module.
    /// </summary>
    public class VsModule
    {
        /// <summary>
        /// the path of vs_vehicle.dll、vs_connect_64.dll、carsim_all.par(defult simulation file)
        /// In unity default is StreamingAssett.
        /// </summary>

        public static string VsPath { get; set; } = "";

        /// <summary>
        /// User-specific directory where we can read/write files.
        /// </summary>
        public static string VsEngineUserDir { get; set; } = "";


        /// <summary>
        /// Use Vsconnect server
        /// </summary>
        static public bool EnableVsConnectServer { get; set; } = false;


        /// <summary>
        /// Use Vsconnect client
        /// Vsconnect client and server can't set true both
        /// </summary>
        static public bool EnableVsConnectClient { get; set; } = false;

        /// <summary>
        /// IP Address 
        /// </summary>
        static public string VsConnectIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// VSConnectServer create funciton.
        /// </summary>
        static public Func<VsConnectServer> CreateVsConnectServerFunc { get; set; } = () => new VsConnectServer();


        /// <summary>
        /// Use VS Vehicle
        /// </summary>
        static public bool EnableVsVehicle { get; set; } = false;


        /// <summary>
        /// VsModule is stated
        /// </summary>
        public static bool ModuleStarted { get; protected set; }


        /// <summary>
        /// Start up 
        /// </summary>
        public static void StartupModule()
        {  
            Log(("VsModule starting..."));

            Log($"VsPass dir: %{VsModule.VsPath}");
            Log($"User Output dir: %{VsModule.VsEngineUserDir}");
            
            //Only Windows 64bit platform
            Check(System.Environment.Is64BitProcess && System.Environment.OSVersion.Platform == PlatformID.Win32NT);

            //Check server and client same
            Check(!(EnableVsConnectServer && EnableVsConnectClient));

            if (ModuleStarted)
            {
                LogError(("Attempting to restart module not allowed."));
            }
            else
            {

                ModuleStarted = true;
                if (System.IO.Directory.Exists(VsModule.VsEngineUserDir) == false) System.IO.Directory.CreateDirectory(VsModule.VsEngineUserDir);

                if(EnableVsConnectServer || EnableVsConnectClient)
                {
                    BestMedia.VsDotnet.vs_connect_api_v3.vsc_Api_V3_t.DllInit(VsModule.VsPath);
                    if(EnableVsConnectServer)
                    {
                        VsConnectServer.StartupModule();
                    }


                    if (EnableVsConnectClient)
                    {
                        
                    }

                }
                if(EnableVsVehicle)
                {
                    VsVehicleSolver.StartupModule();
                }

            }
        }

        /// <summary>
        /// Processing at the end 
        /// </summary>
        public static void ShutdownModule()
        {
            

            Log(("VsModule shutting down..."));
            if (VsModule.EnableVsConnectServer)
            {

                VsConnectServer.ShutdownModule();

                BestMedia.VsDotnet.vs_connect_api_v3.vsc_Api_V3_t.DLLShutDown();
            }

            if(VsModule.EnableVsVehicle)
            {
                VsVehicleSolver.ShutdownModule();
            }
            ModuleStarted = false;

        }




    }
}
