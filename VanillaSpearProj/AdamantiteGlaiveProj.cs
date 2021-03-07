using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class AdamantiteGlaiveProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.AdamantiteGlaive;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.penetrate = -1;
            projectile.timeLeft = 5 * 60;
            projectile.usesLocalNPCImmunity = true; projectile.localNPCHitCooldown = 7;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = 1;
                projectile.penetrate = 1;
                projectile.extraUpdates = 0;
                if (Main.rand.Next(8) == 0)
                {
                    if (S2JUtils.GoodNetMode)
                    {
                        float amt = 0.05f;
                        Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(amt) / 2,
                            projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 2);
                        Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(-amt) / 2,
                            projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 2);
                    }
                }
            }
            if (projectile.ai[0] == 2)
            {
                SpawnDust(projectile.Center);
            }
            else
            {
                ref Vector2 velocity = ref projectile.velocity;
                NPC target = null;
                float MinDist = 320f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy() && npc.active && npc.Distance(projectile.Center) < MinDist && !npc.friendly)
                    {
                        target = npc;
                        MinDist = npc.Distance(projectile.Center);
                    }
                }
                if (target != null) velocity = Vector2.Lerp(velocity, projectile.DirectionTo(target.Center) * 32f, 0.1f);
            }
            projectile.SpearRotate();
        }
        private int GetDust() => Utils.SelectRandom(Main.rand, new int[] { DustID.Blood, DustID.t_Flesh, DustID.Platinum });
        public override void Kill(int timeLeft)
        {
            S2JUtils.AttackOverArena(projectile.Center, 80f, projectile.damage / 2f, projectile.owner);
            for (int i = 0; i < 40; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360 * i / 40f).ToRotationVector2();
                SpawnDust(projectile.Center + rotate * 80f, DustID.Silver);
                SpawnDust(projectile.Center + rotate * 40f, DustID.Silver);
            }
            float rand = Main.rand.Next(360);
            for (int i = 0; i < 6; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360 * i / 6f + rand).ToRotationVector2();
                Vector2 rotate2 = rotate.RotatedBy(MathHelper.PiOver2);
                for (int j = -10; j <= 10; j++)
                {
                    SpawnDust(projectile.Center + rotate * 40f + rotate2 * j * 8f, DustID.Blood);
                }
            }
        }
        private Dust SpawnDust(Vector2 pos, int DustID = -1)
        {
            Dust dust = Dust.NewDustPerfect(pos, DustID == -1 ? GetDust() : DustID);
            dust.velocity = Vector2.Zero; dust.scale = 1.3f; dust.noGravity = true;
            return dust;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(33, 11);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}