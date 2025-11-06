using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Base.Effect;
using System.Collections;

namespace Silk_Song_Clan.Plugin
{
    public class RelicEffectAcrobatNotRemoved : RelicEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = RelicEffectFieldNames.ParamBool.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Acrobat Not Removed on Hit", "", null, false);
            return propDescriptions;
        }
    }
}