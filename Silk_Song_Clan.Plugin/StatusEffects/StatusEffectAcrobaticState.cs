using System.Collections;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Trigger;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Enum;
using TrainworksReloaded.Base.Extensions;

namespace Silk_Song_Clan.Plugin
{
    public class StatusEffectAcrobaticState : StatusEffectState
    {
        public enum PermanentEffectType
        {
            StatBuff = 0,
            Upgrade = 1
        }

        public enum StatBuffType
        {
            Damage = 0,
            MaxHP = 1
        }

        public Lazy<CardUpgradeData?> UpgradeData { get; set; }

        public UnitUpgradeLifetime UpgradeLifetime => (UnitUpgradeLifetime)this.GetParamInt();
        public PermanentEffectType PermanentEffect => (PermanentEffectType)this.GetParamSecondaryInt();

        public StatusEffectAcrobaticState()
        {
            UpgradeData = new Lazy<CardUpgradeData?>(() => LoadUpgradeData());
        }

        private StatBuffType? GetStatBuffType()
        {
            if (PermanentEffect != PermanentEffectType.StatBuff)
            {
                return null;
            }

            var buffName = this.GetParamStr();
            return buffName switch
            {
                "damage" => StatBuffType.Damage,
                "max_hp" => StatBuffType.MaxHP,
                _ => null
            };
        }

        private CardUpgradeData? LoadUpgradeData()
        {
            if (PermanentEffect != PermanentEffectType.Upgrade)
            {
                return null;
            }

            var container = Railend.GetContainer();
            var upgradeName = this.GetParamStr();
            var upgradeId = upgradeName.ToId(MyPluginInfo.PLUGIN_GUID, TemplateConstants.Upgrade);

            if (!container.GetInstance<IRegister<CardUpgradeData>>().TryLookupId(upgradeId, out var upgrade, out var isModded))
            {
                return null;
            }

            return upgrade;
        }

        protected override IEnumerator OnTriggered(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            var character = inputTriggerParams.associatedCharacter;
            var statusId = base.GetStatusId();
            int statusEffectStacks = character.GetStatusEffectStacks(statusId);

            // Remove all stacks of this status effect
            character.RemoveStatusEffect(statusId, statusEffectStacks);

            // If triggered from taking damage, don't apply any buffs
            if (inputTriggerParams.damage > 0 || inputTriggerParams.attacked != null)
            {
                yield break;
            }

            switch (UpgradeLifetime)
            {
                case UnitUpgradeLifetime.Permanent:
                    yield return ApplyPermanentEffect(character, statusEffectStacks);
                    break;

                case UnitUpgradeLifetime.TemporaryUntilEndOfBattle:
                case UnitUpgradeLifetime.TemporaryUntilUnitDeath:
                    yield return ApplyTemporaryEffect(character, statusEffectStacks);
                    break;
            }
        }

        private IEnumerator ApplyPermanentEffect(CharacterState character, int stacks)
        {
            var spawner = character.GetSpawnerCard();
            var modifiers = spawner.GetCardStateModifiers();

            switch (PermanentEffect)
            {
                case PermanentEffectType.StatBuff:
                    yield return ApplyPermanentStatBuff(character, modifiers, stacks);
                    break;

                case PermanentEffectType.Upgrade:
                    yield return ApplyPermanentUpgrade(modifiers, stacks);
                    break;
            }
        }

        private IEnumerator ApplyPermanentStatBuff(CharacterState character, CardStateModifiers modifiers, int stacks)
        {
            var statBuffType = GetStatBuffType();
            if (statBuffType == null)
            {
                yield break;
            }

            yield return ApplyStatBuffWithModifiers(character, modifiers, statBuffType.Value, stacks);
        }

        private IEnumerator ApplyPermanentUpgrade(CardStateModifiers modifiers, int stacks)
        {
            var upgrade = UpgradeData.Value;
            if (upgrade == null)
            {
                yield break;
            }

            var cardUpgradeState = new CardUpgradeState();
            cardUpgradeState.Setup(upgrade);

            for (int i = 0; i < stacks; i++)
            {
                modifiers.AddUpgrade(cardUpgradeState);
            }
        }

        private IEnumerator ApplyTemporaryEffect(CharacterState character, int stacks)
        {
            var statBuffType = GetStatBuffType();
            if (statBuffType == null)
            {
                yield break;
            }

            yield return ApplyTemporaryStatBuff(character, statBuffType.Value, stacks);
        }

        private IEnumerator ApplyTemporaryStatBuff(CharacterState character, StatBuffType statBuffType, int stacks)
        {
            switch (statBuffType)
            {
                case StatBuffType.MaxHP:
                    yield return character.BuffMaxHP(stacks, false, null, true);
                    break;

                case StatBuffType.Damage:
                    character.BuffDamage(stacks, null, false);
                    break;
            }
        }

        private IEnumerator ApplyStatBuffWithModifiers(CharacterState character, CardStateModifiers modifiers, StatBuffType statBuffType, int stacks)
        {
            switch (statBuffType)
            {
                case StatBuffType.Damage:
                    character.BuffDamage(stacks, null, false);
                    modifiers.IncrementAdditionalDamage(stacks);
                    break;

                case StatBuffType.MaxHP:
                    yield return character.BuffMaxHP(stacks, false, null, true);
                    modifiers.IncrementAdditionalHP(stacks);
                    break;
            }
        }
    }
}