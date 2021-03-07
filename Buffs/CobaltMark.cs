using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpearToJavelin.Buffs
{
    public class CobaltMark : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cobalt Mark");
            DisplayName.AddTranslation(GameCulture.Chinese, "钴蓝标记");
            Description.SetDefault("Explode on hit");
            Description.AddTranslation(GameCulture.Chinese, "被击中时爆炸");
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffDoubleApply[Type] = false;
            Main.debuff[Type] = true;
        }
    }
}
