using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class DarkLanceProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.DarkLance;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.penetrate = -1;
            projectile.timeLeft = 20 * 60;
            projectile.extraUpdates = 5;
            projectile.SetupImmuFrame(40);
        }
        public override void AI()
        {
            ref Vector2 velocity = ref projectile.velocity;
            projectile.Fall(1f, 0.005f);
            projectile.SpearRotate();
            Dust dust = Dust.NewDustPerfect(projectile.Center, GetDust());
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale *= 1.3f;
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = 1;
                if (S2JUtils.GoodNetMode)
                    Projectile.NewProjectile(projectile.Center, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner,
                        Main.rand.Next(1, 3) * 2);
            }
            else if (projectile.ai[0] == 2)
            {
                projectile.ai[0] = 3;
                projectile.extraUpdates = 3;
                projectile.tileCollide = false;
                projectile.penetrate = 5;
                projectile.timeLeft = 4 * 60;
            }
            else if (projectile.ai[0] == 4)
            {
                projectile.ai[0] = 5;
                projectile.extraUpdates = 2;
                projectile.tileCollide = false;
                projectile.penetrate = 1;
                projectile.timeLeft = 6 * 60;
            }
            else if (projectile.ai[0] == 5)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && npc.CanBeChasedBy() && npc.Distance(projectile.Center) < 480f)
                    {
                        velocity = Vector2.Lerp(velocity, projectile.DirectionTo(npc.Center) * 16f, 3f / projectile.Distance(npc.Center));
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            Vector2 dir = projectile.velocity;
            if (dir.Length() == 0) return;
            dir.Normalize();
            for (int i = 0; i < 21; i++)
            {
                Vector2 pos = projectile.Center + dir * (-64f + i * 4f);
                Dust dust = Dust.NewDustPerfect(pos, GetDust());
                dust.velocity = Vector2.Zero;
                dust.scale = ((i - System.Math.Abs(i - 10.5f)) / 10.5f + 1.2f) * 1.1f;
                dust.noGravity = true;
            }
        }
        private int GetDust()
        {
            return Utils.SelectRandom(Main.rand, new int[] { DustID.Shadowflame, DustID.PurpleCrystalShard, });
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(41, 11);
            Color color = Color.White;
            if (projectile.ai[0] > 1) color = new Color(255, 0, 155);
            spriteBatch.SpearDraw(color, projectile, origin);
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            ref Vector2 velocity = ref projectile.velocity;
            if (projectile.ai[1] >= 5) return true;
            else
            {
                projectile.ai[1]++;
                projectile.BounceOverTile(oldVelocity);
                if (projectile.velocity.Y < 0) projectile.velocity.Y *= 0.8f;
            }
            return false;
        }
    }
}