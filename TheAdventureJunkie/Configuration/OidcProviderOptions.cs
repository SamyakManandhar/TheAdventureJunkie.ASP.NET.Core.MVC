using System.ComponentModel.DataAnnotations;

namespace TheAdventureJunkie.Configuration
{
    public class OidcProviderOptions
    {
        public const string Google = "Google";
        public const string Microsoft = "Microsoft";

        [Required]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
