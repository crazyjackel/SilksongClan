
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Effect;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin
{
    public sealed class RelicEffectAddStatusEffectOnOtherStatusAdded : RelicEffectBase, IOnStatusEffectAddedRelicEffect, IRelicEffect, IStatusEffectRelicEffect
    {
        public override bool CanApplyInPreviewMode => true;

        public override bool CanShowNotifications => false;

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = RelicEffectFieldNames.ParamString.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Other Status ID", "", null, false);
            string fieldName2 = RelicEffectFieldNames.ParamSourceTeam.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Target Team", "", null, false);
            string fieldName3 = RelicEffectFieldNames.ParamStatusEffects.GetFieldName();
            propDescriptions[fieldName3] = new PropDescription("Status Effects", "", null, false);
            return propDescriptions;
        }

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            this.team = relicEffectData.GetParamSourceTeam();
            
            // Resolve the other status effect ID using Railend
            var otherStatusString = relicEffectData.GetParamString();
            this.otherStatusId = otherStatusString.ToId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.StatusEffect);
            StatusEffectStackData[] paramStatusEffects = relicEffectData.GetParamStatusEffects();
            if (paramStatusEffects.Length != 0)
            {
                this.statusId = paramStatusEffects[0].statusId;
                this.additionalStacks = paramStatusEffects[0].count;
            }
        }

        public void OnStatusEffectAddedApplyMultiplier(OnStatusEffectAddedRelicEffectParams relicEffectParams)
        {
        }

        public void OnStatusEffectAddedApplyAdder(OnStatusEffectAddedRelicEffectParams relicEffectParams)
        {
            if (relicEffectParams.statusId != this.otherStatusId)
            {
                return;
            }
            if (this.team != Team.Type.None && relicEffectParams.characterState.GetTeamType() != this.team)
            {
                return;
            }
            if (relicEffectParams.fromEffectType == typeof(CardEffectTransferAllStatusEffects) || 
                relicEffectParams.fromEffectType == typeof(CardEffectMultiplyStatusEffect) || 
                relicEffectParams.fromEffectType == typeof(CardEffectTransferAllStatusEffectsFromUnit))
            {
                return;
            }
            relicEffectParams.characterState.AddStatusEffect(this.statusId, this.additionalStacks * relicEffectParams.stacksAdded, null, true, false);
            base.NotifyRelicTriggered(relicEffectParams.relicManager, relicEffectParams.characterState);
        }

        public void OnStatusEffectRemoved(OnStatusEffectAddedRelicEffectParams relicEffectParams)
        {
        }

        public int GetModifiedStatusEffectStacksFromMultiplier(StatusEffectStackData statusEffectStackData, CharacterState? onCharacter)
        {
            return this.additionalStacks;
        }

        public int GetStatusEffectStacksToAdd(StatusEffectStackData statusEffectStackData, CharacterState? onCharacter)
        {
            return 0;
        }

        public StatusEffectStackData[] GetStatusEffects()
        {
            return
            [
                new StatusEffectStackData
                {
                    statusId = this.statusId,
                    count = this.additionalStacks
                }
            ];
        }

        public int GetStatusEffectStacksToAddDeferred(StatusEffectStackData statusEffectStackData, CharacterState? onCharacter)
        {
            return 0;
        }

        private Team.Type team;
        private string otherStatusId = string.Empty;
        private string statusId = string.Empty;
        private int additionalStacks;
    }
}
