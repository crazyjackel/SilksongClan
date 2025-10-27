using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Impl;
using HarmonyLib;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using TrainworksReloaded.Core.Enum;
using TrainworksReloaded.Core;
using System.Collections;
using UnityEngine;

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
            Plugin.Logger.LogInfo("Getting silk cost for card: " + cardState.GetAssetName());
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


        public void RewardSilk(int amount)
        {
            Plugin.Logger.LogInfo("Rewarding Silk: " + amount);
            var silkSaveData = GetSilkSaveData() ?? new SilkSaveData
            {
                Silk = 0
            };
            var newSilk = Mathf.Clamp(silkSaveData.Silk + amount, 0, GetMaxSilk());
            silkSaveData.Silk = newSilk;
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
                    silkSaveDataJson = silkSaveDataJson.Replace(SilkSaveDataKeyMagic, "");
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
                this.cardManager = null;
            }
        }

        public void NewProviderFullyInstalled(IProvider newProvider)
        {

        }
    }
}