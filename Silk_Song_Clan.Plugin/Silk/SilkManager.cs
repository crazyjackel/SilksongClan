using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Impl;
using HarmonyLib;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using TrainworksReloaded.Core.Enum;
using TrainworksReloaded.Core;

namespace Silk_Song_Clan.Plugin
{
    public class SilkManager : IProvider, IClient
    {
        public const string SilkSaveDataKeyMagic = "__silk";
        public const int BaseMaxSilk = 12;
        private SaveManager? saveManager = null;
        private RelicManager? relicManager = null;
        private CardManager? cardManager = null;
        public SilkManager()
        {
        }

        public int GetMaxSilk()
        {
            return BaseMaxSilk + GetAdditionalMaxSilkFromRelics();
        }

        public IRegister<SilkData> GetSilkDataRegister()
        {
            var container = Railend.GetContainer();
            return container.GetInstance<IRegister<SilkData>>();
        }

        public int GetAdditionalMaxSilkFromRelics()
        {
            if (relicManager == null)
            {
                return 0;
            }
            int num = 0;
            var relicEffects = new List<IModifyMaxSilkRelicEffect>();
            relicEffects = relicManager.GetRelicEffects<IModifyMaxSilkRelicEffect>(relicEffects);
            foreach (var relicEffect in relicEffects)
            {
                num += relicEffect.GetMaxSilkAmount();
            }
            return num;
        }

        public int? GetSilkCost(CardState cardState)
        {
            var silkDataRegister = GetSilkDataRegister();
            if (!silkDataRegister.TryLookupIdentifier(
                cardState.GetCardDataID(), 
                RegisterIdentifierType.ReadableID,
                out SilkData? silkData, 
                out bool? IsModded))
            {
                return null;
            }
            return silkData.Silk;
        }

        public int GetCurrentSilk()
        {
            var silkSaveData = GetSilkSaveData();
            return silkSaveData?.Silk ?? 0;
        }


        public void AddSilk(int amount)
        {
            var silkSaveData = GetSilkSaveData();
            if (silkSaveData == null)
            {
                silkSaveData = new SilkSaveData
                {
                    Silk = 0
                };
            }
            silkSaveData.Silk += amount;
            if (silkSaveData.Silk > GetMaxSilk())
            {
                silkSaveData.Silk = GetMaxSilk();
            }
            SaveSilkSaveData(silkSaveData);
        }

        public void RemoveSilk(int amount)
        {
            var silkSaveData = GetSilkSaveData();
            if (silkSaveData == null)
            {
                silkSaveData = new SilkSaveData
                {
                    Silk = 0
                };
            }
            silkSaveData.Silk -= amount;
            if (silkSaveData.Silk < 0)
            {
                silkSaveData.Silk = 0;
            }
            SaveSilkSaveData(silkSaveData);
        }

        public SilkSaveData? GetSilkSaveData()
        {
            if (saveManager != null)
            {
                var activeSaveData = (SaveData)AccessTools.Property(typeof(SaveManager), "ActiveSaveData").GetValue(saveManager);
                var permanentlyDisabledAbilities = (List<string>)AccessTools.Field(typeof(SaveData), "permanentlyDisabledAbilities").GetValue(activeSaveData);
                // Find string begining with magic key and load it as SilkSaveData
                var silkSaveDataJson = permanentlyDisabledAbilities.FirstOrDefault(s => s.StartsWith(SilkSaveDataKeyMagic));
                if (silkSaveDataJson != null)
                {
                    var silkSaveData = JsonSerializer.Deserialize<SilkSaveData>(silkSaveDataJson);
                    return silkSaveData;
                }
            }
            return null;
        }

        public void SaveSilkSaveData(SilkSaveData silkSaveData)
        {
            if (saveManager != null)
            {
                var activeSaveData = (SaveData)AccessTools.Property(typeof(SaveManager), "ActiveSaveData").GetValue(saveManager);
                var permanentlyDisabledAbilities = (List<string>)AccessTools.Field(typeof(SaveData), "permanentlyDisabledAbilities").GetValue(activeSaveData);
                // Find string begining with magic key and remove it if it exists
                permanentlyDisabledAbilities.RemoveAll(s => s.StartsWith(SilkSaveDataKeyMagic));
                // JSON Serialize SilkSaveData
                var silkSaveDataJson = JsonSerializer.Serialize(silkSaveData);
                permanentlyDisabledAbilities.Add(SilkSaveDataKeyMagic + silkSaveDataJson);
            }
        }

        public void NewProviderAvailable(IProvider provider)
        {
            if (provider is SaveManager saveManager)
            {
                this.saveManager = saveManager;
            }
            if (provider is RelicManager relicManager)
            {
                this.relicManager = relicManager;
            }
            if (provider is CardManager cardManager)
            {
                this.cardManager = cardManager;
                this.cardManager.OnCardPlayedCallback += OnCardPlayed;
            }
        }

        private void OnCardPlayed(CardState cardState, int roomIndex, CharacterState selfTarget, SpawnPoint dropLocation, bool fromPlayedCard, CharacterState characterThatActivatedAbility, CombatManager.ApplyPreEffectsVfxAction onPreEffectsFiredVfx, CombatManager.ApplyEffectsAction onEffectsFired)
        {
            var silkCost = GetSilkCost(cardState);
            if (silkCost != null)
            {
                RemoveSilk(silkCost.Value);
            }
        }

        public void ProviderRemoved(IProvider provider)
        {
            if (provider is SaveManager _)
            {
                this.saveManager = null;
            }
            if (provider is RelicManager _)
            {
                this.relicManager = null;
            }
            if (provider is CardManager cardManager && this.cardManager != null)
            {
                this.cardManager.OnCardPlayedCallback -= OnCardPlayed;
                this.cardManager = null;
            }
        }

        public void NewProviderFullyInstalled(IProvider newProvider)
        {

        }
    }
}