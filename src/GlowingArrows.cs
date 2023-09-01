using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace GlowingArrows;

public class GlowingArrows : ModSystem {
    private static readonly List<string> PROJECTILE = new() { "arrow", "spear" };

    public override bool ShouldLoad(EnumAppSide side) {
        return side.IsClient();
    }

    public override void StartClientSide(ICoreClientAPI api) {
        api.Event.RegisterCallback(_ => {
            foreach (EntityProperties entityProperties in api.World.EntityTypes.Where(entityProperties => PROJECTILE.Any(entityProperties.Code.ToString()!.Contains))) {
                entityProperties.Client.GlowLevel = 255;
                Mod.Logger.Event($"Set {entityProperties.Code} to glow!");
            }
        }, 1000);
    }
}
