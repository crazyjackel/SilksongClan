using Microsoft.Extensions.Configuration;
using TrainworksReloaded.Base.Card;
using TrainworksReloaded.Core.Extensions;
using TrainworksReloaded.Core.Interfaces;

namespace Silk_Song_Clan.Plugin{
    public class CardSilkDataPipelineDecorator : IDataPipeline<IRegister<CardData>, CardData>
    {
        private readonly IDataPipeline<IRegister<CardData>, CardData> decoratee;
        private readonly IRegister<SilkData> silkData;
        public CardSilkDataPipelineDecorator(IDataPipeline<IRegister<CardData>, CardData> decoratee, IRegister<SilkData> silkData)
        {
            this.decoratee = decoratee;
            this.silkData = silkData;
        }

        public List<IDefinition<CardData>> Run(IRegister<CardData> service)
        {
            var definitions = decoratee.Run(service);
            foreach (var definition in definitions)
            {
                var configuration = definition.Configuration;
                var configuration_extensions_silksong = configuration.GetSection("extensions")
                    .GetChildren().Where(xs => xs.GetSection("silksong").Exists())
                    .Select(xs => xs.GetSection("silksong"))
                    .FirstOrDefault();
                if (configuration_extensions_silksong == null)
                {
                    continue;
                }
                var silkCost = configuration_extensions_silksong.GetSection("silk_cost").ParseInt() ?? 0;
                this.silkData.Register(definition.Key, new SilkData(){
                    Silk = silkCost
                });
            }
            return definitions;
        }
    }
}