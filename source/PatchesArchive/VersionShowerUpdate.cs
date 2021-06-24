using BepInEx.IL2CPP;
using HarmonyLib;
using System.Linq;

namespace TownOfUs
{
    [HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerUpdate
    {
        public static void Postfix(VersionShower __instance)
        {
            var text = __instance.text;
            //text.text += "\nloaded <color=#FFBFCCFF>T[FF80D5FF]o[FFCC00FF]w[704FA8FF]n[FF0000FF] of[CC4D00FF] Us [FFFFFFFF]by [00FF00FF]slushiegoose[FFFFFFFF] </color>;
            text.text += $" - <color=#00FF00FF>TownOfUs {IL2CPPChainloader.Instance.Plugins.Values.Single(x => x.Instance is TownOfUs).Metadata.Version}</color>";
        }
    }
}