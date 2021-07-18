
using System.Collections.Generic;
using System.Reflection;
using System;



namespace XhO_OKit
{
    public static class ReflectionTool
    {
        /// <summary>
        /// 当前的运行时程序集
        /// </summary>
        private static HashSet<string> RunTimeAssemblies = new HashSet<string>()
        {
            "Assembly-CSharp",
            "UnityEngine",
            "UnityEngine.CoreModule",
            "UnityEngine.UI",
            "UnityEngine.PhysicsModule",
            "ASimpleFramework.RunTime"
        };
        /// <summary>
        /// 当前程序域运行时，获得指定类型
        /// </summary>
        public static Type GetTypeInRunTimeAssemblies(string typeName)
        {
            Type type = null;
            foreach (var assembly in RunTimeAssemblies)
            {
                type = Type.GetType(typeName + "," + assembly);
                if (type != null)
                {
                    return type;
                }   
            }
            //"获取失败，当前运行时程序集中不存在" + typeName
            return null;
        }

        /// <summary>
        /// 根据条件 获得程序运行时 类型集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<Type> GetTypesInRunTimeAssemblies(Func<Type, bool> filter)
        {
            List<Type> types = new List<Type>();
            Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies(); //获取已加载到此应用程序域的执行上下文中的程序集
            for (int i = 0; i < assemblys.Length; i++)
            {
                if (RunTimeAssemblies.Contains(assemblys[i].GetName().Name))
                {
                    Type[] types1 = assemblys[i].GetTypes(); //获得此程序集所有类型
                    foreach (Type type in types1)
                    {
                        if (filter(type))
                            types.Add(type);
                    }
                }
            }
            return types;
        }
    }
}
