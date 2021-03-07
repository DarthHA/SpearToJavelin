using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class TheRottedForkProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.TheRottedFork;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 28;
            projectile.penetrate = -1;
            projectile.timeLeft = 2 * 60;
            projectile.usesLocalNPCImmunity = true; projectile.localNPCHitCooldown = 7;
        }
        public override void AI()
        {
            if (projectile.velocity.Length() < 16f) projectile.velocity *= 16f / projectile.velocity.Length();
            projectile.SpearRotate();
            ref float ai1 = ref projectile.ai[1];
            ai1++;
            if (ai1 >= 20)
            {
                ai1 = 0;
                projectile.tileCollide = true;
            }
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Blood);
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 5 * 60);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(38, 14);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
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
                Dust dust = Dust.NewDustPerfect(pos, DustID.Blood);
                dust.velocity = projectile.velocity / projectile.velocity.Length() * 4f;
                dust.noGravity = false;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            ref Vector2 velocity = ref projectile.velocity;
            NPC target = null;
            float MinDist = 480f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy() && npc.active && npc.Distance(projectile.Center) < MinDist && !npc.friendly)
                {
                    target = npc;
                    MinDist = npc.Distance(projectile.Center);
                }
            }
            if (target == null)
            {
                if (velocity.X != oldVelocity.X) velocity.X = -oldVelocity.X;
                if (velocity.Y != oldVelocity.Y) velocity.Y = -oldVelocity.Y;
                velocity = velocity.RotatedByRandom(0.1f);
            }
            else
            {
                velocity = projectile.DirectionTo(target.Center) * velocity.Length();
            }
            projectile.tileCollide = false;
            Main.PlaySound(SoundID.Item10, projectile.Center);
            for (int i = 0; i < 18; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360f * i / 18f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Blood);
                dust.noGravity = true;
                dust.velocity = rotate * 3f;
                dust.scale = 2f;
            }
            return false;
        }
    }
}