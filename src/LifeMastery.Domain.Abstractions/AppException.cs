namespace LifeMastery.Domain.Abstractions;

public class AppException(string message) : Exception(message)
{
}