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
      // servis instance yada mock nesne setup yapt���m�z yer
      bool ok = false;
      service.Close("324324","Kapan��");

      // Act
      // servis method �a��r�s�

      // Assert
      // actual de�er ile expected value kontrol etti�imiz yer.
      Assert.True(ok);
    }
  }
}