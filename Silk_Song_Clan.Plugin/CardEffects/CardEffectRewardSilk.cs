using System.Collections;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using UnityEngine;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectRewardSilk : CardEffectBase
    {
        public override bool CanApplyInPreviewMode => true;
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Silk Amount", "", typeof(int), false);
            return propDescriptions;
        }
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            var container = Railend.GetContainer();
            var silkManager = container.GetInstance<SilkManager>();
            if (silkManager == null)
            {
                return false;
            }
            var silkAmount = cardEffectState.GetParamInt();
            return silkManager.GetCurrentSilk() + silkAmount >= 0;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var saveManager = coreGameManagers.GetSaveManager();
            if(saveManager.PreviewMode)
            {
                yield break;
            }
            var silkAmount = cardEffectState.GetParamInt();
            var container = Railend.GetContainer();
            var silkManager = container.GetInstance<SilkManager>();
            
            if (silkManager != null)
            {
                yield return silkManager.RewardSilk(silkAmount);
                if (silkAmount < 0)
                {
                    yield return HandleSilksongTrigger(coreGameManagers.GetHeroManager(), coreGameManagers.GetMonsterManager(), coreGameManagers.GetCombatManager());
                }
            }
            
            yield break;
        }
        public IEnumerator HandleSilksongTrigger(HeroManager heroManager, MonsterManager monsterManager, CombatManager combatManager)
        {
            Plugin.Logger.LogInfo("Handling Silksong Trigger");
            if (heroManager != null)
            {
                HandleSilksongTrigger(heroManager, combatManager);
            }
            if (monsterManager != null)
            {
                HandleSilksongTrigger(monsterManager, combatManager);
            }
            yield return combatManager?.RunTriggerQueue();
        }

        public void HandleSilksongTrigger(ICharacterManager characterManager, CombatManager combatManager)
        {
            for (int i = 0; i < characterManager.GetNumCharacters(); i++)
            {
                var charState = characterManager.GetCharacter(i);
                if (charState == null)
                {
                    continue;
                }
                if (!charState.IsDestroyed && charState.IsAlive)
                {
                    QueueCustomTrigger(charState, CharacterTriggers.Silksong, combatManager);
                }
            }
        }

        private void QueueCustomTrigger(CharacterState character, CharacterTriggerData.Trigger trigger, CombatManager combatManager)
        {
            combatManager?.QueueTrigger(character, trigger, dyingCharacter: null, canAttackOrHeal: true,
                                       canFireTriggers: true, fireTriggersData: null, triggerCount: 1,
                                       exclusiveTrigger: null);
        }
    }
}
