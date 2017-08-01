using UnityEngine;
using System;
using System.Collections.Generic;
namespace SimpleFramework
{
    public class DllManager
    {
        //  static System.Reflection.Assembly myMonoAssembly;

        public static Type GetType(string monoName)
        {
            return null;
            //if (myMonoAssembly != null)
            //    return myMonoAssembly.GetType(monoName);
            //return null;
        }

        private static void Import(AssetBundle dllbundle)
        {
            //获取正确的程序集

            //System.Reflection.Assembly aly = System.Reflection.Assembly.Load("");
            //foreach (var tem in aly.GetTypes())
            //{
            //    if (tem.Namespace.Contains("_mono"))
            //    {
            //        monoDic.Add(tem.Name,tem.Assembly)
            //    }
            //}
        }
    }
}