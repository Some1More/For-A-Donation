namespace For_A_Donation.Services.Interfaces.Exceptions;

public class ForbiddenExeption: Exception
{
    public ForbiddenExeption(string msg): base(msg)
    { }
}
