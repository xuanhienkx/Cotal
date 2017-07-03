using System.Collections.Generic;
using System.Linq;
using Cotal.App.Model.Models;
using Cotal.Core.Common;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface ICommonService
  {
    Footer GetFooter();

    IEnumerable<Slide> GetSlides();

    SystemConfig GetSystemConfig(string code);
  }

  public class CommonService : ServiceBace<Footer, string>, ICommonService
  {
    private readonly IRepository<Slide, int> _slideRepository;
    private readonly IRepository<SystemConfig, int> _systemConfigRepository;

    public CommonService(IUowProvider uowProvider) : base(uowProvider)
    {
      _slideRepository = UnitOfWork.GetRepository<Slide, int>();
      _systemConfigRepository = UnitOfWork.GetRepository<SystemConfig, int>();
    }

    public Footer GetFooter()
    {
      return Repository.Get(CommonConstants.DefaultFooterId);
    }

    public IEnumerable<Slide> GetSlides()
    {
      return _slideRepository.Query(x => x.Status);
    }

    public SystemConfig GetSystemConfig(string code)
    {
      return _systemConfigRepository.Query(x => x.Code == code).FirstOrDefault();
    }
  }
}