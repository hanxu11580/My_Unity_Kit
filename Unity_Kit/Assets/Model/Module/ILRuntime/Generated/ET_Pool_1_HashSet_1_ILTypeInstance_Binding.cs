using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class ET_Pool_1_HashSet_1_ILTypeInstance_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>>);
            args = new Type[]{};
            method = type.GetMethod("Fetch", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Fetch_0);
            args = new Type[]{typeof(System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>)};
            method = type.GetMethod("Recycle", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Recycle_1);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* Fetch_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Fetch();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Recycle_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance> @t = (System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>)typeof(System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Recycle(@t);

            return __ret;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new ET.Pool<System.Collections.Generic.HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
