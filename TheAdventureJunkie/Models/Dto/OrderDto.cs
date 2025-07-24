namespace TheAdventureJunkie.Models.Dto
{
    public record OrderDto(
            int OrderId,
            string FirstName,
            string LastName,
            string AddressLine1,
            string? AddressLine2,
            string ZipCode,
            string City,
            string? State,
            string Country,
            string PhoneNumber,
            string Email,
            decimal OrderTotal,
            DateTime OrderPlaced
        );
}
