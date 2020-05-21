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
using BestMedia.VsDotnet.vs_connect_api_v3;
using static BestMedia.VsDotnet.VsUtility;


namespace BestMedia.VsDotnet
{
    
    using VscLink = System.IntPtr;

    /// <summary>
    /// This class is VsConnectNode. This extract from  VsConnectNode.h(Unreal).
    /// See VsConnectNode.h for details
    /// </summary>
    public class VsConnectNode
    {
        
        VscNode mVsConnectNode;


        public bool ShuttingDown { get; set; } = false;
        public VscNode VscNode => mVsConnectNode;


        static public VsConnectNode GetInstanceFromLink(VscLink link)
        {
            VsConnectNode retNode = null;

            var vscNode = vsc_Api_V3_t.Link_GetNode(link);


            if (vscNode != IntPtr.Zero)
            {
                var nodeAppData = vsc_Api_V3_t.Node_GetAppData(vscNode);
                retNode = VsUtility.ToObject(nodeAppData) as VsConnectNode;              
            }

            return retNode;

        }


        /// <summary>
        /// Completely shuts down this VS Connect Node: disconnects all VS Connect Links, shutsdown and releases underlying resources(sockets, etc).
        /// </summary>
        public void Shutdown()
        {
            if (mVsConnectNode == IntPtr.Zero) return;
            vsc_Api_V3_t.Node_HandleRelease(out mVsConnectNode);

            Check(mVsConnectNode==IntPtr.Zero);
        }

        public VsConnectNode(VscNode setNode )
        {
            mVsConnectNode = setNode;
        }
             

        ~VsConnectNode()
        {
            Shutdown();
        }

    }
}
