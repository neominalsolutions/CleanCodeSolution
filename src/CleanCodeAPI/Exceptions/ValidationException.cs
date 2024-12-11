namespace CleanCodeAPI.Exceptions
{
  public abstract class ValidationException : Exception
  {
        protected ValidationException(string message):base(message)
        {
            
        }


    }

  public class RequiredValidationException : ValidationException
  {
    public RequiredValidationException(string message) : base(message)
    {
    }
  }
}
