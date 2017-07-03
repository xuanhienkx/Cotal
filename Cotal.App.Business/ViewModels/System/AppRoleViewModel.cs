using System.ComponentModel.DataAnnotations;

namespace Cotal.App.Business.ViewModels.System
{
  public class AppRoleViewModel
  {
    public int Id { set; get; }

    [Required(ErrorMessage = "Bạn phải nhập tên")]
    public string Name { set; get; }

    public string Description { set; get; }
  }
}