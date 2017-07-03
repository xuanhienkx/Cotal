using AutoMapper;

namespace Cotal.App.Business.Mappings
{
  public class AutoMapperConfig
  {
    public static MapperConfiguration RegisterMappings()
    {
      return new MapperConfiguration(cfg => { cfg.AddProfile(new ViewModelMappingProfile()); });
    }
  }
}