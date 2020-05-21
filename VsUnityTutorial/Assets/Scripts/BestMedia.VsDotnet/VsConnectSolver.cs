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
using sVsWrapperApi = BestMedia.VsDotnet.vs_vehicle.VS_Vehicle;




namespace BestMedia.VsDotnet
{

    /// <summary>
    /// Solver for vs connect.    
    /// </summary>
    unsafe public class VsConnectSolver 
    {

        /// <summary>
        /// Function container that returns a double value from name.
        /// </summary>
        public Dictionary<string, Func<double>> GetDoubleFuncs = new Dictionary<string, Func<double>>();
        /// <summary>
        /// Function container that returns a double array value from name.
        /// </summary>
        public Dictionary<string, Func<double[]>> GetDoubleArrayFuncs = new Dictionary<string, Func<double[]>>();
        /// <summary>
        /// Action container that sets a double value from name.
        /// </summary>
        public Dictionary<string, Action<double>> SetDoubleActions = new Dictionary<string, Action<double>>();
        /// <summary>
        /// Action container that sets a double array value from name.
        /// </summary>
        public Dictionary<string, Action<double[]>> SetDoubleArrayActions = new Dictionary<string, Action<double[]>>();
        

        /// <summary>
        /// VS connect object name
        /// </summary>
        public string VscObjectName { get; set; }


        /// <summary>
        /// Register to vs connect server
        /// </summary>
        /// <returns></returns>
        public bool RegisterWithVsConnect()
        {
            bool retSuccess = false;


            Check(VsConnectServer.Singleton != null);

            if (string.IsNullOrEmpty(VscObjectName) == false)
            {
                retSuccess = VsConnectServer.Singleton.RegisterObject(this);
            }

            return retSuccess;
        }

        /// <summary>
        /// DeRegister from vs connect server
        /// </summary>
        /// <returns></returns>

        public bool DeregisterWithVsConnect()
        {
            bool retSuccess = false;
            Check(VsConnectServer.Singleton != null);

            retSuccess = VsConnectServer.Singleton.DeregisterObject(this);

            return retSuccess;
        }

        


    }
}
