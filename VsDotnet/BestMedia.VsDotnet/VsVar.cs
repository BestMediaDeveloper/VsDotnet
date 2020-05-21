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
using static BestMedia.VsDotnet.VsUtility;


namespace BestMedia.VsDotnet
{
   
   
    /// <summary>
    /// Genericed VS Variable 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class VsVar<T> : VSVarBase
    {
        public override Type VariableType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// Variable value parameter;
        /// </summary>
        public virtual  T Value
        {
            get
            {
                return default(T);
            }

            set
            {
                if (Direction == VsVarDirection.Output) { VsUtility.LogError("This value is only getter"); return; }

            }
        }

        public VsVar(string name, VsVarDirection direction): base(name,direction)
        {

        }
    }


    /// <summary>
    /// Variable Direction
    /// </summary>
    public enum VsVarDirection
    {
        /// <summary>
        /// Value set to VS.
        /// </summary>
        Input,
        /// <summary>
        /// Value get from VS.
        /// </summary>
        Output,
        //InputOutput


    }


    /// <summary>
    /// VsDotnet variable base
    /// </summary>
    public abstract class  VSVarBase
    {
        VsVarDirection _direction;
        string _name;

        /// <summary>
        /// Variable Type
        /// </summary>
        abstract public System.Type VariableType { get; }
        /// <summary>
        /// Variable Name
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Variable Direction
        /// </summary>
        public VsVarDirection Direction => _direction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="direction"></param>
        public  VSVarBase(string name,VsVarDirection direction) 
        {
            Check(!string.IsNullOrEmpty(name));
            _name = name;
            _direction = direction;
        }
    }

    

    
}
