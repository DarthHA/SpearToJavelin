using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class MythrilHalberdProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.MythrilHalberd;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 18;
            projectile.penetrate = -1;
            projectile.timeLeft = 5 * 60;
            projectile.usesLocalNPCImmunity = true; projectile.localNPCHitCooldown = 7;
            projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            projectile.Fall(0.997f, 0.03f);
            projectile.SpearRotate();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SpawnDust2();
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            SpawnDust();
        }
        private void SpawnDust()
        {
            Vector2 dir = projectile.velocity;
            if (dir.Length() == 0) return;
            dir.Normalize();
            for (int i = 0; i < 20; i++)
            {
                Vector2 pos = projectile.Center + dir * (-32f + i * 3f);
                Dust dust = Dust.NewDustPerfect(pos, GetDust());
                dust.velocity = projectile.velocity / projectile.velocity.Length() * 4f;
                dust.noGravity = true;
                dust.scale *= 2f;
            }
        }
        private void SpawnDust2()
        {
            int rand = Main.rand.Next(360);
            for (int i = 0; i < 12; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360f * i / 12f + rand).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, GetDust());
                dust.noGravity = true;
                dust.velocity = rotate * 2f;
                dust.scale = 1.2f;
            }
        }
        public virtual int GetDust() => Utils.SelectRandom(Main.rand, new int[] { DustID.GreenBlood, DustID.Grass, DustID.t_Granite });
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            SpawnDust2();
            projectile.BounceOverTile(oldVelocity);
            ref Vector2 velocity = ref projectile.velocity;
            if (velocity.Y < 0) velocity.Y *= 0.8f;
            //for (int i = 0; i < Main.npc.Length; i++)
            //{
            //    NPC npc = Main.npc[i];
            //    if (npc.CanBeChasedBy() && npc.active && npc.Distance(projectile.Center) < 1600f && !npc.friendly)
            //    {
            //        velocity = Vector2.Lerp(velocity, projectile.DirectionTo(npc.Center) * 12f, 0.5f);
            //    }
            //}
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(33, 15);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}