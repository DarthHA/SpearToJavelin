using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpearToJavelin
{
    public static class S2JList
    {
        public static void Load()
        {
            VanillaSpearItemList = new List<int>
            {
                ItemID.Spear,
                ItemID.Trident,
                ItemID.TheRottedFork,
                ItemID.Swordfish,
                ItemID.DarkLance,
                ItemID.CobaltNaginata,ItemID.PalladiumPike,
                ItemID.MythrilHalberd,ItemID.OrichalcumHalberd,
                ItemID.AdamantiteGlaive,ItemID.TitaniumTrident,
                ItemID.ObsidianSwordfish,
                ItemID.Gungnir,
                ItemID.MushroomSpear,

                ItemID.ChlorophytePartisan,
                ItemID.NorthPole
            };
        }
        public static void Unload()
        {
            VanillaSpearItemList = null;
        }
        public static List<int> VanillaSpearItemList;
    }
}
