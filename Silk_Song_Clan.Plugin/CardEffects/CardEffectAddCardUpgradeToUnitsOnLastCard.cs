using HarmonyLib;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddCardUpgradeToUnitsOnLastCard : CardEffectAddCardUpgradeToUnits
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamCardUpgradeData.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Card Upgrade", "", null, false);
            string fieldName2 = CardEffectFieldNames.AdditionalParamInt1.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Upgrade Lifetime", CardUpgradeHelper.UpgradeLifetimeEditorTooltip, typeof(UnitUpgradeLifetime), false);
            string fieldName3 = CardEffectFieldNames.ParamCardFilter.GetFieldName();
            propDescriptions[fieldName3] = new PropDescription("Card Filter", "", null, false);
            return propDescriptions;
        }

        public override void Setup(CardEffectState cardEffectState)
        {
            this.upgradeLifetime = (UnitUpgradeLifetime)cardEffectState.GetAdditionalParamInt1();
        }

        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            var baseResult = base.TestEffect(cardEffectState, cardEffectParams, coreGameManagers);
            if (!baseResult)
            {
                return false;
            }

            return ShouldUpgrade(cardEffectState, coreGameManagers);
        }

        public bool ShouldUpgrade(CardEffectState cardEffectState, ICoreGameManagers coreGameManagers)
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

            return cardFilter.FilterCard<CardState>(lastCard, coreGameManagers.GetRelicManager());
        }

        protected override void CollectTargetsForUpgrade(CardEffectState cardEffectState, CardEffectParams cardEffectParams, List<CharacterState> upgradeTargets, ICoreGameManagers coreGameManagers)
        {
            if (!ShouldUpgrade(cardEffectState, coreGameManagers))
            {
                upgradeTargets.Clear();
                return;
            }

            base.CollectTargetsForUpgrade(cardEffectState, cardEffectParams, upgradeTargets, coreGameManagers);
        }
    }
}