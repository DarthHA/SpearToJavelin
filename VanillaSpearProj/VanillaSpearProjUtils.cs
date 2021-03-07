using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpearToJavelin.VanillaSpearProj
{
    public class VanillaSpearProjUtils:ModProjectile
    {
        public override string Texture => S2JUtils.OmniTexture;
        public virtual int Weapon => 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Weapon.ToString());
            Main.projectileTexture[projectile.type] = Main.itemTexture[Weapon];
            drawHeldProjInFrontOfHeldItemAndArms = true;
            SafeSSD();
        }
        public virtual void SafeSSD()
        {
        }
        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.friendly = true;
            SafeSetDefaults();
        }
        public virtual void SafeSetDefaults()
        {
        }
    }
}
