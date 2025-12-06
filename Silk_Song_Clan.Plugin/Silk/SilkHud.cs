using Conductor.TrackedValues;
using Conductor.UI;
using Silk_Song_Clan.Plugin;

namespace Silk_Song_Clan.Plugin
{
    public class SilkHud : ClassMechanicHud
    {
        protected override void HandleValueChanged(TrackedValueChangedParams changedParams)
        {
            Plugin.Logger.LogInfo($"SilkHud: Value changed from {changedParams.previousValue} to {changedParams.value}");
            base.HandleValueChanged(changedParams);
        }
    }
}