using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin.VanillaSpearProj
{
    public class OrichalcumHalberdProj : MythrilHalberdProj
    {
        public override int Weapon => ItemID.OrichalcumHalberd;
        public override int GetDust() => Utils.SelectRandom(Main.rand, new int[] { DustID.PinkCrystalShard, DustID.PinkFlame,DustID.Silver });
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 origin = new Vector2(35, 11);
            spriteBatch.SpearDraw(lightColor, projectile, origin);
            return false;
        }
    }
}