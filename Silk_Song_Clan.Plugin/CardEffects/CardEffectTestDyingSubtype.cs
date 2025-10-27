using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Extensions;
using TrainworksReloaded.Core.Interfaces;
using System.Collections;

namespace Silk_Song_Clan.Plugin
{
    public class CardEffectTestDyingSubtype : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            return propDescriptions;
        }
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            if (cardEffectParams.dyingCharacter == null)
            {
                return false;
            }
            if (!cardEffectParams.dyingCharacter.GetHasSubtype(cardEffectState.GetParamSubtype()))
            {
                return false;
            }
            return true;
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            yield break;
        }
    }
}