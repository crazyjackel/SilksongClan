using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;
using System.Collections;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectTestLastCardName : CardEffectTestLastCard
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
                Plugin.Logger.LogInfo("No last card played this turn");
                return false;
            }
            var cardName = cardEffectState.GetParamStr();
            var fullCardName = cardName.ToId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.Card);
            Plugin.Logger.LogInfo("Testing last card name: " + lastCard.GetAssetName());
            Plugin.Logger.LogInfo("Card name: " + fullCardName);
            var result = lastCard.GetID() == fullCardName;
            Plugin.Logger.LogInfo("Result: " + result);
            return result;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            yield break;
        }
    }
}