using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class GungnirProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.Gungnir;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.penetrate = 1;
            projectile.timeLeft = 10 * 60;
            projectile.tileCollide = false;
            projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            ref float ai0 = ref projectile.ai[0];
            ai0++;
            if (ai0 < 120)
            {
                projectile.velocity *= 0.98f;
            }
            else
            {
                if (ai0 == 120)
                {
                    Main.PlaySound(SoundID.Item35, projectile.Center);
                    projectile.extraUpdates = projectile.timeLeft;
                    float rand = Main.rand.Next(360);
                    for (int i = 0; i < 36; i++)
                    {
                        Vector2 rotate = MathHelper.ToRadians(10f * i + rand).ToRotationVector2();
                        Dust dust = Dust.NewDustPerfect(projectile.Center , DustID.GoldFlame);
                        dust.noGravity = true;
                        dust.velocity = rotate * 3f;
                        dust.scale *= 2f;
                    }
                }
                projectile.Chase(4000f, 6f, 0.01f);
            }
            SpawnDust(projectile.Center);
            projectile.SpearRotate();
        }
        private void SpawnDust(Vector2 pos, float scaleMult = 1.5f)
        {
            Dust dust = Dust.NewDustPerfect(pos, GetDust());
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale *= scaleMult;
        }
        private int GetDust() => Utils.SelectRandom(Main.rand, new int[] { DustID.AmberBolt, DustID.GoldCoin, DustID.GoldFlame, DustID.SilverCoin });
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath56, projectile.Center);
            S2JUtils.AttackOverArena(projectile.Center, 80f, projectile.damage, projectile.owner);
            Vector2 dir = projectile.velocity.NormalizeV();
            for (int i = -5; i < 20; i++)
            {
                SpawnDust(projectile.Center + i * dir * 3f, 3f);
            }
            float rand = Main.rand.Next(360);
            for (int i = 0; i < 36; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(10f * i + rand).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center + rotate * 8f, DustID.GoldFlame);
                dust.noGravity = true;
                dust.velocity = rotate * 8f;
                dust.scale *= 3f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(37, 17);
            spriteBatch.SpearDraw(Color.White, projectile, origin);
            return false;
        }
    }
}