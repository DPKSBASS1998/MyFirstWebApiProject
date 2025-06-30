namespace KBDTypeServer.Domain.ValueObjects
{
    public record OrderInitData(
        string FirstName,
        string LastName,
        string? Email,
        string PhoneNumber,
        string Region,
        string City,
        string Street,
        string Building,
        string? Apartment,
        string PostalCode,
        string? Comment,
        List<OrderItem> Items
    );
}