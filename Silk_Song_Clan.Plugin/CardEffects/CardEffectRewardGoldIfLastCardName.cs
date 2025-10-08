using System.Collections;
using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectRewardGoldIfLastCardName : CardEffectRewardGold
    {
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            if (!ShouldRewardGold(cardEffectState, coreGameManagers))
            {
                yield break;
            }
            yield return base.ApplyEffect(cardEffectState, cardEffectParams, coreGameManagers, sysManagers);
        }
        public bool ShouldRewardGold(CardEffectState cardEffectState, ICoreGameManagers coreGameManagers)
        {
            var cardFilter = cardEffectState.GetParamCardFilter();
            if (cardFilter == null)
            {
                return false;
            }

            var cardManager = coreGameManagers.GetCardManager();
            var cardsPlayedThisTurn = (List<CardState>)AccessTools.Field(typeof(CardManager), "cardsPlayedThisTurn").GetValue(cardManager);
            var lastCard = cardsPlayedThisTurn.LastOrDefault();
            if (lastCard == null)
            {
                return false;
            }

            return lastCard.GetID() == cardEffectState.GetParamStr().ToId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.Card);
        }
    }
}