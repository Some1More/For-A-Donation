namespace AnimalAPI.Exceptions;

public class ForbiddenExeption: Exception
{
    public ForbiddenExeption(string msg): base(msg)
    { }
}
