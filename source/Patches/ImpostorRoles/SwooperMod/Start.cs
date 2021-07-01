using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.ImpostorRoles.SwooperMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class Start
    {
        public static void Postfix(ShipStatus __instance)
        {
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Swooper>())
            {
                var miner = (Swooper)role;
                miner.LastSwooped = DateTime.UtcNow;
                miner.LastSwooped = miner.LastSwooped.AddSeconds(-10f);
            }
        }
    }
}