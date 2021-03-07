using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class PalladiumPikeProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.PalladiumPike;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 20;
            projectile.penetrate = 1;
            projectile.timeLeft = 5 * 60;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            projectile.Fall();
            ref Vector2 velocity = ref projectile.velocity;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.CanBeChasedBy() && npc.Distance(projectile.Center) < 480f)
                {
                    velocity = Vector2.Lerp(velocity, projectile.DirectionTo(npc.Center) * 32f, 5f / projectile.Distance(npc.Center));
                }
            }
            projectile.SpearRotate();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 60 * 5);
            if (S2JUtils.GoodNetMode) if (Main.rand.Next(15) == 0) Item.NewItem(projectile.Hitbox, ItemID.Heart);
            Main.player[projectile.owner].AddBuff(ModContent.BuffType<Buffs.PalladiumHeartreach>(), 30);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            for (int i = 0; i < 18; i++)
            {
                Vector2 rotate = MathHelper.ToRadians(360f * i / 18f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center, Utils.SelectRandom(Main.rand, new int[] { DustID.GoldFlame, DustID.Fire }));
                dust.noGravity = true;
                dust.velocity = rotate * 6f;
                dust.scale = 2f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(38, 10);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}
