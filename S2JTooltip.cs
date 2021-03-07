using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpearToJavelin
{
    public static class S2JTooltip
    {
        public static void Load()
        {
            Spear = AddMT("Spear", 
                "Right click to throw a gravity-affected, infinite-piercing, slowly-flying Spear", 
                "右键掷出受重力影响的无限穿透的缓慢飞行的矛");
            Trident = AddMT("Trident",
                "Right click to throw a accelerating trident, which explodes in a REALLY small range",
                "右键掷出一个直线飞行的逐渐加速的三叉戟，并炸开很小的范围");
            Swordfish = AddMT("Swordfish",
                "'These Swordfishes are innocent!'\nRight click to throw out a swordfish",
                "‘这些剑鱼是无辜的！’\n右键掷出剑鱼");
            TheRottedFork = AddMT("TheRottedFork",
                "Right click to throw a non-gravity Rotted Fork, piercing infinitely and point at nearby enermies on hitting a tile",
                "右键掷出腐叉，无限穿透并在击中方块后指向敌人");
            DarkLance = AddMT("DarkLance",
                "Right click to throw a lance which spawns another lance with it\nThe first lance bounces overe tile and pierces infinitely\n" +
                "The second lance passes through blocks while piercing only 5 times or chase enermies while cannot pierce",
                "右键掷出黑暗长矛，它生成另一个长矛伴随它\n第一个长矛在物块上反弹，无限穿透\n" +
                "第二个长矛穿墙并5穿透，或者追踪但不穿透");
            Cobalt = AddMT("Cobalt",
                "Right click to throw a \"Common\" spear which marks enermies\nMarked enermy explodes on hit by other projectiles",
                "右键掷出“普通”的长矛，标记击中的敌人\n被标记的敌人若被其他弹幕击中，则会爆炸");
            Palladium = AddMT("Palladium",
                "Right click to throw a chasing spear, which inflicts Ichor debuff and have a slight chance to spawn a heart upon hitting an enermy\n" +
                "Also, using the spear hitting enermy grants you \"Palladium\" buff, which causes you to absorb a really large range of hearts",
                "右键掷出追踪的长矛，造成灵液减益并有微小的几率生成一个红心\n" +
                "同时，用它击中敌人会给你“钯金拾心”增益，让你捡起非常大范围内的红心");
            Mythril = AddMT("Mythril",
                "Right click to slowly throw a high-damage spear",
                "右键缓慢掷出高伤害长矛");
            Orichalcum = AddMT("Orichalcum",
                "Right click to rapidly throw a low-damage spear",
                "右键快速掷出低伤害长矛");
            Adamantite = AddMT("Adamantite",
                "Right click to throw a chasing glaive which explodes into a small area\n1/8 Chance to throw 2 extra glaives which pierce infinitely instead of chasing",
                "右键掷出追踪的关刀，炸开成一片小区域\n有1/8几率额外掷出不追踪但是无限穿透的2个长矛");
            Titanium = AddMT("Titanium",
                "Right click to throw a accelerating trident which spawns accelerating shards on flying",
                "右键掷出加速的三叉戟，三叉戟飞行过程中会生成一起加速的碎片");
            ObsidianSwordfish = AddMT("ObsidianSwordfish",
                "'These Swordfishes are too strong!'\nRight click to rapidly throw Obsidian Swordfishes which explodes on enermy hit",
                "‘这些剑鱼过分强了！’\n右键快速掷出击中敌人爆炸的黑曜石剑鱼");
            Gungnir = AddMT("Gungnir",
                "'The most splendid weapon I ever created'\nRight click to throw a true Gungnir",
                "‘我创造过的最帅的武器’\n右键掷出真正的冈格尼尔");
            MushroomSpear = AddMT("MushroomSpear",
                "'Phantasmal Ruin'\nRight click to throw a shroom spear\nThe spear leaves chasing shroom spores on its trail\n" +
                "Hitting a tile or a target causes it explodes into 7 chasing Glowing Mushroom",
                "‘幻魂归墟’\n右键掷出蘑菇矛\n矛飞行时在轨迹上留下孢子\n" +
                "撞到物块或者目标时，矛爆炸成7个追踪的发光蘑菇");
            Chloropyte = AddMT("Chloropyte",
                "'Ability to overwelm the frigid moon'\nRight click to throw a partisan, which spawn 10 spore clouds on death\n" +
                "If there are enermies nearby and the directly-hitted-target isn't strong enough, some of the spore clouds will spawn on them",
                "‘**霜月我来*你*了！’\n右键掷出镋，它消失时生成10朵孢子云\n" +
                "如果消失时周围有敌人，并且被直接击中的敌人（如果有的话）不够强大，孢子云中的一部分会在周围的敌人处生成");
            NorthPole = AddMT("NorthPole",
                "'Winter is coming'\nRight click to summon a rising North Pole, and a large snowfall arrives around it\n" +
                "After 5 seconds, the North Pole fades into 3 shades,chasing enermies and explode on them",
                "‘凛冬将至’\n右键召唤一把北极，一场暴雪将会在其周围汇聚\n" +
                "5秒后，北极淡开成三个残影，追踪敌人并在它们身上炸裂");

        }
        public static void Unload()
        {
            Spear =  Trident = Swordfish = TheRottedFork = null;
            DarkLance =ObsidianSwordfish = null;
            Cobalt = Palladium = Mythril = Orichalcum = Adamantite = Titanium = null;
            Gungnir = MushroomSpear = null;
            Chloropyte = NorthPole = null;
            
        }
        public static ModTranslation AddMT(string key, string def, string chineseTrans)
        {
            ModTranslation translation = SpearToJavelin.instance.CreateTranslation(key);
            translation.SetDefault("\n" + def);
            translation.AddTranslation(GameCulture.Chinese, "\n" + chineseTrans);
            return translation;
        }
        public static ModTranslation Spear;
        public static ModTranslation Trident;
        public static ModTranslation Swordfish;
        public static ModTranslation TheRottedFork;
        public static ModTranslation DarkLance;
        public static ModTranslation Cobalt;
        public static ModTranslation Palladium;
        public static ModTranslation Mythril;
        public static ModTranslation Orichalcum;
        public static ModTranslation Adamantite;
        public static ModTranslation Titanium;
        public static ModTranslation ObsidianSwordfish;
        public static ModTranslation Gungnir;
        public static ModTranslation MushroomSpear;
        public static ModTranslation Chloropyte;
        public static ModTranslation NorthPole;
    }
}