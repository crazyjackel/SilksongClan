using System.Collections;
using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using UnityEngine;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddCollectableRelicIfGoldSpent : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamStr.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Relic", "", null, false);
            string fieldName2 = CardEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Gold Required", "", typeof(int), false);
            return propDescriptions;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var goldRequired = cardEffectState.GetParamInt();
            var spentGold = GetSpentGold(cardEffectState, cardEffectParams, coreGameManagers);
            if (spentGold < goldRequired)
            {
                yield break;
            }
            var relic = cardEffectState.GetParamStr().ToId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.RelicData);
            var allGameData = coreGameManagers.GetSaveManager().GetAllGameData();
            var RelicDatas =
                (List<CollectableRelicData>)
                    AccessTools.Field(typeof(AllGameData), "collectableRelicDatas").GetValue(allGameData);
            var collectableRelic = RelicDatas.FirstOrDefault(x => x.GetID() == relic);
            if (collectableRelic == null)
            {
                yield break;
            }
            var container = Railend.GetContainer();
            var relicDataRegister = container.GetInstance<IRegister<RelicData>>();
            if (!relicDataRegister.TryLookupIdentifier(relic, TrainworksReloaded.Core.Enum.RegisterIdentifierType.ReadableID, out RelicData? relicData, out bool? _))
            {
                yield break;
            }
            coreGameManagers.GetSaveManager().AddRelic(relicData);
            yield break;
        }
        private int GetSpentGold(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            CardState? playedCard = cardEffectParams.playedCard;
            if (playedCard == null)
            {
                return 0;
            }
            foreach (CardEffectState cardEffectState2 in playedCard.GetEffectStates())
            {
                if (cardEffectState2.GetCardEffect() is CardEffectRewardGold cardEffectRewardGold)
                {
                    int gold = coreGameManagers.GetSaveManager().GetGold();
                    int num = -cardEffectRewardGold.GetModifiedGoldAmount(cardEffectState2, coreGameManagers.GetCardStatistics());
                    return Mathf.Min(gold, num);
                }
            }
            return 0;
        }
    }
}