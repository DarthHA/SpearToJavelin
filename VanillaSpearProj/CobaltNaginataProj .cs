using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class CobaltNaginataProj : VanillaSpearProjUtils
    {
        public override int Weapon => ItemID.CobaltNaginata;
        public override void SafeSetDefaults()
        {
            projectile.width = projectile.height = 14;
            projectile.penetrate = -1;
            projectile.timeLeft = 5 * 60;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            projectile.Fall();
            projectile.SpearRotate();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.buffImmune[ModContent.BuffType<Buffs.CobaltMark>()] = false;
            target.AddBuff(ModContent.BuffType<Buffs.CobaltMark>(), 60 * 20);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            if (S2JUtils.GoodNetMode)
            {
                Projectile lightning = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ProjectileID.Electrosphere, 0, 0f, projectile.owner);
                lightning.friendly = false;
                lightning.timeLeft = 30;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(35, 9);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}
