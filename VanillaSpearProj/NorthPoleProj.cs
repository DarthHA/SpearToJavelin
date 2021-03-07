using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class NorthPoleProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.NorthPole;
        public override void SafeSSD()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 26;
            projectile.timeLeft = 5 * 60;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            ref Vector2 velocity = ref projectile.velocity;
            ref float ai0 = ref projectile.ai[0];
            ref float ai1 = ref projectile.ai[1];
            ref int ai2 = ref projectile.frame;
            ai1 += 3;
            ai2 += (int)(ai1 / 6f);
            Vector2 rotation = MathHelper.ToRadians(ai2).ToRotationVector2();
            float speed = (float)System.Math.Pow(ai1, 0.5f) / 2f;
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PlatinumCoin);
                dust.velocity = rotation.RotatedBy(i * MathHelper.TwoPi / 3f) * speed; dust.noGravity = true; dust.scale *= 3f;
            }
            if (ai0 == 0)
            {
                ai0++;
                projectile.velocity = -Vector2.UnitY * 20f;
            }
            velocity *= 0.9f;
            if (S2JUtils.GoodNetMode && ai1 % 2 == 0)
            {
                Vector2 pos = new Vector2(Main.rand.Next(-800, 801), Main.rand.Next(-720, -640));
                float radius = 3f;
                Projectile flake = Projectile.NewProjectileDirect(projectile.Center + pos, Vector2.UnitY * 9f, ProjectileID.NorthPoleSnowflake,
                         (int)(projectile.damage / radius), projectile.knockBack / radius, projectile.owner);
                flake.penetrate = 1; flake.tileCollide = false; flake.extraUpdates = 1;
                flake.timeLeft = 120 * (1 + projectile.extraUpdates); flake.ai[1] = Main.rand.Next(3); 
            }
            projectile.SpearRotate();
        }
        public override bool CanDamage() => false;
        public override void Kill(int timeLeft)
        {
            for(int i = -1; i <= 1; i++)
            {
                Projectile.NewProjectile(projectile.Center, i * Vector2.UnitX * 16f + Vector2.UnitY * 2f, ModContent.ProjectileType<NorthPoleExplosion>(),
                    projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(31, 21);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            for (int i = 0; i < 4; i++)
            {
                Color color = new Color(106, 210, 255, 100);
                Vector2 rand = new Vector2(Main.rand.Next(-5, 6),0);if (Utils.NextBool(Main.rand, 2)) rand.RotatedBy(MathHelper.PiOver2);
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(13, 13) - Main.screenPosition + rand, sourceRectangle, color,
                    projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}