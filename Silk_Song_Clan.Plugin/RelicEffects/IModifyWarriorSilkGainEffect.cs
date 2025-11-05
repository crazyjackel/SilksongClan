namespace Silk_Song_Clan.Plugin
{
    public interface IModifyWarriorSilkGainEffect : IRelicEffect
    {
        int ApplyOrder {get; }
        int GetSilkGainAmount();
        int GetSilkGainMultiplier();
    }
}

