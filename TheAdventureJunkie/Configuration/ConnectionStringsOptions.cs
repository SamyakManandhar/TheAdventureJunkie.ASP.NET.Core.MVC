using System.ComponentModel.DataAnnotations;

namespace TheAdventureJunkie.Configuration
{
    public class ConnectionStringsOptions
    {
        [Required]
        public required string TheAdventureJunkieDbContextConnection { get; set; }
    }
}
