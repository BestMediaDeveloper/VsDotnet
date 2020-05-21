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

namespace BestMedia.VsUnity
{

    public class VsConnectSolverUnity : BestMedia.VsDotnet.VsConnectSolver
    {
        readonly public VsConnectComponent VsComponent;


        public VsConnectSolverUnity(VsConnectComponent setVsComponent)
        {
            VsComponent = setVsComponent;


            /*
            this.GetDoubleParameters.Add("VSAP_VS_X", () => VsComponent.transform.position.VsFoward());
            this.GetDoubleParameters.Add("VSAP_VS_Y", () => VsComponent.transform.position.VsLeft());
            this.GetDoubleParameters.Add("VSAP_VS_Z", () => VsComponent.transform.position.VsUp());
            this.GetDoubleParameters.Add("VSAP_VS_ROLL", () => VsComponent.transform.rotation.VsRoll());
            this.GetDoubleParameters.Add("VSAP_VS_PITCH", () => VsComponent.transform.rotation.VsPitch());
            this.GetDoubleParameters.Add("VSAP_VS_YAW", () => VsComponent.transform.rotation.VsYaw());
            */
        }        



    }
}
