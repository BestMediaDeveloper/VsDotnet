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
using UnityEngine;
using BestMedia.VsDotnet;
using BestMedia.VsDotnet.vs_connect_api_v3;

namespace BestMedia.VsUnity
{
    /// <summary>
    /// VS Connect server for Unity.    
    /// </summary>
    public class VsConnectServerUnity :VsConnectServer
    {
        GameObject vsConnectFixupdateObject = null;        


        protected override void InitializeServer()
        {
            base.InitializeServer();

            
            
            vsConnectFixupdateObject = new GameObject();
            vsConnectFixupdateObject.name = "VSConnectTickGameObject";
            vsConnectFixupdateObject.AddComponent(typeof(VsUnity.VSConnectTickComponent));
            vsConnectFixupdateObject.isStatic = true;
#if !UNITY_EDITOR
		    mVSConnectFixupdateObject.hideFlags = HideFlags.HideAndDontSave;
#endif
            MonoBehaviour.DontDestroyOnLoad(vsConnectFixupdateObject);

        }


        protected override void DestroyServer()
        {
            base.DestroyServer();

            MonoBehaviour.Destroy(vsConnectFixupdateObject);
            vsConnectFixupdateObject = null;

        }

        

    }
}
