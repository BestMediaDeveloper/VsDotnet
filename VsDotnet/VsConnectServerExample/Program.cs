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
using BestMedia.VsDotnet;


/// <summary>
/// VS connect server example
/// This example allows the VS_SDK client vs_connect_example_client to interact with Age.
/// </summary>
namespace VsConnectServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set VSPath to the execution path
            VsModule.VsPath = System.Windows.Forms.Application.StartupPath;
            //
            VsModule.VsEngineUserDir = System.IO.Path.Combine(VsModule.VsPath, "Engine");

            //VSConnectServer is enabled.
            VsModule.EnableVsConnectServer = true;
            //Start Module
            VsModule.StartupModule();


            //Server time(second)
            double worldTime = 0;

            ///Tick　Interval time 1 second
            TimeSpan Interval = TimeSpan.FromSeconds(1);
           

            
            double nextWorldTime = 0;
            double clientTime = 0;


            
            //get server object 
            var server = VsConnectServer.Singleton;
            BestMedia.VsDotnet.VsUtility.Check(server != null);

            //Add Server object  and register object
            BestMedia.VsDotnet.VsConnectSolver serverSolver = new BestMedia.VsDotnet.VsConnectSolver() { VscObjectName = "ServerObject" };
            server.RegisterObject(serverSolver);


            //Add age name action. world Time second to minutes.
            serverSolver.GetDoubleFuncs.Add("Age", () => worldTime/60);

            
            //Add Client object and register object
            BestMedia.VsDotnet.VsConnectSolver clientSolver = new BestMedia.VsDotnet.VsConnectSolver() { VscObjectName = "ClientObject" };
            server.RegisterObject(clientSolver);

            //Add age name funcrtion that set climent time
            clientSolver.SetDoubleActions.Add("Age", (v) => { clientTime = v; Log($"Client Age{clientTime:0.00}"); });
            

            //set loop start time
            DateTime LoopStartTime = DateTime.Now;




            while (true)
            {
                ///Calc world time;
                worldTime = (DateTime.Now - LoopStartTime).TotalSeconds;
                Log($"World Age{worldTime/60:0.00}[minutes]");

                //Tick server object
                server.Tick(worldTime, 1);

                //Calc next loop time
                nextWorldTime += Interval.TotalSeconds;
                worldTime = (DateTime.Now - LoopStartTime).TotalSeconds;

                //Sleep time until next  time
                if (nextWorldTime > worldTime)
                {
                    System.Threading.Thread.Sleep((int)(1000 * (nextWorldTime - worldTime)));

                }

                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                    if (consoleKey.Key == ConsoleKey.Q) break;
                }
            }



            VsModule.ShutdownModule();

        }
    }
}
