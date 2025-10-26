using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;
using System.Collections;
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
            var cardManager = coreGameManagers.GetCardManager();
            var cardsPlayedThisTurn = (List<CardState>)AccessTools.Field(typeof(CardManager), "cardsPlayedThisTurn").GetValue(cardManager);
            var lastCard = cardsPlayedThisTurn.LastOrDefault();
            if (lastCard == null)
            {
                Plugin.Logger.LogDebug("No last card played this turn");
                return false;
            }
            var cardFilter = cardEffectState.GetParamCardFilter();
            if (cardFilter == null)
            {
                Plugin.Logger.LogDebug("No card filter provided");
                return false;
            }
            Plugin.Logger.LogDebug("Testing last card: " + lastCard.GetAssetName());
            Plugin.Logger.LogDebug("Card filter: " + cardFilter.GetName());
            var result = cardFilter.FilterCard<CardState>(lastCard, coreGameManagers.GetRelicManager());
            Plugin.Logger.LogDebug("Result: " + result);
            return result;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            yield break;
        }
    }
}