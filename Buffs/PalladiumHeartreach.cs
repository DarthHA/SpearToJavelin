using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpearToJavelin.Buffs
{
   public class PalladiumHeartreach:ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Palladium Heartreach");
            DisplayName.AddTranslation(GameCulture.Chinese, "钯金拾心");
            Description.SetDefault("Absorb a REALLY large area of hearts");
            Description.AddTranslation(GameCulture.Chinese, "吸收非常大范围内的红心");
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffDoubleApply[Type]= false;
            Main.debuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            for(int i = 0; i < Main.item.Length; i++)
            {
                Item heart = Main.item[i];
                if (heart.active && heart.type == ItemID.Heart && !heart.isBeingGrabbed)
                {
                    heart.velocity = heart.DirectionTo(player.Center) * 45f;
                }
            }
        }
    }
}
