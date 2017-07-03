using AutoMapper;
using Cotal.App.Business.ViewModels.Common;
using Cotal.App.Business.ViewModels.Post;
using Cotal.App.Business.ViewModels.System;
using Cotal.App.Model.Models;
using Cotal.Core.Identity.Models;

namespace Cotal.App.Business.Mappings
{
  public class ViewModelMappingProfile : Profile
  {
    public ViewModelMappingProfile()
    {
      CreateMap<Function, FunctionViewModel>();
      CreateMap<Announcement, AnnouncementViewModel>();
      CreateMap<AnnouncementUser, AnnouncementUserViewModel>();
      CreateMap<AppUser, AppUserViewModel>();
      CreateMap<AppRole, AppRoleViewModel>();
      CreateMap<Permission, PermissionViewModel>();
      CreateMap<Post, PostViewModel>();
      CreateMap<PostCategory, PostCategoryViewModel>();
      CreateMap<Tag, TagViewModel>();
      CreateMap<Footer, FooterViewModel>();
      CreateMap<Slide, SlideViewModel>();
      CreateMap<Page, PageViewModel>();
    }
    /* public static void Configure()
     {
         Mapper.Initialize(cfg =>
         {
             //cfg.CreateMap<Post, PostViewModel>();
             //cfg.CreateMap<PostCategory, PostCategoryViewModel>();
             //cfg.CreateMap<Tag, TagViewModel>();
             //cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
             //cfg.CreateMap<Product, ProductViewModel>();
             //cfg.CreateMap<ProductTag, ProductTagViewModel>();
             //cfg.CreateMap<Footer, FooterViewModel>();
             //cfg.CreateMap<Slide, SlideViewModel>();
             //cfg.CreateMap<Page, PageViewModel>();
             //cfg.CreateMap<ContactDetail, ContactDetailViewModel>();
             //cfg.CreateMap<AppRole, AppRoleViewModel>();
             //cfg.CreateMap<AppUser, AppUserViewModel>();
             cfg.CreateMap<Function, FunctionViewModel>();
             //cfg.CreateMap<Permission, PermissionViewModel>();
             //cfg.CreateMap<ProductImage, ProductImageViewModel>();
             //cfg.CreateMap<ProductQuantity, ProductQuantityViewModel>();
             //cfg.CreateMap<Color, ColorViewModel>();
             //cfg.CreateMap<Size, SizeViewModel>();
             //cfg.CreateMap<Order, OrderViewModel>();
             //cfg.CreateMap<OrderDetail, OrderDetailViewModel>();
             //cfg.CreateMap<Announcement, AnnouncementViewModel>();
             //cfg.CreateMap<AnnouncementUser, AnnouncementUserViewModel>();
         });
     }*/
  }
}