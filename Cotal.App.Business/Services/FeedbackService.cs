using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IFeedbackService
  {
    Feedback Create(Feedback feedback);

    void Save();
  }
  public class FeedbackService : ServiceBace<Feedback, int>, IFeedbackService
  {
    public FeedbackService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public Feedback Create(Feedback feedback)
    {
      return Repository.Add(feedback);
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}