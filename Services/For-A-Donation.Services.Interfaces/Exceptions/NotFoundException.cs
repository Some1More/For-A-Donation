namespace For_A_Donation.Services.Interfaces.Exceptions;

public class NotFoundException: ArgumentException
{
    public NotFoundException() : base() { }
    public NotFoundException(string objectName) : base(objectName) { }
    public NotFoundException(string objectName, string message) :
        base(message, objectName) { }
}
