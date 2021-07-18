using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            ET_IdGenerater_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_String_Binding.Register(app);
            ET_Log_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int64_ILTypeInstance_Binding.Register(app);
            System_Byte_Binding.Register(app);
            System_Text_StringBuilder_Binding.Register(app);
            System_Text_Encoding_Binding.Register(app);
            LitJson_JsonMapper_Binding.Register(app);
            System_Single_Binding.Register(app);
            LitJson_JsonWriter_Binding.Register(app);
            System_Reflection_MethodBase_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            System_Net_NetworkInformation_NetworkInterface_Binding.Register(app);
            System_Net_NetworkInformation_IPInterfaceProperties_Binding.Register(app);
            System_Net_NetworkInformation_UnicastIPAddressInformationCollection_Binding.Register(app);
            System_Collections_Generic_IEnumerator_1_UnicastIPAddressInformation_Binding.Register(app);
            System_Net_NetworkInformation_IPAddressInformation_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Net_IPAddress_Binding.Register(app);
            System_Net_IPEndPoint_Binding.Register(app);
            System_Int32_Binding.Register(app);
            System_IO_MemoryStream_Binding.Register(app);
            ProtoBuf_Serializer_Binding.Register(app);
            ET_Game_Binding.Register(app);
            ET_Hotfix_Binding.Register(app);
            System_Collections_Generic_List_1_Type_Binding.Register(app);
            System_Collections_Generic_List_1_Type_Binding_Enumerator_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            ProtoBuf_PType_Binding.Register(app);
            System_Exception_Binding.Register(app);
            System_Collections_Generic_HashSet_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_HashSet_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int64_ILTypeInstance_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int64_ILTypeInstance_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            ET_Pool_1_Dictionary_2_Int64_ILTypeInstance_Binding.Register(app);
            ET_Pool_1_HashSet_1_ILTypeInstance_Binding.Register(app);
            ET_Pool_1_Dictionary_2_Type_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Type_ILTypeInstance_Binding.Register(app);
            System_Activator_Binding.Register(app);
            ET_UnOrderMultiMapSet_2_Type_Type_Binding.Register(app);
            ET_UnOrderMultiMap_2_Type_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_HashSet_1_Type_Binding.Register(app);
            System_Collections_Generic_HashSet_1_Type_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_List_1_Object_Binding.Register(app);
            System_Collections_Generic_List_1_Object_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Assembly_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Assembly_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Assembly_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            System_Reflection_Assembly_Binding.Register(app);
            System_Collections_Generic_Queue_1_Int64_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            ET_ETAsyncTaskMethodBuilder_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_Int32_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int64_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Int64_ILTypeInstance_Binding.Register(app);
            System_Linq_Enumerable_Binding.Register(app);
            System_Collections_Generic_IEnumerable_1_KeyValuePair_2_Type_Int32_Binding.Register(app);
            System_Collections_Generic_IEnumerator_1_KeyValuePair_2_Type_Int32_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Type_Int32_Binding.Register(app);
            System_Collections_Generic_Queue_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_Object_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            ET_ETTask_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
