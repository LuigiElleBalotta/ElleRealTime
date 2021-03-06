﻿#pragma warning disable 618
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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(4)
            {
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity[]), 0 },
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity), 1 },
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureTemplateUnity), 2 },
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player), 3 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ArrayFormatter<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity>();
                case 1: return new MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnityFormatter();
                case 2: return new MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureTemplateUnityFormatter();
                case 3: return new MessagePack.Formatters.ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.PlayerFormatter();
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
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.Position, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Serialize(ref bytes, offset, value.Rotation, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CreatureID);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Guid);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.PrefabName, formatterResolver);
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

            var __Position__ = default(global::UnityEngine.Vector3);
            var __Rotation__ = default(global::UnityEngine.Quaternion);
            var __CreatureID__ = default(int);
            var __Guid__ = default(int);
            var __PrefabName__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Position__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Rotation__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __CreatureID__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __Guid__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 4:
                        __PrefabName__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity();
            ____result.Position = __Position__;
            ____result.Rotation = __Rotation__;
            ____result.CreatureID = __CreatureID__;
            ____result.Guid = __Guid__;
            ____result.PrefabName = __PrefabName__;
            return ____result;
        }
    }


    public sealed class CreatureTemplateUnityFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureTemplateUnity>
    {

        public int Serialize(ref byte[] bytes, int offset, global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureTemplateUnity value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ID);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.PrefabName, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity[]>().Serialize(ref bytes, offset, value.Creatures, formatterResolver);
            return offset - startOffset;
        }

        public global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureTemplateUnity Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __ID__ = default(int);
            var __PrefabName__ = default(string);
            var __Name__ = default(string);
            var __Creatures__ = default(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity[]);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __ID__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __PrefabName__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Creatures__ = formatterResolver.GetFormatterWithVerify<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureTemplateUnity();
            ____result.ID = __ID__;
            ____result.PrefabName = __PrefabName__;
            ____result.Name = __Name__;
            ____result.Creatures = __Creatures__;
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
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 10);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.Position, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Serialize(ref bytes, offset, value.Rotation, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ID);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Health);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaxHealth);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Damage);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Level);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Experience);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ExpToNextLevel);
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
            var __Health__ = default(int);
            var __MaxHealth__ = default(int);
            var __Damage__ = default(int);
            var __Level__ = default(int);
            var __Experience__ = default(int);
            var __ExpToNextLevel__ = default(int);

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
                    case 4:
                        __Health__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 5:
                        __MaxHealth__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 6:
                        __Damage__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 7:
                        __Level__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 8:
                        __Experience__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 9:
                        __ExpToNextLevel__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
            ____result.Health = __Health__;
            ____result.MaxHealth = __MaxHealth__;
            ____result.Damage = __Damage__;
            ____result.Level = __Level__;
            ____result.Experience = __Experience__;
            ____result.ExpToNextLevel = __ExpToNextLevel__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
