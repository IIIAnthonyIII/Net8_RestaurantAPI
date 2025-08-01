namespace Restaurants.Domain.Exceptions;

public class NotFoundException (string resourceType, string resorceIdentifier) 
    : Exception($"{resourceType} con id {resorceIdentifier} no existe")
{
    //Otra forma de hacerlo es con un constructor que reciba un mensaje y lo pase a la clase base Exception
    //public NotFoundException (string message) : base(message)
    //{
    //}
}
