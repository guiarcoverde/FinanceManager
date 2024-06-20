using AutoMapper;
using FinanceManager.Application.AutoMapper;

namespace Common.TestUtilities.MapperBuilder;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });

        return mapper.CreateMapper();
    }
}
