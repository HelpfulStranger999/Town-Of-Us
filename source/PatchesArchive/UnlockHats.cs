using HarmonyLib;
using System.Linq;
using UnhollowerBaseLib;

namespace TownOfUs
{
    [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetUnlockedHats))]
    public class UnlockHats
    {
        public static bool Prefix(HatManager __instance, ref Il2CppReferenceArray<HatBehaviour> __result)
        {
            __result = __instance.AllHats.ToArray()
                .Where(hat => !HatManager.IsMapStuff(hat.ProdId) || SaveManager.GetPurchase(hat.ProductId))
                .OrderByDescending(hat => hat.Order).ThenBy(hat => hat.name).ToArray();
            return false;
        }
    }
}