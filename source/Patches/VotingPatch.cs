using HarmonyLib;

using System.Collections.Generic;
using System.Linq;
using TownOfUs.Services;
using Unity;

namespace TownOfUs.Patches
{
    public class VotingPatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.CalculateVotes))]
        public static void PatchCalculateVotes(Dictionary<byte, int> __result)
        {
            var voteService = PluginSingleton<TownOfUs>.Instance.Container.Resolve<VoteService>();
            __result = voteService.TallyResults().GroupBy(vote => vote.Serialize())
                .ToDictionary(group => group.Key, group => group.Count());
        }

        [HarmonyPrefix, HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.PopulateResults))]
        public static bool PatchPopulateResults()
        {
            var voteService = PluginSingleton<TownOfUs>.Instance.Container.Resolve<VoteService>();
            voteService.RenderResults();
            return false;
        }
    }
}