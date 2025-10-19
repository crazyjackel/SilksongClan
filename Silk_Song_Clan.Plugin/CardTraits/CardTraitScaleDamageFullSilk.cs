using TrainworksReloaded.Base.Card;
using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Extensions;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Silk_Song_Clan.Plugin
{
	public class CardTraitScaleDamageFullSilk : CardTraitState
	{
		public override PropDescriptions CreateEditorInspectorDescriptions()
		{
			PropDescriptions propDescriptions = new PropDescriptions();
			string fieldName = CardTraitFieldNames.ParamFloat.GetFieldName();
			propDescriptions[fieldName] = new PropDescription("Damage Multiplier", "", null, false);
			return propDescriptions.Add(base.CreateStatValueDataInspectorDescriptions());
		}
		private SilkManager GetSilkManager()
		{
			var container = Railend.GetContainer();
			return container.GetInstance<SilkManager>();
		}

        public override int OnApplyingDamage(ApplyingDamageParameters damageParams, ICoreGameManagers coreGameManagers)
        {
			int num = damageParams.damage;
			var silkManager = GetSilkManager();
			if (silkManager != null && silkManager.GetCurrentSilk() == silkManager.GetMaxSilk())
			{
				num = (int)(num * this.GetParamFloat());
			}
			return num;
        }
	}
}