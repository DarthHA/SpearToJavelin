using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class TitaniumShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            projectile.velocity *= 1.04f;
            projectile.rotation += System.Math.Sign(projectile.velocity.X) * 0.2f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item29, projectile.Center);
            for (int i = 0; i < 18; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360 * i / 18).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PlatinumCoin);
                dust.noGravity = true;
                dust.position += rotate * 8;
                dust.velocity = rotate * 1.2f;
                dust.scale *= 1.2f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 28 * (int)projectile.ai[0], 22, 26);
            Vector2 origin = new Vector2(11, 13);
            Color color = Color.White;
            for (int i = 0; i < 2; i++)
            {
                color *= 0.5f; color.B = (byte)(color.B / 0.9f); color.G = (byte)(color.G / 0.7f);
                spriteBatch.Draw(texture, projectile.oldPos[i] + origin - Main.screenPosition, sourceRectangle, color, projectile.oldRot[i], origin,
                    projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, projectile.Center, sourceRectangle, Color.White, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}