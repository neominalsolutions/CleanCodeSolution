using CleanCodeSolution.Domain.Services;

namespace CleanCodeSolution.Domain.Test
{
  public class UnitTest1
  {
    [Fact]
    public void Test1()
    {
      var service = new IndividualAccountService();

     
      // Arrangement
      // servis instance yada mock nesne setup yaptýðýmýz yer
      bool ok = false;
      service.Close("324324","Kapanýþ");

      // Act
      // servis method çaðýrýsý

      // Assert
      // actual deðer ile expected value kontrol ettiðimiz yer.
      Assert.True(ok);
    }
  }
}