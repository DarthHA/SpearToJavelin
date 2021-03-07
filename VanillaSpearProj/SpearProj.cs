using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class SpearProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.Spear;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 18;
            projectile.penetrate = -1;
            projectile.timeLeft = 5 * 60;
            projectile.usesLocalNPCImmunity = true; projectile.localNPCHitCooldown = 21;
        }
        public override void AI()
        {
            projectile.Fall();
            projectile.SpearRotate();
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            for (int i = 0; i < 18; i++)
            {
                int dustid = Utils.NextBool(Main.rand) ? DustID.Iron : DustID.Tin;
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, dustid);
                dust.velocity = Vector2.Lerp(dust.velocity, projectile.velocity * Main.rand.Next(20, 101) / 100f, 0.75f);
                dust.scale *= 2f;
                dust.noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(29, 9);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}