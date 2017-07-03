using System.Linq;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IContactDetailService
  {
    ContactDetail GetDefaultContact();
  }

  public class ContactDetailService : ServiceBace<ContactDetail, int>, IContactDetailService
  {
    public ContactDetailService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public ContactDetail GetDefaultContact()
    {
      return Repository.Query(x => x.Status).FirstOrDefault();
    }
  }
}