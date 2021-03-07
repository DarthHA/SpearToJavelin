using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class TitaniumTridentProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.TitaniumTrident;
        public override void SafeSSD()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 24;
            projectile.penetrate = 1;
            projectile.timeLeft = 5 * 60;
            projectile.usesLocalNPCImmunity = true; projectile.localNPCHitCooldown = 7;
            projectile.tileCollide = false;
            projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            projectile.velocity *= 1.01f;
            projectile.SpearRotate();
            ref float ai1 = ref projectile.ai[1];
            ai1++;
            if (ai1 >= 45)
            {
                ai1 = 0;
                if (S2JUtils.GoodNetMode) Projectile.NewProjectile(projectile.Center, projectile.velocity, S2JProjiectileID.TitaniumShard,
                    projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(5));
            }
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PlatinumCoin);
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(38, 12);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Color color = Color.White;
            for (int i = 0; i < 4; i++)
            {
                color *= 0.7f; color.B = (byte)(color.B / 0.9f); color.G = (byte)(color.G / 0.7f);
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(12, 12) - Main.screenPosition, sourceRectangle, color, projectile.rotation, origin,
                    projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.SpearDraw(Color.White, projectile, origin);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            Vector2 dir = projectile.velocity;
            if (dir.Length() == 0) return;
            dir.Normalize();
            for (int i = 0; i < 20; i++)
            {
                Vector2 pos = projectile.Center + dir * (-32f + i * 3f);
                Dust dust = Dust.NewDustPerfect(pos, DustID.AncientLight);
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
                dust.scale *= 2F;
            }
        }
    }
}