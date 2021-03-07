using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class NorthPoleExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.melee = true; projectile.friendly = true;
            projectile.width = projectile.height = 36;
            projectile.penetrate = 1;
            projectile.timeLeft = 20 * 60;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            ref Vector2 velocity = ref projectile.velocity;
            ref float ai0 = ref projectile.ai[0];
            if (ai0 == 0)
            {
                ref float ai1 = ref projectile.ai[1];
                ai1++;
                if (ai1 < 10)
                {
                    velocity *= 0.9f;
                    projectile.rotation = -MathHelper.PiOver4;
                }
                else
                {
                    if (ai1 < 12) velocity.Y -= 10f;
                    else
                    {
                        if (!projectile.Chase(800f, 40f, 0.04f)) projectile.Fall(1f, 0.2f);
                    }
                    projectile.SpearRotate();
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.AncientLight);
                    dust.noGravity = true; dust.scale *= 2f; dust.velocity = Vector2.Zero; 
                }
            }
            else
            {
                float radius = projectile.timeLeft / 30f;
                projectile.scale = MathHelper.Lerp(2f, 1f, radius);
                projectile.alpha = (int)MathHelper.Lerp(200, 0, radius);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ref float ai0 = ref projectile.ai[0];
            if (ai0 == 0)
            {
                projectile.penetrate = -1;
                projectile.timeLeft = 30;
                projectile.friendly = false;
                projectile.velocity = Vector2.Zero;
                ai0++;
            }
        }
        public override void Kill(int timeLeft)
        {
            S2JUtils.AttackOverArena(projectile.Center, 160f, projectile.damage, projectile.owner);
            for (int i = 0; i < 36; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(i * 10f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.AncientLight);
                dust.noGravity = true; dust.scale *= 2f; dust.velocity = rotate * 8f; dust.noLight = false;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(18, 18);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Color color = Color.White;
            for (int i = 0; i < 4; i++)
            {
                color *= 0.7f; color.G = (byte)(color.G / 0.75f); color.G = (byte)(color.G / 0.7f);
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(13, 13) - Main.screenPosition, sourceRectangle, projectile.GetAlpha(color),
                    projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}