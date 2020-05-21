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
using BestMedia.VsDotnet;


namespace BestMedia.VsUnity
{

    /// <summary>
    ///  Component class that facilitates VS Connect communication.
    /// </summary>
    public class VsConnectComponent : MonoBehaviour
    {
        public String vsConnectObjectName = "";
        public bool AutoRegisterWithVsConnect = true;

        VsConnectSolverUnity vsConnectSolver;
        private void Start()
        {
            vsConnectSolver = new VsConnectSolverUnity(this);
            vsConnectSolver.VscObjectName = vsConnectObjectName;

            if (AutoRegisterWithVsConnect)
            {
                vsConnectSolver.RegisterWithVsConnect();
            }

        }



        private void OnDestroy()
        {
            VsConnectServer.Singleton.DeregisterObject(vsConnectSolver);
        }
    }
}
