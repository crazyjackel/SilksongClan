namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddCardUpgradeToUnitsFilter : CardEffectAddCardUpgradeToUnits
    {
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