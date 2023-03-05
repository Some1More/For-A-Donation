﻿namespace For_A_Donation.Exceptions;

public class ObjectNotUniqueException: ArgumentException
{
    public ObjectNotUniqueException() : base() { }
    public ObjectNotUniqueException(string objectName) : base(objectName) { }
    public ObjectNotUniqueException(string objectName, string message)
            : base(message, objectName) { }
}
