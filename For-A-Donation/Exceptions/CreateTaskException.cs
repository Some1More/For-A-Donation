namespace For_A_Donation.Exceptions;

public class CreateTaskException : ArgumentException
{
    public CreateTaskException() : base() { }
    public CreateTaskException(string objectName) : base(objectName) { }
    public CreateTaskException(string objectName, string message) :
        base(message, objectName)
    { }
}
