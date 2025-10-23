using HarmonyLib;
using Silk_Song_Clan.Plugin;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using ShinyShoe;

namespace Silk_Song_Clan.Plugin
{
    [HarmonyPatch(typeof(CardStatistics), "GetStatValue")]
    public static class CardStatisticsGetStatValuePatch
    {
        static bool Prefix(CardStatistics __instance,
            CardStatistics.StatValueData statValueData,
            ref int __result)
        {
            Plugin.Logger.LogInfo("GetStatValue: " + statValueData.trackedValue);
            var trackedValueType = statValueData.trackedValue;
            if (trackedValueType == TrackedValues.Silk)
            {
                var silkManager = Railend.GetContainer().GetInstance<SilkManager>();
                if (silkManager == null)
                {
                    return true;
                }
                __result = silkManager.GetCurrentSilk();
                Plugin.Logger.LogInfo("Silk: " + __result);
                return false;
            }
            return true;
        }
    }
}