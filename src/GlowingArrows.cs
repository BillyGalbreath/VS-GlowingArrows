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
            foreach (EntityProperties properties in from properties in api.World.EntityTypes
                     let mapping = ((ClassRegistryAPI)api.ClassRegistry).GetField<ClassRegistry>("registry").entityClassNameToTypeMapping
                     where mapping[properties.Class].IsAssignableFrom(typeof(EntityProjectile))
                           || properties.Class.ToLower().Contains("projectile")
                           || (properties.Code.ToString()?.ToLower().Contains("projectile") ?? false)
                     select properties) {
                properties.Client.GlowLevel = 0xFF;
            }
        }, 1000);
    }
}
