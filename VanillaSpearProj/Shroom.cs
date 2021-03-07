using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class Shroom : ModProjectile
    {
        public override string Texture => S2JUtils.OmniTexture;
        public override void SetStaticDefaults()
        {
            Main.projectileTexture[projectile.type] = Main.itemTexture[ItemID.GlowingMushroom];
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 22; projectile.height = 24;
            projectile.melee = true; projectile.friendly = false;
            projectile.penetrate = 1;projectile.tileCollide = false;
            projectile.timeLeft = 3 * 60;
        }
        public override void AI()
        {
            ref float ai0 = ref projectile.ai[0];
            ai0++;
            if (ai0 > 7)
            {
                projectile.friendly = true;
                projectile.Chase(480f, 40f, 0.04f);
            }
            projectile.SpearRotate(MathHelper.PiOver4);
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 88);
            dust.noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath39, projectile.Center);
            float rand = Main.rand.Next(360);
            for (int i = 0; i < 20; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(18f * i + rand).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.BlueCrystalShard);
                dust.noGravity = true;
                dust.velocity = rotate * 6f;
                dust.scale *= 2.5f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(11, 12);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Color color = Color.White;
            for (int i = 0; i < 4; i++)
            {
                color *= 0.7f; color.B = (byte)(color.B / 0.7f);
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(11, 12) - Main.screenPosition, sourceRectangle, color, projectile.oldRot[i], origin,
                    projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.SpearDraw(Color.White, projectile, origin);
            return false;
        }
    }
}
