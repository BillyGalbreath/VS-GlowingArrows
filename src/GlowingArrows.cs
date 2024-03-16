using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.Common;
using Vintagestory.GameContent;

namespace GlowingArrows;

public class GlowingArrows : ModSystem {
    public override bool ShouldLoad(EnumAppSide side) {
        return side.IsClient();
    }

    public override void StartClientSide(ICoreClientAPI api) {
        api.Event.RegisterCallback(_ => {
            Dictionary<string, Type>? mapping = ((ClassRegistryAPI)api.ClassRegistry).GetField<ClassRegistry>("registry")?.entityClassNameToTypeMapping;
            foreach (EntityProperties entityProperties in api.World.EntityTypes.Where(properties =>
                         (mapping != null && mapping[properties.Class].IsAssignableFrom(typeof(EntityProjectile)))
                         || properties.Class.ToLower().Contains("projectile")
                         || (properties.Code.ToString()?.ToLower().Contains("projectile") ?? false)
                     )) {
                entityProperties.Client.GlowLevel = 0xFF;
            }
        }, 1000);
    }
}
