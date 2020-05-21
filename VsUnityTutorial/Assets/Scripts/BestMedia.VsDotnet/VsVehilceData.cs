//
// Copyright (C) 2019-2020 BestMedia.  All rights reserved.
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
using BestMedia.VsDotnet.vs_vehicle;

namespace BestMedia.VsDotnet
{


    /// <summary>
    /// This file is vehicle setting data. This extract from  CarSimMovementComponent.h(Unreal).
    /// See CarSimMovementComponent.h for details    
    /// </summary>
    [Serializable]
    public class VsVehilceData
    {


        #region Unreal Variable

        /// <summary>
        /// Specify a *.par (Parsfile) or *.sim (Simfile).
        ///If a Parsfile is specified(with extension "par", or with no extension),
        /// a Simfile will be generated.If there is a CarSim Solver DLL with the
        /// same name as the Parsfile(and in the same directory), it will be used,
        ///otherwise the Parsfile will be scanned for the VEHICLE_CODE and PRODUCT_NAME
        /// keywords to determine which of the solvers that are built-in to this plugin
        /// should be used.
        /// The included solver(s) can be found here:
        /// <StreamingAssetFolder>\Runtime\CarSim\ThirdParty\CarSim\CarSim_Prog\Programs\solvers\
        /// If the VEHICLE_CODE and PRODUCT_NAME keywords are not found in the Parsfile,
        /// it is assumed that the carsim_64.dll solver can be used (if this is
        /// inappropriate for the contents of the Parsfile, the solver may crash or behave
        /// unexpectedly).
        /// If a Simfile is specified, it will be loaded directly in the "normal"
        /// VehicleSim Solver Wrapper manner. See the VehicleSim documentation for details
        /// about the Simfile and how it is processed by the VS Solver.
        /// Relative paths will be treated as relative to this plugin's internal
        /// VehicleSim "Runs" folder:
        /// </summary>
        public string VsConfigFile = "";

        /// <summary>
        /// if checked, sets the VS Solver's OPT_STOP option to -1, which suppresses automatically stopping the VS Run based on time or station(solver parametersTSTOP & SSTOP).        
        /// </summary>
        public bool SolverRunForever = true;

        /// <summary>
        /// If true, the vehicle will be initialized/settled on the road/terrain  before the simulation begins.  If false, vehicle will be positioned exactly where it is placed in the  editor at the start of the simulation.
        /// </summary>
        public bool InitOnGround = true;

        /// <summary>
        /// If true, this vehicle will be automatically and immediately restart  whenever it is stopped (whether it stopped due to preset TSTOP/SSTOP, a  VS Command, bad data, etc.).
        /// </summary>
        public bool AutoResetSolver = true;
        /// <summary>
        /// If true, the CarSim solver will use it's internal definition of the road surface.  If false, the solver will query the Unreal terrain below the vehicle's tires
        /// </summary>
        public bool UseVehicleSimRoad = false;

        /// <summary>
        /// If true, the vehicle will be positioned and oriented based on the  parameters in the Parsfile, not where the Pawn/Actor is placed in the  Unreal Editor. 
        /// </summary>
        public bool UseVehicleSimStartPosition = false;

        /// <summary>
        /// if true, the CarSim driver model will be used and control inputs will be  ignored.  If false, vehicle inputs will be passed to the VS Solver and the internal  VS Solver driver model will be suppressed. 
        /// </summary>
        public bool UseVehicleSimDriver = false;

        /// <summary>
        /// If true, this vehicle will be disabled, and not attempt to get a license to run.
        /// </summary>
        public bool DisableVehicle = false;

       


        /// <summary>
        /// Hand-wheel angle in degrees that results from a user input value of 1.0. 
        /// </summary>
        public float MaxHandWheelAngleDegrees = 360;
        /// <summary>
        ///  Throttle input to VS Solver when user input is at 100%. Default/normal value is 1.0.
        /// </summary>
        public float MaxThrottle = 1.0f;
        /// <summary>
        /// For datasets that control braking via brake pedal pressure, the pedal force (Newtons) that results from a user controller input value of 1.0. 
        /// </summary>
        public float MaxBrakePedalForceNewtons = 750.0f;


        #endregion

        /// <summary>
        /// Delegate to a function to get road information
        /// </summary>
        public GetRoadInfo RoadInfoDelegate;
        /// <summary>
        /// callbackData in RoadInfoDelegate
        /// </summary>
        public object RoadInfoData = null;


        /// <summary>
        /// Filename of the Simfile for the VS Solver.
        /// </summary>
        internal string Simfile = "";
        /// <summary>
        /// Array of macros to set in the VS Solver before loading the Simfile.　
        /// Currently unused.
        /// </summary>
        internal string[][] SimfileMacros = null;
    }
}
