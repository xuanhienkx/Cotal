using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cotal.App.Data.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        "Announcements",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Content = table.Column<string>(nullable: true),
          CreatedDate = table.Column<DateTime>(nullable: false),
          Status = table.Column<bool>(nullable: false),
          Title = table.Column<string>(maxLength: 250, nullable: false),
          UserId = table.Column<string>(maxLength: 128, nullable: true),
          UserName = table.Column<string>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Announcements", x => x.Id); });

      migrationBuilder.CreateTable(
        "Colors",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Code = table.Column<string>(maxLength: 250, nullable: true),
          Name = table.Column<string>(maxLength: 250, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Colors", x => x.Id); });

      migrationBuilder.CreateTable(
        "ContactDetails",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Address = table.Column<string>(maxLength: 250, nullable: true),
          Email = table.Column<string>(maxLength: 250, nullable: true),
          Lat = table.Column<double>(nullable: true),
          Lng = table.Column<double>(nullable: true),
          Name = table.Column<string>(maxLength: 250, nullable: false),
          Other = table.Column<string>(nullable: true),
          Phone = table.Column<string>(maxLength: 50, nullable: true),
          Status = table.Column<bool>(nullable: false),
          Website = table.Column<string>(maxLength: 250, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_ContactDetails", x => x.Id); });

      migrationBuilder.CreateTable(
        "Errors",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          CreatedDate = table.Column<DateTime>(nullable: false),
          Message = table.Column<string>(nullable: true),
          StackTrace = table.Column<string>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Errors", x => x.Id); });

      migrationBuilder.CreateTable(
        "Feedbacks",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          CreatedDate = table.Column<DateTime>(nullable: false),
          Email = table.Column<string>(maxLength: 250, nullable: true),
          Message = table.Column<string>(maxLength: 500, nullable: true),
          Name = table.Column<string>(maxLength: 250, nullable: false),
          Status = table.Column<bool>(nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Feedbacks", x => x.Id); });

      migrationBuilder.CreateTable(
        "Footers",
        table => new
        {
          Id = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
          Content = table.Column<string>(nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Footers", x => x.Id); });

      migrationBuilder.CreateTable(
        "Functions",
        table => new
        {
          Id = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
          DisplayOrder = table.Column<int>(nullable: false),
          IconCss = table.Column<string>(nullable: true),
          Name = table.Column<string>(maxLength: 50, nullable: false),
          ParentId = table.Column<string>("varchar(50)", maxLength: 50, nullable: true),
          Status = table.Column<bool>(nullable: false),
          URL = table.Column<string>(maxLength: 256, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Functions", x => x.Id);
          table.ForeignKey(
            "FK_Functions_Functions_ParentId",
            x => x.ParentId,
            "Functions",
            "Id",
            onDelete: ReferentialAction.Restrict);
        });

      migrationBuilder.CreateTable(
        "Pages",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Alias = table.Column<string>("varchar(256)", maxLength: 256, nullable: false),
          Content = table.Column<string>(nullable: true),
          CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
          CreatedDate = table.Column<DateTime>(nullable: true),
          MetaDescription = table.Column<string>(maxLength: 256, nullable: true),
          MetaKeyword = table.Column<string>(maxLength: 256, nullable: true),
          Name = table.Column<string>(maxLength: 256, nullable: false),
          Status = table.Column<bool>(nullable: false),
          UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
          UpdatedDate = table.Column<DateTime>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Pages", x => x.Id); });

      migrationBuilder.CreateTable(
        "PostCategories",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Alias = table.Column<string>("varchar(256)", maxLength: 256, nullable: false),
          CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
          CreatedDate = table.Column<DateTime>(nullable: true),
          Description = table.Column<string>(maxLength: 500, nullable: true),
          DisplayOrder = table.Column<int>(nullable: true),
          HomeFlag = table.Column<bool>(nullable: true),
          Image = table.Column<string>(maxLength: 256, nullable: true),
          MetaDescription = table.Column<string>(maxLength: 256, nullable: true),
          MetaKeyword = table.Column<string>(maxLength: 256, nullable: true),
          Name = table.Column<string>(maxLength: 256, nullable: false),
          ParentID = table.Column<int>(nullable: true),
          Status = table.Column<bool>(nullable: false),
          UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
          UpdatedDate = table.Column<DateTime>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_PostCategories", x => x.Id); });

      migrationBuilder.CreateTable(
        "ProductCategories",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Alias = table.Column<string>(maxLength: 256, nullable: false),
          CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
          CreatedDate = table.Column<DateTime>(nullable: true),
          Description = table.Column<string>(maxLength: 500, nullable: true),
          DisplayOrder = table.Column<int>(nullable: true),
          HomeFlag = table.Column<bool>(nullable: true),
          HomeOrder = table.Column<int>(nullable: true),
          Image = table.Column<string>(maxLength: 256, nullable: true),
          MetaDescription = table.Column<string>(maxLength: 256, nullable: true),
          MetaKeyword = table.Column<string>(maxLength: 256, nullable: true),
          Name = table.Column<string>(maxLength: 256, nullable: false),
          ParentId = table.Column<int>(nullable: true),
          Status = table.Column<bool>(nullable: false),
          UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
          UpdatedDate = table.Column<DateTime>(nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_ProductCategories", x => x.Id); });

      migrationBuilder.CreateTable(
        "Sizes",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Name = table.Column<string>(maxLength: 250, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Sizes", x => x.Id); });

      migrationBuilder.CreateTable(
        "Slides",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Content = table.Column<string>(nullable: true),
          Description = table.Column<string>(maxLength: 256, nullable: true),
          DisplayOrder = table.Column<int>(nullable: true),
          Image = table.Column<string>(maxLength: 256, nullable: true),
          Name = table.Column<string>(maxLength: 256, nullable: false),
          Status = table.Column<bool>(nullable: false),
          Url = table.Column<string>(maxLength: 256, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_Slides", x => x.Id); });

      migrationBuilder.CreateTable(
        "SupportOnlines",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Department = table.Column<string>(maxLength: 50, nullable: true),
          DisplayOrder = table.Column<int>(nullable: true),
          Email = table.Column<string>(maxLength: 50, nullable: true),
          Facebook = table.Column<string>(maxLength: 50, nullable: true),
          Mobile = table.Column<string>(maxLength: 50, nullable: true),
          Name = table.Column<string>(maxLength: 50, nullable: false),
          Skype = table.Column<string>(maxLength: 50, nullable: true),
          Status = table.Column<bool>(nullable: false),
          Yahoo = table.Column<string>(maxLength: 50, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_SupportOnlines", x => x.Id); });

      migrationBuilder.CreateTable(
        "SystemConfigs",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Code = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
          ValueInt = table.Column<int>(nullable: true),
          ValueString = table.Column<string>(maxLength: 50, nullable: true)
        },
        constraints: table => { table.PrimaryKey("PK_SystemConfigs", x => x.Id); });

      migrationBuilder.CreateTable(
        "Tags",
        table => new
        {
          Id = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
          Name = table.Column<string>(maxLength: 50, nullable: false),
          Type = table.Column<string>(maxLength: 50, nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Tags", x => x.Id); });

      migrationBuilder.CreateTable(
        "VisitorStatistics",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          IpAddress = table.Column<string>(maxLength: 50, nullable: true),
          VisitedDate = table.Column<DateTime>(nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_VisitorStatistics", x => x.Id); });

      migrationBuilder.CreateTable(
        "AnnouncementUsers",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          AnnouncementId = table.Column<int>(nullable: false),
          HasRead = table.Column<bool>(nullable: false),
          UserId = table.Column<int>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AnnouncementUsers", x => x.Id);
          table.ForeignKey(
            "FK_AnnouncementUsers_Announcements_AnnouncementId",
            x => x.AnnouncementId,
            "Announcements",
            "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        "Permissions",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          CanCreate = table.Column<bool>(nullable: false),
          CanDelete = table.Column<bool>(nullable: false),
          CanRead = table.Column<bool>(nullable: false),
          CanUpdate = table.Column<bool>(nullable: false),
          FunctionId = table.Column<string>("varchar(50)", maxLength: 50, nullable: true),
          RoleId = table.Column<int>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Permissions", x => x.Id);
          table.ForeignKey(
            "FK_Permissions_Functions_FunctionId",
            x => x.FunctionId,
            "Functions",
            "Id",
            onDelete: ReferentialAction.Restrict);
        });

      migrationBuilder.CreateTable(
        "Posts",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Alias = table.Column<string>("varchar(256)", maxLength: 256, nullable: false),
          CategoryId = table.Column<int>(nullable: false),
          Content = table.Column<string>(nullable: true),
          CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
          CreatedDate = table.Column<DateTime>(nullable: true),
          Description = table.Column<string>(maxLength: 500, nullable: true),
          HomeFlag = table.Column<bool>(nullable: true),
          HotFlag = table.Column<bool>(nullable: true),
          Image = table.Column<string>(maxLength: 256, nullable: true),
          MetaDescription = table.Column<string>(maxLength: 256, nullable: true),
          MetaKeyword = table.Column<string>(maxLength: 256, nullable: true),
          Name = table.Column<string>(maxLength: 256, nullable: false),
          Status = table.Column<bool>(nullable: false),
          UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
          UpdatedDate = table.Column<DateTime>(nullable: true),
          ViewCount = table.Column<int>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Posts", x => x.Id);
          table.ForeignKey(
            "FK_Posts_PostCategories_CategoryId",
            x => x.CategoryId,
            "PostCategories",
            "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        "Products",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Alias = table.Column<string>(maxLength: 256, nullable: false),
          CategoryID = table.Column<int>(nullable: false),
          Content = table.Column<string>(nullable: true),
          CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
          CreatedDate = table.Column<DateTime>(nullable: true),
          Description = table.Column<string>(maxLength: 500, nullable: true),
          HomeFlag = table.Column<bool>(nullable: true),
          HotFlag = table.Column<bool>(nullable: true),
          IncludedVAT = table.Column<bool>(nullable: false),
          MetaDescription = table.Column<string>(maxLength: 256, nullable: true),
          MetaKeyword = table.Column<string>(maxLength: 256, nullable: true),
          Name = table.Column<string>(maxLength: 256, nullable: false),
          OriginalPrice = table.Column<decimal>(nullable: false),
          Price = table.Column<decimal>(nullable: false),
          PromotionPrice = table.Column<decimal>(nullable: true),
          Status = table.Column<bool>(nullable: false),
          Tags = table.Column<string>(nullable: true),
          ThumbnailImage = table.Column<string>(maxLength: 256, nullable: true),
          UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
          UpdatedDate = table.Column<DateTime>(nullable: true),
          ViewCount = table.Column<int>(nullable: true),
          Warranty = table.Column<int>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Products", x => x.Id);
          table.ForeignKey(
            "FK_Products_ProductCategories_CategoryID",
            x => x.CategoryID,
            "ProductCategories",
            "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        "PostTags",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          PostId = table.Column<int>(nullable: false),
          TagId = table.Column<string>("varchar(50)", maxLength: 50, nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_PostTags", x => x.Id);
          table.ForeignKey(
            "FK_PostTags_Posts_PostId",
            x => x.PostId,
            "Posts",
            "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            "FK_PostTags_Tags_TagId",
            x => x.TagId,
            "Tags",
            "Id",
            onDelete: ReferentialAction.Restrict);
        });

      migrationBuilder.CreateTable(
        "ProductImages",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          Caption = table.Column<string>(maxLength: 250, nullable: true),
          Path = table.Column<string>(maxLength: 250, nullable: true),
          ProductId = table.Column<int>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductImages", x => x.Id);
          table.ForeignKey(
            "FK_ProductImages_Products_ProductId",
            x => x.ProductId,
            "Products",
            "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        "ProductQuantities",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          ColorId = table.Column<int>(nullable: false),
          ProductId = table.Column<int>(nullable: false),
          Quantity = table.Column<int>(nullable: false),
          SizeId = table.Column<int>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductQuantities", x => x.Id);
          table.ForeignKey(
            "FK_ProductQuantities_Colors_ColorId",
            x => x.ColorId,
            "Colors",
            "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            "FK_ProductQuantities_Products_ProductId",
            x => x.ProductId,
            "Products",
            "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            "FK_ProductQuantities_Sizes_SizeId",
            x => x.SizeId,
            "Sizes",
            "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateTable(
        "ProductTags",
        table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
          ProductId = table.Column<int>(nullable: false),
          TagId = table.Column<string>(maxLength: 50, nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductTags", x => x.Id);
          table.ForeignKey(
            "FK_ProductTags_Products_ProductId",
            x => x.ProductId,
            "Products",
            "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            "FK_ProductTags_Tags_TagId",
            x => x.TagId,
            "Tags",
            "Id",
            onDelete: ReferentialAction.Restrict);
        });

      migrationBuilder.CreateIndex(
        "IX_AnnouncementUsers_AnnouncementId",
        "AnnouncementUsers",
        "AnnouncementId");

      migrationBuilder.CreateIndex(
        "IX_Functions_ParentId",
        "Functions",
        "ParentId");

      migrationBuilder.CreateIndex(
        "IX_Permissions_FunctionId",
        "Permissions",
        "FunctionId");

      migrationBuilder.CreateIndex(
        "IX_Posts_CategoryId",
        "Posts",
        "CategoryId");

      migrationBuilder.CreateIndex(
        "IX_PostTags_PostId",
        "PostTags",
        "PostId");

      migrationBuilder.CreateIndex(
        "IX_PostTags_TagId",
        "PostTags",
        "TagId");

      migrationBuilder.CreateIndex(
        "IX_Products_CategoryID",
        "Products",
        "CategoryID");

      migrationBuilder.CreateIndex(
        "IX_ProductImages_ProductId",
        "ProductImages",
        "ProductId");

      migrationBuilder.CreateIndex(
        "IX_ProductQuantities_ColorId",
        "ProductQuantities",
        "ColorId");

      migrationBuilder.CreateIndex(
        "IX_ProductQuantities_ProductId",
        "ProductQuantities",
        "ProductId");

      migrationBuilder.CreateIndex(
        "IX_ProductQuantities_SizeId",
        "ProductQuantities",
        "SizeId");

      migrationBuilder.CreateIndex(
        "IX_ProductTags_ProductId",
        "ProductTags",
        "ProductId");

      migrationBuilder.CreateIndex(
        "IX_ProductTags_TagId",
        "ProductTags",
        "TagId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        "AnnouncementUsers");

      migrationBuilder.DropTable(
        "ContactDetails");

      migrationBuilder.DropTable(
        "Errors");

      migrationBuilder.DropTable(
        "Feedbacks");

      migrationBuilder.DropTable(
        "Footers");

      migrationBuilder.DropTable(
        "Pages");

      migrationBuilder.DropTable(
        "Permissions");

      migrationBuilder.DropTable(
        "PostTags");

      migrationBuilder.DropTable(
        "ProductImages");

      migrationBuilder.DropTable(
        "ProductQuantities");

      migrationBuilder.DropTable(
        "ProductTags");

      migrationBuilder.DropTable(
        "Slides");

      migrationBuilder.DropTable(
        "SupportOnlines");

      migrationBuilder.DropTable(
        "SystemConfigs");

      migrationBuilder.DropTable(
        "VisitorStatistics");

      migrationBuilder.DropTable(
        "Announcements");

      migrationBuilder.DropTable(
        "Functions");

      migrationBuilder.DropTable(
        "Posts");

      migrationBuilder.DropTable(
        "Colors");

      migrationBuilder.DropTable(
        "Sizes");

      migrationBuilder.DropTable(
        "Products");

      migrationBuilder.DropTable(
        "Tags");

      migrationBuilder.DropTable(
        "PostCategories");

      migrationBuilder.DropTable(
        "ProductCategories");
    }
  }
}