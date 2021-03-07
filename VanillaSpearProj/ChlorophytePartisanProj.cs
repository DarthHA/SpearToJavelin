using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class ChlorophytePartisanProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.ChlorophytePartisan;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 28;
            projectile.penetrate = 1;
            projectile.timeLeft = 5 * 60;
        }
        public override void AI()
        {
            projectile.Fall(1f);
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PinkCrystalShard);
            dust.noGravity = true; dust.scale *= 1.2f; dust.velocity = Vector2.Zero;dust.color = Color.DarkGreen;
            projectile.SpearRotate();
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[0] == 0) projectile.ai[0] = 1;
            if (S2JUtils.GoodNetMode)
            {
                if (target.life >= 2000)
                {
                    for (int i = 0; i < 10; i++) SpawnCloud(target.Center);
                }
                else
                {
                    SpawnClouds(target.whoAmI);
                }
            }
        }
        private void SpawnClouds(int whoami = -1)
        {
            int j = 10;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.dontTakeDamage && !npc.friendly && npc.Distance(projectile.Center) < 800f)
                {
                    SpawnCloud(npc.Center);
                    j--;
                    if (j <= 0) break;
                }
            }
            if (j > 0)
            {
                for (int i = 0; i < j; i++)
                {
                    if (whoami == -1) SpawnCloud(projectile.Center);
                    else SpawnCloud(Main.npc[whoami].Center);
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            SpawnDusts();
            if (projectile.ai[0] == 0 && S2JUtils.GoodNetMode)
            {
                SpawnClouds();
            }
        }
        private void SpawnDusts()
        {
            float veloRot = projectile.velocity.ToRotation();
            Vector2 dir = veloRot.ToRotationVector2();
            Vector2 dir1 = dir.RotatedBy(MathHelper.PiOver2);
            for (int i = 0; i < 9; i++)
            {
                SpawnDust(i * dir);
            }
            for (int i = 0; i < 9; i++)
            {
                SpawnDust((i - 4) * dir1);
            }
            for (int i = 0; i < 9; i++)
            {
                SpawnDust((i - 4) * dir1 + System.Math.Abs(i - 4) * dir);
            }
            for (int i = 0; i < 9; i++)
            {
                Vector2 rotate = (MathHelper.ToRadians(360f * i / 18f) + veloRot).ToRotationVector2();
                SpawnDust((rotate + dir - dir1) * 4f);
            }
            for (int i = 0; i < 9; i++)
            {
                Vector2 rotate = (MathHelper.ToRadians(360f * i / 18f) + veloRot + MathHelper.Pi).ToRotationVector2();
                SpawnDust((rotate + dir + dir1) * 4f);
            }
        }
        private void SpawnDust(Vector2 velo)
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.BlueCrystalShard);
            dust.noGravity = true;
            dust.velocity = velo;
            dust.noLight = false;
            dust.scale = 1.5f + velo.Length() / 12f;dust.color = Color.DarkGreen;
        }
        private void SpawnCloud(Vector2 pos)
        {
            Vector2 rotate = MathHelper.ToRadians(Main.rand.Next(360)).ToRotationVector2();
            Projectile cloud = Projectile.NewProjectileDirect(pos, -rotate * 8f,
                ProjectileID.SporeCloud, projectile.damage / 2, projectile.knockBack / 2f, projectile.owner);
            cloud.usesLocalNPCImmunity = true; cloud.localNPCHitCooldown = 24;
            for (int i = 0; i < 12; i++)
            {
                Vector2 rotate1 = MathHelper.ToRadians(i * 30f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(pos + rotate1 * 3f, DustID.GrassBlades);
                dust.noGravity = true; dust.scale *= 2f; dust.velocity = rotate1 * 4f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(39, 16);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}