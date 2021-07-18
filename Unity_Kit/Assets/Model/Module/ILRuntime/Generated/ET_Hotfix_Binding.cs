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
    unsafe class ET_Hotfix_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ET.Hotfix);
            args = new Type[]{};
            method = type.GetMethod("GetHotfixTypes", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetHotfixTypes_0);

            field = type.GetField("HotfixUpdate", flag);
            app.RegisterCLRFieldGetter(field, get_HotfixUpdate_0);
            app.RegisterCLRFieldSetter(field, set_HotfixUpdate_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_HotfixUpdate_0, AssignFromStack_HotfixUpdate_0);
            field = type.GetField("HotfixLateUpdate", flag);
            app.RegisterCLRFieldGetter(field, get_HotfixLateUpdate_1);
            app.RegisterCLRFieldSetter(field, set_HotfixLateUpdate_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_HotfixLateUpdate_1, AssignFromStack_HotfixLateUpdate_1);
            field = type.GetField("HotfixApplicationQuit", flag);
            app.RegisterCLRFieldGetter(field, get_HotfixApplicationQuit_2);
            app.RegisterCLRFieldSetter(field, set_HotfixApplicationQuit_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_HotfixApplicationQuit_2, AssignFromStack_HotfixApplicationQuit_2);


        }


        static StackObject* GetHotfixTypes_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ET.Hotfix instance_of_this_method = (ET.Hotfix)typeof(ET.Hotfix).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetHotfixTypes();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_HotfixUpdate_0(ref object o)
        {
            return ((ET.Hotfix)o).HotfixUpdate;
        }

        static StackObject* CopyToStack_HotfixUpdate_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.Hotfix)o).HotfixUpdate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_HotfixUpdate_0(ref object o, object v)
        {
            ((ET.Hotfix)o).HotfixUpdate = (System.Action)v;
        }

        static StackObject* AssignFromStack_HotfixUpdate_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @HotfixUpdate = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((ET.Hotfix)o).HotfixUpdate = @HotfixUpdate;
            return ptr_of_this_method;
        }

        static object get_HotfixLateUpdate_1(ref object o)
        {
            return ((ET.Hotfix)o).HotfixLateUpdate;
        }

        static StackObject* CopyToStack_HotfixLateUpdate_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.Hotfix)o).HotfixLateUpdate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_HotfixLateUpdate_1(ref object o, object v)
        {
            ((ET.Hotfix)o).HotfixLateUpdate = (System.Action)v;
        }

        static StackObject* AssignFromStack_HotfixLateUpdate_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @HotfixLateUpdate = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((ET.Hotfix)o).HotfixLateUpdate = @HotfixLateUpdate;
            return ptr_of_this_method;
        }

        static object get_HotfixApplicationQuit_2(ref object o)
        {
            return ((ET.Hotfix)o).HotfixApplicationQuit;
        }

        static StackObject* CopyToStack_HotfixApplicationQuit_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.Hotfix)o).HotfixApplicationQuit;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_HotfixApplicationQuit_2(ref object o, object v)
        {
            ((ET.Hotfix)o).HotfixApplicationQuit = (System.Action)v;
        }

        static StackObject* AssignFromStack_HotfixApplicationQuit_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @HotfixApplicationQuit = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((ET.Hotfix)o).HotfixApplicationQuit = @HotfixApplicationQuit;
            return ptr_of_this_method;
        }



    }
}
