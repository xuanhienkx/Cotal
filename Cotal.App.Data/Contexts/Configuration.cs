using System;
using System.Collections.Generic;
using System.Linq;
using Cotal.App.Model.Models;
using Cotal.Core.Common;

namespace Cotal.App.Data.Contexts
{
    public interface IDbCotalInitializer
    {
        void Initialize();
    }
    public class DbCotalInitializer : IDbCotalInitializer
    {
        private CotalContex contex;

        public DbCotalInitializer(CotalContex dbcontex)
        {
            contex = dbcontex;
        }

        public void Initialize()
        {
            contex.Database.EnsureCreated();
            CreateSlide(contex);
            CreatePage(contex);
            CreateContactDetail(contex);

            CreateConfigTitle(contex);
            CreateFooter(contex);
            CreateFunction(contex);
        }
        private void CreateFunction(CotalContex context)
        {
            if (!context.Functions.Any())
            {
                context.Functions.AddRange(new List<Function>()
              {
                  new Function() {Id = "SYSTEM", Name = "Hệ thống",ParentId = null,DisplayOrder = 1,Status = true,URL = "/",IconCss = "fa-desktop"  },
                  new Function() {Id = "ROLE", Name = "Nhóm",ParentId = "SYSTEM",DisplayOrder = 1,Status = true,URL = "/main/role/index",IconCss = "fa-home"  },
                  new Function() {Id = "FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",DisplayOrder = 2,Status = true,URL = "/main/function/index",IconCss = "fa-home"  },
                  new Function() {Id = "USER", Name = "Người dùng",ParentId = "SYSTEM",DisplayOrder =3,Status = true,URL = "/main/user/index",IconCss = "fa-home"  },
                  new Function() {Id = "ACTIVITY", Name = "Nhật ký",ParentId = "SYSTEM",DisplayOrder = 4,Status = true,URL = "/main/activity/index",IconCss = "fa-home"  },
                  new Function() {Id = "ERROR", Name = "Lỗi",ParentId = "SYSTEM",DisplayOrder = 5,Status = true,URL = "/main/error/index",IconCss = "fa-home"  },
                  new Function() {Id = "SETTING", Name = "Cấu hình",ParentId = "SYSTEM",DisplayOrder = 6,Status = true,URL = "/main/setting/index",IconCss = "fa-home"  },

                  new Function() {Id = "PRODUCT",Name = "Sản phẩm",ParentId = null,DisplayOrder = 2,Status = true,URL = "/",IconCss = "fa-chevron-down"  },
                  new Function() {Id = "PRODUCT_CATEGORY",Name = "Danh mục",ParentId = "PRODUCT",DisplayOrder =1,Status = true,URL = "/main/product-category/index",IconCss = "fa-chevron-down"  },
                  new Function() {Id = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",DisplayOrder = 2,Status = true,URL = "/main/product/index",IconCss = "fa-chevron-down"  },
                  new Function() {Id = "ORDER",Name = "Hóa đơn",ParentId = "PRODUCT",DisplayOrder = 3,Status = true,URL = "/main/order/index",IconCss = "fa-chevron-down"  },

                  new Function() {Id = "CONTENT",Name = "Nội dung",ParentId = null,DisplayOrder = 3,Status = true,URL = "/",IconCss = "fa-table"  },
                  new Function() {Id = "POST_CATEGORY",Name = "Danh mục",ParentId = "CONTENT",DisplayOrder = 1,Status = true,URL = "/main/post-category/index",IconCss = "fa-table"  },
                  new Function() {Id = "POST",Name = "Bài viết",ParentId = "CONTENT",DisplayOrder = 2,Status = true,URL = "/main/post/index",IconCss = "fa-table"  },

                  new Function() {Id = "UTILITY",Name = "Tiện ích",ParentId = null,DisplayOrder = 4,Status = true,URL = "/",IconCss = "fa-clone"  },
                  new Function() {Id = "FOOTER",Name = "Footer",ParentId = "UTILITY",DisplayOrder = 1,Status = true,URL = "/main/footer/index",IconCss = "fa-clone"  },
                  new Function() {Id = "FEEDBACK",Name = "Phản hồi",ParentId = "UTILITY",DisplayOrder = 2,Status = true,URL = "/main/feedback/index",IconCss = "fa-clone"  },
                  new Function() {Id = "ANNOUNCEMENT",Name = "Thông báo",ParentId = "UTILITY",DisplayOrder = 3,Status = true,URL = "/main/announcement/index",IconCss = "fa-clone"  },
                  new Function() {Id = "CONTACT",Name = "Lien hệ",ParentId = "UTILITY",DisplayOrder = 4,Status = true,URL = "/main/contact/index",IconCss = "fa-clone"  },

                  new Function() {Id = "REPORT",Name = "Báo cáo",ParentId = null,DisplayOrder = 5,Status = true,URL = "/",IconCss = "fa-bar-chart-o"  },
                  new Function() {Id = "REVENUES",Name = "Báo cáo doanh thu",ParentId = "REPORT",DisplayOrder = 1,Status = true,URL = "/main/report/revenues",IconCss = "fa-bar-chart-o"  },
                  new Function() {Id = "ACCESS",Name = "Báo cáo truy cập",ParentId = "REPORT",DisplayOrder = 2,Status = true,URL = "/main/report/visitor",IconCss = "fa-bar-chart-o"  },
                  new Function() {Id = "READER",Name = "Báo cáo độc giả",ParentId = "REPORT",DisplayOrder = 3,Status = true,URL = "/main/report/reader",IconCss = "fa-bar-chart-o"  },

              });
                context.SaveChanges();
            }
        }

        private void CreateConfigTitle(CotalContex context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ TeduShop",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ TeduShop",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ TeduShop",
                });
            }
        }

        private void CreateFooter(CotalContex context)
        {
            if (context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = content
                });
                context.SaveChanges();
            }
        }

        private void CreateSlide(CotalContex context)
        {
            if (!context.Slides.Any())
            {
                List<Slide> listSlide = new List<Slide>()
              {
                  new Slide() {
                      Name ="Slide 1",
                      DisplayOrder =1,
                      Status =true,
                      Url ="#",
                      Image ="/Assets/client/images/bag.jpg",
                      Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                  new Slide() {
                      Name ="Slide 2",
                      DisplayOrder =2,
                      Status =true,
                      Url ="#",
                      Image ="/Assets/client/images/bag1.jpg",
                      Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"},
              };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(CotalContex context)
        {
            if (!context.Pages.Any())
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    /* foreach (var eve in ex.EntityValidationErrors)
                     {
                         Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                         foreach (var ve in eve.ValidationErrors)
                         {
                             Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                         }
                     }*/

                }
            }
        }

        private void CreateContactDetail(CotalContex context)
        {
            if (!context.ContactDetails.Any())
            {
                try
                {
                    var contactDetail = new ContactDetail()
                    {
                        Name = "Cotal",
                        Address = "Villa số 3, 02 Bis Nguyễn Thị Minh Khai, Quận 1, TP Hồ Chí Minh",
                        Email = "infro@cotal.com.vn",
                        Lat = 10.790648,
                        Lng = 106.705371,
                        Phone = "095423233",
                        Website = "http://cotal.com.vn",
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    /*foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }*/
                }
            }
        }
    }
}