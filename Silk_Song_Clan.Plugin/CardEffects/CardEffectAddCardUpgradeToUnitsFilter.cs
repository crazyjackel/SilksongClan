using System.Collections.Generic;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddCardUpgradeToUnitsFilter : CardEffectAddCardUpgradeToUnits
    {
        public override void Setup(CardEffectState cardEffectState)
        {
            upgradeLifetime = (UnitUpgradeLifetime)cardEffectState.GetAdditionalParamInt1();
        }
        protected override void CollectTargetsForUpgrade(CardEffectState cardEffectState, CardEffectParams cardEffectParams, List<CharacterState> upgradeTargets, ICoreGameManagers coreGameManagers)
        {
            base.CollectTargetsForUpgrade(cardEffectState, cardEffectParams, upgradeTargets, coreGameManagers);
            upgradeTargets.RemoveAll(unit => !ShouldUpgrade(cardEffectState, coreGameManagers, unit));
        }

        public bool ShouldUpgrade(CardEffectState cardEffectState, ICoreGameManagers coreGameManagers, CharacterState unit)
        {
            var cardFilter = cardEffectState.GetParamCardFilter();
            if (cardFilter == null)
            {
                return true;
            }
            return cardFilter.FilterCharacter(unit, coreGameManagers.GetRelicManager());
        }
    }
}