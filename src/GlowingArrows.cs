using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace GlowingArrows;

public class GlowingArrows : ModSystem {
    private ICoreClientAPI api;

    public override bool ShouldLoad(EnumAppSide side) {
        return side.IsClient();
    }

    public override void StartClientSide(ICoreClientAPI capi) {
        api = capi;

        api.Event.OnEntitySpawn += GlowThoseArrows;
    }

    public override void Dispose() {
        api.Event.OnEntitySpawn -= GlowThoseArrows;
    }

    private static void GlowThoseArrows(Entity entity) {
        if (entity.GetType().ToString().ToLower().Contains("projectile")) {
            entity.Properties.Client.GlowLevel = 255;
        }
    }
}
