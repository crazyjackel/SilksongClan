using System.Collections;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin
{
    public class RelicEffectDrawAdditionalNextTurnOnTrigger : RelicEffectBase, ICharacterActionRelicEffect, IRelicEffect
    {
        private int cardCount;
        private CharacterTriggerData.Trigger trigger;
        private Team.Type sourceTeam;

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            return new PropDescriptions
            {
                [RelicEffectFieldNames.ParamInt.GetFieldName()] = new PropDescription("Card Count"),
                [RelicEffectFieldNames.ParamTrigger.GetFieldName()] = new PropDescription("Trigger"),
                [RelicEffectFieldNames.ParamSourceTeam.GetFieldName()] = new PropDescription("Source Team")
            };
        }

        public override void Initialize(RelicState relicState, RelicData srcRelicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, srcRelicData, relicEffectData);
            cardCount = relicEffectData.GetParamInt();
            trigger = relicEffectData.GetParamTrigger();
            sourceTeam = relicEffectData.GetParamSourceTeam();
        }

        public bool TestCharacterTriggerEffect(CharacterTriggerRelicEffectParams relicEffectParams, ICoreGameManagers coreGameManagers)
        {
            if (relicEffectParams.trigger == trigger && relicEffectParams.characterState.GetTeamType() == sourceTeam)
            {
                if (trigger == CharacterTriggerData.Trigger.OnDeath && relicEffectParams.characterState.IsAlive && relicEffectParams.paramInt != 1)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public IEnumerator ApplyCharacterTriggerEffect(CharacterTriggerRelicEffectParams relicEffectParams, ICoreGameManagers coreGameManagers)
        {
            CharacterState characterState = relicEffectParams.characterState;
            characterState?.GetCharacterUI().ShowEffectVFX(characterState, _srcRelicEffectData.GetAppliedVfx());
            
            CardManager cardManager = coreGameManagers.GetCardManager();
            if (cardManager != null)
            {
                cardManager.AdjustDrawCountModifier(cardCount, null);
            }
            
            NotifyRelicTriggered(coreGameManagers.GetRelicManager(), relicEffectParams.characterState);
            yield break;
        }
    }
}
