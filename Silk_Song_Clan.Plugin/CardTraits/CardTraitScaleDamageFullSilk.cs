namespace Silk_Song_Clan.Plugin{
	public override PropDescriptions CreateEditorInspectorDescriptions()
	{
		PropDescriptions propDescriptions = new PropDescriptions();
		string fieldName = CardTraitFieldNames.ParamFloat.GetFieldName();
		propDescriptions[fieldName] = new PropDescription("Stacks Per Stat", "", null, false);
		return propDescriptions.Add(base.CreateStatValueDataInspectorDescriptions());
	}
	public override int OnApplyingBuffDamageToUnit(CardTraitState.ApplyingDamageParameters parameters, ICoreGameManagers coreGameManagers)
	{
		return this.GetExtraDamage(coreGameManagers.GetCardStatistics());
	}

	private int GetExtraDamage(CardStatistics cardStats)
	{
        return 0;
		int statValue = cardStats.GetStatValue(base.StatValueData);
		return base.GetParamInt() * statValue;
	}

	[NullableContext(2)]
	[return: Nullable(1)]
	public override string GetCurrentEffectText(CardStatistics cardStatistics, SaveManager saveManager, RelicManager relicManager)
	{
		if (cardStatistics != null && cardStatistics.GetStatValueShouldDisplayOnCardNow(base.StatValueData))
		{
			int extraDamage = this.GetExtraDamage(cardStatistics);
			return "CardTraitScalingBuffDamage_CurrentScaling_CardText".Localize(new LocalizedIntegers(new int[] { extraDamage }));
		}
		return string.Empty;
	}
}