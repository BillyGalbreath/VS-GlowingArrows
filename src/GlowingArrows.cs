using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace GlowingArrows;

public class GlowingArrows : ModSystem {
    // internal reference to the client's api
    private ICoreClientAPI api;

    public override bool ShouldLoad(EnumAppSide side) {
        // only load if on the client side
        return side.IsClient();
    }

    public override void StartClientSide(ICoreClientAPI capi) {
        // store reference to the api (for later use)
        api = capi;

        // add our logic to entity spawn event
        api.Event.OnEntitySpawn += MakeProjectilesGlow;
    }

    public override void Dispose() {
        // make sure api is not null (in case the mod never fully started)
        if (api != null) {
            // remove our logic from entity spawn event
            api.Event.OnEntitySpawn -= MakeProjectilesGlow;
        }
    }

    /// <summary>
    /// If the passed entity is a projectile, make it glow at full brightness (0xFF)
    /// </summary>
    /// <param name="entity"></param>
    private static void MakeProjectilesGlow(Entity entity) {
        // verify the entity is a projectile
        if (IsProjectile(entity)) {
            // make the projectile glow
            entity.Properties.Client.GlowLevel = 0xFF;
        }
    }

    /// <summary>
    /// Check if the passed object is a projectile.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Returns true if one of the following are true:
    /// <li>a standard <see cref="Vintagestory.GameContent.EntityProjectile">EntityProjectile</see></li>
    /// <li>registered class name contains the word "projectile" (case insensitive)</li>
    /// <li>object's type name contains the word "projectile" (case insensitive)</li>
    /// </returns>
    private static bool IsProjectile(RegistryObject obj) {
        return obj is EntityProjectile ||
               obj.Class.ToLower().Contains("projectile") ||
               obj.GetType().ToString().ToLower().Contains("projectile");
    }
}
