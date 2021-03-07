using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpearToJavelin
{
    public static class S2JUtils
    {
        public static string OmniTexture => "SpearToJavelin/OmniTexture";
        public static bool IsVanillaSpear(this Item item) => S2JList.VanillaSpearItemList.Contains(item.type);
        public static bool AltFunctionUse(this Player player) => player.altFunctionUse == 2;
        public static bool GoodNetMode => Main.netMode != NetmodeID.MultiplayerClient;
        public static void Fall(this Projectile projectile, float XMult = 0.98f, float YAdd = 0.15f, float YMax = 20f)
        {
            projectile.velocity.X *= XMult;
            if (projectile.velocity.Y < YMax) projectile.velocity.Y += YAdd;
        }
        public static void SpearRotate(this Projectile projectile, float ExtraRotate = 0f)
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4 + ExtraRotate;
        }
        public static void SpearDraw(this SpriteBatch spriteBatch, Color lightColor, Projectile projectile, Vector2 origin)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, sourceRectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin,
                projectile.scale, SpriteEffects.None, 0f);
        }
        public static Vector2 NormalizeV(this Vector2 vector)
        {
            if (vector.Length() == 0) return Vector2.UnitY;
            vector.Normalize();
            return vector;
        }
        public static void SetupImmuFrame(this Projectile projectile, int Frames = 20)
        {
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = Frames;
        }
        public static void BounceOverTile(this Projectile projectile, Vector2 oldVelocity)
        {
            ref Vector2 velocity = ref projectile.velocity;
            if (velocity.X != oldVelocity.X) velocity.X = -oldVelocity.X;
            if (velocity.Y != oldVelocity.Y) velocity.Y = -oldVelocity.Y;
        }
        public static void AttackOverArena(Vector2 position, float Range, float damage, int owner = 0, float knockBack = 0f, int hitDir = 0, bool damageFloats = true)
        {
            if (owner==Main.myPlayer)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Hitbox.Distance(position) < Range)
                    {
                        if (S2JUtils.GoodNetMode)
                        {
                            float damage1 = damage;
                            if (damageFloats) damage1 *= Main.rand.Next(85, 116) / 100f;
                            damage1 = (float)npc.StrikeNPC((int)damage1, knockBack, hitDir);
                            Main.player[owner].addDPS((int)damage1);
                        }
                    }
                }
            }
        }
        public static bool Chase(this Projectile projectile,float range=16000f,float maxSpeed=32f,float lerpAmount = 0.05f)
        {
            ref Vector2 velocity = ref projectile.velocity;
            NPC target = null;
            float MinDist = range;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy() && npc.active && npc.Distance(projectile.Center) < MinDist && !npc.friendly)
                {
                    target = npc;
                    MinDist = npc.Distance(projectile.Center);
                }
            }
            if (target == null) return false;
            velocity = Vector2.Lerp(velocity, projectile.DirectionTo(target.Center) * maxSpeed, lerpAmount);
            return true;
        }
    }
}