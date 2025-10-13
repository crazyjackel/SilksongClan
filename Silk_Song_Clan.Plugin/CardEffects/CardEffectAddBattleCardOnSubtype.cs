using System.Collections;
using TrainworksReloaded.Base.Effect;
using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddBattleCardOnSubtype : CardEffectBase
    {
        private bool _canPlayWhenHandFull = true;
        private CardEffectAddBattleCard _cardEffectAddBattleCard = new CardEffectAddBattleCard();
        public override bool CanPlayAfterBossDead
        {
            get
            {
                return false;
            }
        }

        public override bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }

        public override bool CanPlayWhenHandFull
        {
            get
            {
                return this._canPlayWhenHandFull;
            }
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var subtype = cardEffectState.GetParamSubtype();
            if (subtype == null)
            {
                yield break;
            }
            if (cardEffectParams.dyingCharacter == null)
            {
                yield break;
            }
            if (!cardEffectParams.dyingCharacter.GetHasSubtype(subtype))
            {
                yield break;
            }
            yield return _cardEffectAddBattleCard.ApplyEffect(cardEffectState, cardEffectParams, coreGameManagers, sysManagers);
        }

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Target Card Pile", "", typeof(CardPile), false);
            string fieldName2 = CardEffectFieldNames.AdditionalParamInt.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Num Cards", "", null, false);
            string fieldName3 = CardEffectFieldNames.ParamCardUpgradeData.GetFieldName();
            propDescriptions[fieldName3] = new PropDescription("Optional Upgrade", "", null, false);
            string fieldName4 = CardEffectFieldNames.FilterBasedOnMainSubClass.GetFieldName();
            propDescriptions[fieldName4] = new PropDescription("Filter Based On Main Subclass", "", null, false);
            string fieldName5 = CardEffectFieldNames.CopyModifiersFromSource.GetFieldName();
            propDescriptions[fieldName5] = new PropDescription("Copy Modifiers From Source", "", null, false);
            string fieldName6 = CardEffectFieldNames.ParamCardPool.GetFieldName();
            propDescriptions[fieldName6] = new PropDescription("Card Pool", "", null, false);
            string fieldName7 = CardEffectFieldNames.ParamCardFilter.GetFieldName();
            propDescriptions[fieldName7] = new PropDescription("Card Filter", "", null, false);
            string fieldName8 = CardEffectFieldNames.ParamBool.GetFieldName();
            propDescriptions[fieldName8] = new PropDescription("Require Space In Hand", "If enabled, will check if the player's hand is full", null, false);
            return propDescriptions;
        }

        public override void Setup(CardEffectState cardEffectState)
        {
            base.Setup(cardEffectState);
            this._canPlayWhenHandFull = !cardEffectState.GetParamBool();
        }
    }
}