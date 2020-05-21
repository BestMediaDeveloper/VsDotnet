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



namespace BestMedia.VsDotnet
{
    /// <summary>
    /// Utility for  VsDotnet
    /// </summary>
    static public class VsUtility
    {
        /// <summary>
        /// Log delegate
        /// </summary>
        /// <param name="message"></param>
        public delegate void LogDelegate(string message);


        /// <summary>
        /// Normal log output delegate
        /// </summary>
        static public LogDelegate Log { get; set; } = Console.WriteLine;

        /// <summary>
        /// Log output for warning
        /// </summary>
        static public LogDelegate LogWarning { get; set; } = Console.WriteLine;

        /// <summary>
        /// Log output for errors
        /// </summary>
        static public LogDelegate LogError { get; set; } = Console.WriteLine;



        public static string TrimAndUnquoteInPlace(this string data)
        {
            if (string.IsNullOrEmpty(data)) return data;
            return data.Trim().Trim('"');
        }
        /// <summary>
        /// Object to unmanaged pointer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IntPtr ToPtr(object data) => data == null ? IntPtr.Zero : (IntPtr)System.Runtime.InteropServices.GCHandle.Alloc(data);
        /// <summary>
        /// Unmanaged pointer to Object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object ToObject(IntPtr data) => data == IntPtr.Zero ? null : System.Runtime.InteropServices.GCHandle.FromIntPtr(data).Target;



        /// <summary>
        /// Check the condition
        /// I really wanted to make it a delegate, but since Asset can't be a delegate, it corresponds with a function.
        /// </summary>
        /// <param name="condition"></param>
        [System.Diagnostics.Conditional("Debug")]
        public static void Check(bool condition)
        {

#if UNITY_2019_1_OR_NEWER
            if (condition == false)
            {
                
                throw new System.InvalidOperationException();
            }
#else
            System.Diagnostics.Debug.Assert(condition);
#endif

        }

    }
}
