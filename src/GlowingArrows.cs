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
            Dictionary<string, Type?>? mapping = ((ClassRegistryAPI)api.ClassRegistry).GetField<ClassRegistry>("registry")?.entityClassNameToTypeMapping;
            foreach (EntityProperties properties in api.World.EntityTypes.Where(properties => IsProjectile(mapping, properties))) {
                properties.Client.GlowLevel = 0xFF;
            }
        }, 1000);
    }

    private static bool IsProjectile(IReadOnlyDictionary<string, Type?>? mapping, EntityProperties properties) {
        if ((mapping?.TryGetValue(properties.Class, out Type? type) ?? false) &&
            (type?.IsAssignableFrom(typeof(EntityProjectile)) ?? false)) {
            return true;
        }

        if (properties.Class.ToLower().Contains("projectile")) {
            return true;
        }

        return properties.Code?.ToString().ToLower().Contains("projectile") ?? false;
    }
}
