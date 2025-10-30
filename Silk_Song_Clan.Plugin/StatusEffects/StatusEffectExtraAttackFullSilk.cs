using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Enum;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Base;
using TrainworksReloaded.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Silk_Song_Clan.Plugin
{
    public class StatusEffectExtraAttackFullSilk : StatusEffectState
    {
        public bool ApplyingBuffToAttacker { get; set; } = false;
        public int ExtraAttackAmount { get; set; } = 0;
        public override void OnStacksAdded(CharacterState character, int numStacksAdded, CharacterState.AddStatusEffectParams addStatusEffectParams, ICoreGameManagers coreGameManagers)
        {
            ExtraAttackAmount += numStacksAdded;
            if (SilkManager.IsFullSilk)
            {
                Plugin.Logger.LogInfo("Applying buff to attacker: " + numStacksAdded);
                character.BuffDamage(numStacksAdded, fromStatusEffect: true);
            }
            base.OnStacksAdded(character, numStacksAdded, addStatusEffectParams, coreGameManagers);
        }

        public override void OnStacksRemoved(CharacterState character, int numStacksRemoved, ICoreGameManagers coreGameManagers)
        {
            ExtraAttackAmount -= numStacksRemoved;
            if (SilkManager.IsFullSilk)
            {
                Plugin.Logger.LogInfo("Removing buff from attacker: " + numStacksRemoved);
                character.DebuffDamage(numStacksRemoved, fromStatusEffect: true);
            }
            base.OnStacksRemoved(character, numStacksRemoved, coreGameManagers);
        }
        public override bool TestTrigger(StatusEffectState.InputTriggerParams inputTriggerParams, StatusEffectState.OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            CharacterTriggerData.Trigger triggerType = inputTriggerParams.triggerType;
            return triggerType == CharacterTriggerData.Trigger.OnSilence || triggerType == CharacterTriggerData.Trigger.OnSilenceLost;
        }
        protected override IEnumerator OnTriggered(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            if(SilkManager.IsFullSilk && !ApplyingBuffToAttacker)
            {
                Plugin.Logger.LogInfo("Applying buff to attacker: " + ExtraAttackAmount);
                inputTriggerParams.associatedCharacter.BuffDamage(ExtraAttackAmount, fromStatusEffect: true);
                ApplyingBuffToAttacker = true;
            }
            else if (!SilkManager.IsFullSilk && ApplyingBuffToAttacker)
            {
                Plugin.Logger.LogInfo("Removing buff from attacker: " + ExtraAttackAmount);
                inputTriggerParams.associatedCharacter.DebuffDamage(ExtraAttackAmount, fromStatusEffect: true);
                ApplyingBuffToAttacker = false;
            }
            yield break;
        }
    }
}