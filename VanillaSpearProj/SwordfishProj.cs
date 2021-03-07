using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class SwordfishProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.Swordfish;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 30;
            projectile.penetrate = 1;
            projectile.timeLeft = 5 * 60;
        }
        public override void AI()
        {
            projectile.Fall(0.985f, 0.1f);
            projectile.SpearRotate();
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 88);
            dust.noGravity = true;
            ref float ai1 = ref projectile.ai[1];
            ai1++;
            if (ai1 >= 25)
            {
                ai1 = 0;
                if (S2JUtils.GoodNetMode) Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.FlaironBubble, 1, 0f, projectile.owner);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 dir = projectile.velocity;
            if (dir.Length() == 0) return;
            dir.Normalize();
            for (int i = 0; i < 36; i++)
            {
                Vector2 pos = projectile.Center + dir * (-48f + i * 3f);
                Dust dust = Dust.NewDustPerfect(pos, 88);
                dust.velocity = Vector2.Zero;
                dust.scale = ((i - System.Math.Abs(i - 17.5f)) / 17.5f + 0.25f) * 3f;
                dust.noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath19, projectile.Center);
            for (int i = 0; i < 18; i++)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Blood);
                dust.velocity = Vector2.Lerp(dust.velocity, projectile.velocity * Main.rand.Next(20, 101) / 100f, 0.75f);
                dust.scale *= 2f;
            }
            if (S2JUtils.GoodNetMode)
                for (int i = 0; i <= 3; i++)
                {
                    Projectile.NewProjectile(projectile.Center, 
                        Vector2.Lerp(new Vector2(Main.rand.Next(-100, 101) / 10f, Main.rand.Next(-100, 101) / 10f), projectile.velocity, 0.5f),
                        ModContent.ProjectileType<FishGore>(), 0, 0f, projectile.owner, i);
                }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(23, 31);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
    public class FishGore : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Why not Gore? My code too bad to do that. Sad.");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 16;
            projectile.timeLeft = 150;
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1]++;
                projectile.rotation = MathHelper.ToRadians(Main.rand.Next(360));
            }
            else
            {
                projectile.velocity.X *= 0.95f;
                projectile.rotation += projectile.velocity.X / 120f;
                projectile.velocity.Y += 0.15f;
                if (projectile.velocity.Y < 0) projectile.velocity.Y += 0.15f;
                projectile.alpha = (int)MathHelper.Lerp(255, 0, projectile.timeLeft / 150f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            ref Vector2 velocity = ref projectile.velocity;
            if (velocity.X != oldVelocity.X) velocity.X = -oldVelocity.X;
            if (velocity.Y != oldVelocity.Y) velocity.Y = -oldVelocity.Y * 0.8f;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 30 * (int)projectile.ai[0], 36, 28);
            Vector2 origin = new Vector2(18, 14);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, sourceRectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin,
                projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}