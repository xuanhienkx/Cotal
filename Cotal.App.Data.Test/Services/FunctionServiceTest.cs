 
using Cotal.App.Business.Services;
using Cotal.Core.InfacBase.Uow;
using Moq;
using Xunit;

namespace Cotal.App.Data.Test.Services
{
    public class FunctionServiceTest
    {
        private IFunctionService functionService;
        private Mock<IUowProvider> _mockUowProvider;

        public FunctionServiceTest()
        {
            _mockUowProvider = new Mock<IUowProvider>();
           // this.functionService = new FunctionService(_mockUowProvider.Object);
        }
        [Fact]
        public void GetAllWithPermissionTest()
        {
            //var reslt = functionService.GetAllWithPermission(1);
           // Assert.NotNull(reslt);   //ACCESS
        }
        [Fact]
        public void GetTest()
        {
            //var reslt = functionService.Get("ACCESS");
            //Assert.NotNull(reslt);   //ACCESS
        }
    }
}