using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
  public  class TridentProj:VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.Trident;
        public override void SafeSSD()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 18;
            projectile.penetrate = 2;
            projectile.timeLeft = 70;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            projectile.SpearRotate();
            projectile.velocity *= 1.1f;
            Dust dust = Dust.NewDustPerfect(projectile.Center, 88);
            dust.noGravity = true; dust.velocity = Vector2.Zero; dust.scale *= 1.2f;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.life < damage * (crit ? 2 : 1))
            {
                damage = (int)(target.life * (crit ? 0.5f : 1));
                projectile.penetrate++;
                projectile.damage -= damage;
                if (projectile.damage <= 0) projectile.Kill();
            }
            else
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item84, projectile.Center);
            Main.PlaySound(SoundID.Item10, projectile.Center);
            for (int i = 0; i < 36; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360f * i / 36).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, 88);
                dust.noGravity = true;
                dust.velocity = rotate * 8f;
                dust.scale = 3f;
            }
            float rand = Main.rand.Next(360);
            int amt = Main.rand.Next(5, 10);
            for (int i = 0; i < amt; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360f * i / amt + rand).ToRotationVector2().RotatedByRandom(0.1f);
                Projectile soul = Projectile.NewProjectileDirect(projectile.Center, rotate * 0.68f, ProjectileID.LostSoulHostile, 0, 0f, projectile.owner);
                soul.hostile = false;
                soul.timeLeft = 45; soul.penetrate = 1; soul.tileCollide = false;
                soul.usesLocalNPCImmunity = true; soul.localNPCHitCooldown = 0;
                soul.scale = 0.5f; soul.Center = projectile.Center + rotate * 16f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(35, 9);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Color color = Color.White;
            for(int i = 0; i < 4; i++)
            {
                color *= 0.7f;color.B = (byte)(color.B / 0.7f);color.G = (byte)(color.G / 0.9f);
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(9, 9) - Main.screenPosition, sourceRectangle, color, projectile.rotation, origin,
                    projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.SpearDraw(Color.White, projectile, origin);
            return false;
        }
    }
}
