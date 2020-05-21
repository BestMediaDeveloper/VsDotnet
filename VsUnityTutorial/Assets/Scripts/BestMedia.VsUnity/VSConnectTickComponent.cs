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
using BestMedia.VsDotnet;
namespace BestMedia.VsUnity
{

    /// <summary>
    /// VSConnect Tick(Fixupdate) executtion component.    
    /// </summary>
    [DefaultExecutionOrder(-2)]
    public class VSConnectTickComponent : MonoBehaviour
    {
        public void FixedUpdate()
        {
            VsConnectServerUnity.Singleton.Tick(Time.time, Time.timeScale);
        }
    }
}
