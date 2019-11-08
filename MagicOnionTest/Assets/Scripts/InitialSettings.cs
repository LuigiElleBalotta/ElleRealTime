using System.Collections;
using System.Collections.Generic;
using MessagePack.Resolvers;
using UnityEngine;

namespace Assets.Scripts
{
    class InitialSettings
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RegisterResolvers()
        {
            CompositeResolver.RegisterAndSetAsDefault
            (
                MagicOnion.Resolvers.MagicOnionResolver.Instance,
                //MessagePack.Resolvers.DynamicGenericResolver.Instance,
                BuiltinResolver.Instance,
                PrimitiveObjectResolver.Instance
            );
        }
    }
}

