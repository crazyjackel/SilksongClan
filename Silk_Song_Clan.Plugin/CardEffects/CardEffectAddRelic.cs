using System.Collections;
using HarmonyLib;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using UnityEngine;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectAddRelic : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamStr.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Relic", "", null, false);
            return propDescriptions;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var saveManager = coreGameManagers.GetSaveManager();
            if(saveManager.PreviewMode)
            {
                yield break;
            }
            var relic = cardEffectState.GetParamStr().ToId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.RelicData);
            var container = Railend.GetContainer();
            var relicDataRegister = container.GetInstance<IRegister<RelicData>>();
            if (!relicDataRegister.TryLookupIdentifier(relic, TrainworksReloaded.Core.Enum.RegisterIdentifierType.ReadableID, out RelicData? relicData, out bool? _))
            {
                yield break;
            }
            coreGameManagers.GetSaveManager().AddRelic(relicData);
            yield break;
        }
    }
}