namespace YF.Application.Common.Exceptions;

public class NotFoundException : CustomException
{
    public NotFoundException(string msg)
        : base(msg, null, System.Net.HttpStatusCode.NotFound)
    {
    }
}
