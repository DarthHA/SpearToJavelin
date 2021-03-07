using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SpearToJavelin
{
    public class S2JVanillaItem : GlobalItem
    {
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.IsVanillaSpear()) return true;
            return base.AltFunctionUse(item, player);
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (player.AltFunctionUse())
            {
                if (item.type == ItemID.Spear && player.ownedProjectileCounts[S2JProjiectileID.SpearProj] > 1) return 1.2f;
                if (item.type == ItemID.Swordfish) return 1.5f;
                if (item.type == ItemID.TheRottedFork) return 1.25f;
                if (item.type == ItemID.CobaltNaginata) return 1.6f;
                if (item.type == ItemID.PalladiumPike) return 1.4f;
                if (item.type == ItemID.OrichalcumHalberd) return 2f;
                if (item.type == ItemID.AdamantiteGlaive) return 1.4f;
                if (item.type == ItemID.TitaniumTrident) return 1.4f;
                if (item.type == ItemID.ObsidianSwordfish) return 1.555f;
                if (item.type == ItemID.Gungnir) return 1.4f;
                if (item.type == ItemID.MushroomSpear) return 1.2f;
            }
            return 1f;
        }
        public override void UseStyle(Item item, Player player)
        {
            if (item.IsVanillaSpear())
            {
                if (player.AltFunctionUse()) item.useStyle = ItemUseStyleID.SwingThrow;
                else item.useStyle = ItemUseStyleID.HoldingOut;
            }
        }
        public override bool CanUseItem(Item item, Player player)
        {
            return true;
        }
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (item.IsVanillaSpear())
            {
                if (player.AltFunctionUse())
                {
                    Vector2 velocity = new Vector2(speedX, speedY);
                    velocity.Normalize();
                    VanillaAltShoot(player, item, position, velocity, damage, knockBack);
                    return false;
                }
            }
            return true;
        }
        public static void VanillaAltShoot(Player player, Item item, Vector2 position, Vector2 velocity, int damage, float knockback)
        {
            #region PreH
            if (item.type == ItemID.Spear)
            {
                Projectile.NewProjectile(position, velocity * 12f, S2JProjiectileID.SpearProj, damage, knockback / 1.3f, player.whoAmI);
                return;
            }
            if (item.type == ItemID.Swordfish)
            {
                Projectile.NewProjectile(position, velocity * 14f, S2JProjiectileID.SwordfishProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.TheRottedFork)
            {
                Projectile.NewProjectile(position, velocity * 16f, S2JProjiectileID.TheRottedForkProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.Trident)
            {
                Projectile.NewProjectile(position, velocity * 0.1f, S2JProjiectileID.TridentProj, (int)(damage * 2.5f), knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.DarkLance)
            {
                Projectile.NewProjectile(position, velocity * 6f, S2JProjiectileID.DarkLanceProj, damage, knockback, player.whoAmI);
                return;
            }
            #endregion
            #region 6Ores
            if (item.type == ItemID.CobaltNaginata)
            {
                Projectile.NewProjectile(position, velocity * 30f, S2JProjiectileID.CobaltNaginataProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.PalladiumPike)
            {
                Projectile.NewProjectile(position, velocity * 24f, S2JProjiectileID.PalladiumPikeProj, damage * 2, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.MythrilHalberd)
            {
                Projectile.NewProjectile(position, velocity * 12f, S2JProjiectileID.MythrilHalberdProj, (int)(damage * 2f), knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.OrichalcumHalberd)
            {
                velocity = velocity.RotatedByRandom(0.2f);
                Projectile.NewProjectile(position, velocity * 12f, S2JProjiectileID.OrichalcumHalberdProj, (int)(damage / 1.3f), knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.AdamantiteGlaive)
            {
                Projectile.NewProjectile(position, velocity * 24f, S2JProjiectileID.AdamantiteGlaiveProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.TitaniumTrident)
            {
                Projectile.NewProjectile(position, velocity * 1f, S2JProjiectileID.TitaniumTridentProj, damage, knockback, player.whoAmI);
                return;
            }
            #endregion
            if (item.type == ItemID.ObsidianSwordfish)
            {
                Projectile.NewProjectile(position, velocity * 33f, S2JProjiectileID.ObsidianSwordfishProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.Gungnir)
            {
                Projectile.NewProjectile(position, velocity * 8f, S2JProjiectileID.GungnirProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.MushroomSpear)
            {
                Projectile.NewProjectile(position, velocity * 16f, S2JProjiectileID.MushroomSpearProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.ChlorophytePartisan)
            {
                Projectile.NewProjectile(position, velocity * 32f, S2JProjiectileID.ChlorophytePartisanProj, damage, knockback, player.whoAmI);
                return;
            }
            if (item.type == ItemID.NorthPole)
            {
                Projectile.NewProjectile(position, velocity * 9f, S2JProjiectileID.NorthPoleProj, damage, knockback, player.whoAmI);
                return;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.IsVanillaSpear())
            {
                ModTranslation translation = null;
                switch (item.type)
                {
                    case ItemID.Spear: translation = S2JTooltip.Spear; break;
                    case ItemID.Trident: translation = S2JTooltip.Trident; break;
                    case ItemID.Swordfish: translation = S2JTooltip.Swordfish; break;
                    case ItemID.TheRottedFork: translation = S2JTooltip.TheRottedFork; break;
                    case ItemID.DarkLance: translation = S2JTooltip.DarkLance; break;
                    case ItemID.CobaltNaginata: translation = S2JTooltip.Cobalt; break;
                    case ItemID.PalladiumPike: translation = S2JTooltip.Palladium; break;
                    case ItemID.MythrilHalberd: translation = S2JTooltip.Mythril; break;
                    case ItemID.OrichalcumHalberd: translation = S2JTooltip.Orichalcum; break;
                    case ItemID.AdamantiteGlaive: translation = S2JTooltip.Adamantite; break;
                    case ItemID.TitaniumTrident: translation = S2JTooltip.Titanium; break;
                    case ItemID.ObsidianSwordfish: translation = S2JTooltip.ObsidianSwordfish; break;
                    case ItemID.Gungnir: translation = S2JTooltip.Gungnir; break;
                    case ItemID.MushroomSpear: translation = S2JTooltip.MushroomSpear; break;
                    case ItemID.ChlorophytePartisan: translation = S2JTooltip.Chloropyte; break;
                    case ItemID.NorthPole: translation = S2JTooltip.NorthPole; break;
                }
                if (translation != null)
                    foreach (TooltipLine line in tooltips)
                    {
                        if (line.mod == "Terraria" && line.Name == "Knockback")
                        {
                            TooltipLine tooltipLine = line;
                            tooltipLine.text += translation.GetTranslation(Terraria.Localization.Language.ActiveCulture);
                        }
                    }
            }
        }
    }
}