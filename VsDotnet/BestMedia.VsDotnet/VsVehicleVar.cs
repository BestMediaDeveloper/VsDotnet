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
using BestMedia.VsDotnet.vs_vehicle;
using static BestMedia.VsDotnet.VsUtility;


namespace BestMedia.VsDotnet
{

    /// <summary>
    /// Variable for VS Vehicle
    /// </summary>
    abstract public class VsVehicleVar<T> : VsVar<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public VsVehicleSolver VsVehicleSolver { get; }

        /// <summary>
        /// This is performed when a vehicle reset is performed and the pointer is set.
        /// </summary>
        protected abstract void OnResetVehicle();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="direction"></param>
        /// <param name="solver"></param>
        public VsVehicleVar(string name, VsVarDirection direction, VsVehicleSolver solver) : base(name, direction)
        {
            VsVehicleSolver = solver;
            VsVehicleSolver.AddVSVar(this);
            VsVehicleSolver.ResetVehicleEvent += (o, e) => OnResetVehicle();
        }
    }


    /// <summary>summary
    /// Double  variable for VS Vehicle
    /// </summary>
    public class VSVehicleVarDouble : VsVehicleVar<double>
    {
        IntPtr _pointer  = IntPtr.Zero;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="direction"></param>
        /// <param name="solver"></param>
        public VSVehicleVarDouble(string name, VsVarDirection direction, VsVehicleSolver solver) : base(name, direction, solver)
        {
            OnResetVehicle();
        }



        /// <summary>
        /// Sets a pointer to a variable when it is reset.In this case, the function to be executed is different for input only or input/output.
        /// </summary>
        protected override void OnResetVehicle()
        {
            unsafe
            {
                if(Direction== VsVarDirection.Input)
                {
                    _pointer = new IntPtr(BestMedia.VsDotnet.vs_vehicle.VS_Vehicle.GetVarPtr_NC(VsVehicleSolver.VsVehicleHandle, this.Name));
                }
                else
                {
                    _pointer = new IntPtr(BestMedia.VsDotnet.vs_vehicle.VS_Vehicle.GetVarPtr(VsVehicleSolver.VsVehicleHandle, this.Name));

                }
            }
        }


        /// <summary>
        /// double value
        /// </summary>
        public override double Value
        {
            get
            {
                if (_pointer == IntPtr.Zero) return 0;
                unsafe
                {
                    return *(double*)_pointer.ToPointer();
                }
            }

            set
            {
                base.Value = value;
                unsafe                
                {
                    if (Direction == VsVarDirection.Input)
                        (*(double*)_pointer.ToPointer()) = value;
                }
            }
        }

    }

    /// <summary>
    /// Int  variable for VS Vehicle
    /// </summary>
    public class VSVehicleVarInt : VsVehicleVar<int>
    {
        IntPtr _pointer = IntPtr.Zero;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="direction"></param>
        /// <param name="solver"></param>
        public VSVehicleVarInt(string name, VsVarDirection direction, VsVehicleSolver solver) : base(name, direction, solver)
        {
            OnResetVehicle();
        }


        /// <summary>
        /// Sets a pointer to a variable when it is reset.In this case, the function to be executed is different for input only or input/output.
        /// </summary>
        protected override void OnResetVehicle()
        {
            unsafe
            {
                if (Direction == VsVarDirection.Input)
                {
                    _pointer = new IntPtr(BestMedia.VsDotnet.vs_vehicle.VS_Vehicle.GetVarPtrInt_NC(VsVehicleSolver.VsVehicleHandle, this.Name));
                }
                else
                {
                    _pointer = new IntPtr(BestMedia.VsDotnet.vs_vehicle.VS_Vehicle.GetVarPtrInt(VsVehicleSolver.VsVehicleHandle, this.Name));

                }
            }
        }


        /// <summary>
        /// Int value
        /// </summary>
        public override int Value
        {
            get
            {
                if (_pointer == IntPtr.Zero) return 0;
                unsafe
                {
                    return *(int*)_pointer.ToPointer();
                }
            }

            set
            {
                base.Value = value;
                unsafe
                {
                    if (Direction == VsVarDirection.Input)
                        (*(int*)_pointer.ToPointer()) = value;
                }
            }
        }

    }


    /// <summary>
    /// Double  variable for VS Vehicle ,that is imported    
    /// </summary>
    public class VSVehicleVarImport:VsVehicleVar<double>
    {
        double _value = 0;
        int _importIndex = 0;

        public VSVehicleVarImport(string name, double defaultValue, VsVehicleSolver solver) : base(name, VsVarDirection.Input, solver)
        {
            _value = defaultValue;

            OnResetVehicle();

        }



        /// <summary>
        /// Sets a import index to a variable when it is reset.
        /// </summary>
        protected override void OnResetVehicle()
        {
            _importIndex = VS_Vehicle.Import_GetOrAdd(VsVehicleSolver.VsVehicleHandle,this.Name, _value, true);
            if (_importIndex < 0)
            {
                LogError($"Unable to locate VS Solver import {Name}.");
                
                
            }
        }


        /// <summary>
        /// Imported value
        /// </summary>
        public override double Value
        {
            get
            {   
                return _value;
            }

            set
            {   
                _value = value;
                if (_importIndex >= 0)
                {
                    VS_Vehicle.Import_SetValue(VsVehicleSolver.VsVehicleHandle, _importIndex, _value);
                }
            }
        }

    }
    






}
