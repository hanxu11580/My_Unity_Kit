using ProtoBuf;
using System;
using System.IO;

namespace Hotfix
{
    public static class ProtobufHelper
    {
        static ProtobufHelper()
        {
            //TODO 预热在老版本的pb是不支持的

            foreach (Type type in ET.Game.Hotfix.GetHotfixTypes())
            {
                if (type.GetCustomAttributes(typeof (ProtoContractAttribute), false).Length == 0 &&
                    type.GetCustomAttributes(typeof (ProtoMemberAttribute), false).Length == 0)
                {
                    continue;
                }

                PType.RegisterType(type.FullName, type);
                //Log.Info($"{type} -------------------------- 注册完毕");
            }
        }

        public static void Init()
        {
        }

        public static object FromBytes(Type type, byte[] bytes, int index, int count)
        {
            using (MemoryStream stream = new MemoryStream(bytes, index, count))
            {
                return ProtoBuf.Serializer.Deserialize(type, stream);
            }
        }

        public static byte[] ToBytes(object message)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, message);
                return stream.ToArray();
            }
        }

        public static void ToStream(object message, MemoryStream stream)
        {
            ProtoBuf.Serializer.Serialize(stream, message);
        }

        public static object FromStream(Type type, MemoryStream stream)
        {
            return ProtoBuf.Serializer.Deserialize(type, stream);
        }
    }
}