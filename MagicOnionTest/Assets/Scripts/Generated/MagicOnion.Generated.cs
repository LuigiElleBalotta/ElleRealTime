﻿#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    public static partial class MagicOnionInitializer
    {
        static bool isRegistered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(isRegistered) return;
            isRegistered = true;

            MagicOnionClientRegistry<ElleRealTimeStd.Shared.Test.Interfaces.Service.IMyFirstService>.Register((x, y, z) => new ElleRealTimeStd.Shared.Test.Interfaces.Service.IMyFirstServiceClient(x, y, z));

            StreamingHubClientRegistry<ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService, ILoginServiceReceiver>.Register((a, _, b, c, d, e) => new ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginServiceClient(a, b, c, d, e));
            StreamingHubClientRegistry<ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub, ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHubReceiver>.Register((a, _, b, c, d, e) => new ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHubClient(a, b, c, d, e));
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion.Resolvers
{
    using System;
    using MessagePack;

    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();

        MagicOnionResolver()
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
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(7)
            {
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity[]), 0 },
                {typeof(global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]), 1 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>), 2 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<int, int>), 3 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, int>), 4 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, string, string>), 5 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, string>), 6 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ArrayFormatter<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity>();
                case 1: return new global::MessagePack.Formatters.ArrayFormatter<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player>();
                case 2: return new global::MagicOnion.DynamicArgumentTupleFormatter<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(default(global::UnityEngine.Vector3), default(global::UnityEngine.Quaternion));
                case 3: return new global::MagicOnion.DynamicArgumentTupleFormatter<int, int>(default(int), default(int));
                case 4: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, int>(default(string), default(int));
                case 5: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, string, string>(default(string), default(string), default(string));
                case 6: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, string>(default(string), default(string));
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace ElleRealTimeStd.Shared.Test.Interfaces.Service {
    using System;
	using MagicOnion;
    using MagicOnion.Client;
    using Grpc.Core;
    using MessagePack;

    public class IMyFirstServiceClient : MagicOnionClientBase<global::ElleRealTimeStd.Shared.Test.Interfaces.Service.IMyFirstService>, global::ElleRealTimeStd.Shared.Test.Interfaces.Service.IMyFirstService
    {
        static readonly Method<byte[], byte[]> SumAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> SumAsyncDelegate;

        static IMyFirstServiceClient()
        {
            SumAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IMyFirstService", "SumAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            SumAsyncDelegate = _SumAsync;
        }

        IMyFirstServiceClient()
        {
        }

        public IMyFirstServiceClient(CallInvoker callInvoker, IFormatterResolver resolver, IClientFilter[] filters)
            : base(callInvoker, resolver, filters)
        {
        }

        protected override MagicOnionClientBase<IMyFirstService> Clone()
        {
            var clone = new IMyFirstServiceClient();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.resolver = this.resolver;
            clone.filters = filters;
            return clone;
        }

        public new IMyFirstService WithHeaders(Metadata headers)
        {
            return base.WithHeaders(headers);
        }

        public new IMyFirstService WithCancellationToken(System.Threading.CancellationToken cancellationToken)
        {
            return base.WithCancellationToken(cancellationToken);
        }

        public new IMyFirstService WithDeadline(System.DateTime deadline)
        {
            return base.WithDeadline(deadline);
        }

        public new IMyFirstService WithHost(string host)
        {
            return base.WithHost(host);
        }

        public new IMyFirstService WithOptions(CallOptions option)
        {
            return base.WithOptions(option);
        }
   
        static ResponseContext _SumAsync(RequestContext __context)
        {
            return CreateResponseContext<DynamicArgumentTuple<int, int>, int>(__context, SumAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<int> SumAsync(int x, int y)
        {
            return InvokeAsync<DynamicArgumentTuple<int, int>, int>("IMyFirstService/SumAsync", new DynamicArgumentTuple<int, int>(x, y), SumAsyncDelegate);
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace ElleRealTimeStd.Shared.Test.Interfaces.Service {
    using Grpc.Core;
    using Grpc.Core.Logging;
    using MagicOnion;
    using MagicOnion.Client;
    using MessagePack;
    using System;
    using System.Threading.Tasks;

    public class ILoginServiceClient : StreamingHubClientBase<global::ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService, global::ILoginServiceReceiver>, global::ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "ILoginService", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService __fireAndForgetClient;

        public ILoginServiceClient(CallInvoker callInvoker, string host, CallOptions option, IFormatterResolver resolver, ILogger logger)
            : base(callInvoker, host, option, resolver, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -1297457280: // OnJoin
                {
                    var result = LZ4MessagePackSerializer.Deserialize<int>(data, resolver);
                    receiver.OnJoin(result); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case 1368362116: // LeaveAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -733403293: // JoinAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<int>(data, resolver);
                    ((TaskCompletionSource<int>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task LeaveAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1368362116, Nil.Default);
        }

        public global::System.Threading.Tasks.Task<int> JoinAsync(string roomName, string username, string password)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<string, string, string>, int> (-733403293, new DynamicArgumentTuple<string, string, string>(roomName, username, password));
        }


        class FireAndForgetClient : global::ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService
        {
            readonly ILoginServiceClient __parent;

            public FireAndForgetClient(ILoginServiceClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::ElleRealTimeStd.Shared.Test.Interfaces.Service.ILoginService FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task LeaveAsync()
            {
                return __parent.WriteMessageAsync<Nil>(1368362116, Nil.Default);
            }

            public global::System.Threading.Tasks.Task<int> JoinAsync(string roomName, string username, string password)
            {
                return __parent.WriteMessageAsyncFireAndForget<DynamicArgumentTuple<string, string, string>, int> (-733403293, new DynamicArgumentTuple<string, string, string>(roomName, username, password));
            }

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub {
    using Grpc.Core;
    using Grpc.Core.Logging;
    using MagicOnion;
    using MagicOnion.Client;
    using MessagePack;
    using System;
    using System.Threading.Tasks;

    public class IGamingHubClient : StreamingHubClientBase<global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub, global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHubReceiver>, global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "IGamingHub", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub __fireAndForgetClient;

        public IGamingHubClient(CallInvoker callInvoker, string host, CallOptions option, IFormatterResolver resolver, ILogger logger)
            : base(callInvoker, host, option, resolver, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -1297457280: // OnJoin
                {
                    var result = LZ4MessagePackSerializer.Deserialize<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player>(data, resolver);
                    receiver.OnJoin(result); break;
                }
                case 532410095: // OnLeave
                {
                    var result = LZ4MessagePackSerializer.Deserialize<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player>(data, resolver);
                    receiver.OnLeave(result); break;
                }
                case 1429874301: // OnMove
                {
                    var result = LZ4MessagePackSerializer.Deserialize<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player>(data, resolver);
                    receiver.OnMove(result); break;
                }
                case -1176718190: // OnAnimStateChange
                {
                    var result = LZ4MessagePackSerializer.Deserialize<DynamicArgumentTuple<int, int>>(data, resolver);
                    receiver.OnAnimStateChange(result.Item1, result.Item2); break;
                }
                case -660277788: // OnPlayerInfoSaved
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    receiver.OnPlayerInfoSaved(); break;
                }
                case 240215745: // OnQueriedCreatures
                {
                    var result = LZ4MessagePackSerializer.Deserialize<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Creatures.CreatureUnity[]>(data, resolver);
                    receiver.OnQueriedCreatures(result); break;
                }
                case 391125637: // OnChatMessage
                {
                    var result = LZ4MessagePackSerializer.Deserialize<DynamicArgumentTuple<string, string>>(data, resolver);
                    receiver.OnChatMessage(result.Item1, result.Item2); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -733403293: // JoinAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]>(data, resolver);
                    ((TaskCompletionSource<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1368362116: // LeaveAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -99261176: // MoveAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 147298429: // SendAnimStateAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -700463433: // SavePlayerAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -1386097739: // QueryCreaturesAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1166548060: // SendChatMessageAsync
                {
                    var result = LZ4MessagePackSerializer.Deserialize<Nil>(data, resolver);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]> JoinAsync(string roomName, int accountId)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<string, int>, global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]> (-733403293, new DynamicArgumentTuple<string, int>(roomName, accountId));
        }

        public global::System.Threading.Tasks.Task LeaveAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1368362116, Nil.Default);
        }

        public global::System.Threading.Tasks.Task MoveAsync(global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>, Nil>(-99261176, new DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(position, rotation));
        }

        public global::System.Threading.Tasks.Task SendAnimStateAsync(int state)
        {
            return WriteMessageWithResponseAsync<int, Nil>(147298429, state);
        }

        public global::System.Threading.Tasks.Task SavePlayerAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(-700463433, Nil.Default);
        }

        public global::System.Threading.Tasks.Task QueryCreaturesAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(-1386097739, Nil.Default);
        }

        public global::System.Threading.Tasks.Task SendChatMessageAsync(string text)
        {
            return WriteMessageWithResponseAsync<string, Nil>(1166548060, text);
        }


        class FireAndForgetClient : global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub
        {
            readonly IGamingHubClient __parent;

            public FireAndForgetClient(IGamingHubClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::ElleRealTimeStd.Shared.Test.Interfaces.StreamingHub.IGamingHub FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task<global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]> JoinAsync(string roomName, int accountId)
            {
                return __parent.WriteMessageAsyncFireAndForget<DynamicArgumentTuple<string, int>, global::ElleRealTimeStd.Shared.Test.Entities.StreamingHub.Player.Player[]> (-733403293, new DynamicArgumentTuple<string, int>(roomName, accountId));
            }

            public global::System.Threading.Tasks.Task LeaveAsync()
            {
                return __parent.WriteMessageAsync<Nil>(1368362116, Nil.Default);
            }

            public global::System.Threading.Tasks.Task MoveAsync(global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>>(-99261176, new DynamicArgumentTuple<global::UnityEngine.Vector3, global::UnityEngine.Quaternion>(position, rotation));
            }

            public global::System.Threading.Tasks.Task SendAnimStateAsync(int state)
            {
                return __parent.WriteMessageAsync<int>(147298429, state);
            }

            public global::System.Threading.Tasks.Task SavePlayerAsync()
            {
                return __parent.WriteMessageAsync<Nil>(-700463433, Nil.Default);
            }

            public global::System.Threading.Tasks.Task QueryCreaturesAsync()
            {
                return __parent.WriteMessageAsync<Nil>(-1386097739, Nil.Default);
            }

            public global::System.Threading.Tasks.Task SendChatMessageAsync(string text)
            {
                return __parent.WriteMessageAsync<string>(1166548060, text);
            }

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
