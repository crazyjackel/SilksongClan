using System.Collections;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using UnityEngine;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddSilk : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Silk Amount", "", typeof(int), false);
            return propDescriptions;
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var silkAmount = cardEffectState.GetParamInt();
            var container = Railend.GetContainer();
            var silkManager = container.GetInstance<SilkManager>();
            
            if (silkManager != null)
            {
                silkManager.AddSilk(silkAmount);
            }
            
            yield break;
        }
    }
}
