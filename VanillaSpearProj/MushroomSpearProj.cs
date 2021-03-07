using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class MushroomSpearProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.MushroomSpear;
        public override void SafeSSD()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.penetrate = 1;
            projectile.timeLeft = 10 * 60;
        }
        public override void AI()
        {
            ref float ai0 = ref projectile.ai[0];
            ai0++;
            if (ai0 >= 10)
            {
                ai0 = 0;
                float radius = 5f;
                if (S2JUtils.GoodNetMode) Projectile.NewProjectile(projectile.Center, Vector2.Zero,
                      ModContent.ProjectileType<ShroomSpore>(), (int)(projectile.damage / radius), projectile.knockBack / radius, projectile.owner);
            }
            if (projectile.velocity.Length() < 24f) projectile.velocity *= 1.03f;
            projectile.SpearRotate();
            Dust dust = Dust.NewDustPerfect(projectile.Center,  88);
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale *= 1.4f;
        }
        public override void Kill(int timeLeft)
        {
            S2JUtils.AttackOverArena(projectile.Center, 48f, projectile.damage / 2f, projectile.owner);
            Main.PlaySound(SoundID.NPCDeath39, projectile.Center);
            float rand = Main.rand.Next(360);
            for (int j = 0; j < 36; j++)
            {
                Vector2 rotate = MathHelper.ToRadians(10f * j + rand).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.BlueCrystalShard);
                dust.noGravity = true;
                dust.velocity = rotate * 8f;
                dust.scale *= 3f;
            }
            int i = 0;
            while (i < 7)
            {
                if (S2JUtils.GoodNetMode) Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedByRandom(MathHelper.TwoPi),
                      ModContent.ProjectileType<Shroom>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                i++;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(37, 15);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Color color = Color.White;
            for (int i = 0; i < 4; i++)
            {
                color *= 0.7f; color.B = (byte)(color.B / 0.7f);
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(11, 11) - Main.screenPosition, sourceRectangle, color, projectile.rotation, origin,
                    projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.SpearDraw(Color.White, projectile, origin);
            return false;
        }
    }
}