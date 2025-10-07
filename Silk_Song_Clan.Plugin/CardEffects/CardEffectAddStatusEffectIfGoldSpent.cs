using System.Collections;
using UnityEngine;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddStatusEffectIfGoldSpent : CardEffectAddStatusEffect
    {
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var goldRequired = cardEffectState.GetAdditionalParamInt();    
            var spentGold = GetSpentGold(cardEffectState, cardEffectParams, coreGameManagers);
            if (spentGold < goldRequired)
            {
                yield break;
            }
            yield return ApplyEffect(cardEffectState, cardEffectParams, coreGameManagers, sysManagers);
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