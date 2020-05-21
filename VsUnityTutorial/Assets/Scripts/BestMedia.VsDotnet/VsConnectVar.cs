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

namespace BestMedia.VsDotnet
{

  

    /// <summary>
    /// Variable for vs connect
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class VsConnectVar<T> : VsVar<T>
    {
        /// <summary>
        /// solver
        /// </summary>
        readonly VsConnectSolver solver;
        /// <summary>
        /// Set value action , direction is input
        /// </summary>
        readonly Action<T> action = null;
        /// <summary>
        /// Get value function, direction is output
        /// </summary>
        readonly Func<T> func = null;

        
        T lastValue = default(T);
        

        /// <summary>
        /// Vallue property        
        /// </summary>
        override public T Value
        {
            get {

                if (Direction == VsVarDirection.Output) return func();
                return lastValue;

            }
            set
            {
                Check(Direction == VsVarDirection.Input);
                base.Value = value;
                lastValue = value;
                action(value);
            }
            
        }
        

        /// <summary>
        /// Get value property constructer
        /// </summary>
        /// <param name="setSolver"></param>
        /// <param name="setName"></param>
        /// <param name="setFunc"></param>
        public VsConnectVar(VsConnectSolver setSolver, string setName, Func<T> setFunc) : base(setName, VsVarDirection.Output )
        {
            Check(setSolver != null);
            Check(setFunc != null);

            solver = setSolver;
            func = setFunc;
        }

        /// <summary>
        /// Set value constructer
        /// </summary>
        /// <param name="setSolver"></param>
        /// <param name="setName"></param>
        /// <param name="setAction"></param>
        /// <param name="defaultValue"></param>
        public VsConnectVar(VsConnectSolver setSolver, string setName, Action<T> setAction, T defaultValue=default(T)) : base(setName, VsVarDirection.Input)
        {
            Check(setSolver != null);
            Check(setAction != null);

            solver = setSolver;
            action = setAction;
            lastValue = defaultValue;
        }

    }



}
