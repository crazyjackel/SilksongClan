using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;
using System.Collections;
using Silk_Song_Song_Clan.Plugin;
namespace Silk_Song_Clan.Plugin
{
    public class CardEffectTestLastCard : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            return propDescriptions;
        }   
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            var relicmanager = coreGameManagers.GetRelicManager();
            if(relicmanager.GetRelicEffect<RelicEffectComboActive>() != null)
            {
                Plugin.Logger.LogInfo("Combo active");
                return true;
            }
            var cardManager = coreGameManagers.GetCardManager();
            var cardsPlayedThisTurn = (List<CardState>)AccessTools.Field(typeof(CardManager), "cardsPlayedThisTurn").GetValue(cardManager);
            var cardsPlayedThisTurnCount = cardsPlayedThisTurn.Count;
            if (cardsPlayedThisTurnCount < 2)
            {
                Plugin.Logger.LogInfo("Not enough cards played this turn");
                return false;
            }
            var lastCard = cardsPlayedThisTurn[cardsPlayedThisTurnCount - 2]; // Since we want the second to last card, we subtract 2 from the count
            var cardFilter = cardEffectState.GetParamCardFilter();
            if (cardFilter == null)
            {
                Plugin.Logger.LogInfo("No card filter provided");
                return false;
            }
            Plugin.Logger.LogInfo("Testing last card: " + lastCard.GetAssetName());
            Plugin.Logger.LogInfo("Card filter: " + cardFilter.GetName());
            var result = cardFilter.FilterCard<CardState>(lastCard, coreGameManagers.GetRelicManager());
            Plugin.Logger.LogInfo("Result: " + result);
            return result;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            yield break;
        }
    }
}