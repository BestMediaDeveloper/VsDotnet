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
using BestMedia.VsDotnet.vs_connect_api_v3;

namespace BestMedia.VsDotnet
{

    /// <summary>
    /// VS Connect Client.Under construction
    /// </summary>
    public class VsConnectClient : VsConnectBase
    {
        void PingCallback(VscNode vscNode, VscLink link)
        {


            /* Note that Ping Statistics include system times from both the Requester
              (us) and the Responder (our peer). When both peers are running on the same
              system and within the same session, they MIGHT both use a common reference
              clock, and therefore the "system time" of the two peers can be directly
              compared with one another. HOWEVER, this is not guaranteed on on all
              operating systems, or even on different versions of the same operating
              system.

              To be safe, you should always assume that the system clocks of two peers do
              not refer to the same underlying reference clock, and can therefore NOT be
              compared with one another.
              */
            /// Peer's system time when VS Connect received our Ping request.
            double peerReceiveTime =
                        vsc_Api_V3_t.Link_PingGetStat(link
                                                  , PingMilestone.RESPONDER_REQUEST_RECEIVED
                                                  , TimeMomentAttr.SYSTEM_TIME);

            /// Peer's system time when it processed our request inside of its
            /// Node_Service() call.
            double peerProcessTime =
                              vsc_Api_V3_t.Link_PingGetStat(link
                                                        , PingMilestone.RESPONDER_REQUEST_PROCESSED
                                                        , TimeMomentAttr.SYSTEM_TIME);

            /// Peer's system time when it queued the Ping reponse. This happens during
            /// the the next call to Node_Service() after the Ping request is processed.
            double peerResponseTime =
                              vsc_Api_V3_t.Link_PingGetStat(link
                                                        , PingMilestone.RESPONDER_RESPONSE_QUEUED
                                                        , TimeMomentAttr.SYSTEM_TIME);

            /// The deduced period at which the Peer is calling Node_Service().
            double peerApparentSericePeriod = peerResponseTime - peerProcessTime;

            /// The time it took for the peer to process and respond to our request.
            double peerTotalProcessingTime = peerResponseTime - peerReceiveTime;

            /// The time that VS Connect queued our initial request for transmission.
            double ourRequestTransmitTime =
                              vsc_Api_V3_t.Link_PingGetStat(link
                                                        , PingMilestone.REQUESTER_REQUEST_QUEUED
                                                        , TimeMomentAttr.SYSTEM_TIME);

            /// The time that VS Connect receved the peer's response.
            double ourResponseReceiveTime =
                              vsc_Api_V3_t.Link_PingGetStat(link
                                                        , PingMilestone.REQUESTER_RESPONSE_RECEIVED
                                                        , TimeMomentAttr.SYSTEM_TIME);

            /// Time between when we sent the request and when we received the response.
            double totalRoundTripTime = ourResponseReceiveTime - ourRequestTransmitTime;

            /** This value will vary dramatically in this example due to the slow
            service frequency of both peers. The higher the service frequency of the
            peers, the lower and more stable this value will be, and the closer it will
            be to the actual network transport latency.
            */
            double apparentTotalRoundNetworkLatency =
                              totalRoundTripTime - peerTotalProcessingTime;

            // We set manual ping's appData to 1 so we can tell the difference between
            // automatic/periodic ping requests and those that we initiated manually.
            bool isManualPing = (vsc_Api_V3_t.Link_PingGetAppData(link).ToInt64() == 1);

            /*
  printf( "\nPing Results%s: Peer update period: %g"
          "\tRound-trip communication time: %g"
        , isManualPing? " (manual)" : " (automatic)"
        , peerApparentSericePeriod
        , apparentTotalRoundNetworkLatency            );
        */
        }
    }




}
