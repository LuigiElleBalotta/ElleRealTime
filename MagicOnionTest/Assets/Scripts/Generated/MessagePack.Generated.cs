#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Resolvers
{
    using System;
    using MessagePack;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(2)
            {
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity), 0 },
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player), 1 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnityFormatter();
                case 1: return new MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.PlayerFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612



#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures
{
    using System;
    using MessagePack;


    public sealed class CreatureUnityFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity>
    {

        public int Serialize(ref byte[] bytes, int offset, global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 5);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.PrefabName, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.Position, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Serialize(ref bytes, offset, value.Rotation, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ID);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Guid);
            return offset - startOffset;
        }

        public global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __PrefabName__ = default(string);
            var __Position__ = default(global::UnityEngine.Vector3);
            var __Rotation__ = default(global::UnityEngine.Quaternion);
            var __ID__ = default(int);
            var __Guid__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __PrefabName__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Position__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Rotation__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __ID__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 4:
                        __Guid__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity();
            ____result.PrefabName = __PrefabName__;
            ____result.Position = __Position__;
            ____result.Rotation = __Rotation__;
            ____result.ID = __ID__;
            ____result.Guid = __Guid__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player
{
    using System;
    using MessagePack;


    public sealed class PlayerFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player>
    {

        public int Serialize(ref byte[] bytes, int offset, global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.Position, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Serialize(ref bytes, offset, value.Rotation, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ID);
            return offset - startOffset;
        }

        public global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(string);
            var __Position__ = default(global::UnityEngine.Vector3);
            var __Rotation__ = default(global::UnityEngine.Quaternion);
            var __ID__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Position__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Rotation__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __ID__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player();
            ____result.Name = __Name__;
            ____result.Position = __Position__;
            ____result.Rotation = __Rotation__;
            ____result.ID = __ID__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
