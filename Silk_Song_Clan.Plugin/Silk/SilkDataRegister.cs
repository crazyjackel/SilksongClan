using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TrainworksReloaded.Core.Enum;
using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin
{
    public class SilkDataRegister : Dictionary<string, SilkData>, IRegister<SilkData>
    {
        private readonly IModLogger<SilkData> logger;
        public SilkDataRegister(IModLogger<SilkData> logger)
        {
            this.logger = logger;
        }

        public List<string> GetAllIdentifiers(RegisterIdentifierType identifierType)
        {
            return [..this.Keys];
        }

        public void Register(string key, SilkData item)
        {
            logger.Log(LogLevel.Info, $"Registering SilkData: {key}");
            Add(key, item);
        }

        public bool TryLookupIdentifier(string identifier, RegisterIdentifierType identifierType, [NotNullWhen(true)] out SilkData? lookup, [NotNullWhen(true)] out bool? IsModded)
        {
            IsModded = true;
            return this.TryGetValue(identifier, out lookup);
        }
    }
}
