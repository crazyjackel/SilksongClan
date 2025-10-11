using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using I2.Loc;
using Microsoft.Extensions.Configuration;
using ShinyShoe.Logging;
using SimpleInjector;
using TrainworksReloaded.Base;
using TrainworksReloaded.Base.Card;
using TrainworksReloaded.Base.CardUpgrade;
using TrainworksReloaded.Base.Character;
using TrainworksReloaded.Base.Class;
using TrainworksReloaded.Base.Effect;
using TrainworksReloaded.Base.Localization;
using TrainworksReloaded.Base.Prefab;
using TrainworksReloaded.Base.Trait;
using TrainworksReloaded.Base.Trigger;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Impl;
using TrainworksReloaded.Core.Interfaces;
using TrainworksReloaded.Core.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TrainworksReloaded.Base.Extensions;

namespace Silk_Song_Clan.Plugin
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger = new(MyPluginInfo.PLUGIN_GUID);
        internal static Lazy<SilkManager> Client = new(() => new SilkManager());
        public void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            // Setup Game Client
            var client = Client.Value;
            DepInjector.AddProvider(client);

            var builder = Railhead.GetBuilder();
            builder.Configure(
                MyPluginInfo.PLUGIN_GUID,
                c =>
                {
                    // Be sure to include all of your json files if you add more.
                    // Be sure to update the project configuration if you include more folders
                    //   the project only copies json files in the json folder and not in subdirectories.
                    c.AddMergedJsonFile(
                        //Class
                        "json/class/silksong.json",

                        //Champions
                        "json/champions/hornet_base.json",
                        "json/champions/hornet_architect.json",
                        "json/champions/hornet_beast.json",
                        "json/champions/hornet_cursed.json",
                        "json/champions/hornet_hunter.json",
                        "json/champions/hornet_reaper.json",
                        "json/champions/hornet_shaman.json",
                        "json/champions/hornet_wanderer.json",
                        "json/champions/hornet_witch.json",

                        //Triggers
                        "json/triggers/silksong.json",

                        //Subtypes
                        "json/subtypes/snail.json",
                        "json/subtypes/bug.json",
                        "json/subtypes/pinstress.json",

                        //Cardpools
                        "json/cardpool/flea_pool.json",

                        //Tokens
                        "json/tokens/flea.json",
                        "json/tokens/bell_flea.json",
                        "json/tokens/huge_flea.json",
                        "json/tokens/brew_flea.json",

                        //Units
                        "json/units/alchemist_zylotol. json",
                        "json/units/ballow.json",  
                        "json/units/sherma.json",
                        "json/units/bell_hermit.json",
                        "json/units/caretaker.json",
                        "json/units/chapel_maid.json",
                        "json/units/crull_and_benjin.json",
                        "json/units/fleamaster_mooshka.json",
                        "json/units/flick_the_fixer.json",
                        "json/units/garamond.json",
                        "json/units/gilly.json",
                        "json/units/green_prince.json",
                        "json/units/grindle.json",
                        "json/units/grishkin.json",
                        "json/units/kratt.json",
                        "json/units/lumble_the_lucky.json",
                        "json/units/nuu.json",
                        "json/units/pavo.json",
                        "json/units/pebb.json",
                        "json/units/pinmaster_plinney.json",
                        "json/units/pinstress.json",
                        "json/units/relic_seeker_scrounge.json",
                        "json/units/seamstress.json",

                        //Status Effects
                        "json/status_effects/imbue.json",
                        "json/status_effects/protection.json",
                        "json/status_effects/permanent_imbue.json",
                        "json/status_effects/permanent_protection.json"
                    );
                }
            );

            Railend.ConfigurePreAction(builder =>
            {
                builder.RegisterInstance(client);
                builder.RegisterSingleton<IRegister<SilkData>, SilkDataRegister>();
                builder.RegisterDecorator<
                    IDataPipeline<IRegister<CardData>, CardData>,
                    CardSilkDataPipelineDecorator
                >();
            });


            Railend.ConfigurePostAction(
                c =>
                {
                    var manager = c.GetInstance<IRegister<CharacterTriggerData.Trigger>>();
                    var triggerManager = c.GetInstance<IRegister<CardTriggerType>>();

                    CharacterTriggerData.Trigger GetTrigger(string id)
                    {
                        return manager.GetValueOrDefault(MyPluginInfo.PLUGIN_GUID.GetId(TemplateConstants.CharacterTriggerEnum, id));
                    }

                    // CardTriggerType GetCardTrigger(string id)
                    // {
                    //     return triggerManager.GetValueOrDefault(MyPluginInfo.PLUGIN_GUID.GetId(TemplateConstants.CardTriggerEnum, id));
                    // }

                    CharacterTriggers.Combo = GetTrigger("Combo");
                    CharacterTriggers.Silksong = GetTrigger("Silksong");
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            
            // Enable harmony patches for silk system
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }
}
