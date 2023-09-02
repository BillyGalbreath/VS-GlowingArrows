using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

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
        if (api != null) {
            api.Event.OnEntitySpawn -= GlowThoseArrows;
        }
    }

    private static void GlowThoseArrows(Entity entity) {
        if (IsProjectile(entity)) {
            entity.Properties.Client.GlowLevel = 255;
        }
    }

    private static bool IsProjectile(RegistryObject entity) {
        return entity is EntityProjectile ||
               entity.Class.ToLower().Contains("projectile") ||
               entity.GetType().ToString().ToLower().Contains("projectile");
    }
}
