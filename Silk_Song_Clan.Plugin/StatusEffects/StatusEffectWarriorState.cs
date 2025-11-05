using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core;

namespace Silk_Song_Clan.Plugin
{
    public class StatusEffectWarriorState : StatusEffectState
    {
        public override int GetTriggerOrder()
        {
            return 4; //Activate Warrior after Trample Damage
        }
        protected override IEnumerator OnTriggered(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            var container = Railend.GetContainer();
            var silkManager = container.GetInstance<SilkManager>();
            if (this.IsPreviewModeCopy())
            {
                yield break;
            }
            
            int silkGainAmount = 1;

            // Check for relic effects that modify warrior silk gain
            var relicManager = coreGameManagers.GetRelicManager();
            if (relicManager != null)
            {
                var relicEffects = new List<IModifyWarriorSilkGainEffect>();
                relicEffects = relicManager.GetRelicEffects(relicEffects).OrderBy(a => a.ApplyOrder).ToList();
                foreach (var relicEffect in relicEffects)
                {
                    silkGainAmount += relicEffect.GetSilkGainAmount();
                    silkGainAmount *= relicEffect.GetSilkGainMultiplier();
                }
            }

            yield return silkManager.RewardSilk(silkGainAmount);
            yield break;
        }
    }
}

