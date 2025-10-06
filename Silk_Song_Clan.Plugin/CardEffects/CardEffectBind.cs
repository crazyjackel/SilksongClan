using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Base.Effect;
using System.Collections;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectBind : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            return new PropDescriptions();
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            foreach (CharacterState target in cardEffectParams.targets)
            {
                // Trigger the StatusEffectAcrobaticState
                var statusEffectstacks = new List<CharacterState.StatusEffectStack>();
                target.GetStatusEffects(ref statusEffectstacks, false);
                foreach (CharacterState.StatusEffectStack statusEffectStack in statusEffectstacks)
                {
                    if (statusEffectStack.State is StatusEffectAcrobaticState statusEffectAcrobaticState)
                    {
                        StatusEffectState.InputTriggerParams inputTriggerParams = new StatusEffectState.InputTriggerParams
                        {
                            associatedCharacter = target
                        };
                        StatusEffectState.OutputTriggerParams outputTriggerParams = new StatusEffectState.OutputTriggerParams();
                        yield return statusEffectAcrobaticState.Trigger(inputTriggerParams, outputTriggerParams, coreGameManagers);
                    }
                }
            }

        }
    }
}