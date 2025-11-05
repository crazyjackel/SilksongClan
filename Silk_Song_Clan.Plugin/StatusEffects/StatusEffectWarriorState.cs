using System;
using System.Collections;
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
            yield return silkManager.RewardSilk(1);
            yield break;
        }
    }
}

