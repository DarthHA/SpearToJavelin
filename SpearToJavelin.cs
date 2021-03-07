using Terraria.ModLoader;

namespace SpearToJavelin
{
	public class SpearToJavelin : Mod
	{
        public override void Load()
        {
            instance = this;
            S2JList.Load();
            S2JTooltip.Load();
        }
        public override void Unload()
        {
            instance = null;
            S2JList.Unload();
            S2JTooltip.Unload();
        }
        public static Mod instance;
    }
}