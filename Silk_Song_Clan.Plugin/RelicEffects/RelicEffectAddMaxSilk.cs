using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Base;

namespace Silk_Song_Clan.Plugin
{
    public class RelicEffectAddMaxSilk : RelicEffectBase, IModifyMaxSilkRelicEffect
    {
        private int maxSilkAmount = 0;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            this.maxSilkAmount = relicEffectData.GetParamInt();
        }

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = RelicEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Max Silk Amount", "", typeof(int), false);
            return propDescriptions;
        }

        public int GetMaxSilkAmount()
        {
            return this.maxSilkAmount;
        }
    }
}

