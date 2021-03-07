using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class ShroomSpore : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 14;
            projectile.melee = true; projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 5 * 60;
        }
        public override void AI()
        {
            ref float ai0 = ref projectile.ai[0];
            ai0++;
            if (ai0 < 30)
            {
                projectile.velocity = -Vector2.UnitY * 3f;
                projectile.rotation = 0;
            }
            else
            {
                projectile.Chase(1600f, 32f, 0.07f);
                projectile.SpearRotate(MathHelper.PiOver4);
            }
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 88);
            dust.noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit13, projectile.Center);
            float rand = Main.rand.Next(360);
            for (int i = 0; i < 36; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(10f * i + rand).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.BlueCrystalShard);
                dust.noGravity = true;
                dust.velocity = rotate * 3f;
                dust.scale *= 2f;
            }
        }
    }
}