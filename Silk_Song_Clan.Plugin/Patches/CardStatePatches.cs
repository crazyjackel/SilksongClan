using HarmonyLib;
using Silk_Song_Clan.Plugin;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using ShinyShoe;

namespace Silk_Song_Clan.Plugin
{
    [HarmonyPatch(typeof(CardState), "IsAffordable")]
    public static class CardStateIsAffordablePatch
    {
        static void Postfix(CardState __instance,
            int energy,
            CardStatistics cardStatistics,
            MonsterManager monsterManager, 
            RelicManager relicManager, 
            RoomState roomState,
            ref bool __result)
        {
            // If already unaffordable by base rules, don't override
            if (!__result)
            {
                return;
            }

            // Check silk affordability via SilkManager
            var container = Railend.GetContainer();
            var silkManager = container.GetInstance<SilkManager>();
            if (silkManager == null)
            {
                return;
            }

            var silkCost = silkManager.GetSilkCost(__instance);
            if (silkCost == null || silkCost.Value <= 0)
            {
                return; // No silk cost
            }

            var silkSave = silkManager.GetCurrentSilk();
            if (silkSave < silkCost.Value)
            {
                __result = false;
            }
        }
    }

}
