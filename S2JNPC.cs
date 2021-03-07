using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin
{
    public class S2JNPC : GlobalNPC
    {
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            int i = npc.FindBuffIndex(ModContent.BuffType<Buffs.CobaltMark>());
            if (i != -1 && projectile.type != S2JProjiectileID.CobaltNaginataProj && projectile.type != ProjectileID.VortexBeaterRocket)
            {
                if (S2JUtils.GoodNetMode)
                {
                    float radius = 0.5f;
                    Projectile explosion = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.VortexBeaterRocket,
                        (int)(damage * radius), knockback * radius, projectile.owner);
                    explosion.ranged = false; explosion.melee = true;
                    explosion.Kill();
                }
                npc.buffTime[i] -= 360;
                if (npc.buffTime[i] <= 0)
                {
                    npc.buffTime[i] = 0;
                    npc.buffType[i] = 0;
                }
            }
        }
    }
}