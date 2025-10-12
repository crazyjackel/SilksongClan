using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core;

namespace Silk_Song_Clan.Plugin
{
    public class StatusEffectWarriorState : StatusEffectState
    {
        protected override IEnumerator OnTriggered(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            var container = Railend.GetContainer();
            var silkManager = container.GetInstance<SilkManager>();
            silkManager.AddSilk(1);
            yield break;
        }
    }
}

