using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class ObsidianSwordfishProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.ObsidianSwordfish;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 28;
            projectile.penetrate = -1;
            projectile.timeLeft = 2 * 60;
            projectile.usesLocalNPCImmunity = true; projectile.localNPCHitCooldown = 40;
        }
        public override void AI()
        {
            projectile.Fall(0.99f,0.1f);
            projectile.SpearRotate();
            if (projectile.ai[1] > 0) projectile.ai[1]--;
        }
        private void Explode(float mult = 1f)
        {
            Main.PlaySound(SoundID.Item14, projectile.Center);
            S2JUtils.AttackOverArena(projectile.Center, 64f, projectile.damage * mult, projectile.owner);
            for (int i = 0; i < 60; i++)
            {
                int dustid = Utils.NextBool(Main.rand) ? DustID.Fire : DustID.Smoke;
                dustid = Utils.NextBool(Main.rand) ? DustID.SolarFlare : dustid;
                Dust dust = Dust.NewDustPerfect(projectile.Center, dustid);
                dust.velocity = MathHelper.ToRadians(Main.rand.Next(360)).ToRotationVector2() * Main.rand.Next(30,60) / 10f;
                dust.scale *= 2f;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 0f)
            {
                Explode(0.5f);
                projectile.ai[1] = 4f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Explode();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(24, 34);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}