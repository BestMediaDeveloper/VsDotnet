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
using BestMedia.VsDotnet.vs_vehicle;
using static BestMedia.VsDotnet.VsUtility;
using vs_vehicleDll = BestMedia.VsDotnet.vs_vehicle.VS_Vehicle;

namespace BestMedia.VsDotnet
{



    /// <summary>
    ///  Solver for vs vehicle.   
    /// </summary>
    unsafe public class VsVehicleSolver 
    {

        /// <summary>
        /// Property of VSEW_VEHICLE_HANDLE
        /// </summary>
        public VSEW_VEHICLE_HANDLE VsVehicleHandle { get; private set; } = System.IntPtr.Zero;


        /// <summary>
        /// Occurs when a vehicle is created
        /// </summary>
        public event EventHandler CreateVehicleEvent;

        /// <summary>
        /// This occurs when the vehicle is reset.
        /// </summary>
        public event EventHandler ResetVehicleEvent;

        /// <summary>
        ///  occurs when a vehicle is destroyed.
        /// </summary>
        public event EventHandler DestroyVehicleEvent;


        /// <summary>
        /// Properties to data for vehicle creation
        /// </summary>
        public VsVehilceData Data { get; set; } = new VsVehilceData();

        /// <summary>
        /// Variables already created
        /// </summary>
        Dictionary<string, VSVarBase> _createdVsVars = new Dictionary<string, VSVarBase>();

        /// <summary>
        /// 
        /// </summary>
        object _vsVarCreticalSection = new object();


        #region VehicleCreateResetDestroy


        /// <summary>
        /// Setting the path to the Sim file
        ///Check the contents of the VSConfig file, get the specified Config file or the default Config file, and execute
        ///If the file does not have an extension or is a par file, load the file and create a SIM file.
        ///Otherwise it will be considered a Sim file and the path will be set to that file.
        /// </summary>
        public void SetSimFile()
        {
            Check(string.IsNullOrEmpty(Data.Simfile));
            //Check(null == Data.SimfileMacros);
            //Check(0 == Data.SimfileMacros.Length);

            if (!vs_vehicleDll.DllIsLoaded)
            {
                LogError(("VsVehicle API not available."));
            }
            else
            {
                Check(!vs_vehicleDll.IsValidVehicle(VsVehicleHandle));

                var VsConfigFile = Data.VsConfigFile.Trim();


                string configFile;

                if (string.IsNullOrEmpty(VsConfigFile))
                {
                    configFile = "carsim_all.par";
                    LogWarning(($"VS Config File not specified. Attempting to use default vehicle configuration: {configFile}"));
                }
                else
                {
                    configFile = VsConfigFile;
                }

                configFile = configFile.Trim();

                string configFileExt = System.IO.Path.GetExtension(configFile);
                bool configFileExtIsEmpty = string.IsNullOrEmpty(configFileExt);
                bool configFileExtIsPar = configFileExt.ToLower() == ".par";

                if (configFileExtIsEmpty || configFileExtIsPar)
                {

                    string rawParsfileNameWithExt = configFileExtIsPar ? configFile : (configFile + ".par");
                    string parsfileFullPathname;

                    if (System.IO.Path.IsPathRooted(rawParsfileNameWithExt) == false)
                    {
                        parsfileFullPathname = (new Uri(new Uri(VsModule.VsPath + "\\"), rawParsfileNameWithExt)).LocalPath;
                    }
                    else
                    {
                        parsfileFullPathname = rawParsfileNameWithExt;
                    }

                    //FPaths::CollapseRelativeDirectories(parsfileFullPathname); 


                    if (!System.IO.File.Exists(parsfileFullPathname))
                    {
                        LogError(($"Simfile not generated: Unable to locate Parsfile: \"{parsfileFullPathname}\""));
                        Check(string.IsNullOrEmpty(parsfileFullPathname));
                    }
                    else
                    {
                        Log($"Generating Simfile for Parsfile \"{parsfileFullPathname}\".");



                        string generatedFileBaseName = System.IO.Path.Combine(VsModule.VsEngineUserDir, System.IO.Path.GetFileNameWithoutExtension(configFile));

                        string tempSimfileName = generatedFileBaseName + ".sim";

                        System.IO.StreamWriter tempSimFileStream = new System.IO.StreamWriter(tempSimfileName);


                        {
                            tempSimFileStream.WriteLine("SIMFILEn");

                            string companionSolverFilename = System.IO.Path.ChangeExtension(parsfileFullPathname, "dll");

                            if (System.IO.File.Exists(companionSolverFilename))
                            {

                                tempSimFileStream.WriteLine("DLLFILE " + companionSolverFilename);
                            }

                            string vehicleCode = "";
                            string productName = "";


                            {
                                System.IO.StreamReader tempParsfileStream = new System.IO.StreamReader(parsfileFullPathname);
                                string parsfileLineBuff;
                                while ((parsfileLineBuff = tempParsfileStream.ReadLine()) != null)
                                {


                                    var items = parsfileLineBuff.Split(new char[] { ' ', '\t', '\r', '\n' });
                                    if (items.Length < 2) continue;
                                    string key = items[0];
                                    string rest = items[1];


                                    if (key == "VEHICLE_CODE")
                                    {
                                        vehicleCode = rest;
                                        vehicleCode = vehicleCode.TrimStart();

                                        int commentLocation = vehicleCode.IndexOf("!");
                                        if (commentLocation >= 0)
                                        {
                                            vehicleCode = vehicleCode.Substring(commentLocation);
                                        }

                                        vehicleCode = vehicleCode.TrimEnd();

                                        if (string.IsNullOrEmpty(vehicleCode))
                                        {
                                            LogWarning(("VEHICLE_CODE keyword specified with empty value."));
                                        }
                                    }
                                    else if (key == "PRODUCT_NAME")
                                    {
                                        productName = rest;
                                        productName.TrimStart();

                                        int commentLocation = productName.IndexOf("!");
                                        if (commentLocation >= 0)
                                        {
                                            productName = productName.Substring(commentLocation);
                                        }

                                        productName.TrimEnd();

                                        if (string.IsNullOrEmpty(productName))
                                        {
                                            LogWarning(("PRODUCT_NAME keyword specified with empty value."));
                                        }
                                    }
                                }

                                tempParsfileStream.Close();
                            }

                            if (string.IsNullOrEmpty(vehicleCode))
                            {
                                vehicleCode = "i_i";
                                LogWarning(($"VEHICLE_CODE not specified, and companion Solver DLL \"{companionSolverFilename}\" not found. Using default/fallback solver \"{vehicleCode}\". This may cause undefined behavior of the VehicleSim vehicle."));
                            }

                            Check(!string.IsNullOrEmpty(vehicleCode));

                            tempSimFileStream.WriteLine("VEHICLE_CODE " + vehicleCode);
                            tempSimFileStream.WriteLine("PRODUCT_ID " + productName);



                            tempSimFileStream.WriteLine("PROGDIR " + VsModule.VsPath);
                            tempSimFileStream.WriteLine("INPUT " + parsfileFullPathname);

                            string echoFileName = generatedFileBaseName + ("_echo.par");
                            string endFileName = generatedFileBaseName + ("_end.par");
                            string logFileName = generatedFileBaseName + ("_log.txt");
                            string erdFileName = generatedFileBaseName + (".erd");


                            tempSimFileStream.WriteLine("ECHO " + echoFileName);
                            tempSimFileStream.WriteLine("FINAL " + endFileName);
                            tempSimFileStream.WriteLine("LOGFILE " + logFileName);
                            tempSimFileStream.WriteLine("ERDFILE " + erdFileName);

                            // vs_vehicle module uses vs_get_dll_path(), which has the windows directory structure to the solvers hardcoded, unless you provide DLLFILE
                            if ("CarSim" == productName)
                            {
                                string solverDLL = System.IO.Path.Combine(VsModule.VsPath, "carsim_64.dll");
                                tempSimFileStream.WriteLine("DLLFILE " + solverDLL);
                            }
                            else if ("TruckSim" == productName)
                            {
                                string solverDLL = System.IO.Path.Combine(VsModule.VsPath, "trucksim_64.dll");
                                tempSimFileStream.WriteLine("DLLFILE " + solverDLL);
                            }
                            else if ("BikeSim" == productName)
                            {
                                string solverDLL = System.IO.Path.Combine(VsModule.VsPath, "bikesim_64.dll");
                                tempSimFileStream.WriteLine("DLLFILE " + solverDLL);
                            }


                            tempSimFileStream.WriteLine("END");
                            this.Data.Simfile = tempSimfileName;


                        }

                        tempSimFileStream.Close();
                    }
                }
                else
                {
                    // Relative paths specify files relative to VsModule.VsPath
                    if (System.IO.Path.IsPathRooted(configFile) == false)
                    {
                        this.Data.Simfile = System.IO.Path.Combine(VsModule.VsPath, "Runs", configFile);
                    }
                    else
                    {
                        this.Data.Simfile = configFile;
                    }
                }
            }

        }


        /// <summary>
        /// Resetting the vehicle
        /// Once you delete a vehicle, create a vehicle.
        /// </summary>
        public void ResetVsVehicle()
        {
            

            if (Data.DisableVehicle)
            {
                LogWarning(("Not initializing vehicle, 'DisableVehicle' is set in UCarSimMovementComponent."));
                return;
            }

            if (!vs_vehicleDll.DllIsLoaded)
            {
                LogError(("Unable to Reset() VsVehicle: VsVehicle API not available."));

                Check(VsVehicleHandle != IntPtr.Zero);
            }
            else
            {
                

                DestroyVsVehicle();

                ulong ERR_BUFF_SIZE = 4096;
                byte[] CarSimErrorMessage = new byte[ERR_BUFF_SIZE];

                if (string.IsNullOrEmpty(Data.Simfile))
                {
                    LogWarning(("Skipping creation of VS vehicle solver instance because there is no Simfile specified."));
                }
                else
                {
                    if (Data.SimfileMacros != null) LogWarning("Current Version is not supoort SimfileMacros");
                    Log(($"Creating VS Vehicle with input file \"{Data.Simfile}\"."));
                    VsVehicleHandle = vs_vehicleDll.CreateVehicle(Data.Simfile
                                                                  //, SimfileMacros
                                                                  , IntPtr.Zero
                                                                  , 0
                                                                  , !Data.UseVehicleSimDriver
                                                                  , Data.UseVehicleSimRoad ? null : Data.RoadInfoDelegate
                                                                  , Data.RoadInfoData==null ?  IntPtr.Zero : VsUtility.ToPtr(Data.RoadInfoData)
                                                                  , CarSimErrorMessage
                                                                  , ERR_BUFF_SIZE
                                                              );



                    if (!vs_vehicleDll.IsValidVehicle(VsVehicleHandle))
                    {
                        string errMsg = "";
                        errMsg += "Unable to create VehicleSim vehicle. Error message from VS Solver:\n";
                        errMsg += System.Text.Encoding.ASCII.GetString(CarSimErrorMessage);
                        LogError(errMsg);
                    }
                    else
                    {
                        if (Data.SolverRunForever)
                        {
                            int optSetError = vs_vehicleDll.ExecuteVsCommand(VsVehicleHandle, "OPT_STOP", "-1");
                            if (optSetError != 0)
                            {
                                LogWarning(("Unable set OPT_STOP for VS Solver. The VS Solver may automatically stop at some time/station specified in it's input file."));
                            }
                        }

                        {
                            int optSetError = vs_vehicleDll.ExecuteVsCommand(VsVehicleHandle, "OPT_INIT_PATH", "0");
                            if (optSetError != 0)
                            {
                                LogWarning(("Unable set OPT_INIT_PATH for VS Solver. This may prevent the vehicle from initalizing in the correct location."));
                            }
                        }

                        CreateVehicleEvent?.Invoke(this, EventArgs.Empty);


                        if (Data.InitOnGround)
                        {
                            vs_vehicleDll.ReinitializeVehicle(VsVehicleHandle);
                        }
                    }
                }
            }

            ResetVehicleEvent?.Invoke(this, EventArgs.Empty);
        }



        /// <summary>
        /// Deleting a vehicle
        /// </summary>
        public void DestroyVsVehicle()
        {
            if (VsVehicleHandle != IntPtr.Zero)
            {
                Check(vs_vehicleDll.DllIsLoaded);
                Check(vs_vehicleDll.IsValidVehicle(VsVehicleHandle));

                vs_vehicleDll.DestroyVehicle(VsVehicleHandle);
                DestroyVehicleEvent?.Invoke(this, EventArgs.Empty);
                
            }
        }


       
        public static bool Initialized { get; private set; } = false;


        /// <summary>
        /// Module initialization.
        /// Loading DLL files
        /// </summary>
        /// <returns></returns>
        static internal bool StartupModule()
        {
            bool retSuccess = false;

            Log(("CarSimMovementComponent Initiating static initialization."));

            Initialized = true;  // Flag indicating that this static initialization method has been called.

            //Check(!sVsDotnetApi.IsInitialized);

            if (!VsModule.ModuleStarted)
            {
                LogError(("Attempt to initialize CarSim Movement Component before plugin module has been successfully loaded."));
            }
            else
            {
                string DllFile = System.IO.Path.Combine(VsModule.VsPath, @"vs_vehicle.dll");
                Log(($"Initializing VS Wrapper: %{DllFile}"));

                vs_vehicleDll.Initialize(DllFile);
            }

            retSuccess = vs_vehicleDll.DllIsLoaded;

            if (!retSuccess)
            {
                LogWarning(("Failed to initialize CarSim Movement Component."));
            }
            else
            {
                Log(("CarSim Movement Component successfully initialized."));
            }

            return retSuccess;
        }

        /// <summary>
        /// Module Shutdown
        /// </summary>
        static internal void ShutdownModule()
        {
            vs_vehicleDll.ShutDown();

        }
        #endregion


        public bool IsValid => vs_vehicleDll.IsValidVehicle(VsVehicleHandle);
        internal void AddVSVar<T>(VsVehicleVar<T> VsVehicleVar)
        {
            Check(VsVehicleVar.VsVehicleSolver == this);
            if (_createdVsVars.ContainsKey(VsVehicleVar.Name)) return;

            lock (_vsVarCreticalSection)
            {
                _createdVsVars.Add(VsVehicleVar.Name, VsVehicleVar);
            }

        }
        
        public VSVarBase GetVsVar(string varName)
        {
            if (_createdVsVars.ContainsKey(varName)) return _createdVsVars[varName];

            return null;
        }

    }
}
