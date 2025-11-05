using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin
{
    public class RelicEffectAdditionalWarriorSilk : RelicEffectBase, IModifyWarriorSilkGainEffect
    {

        private int silkGainAmount = 0;
        private int silkGainMultiplier = 1;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, relicData, relicEffectData);
            this.silkGainAmount = relicEffectData.GetParamInt();
            this.silkGainMultiplier = relicEffectData.GetParamInt2();
        }
        public int ApplyOrder => 1;

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = RelicEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Silk Gain Amount", "", typeof(int), false);
            fieldName = RelicEffectFieldNames.ParamInt2.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Silk Gain Multiplier", "", typeof(int), false);
            return propDescriptions;
        }


        public int GetSilkGainAmount()
        {
            return this.silkGainAmount;
        }

        public int GetSilkGainMultiplier()
        {
            return this.silkGainMultiplier;
        }
    }
}

