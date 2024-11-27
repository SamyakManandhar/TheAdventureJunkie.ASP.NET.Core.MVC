using System.IO.Pipelines;

namespace TheAdventureJunkie.Models
{
	public static class DbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			TheAdventureJunkieDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<TheAdventureJunkieDbContext>();

			if (!context.Categories.Any())
			{
				context.Categories.AddRange(Categories.Select(c => c.Value));
			}

			if (!context.Events.Any())
			{
				context.AddRange
				(new Event { Name = "Bunjee Jumping", Price = 249.95M, ShortDescription = "Death Fall", LongDescription = "Experience Life as you fall through 5000ft in the sky. Watch as your life passes you by. Done by professional and certified trainers only.", Category = Categories["Sucidal"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/bunjee.jpg", EventDateTime = new DateTime(2025, 01, 02, 16, 15, 00) },

				new Event { Name = "Cable car", Price = 18.95M, ShortDescription = "Ride to mystical temple", LongDescription = "Join us on a cable car jouney from the bottom of the hill to the top, to visit a mystical kali temple located on the top. Lunch provided and family friendly atmosphere.", Category = Categories["Novice"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/cablecar.jpg", EventDateTime = new DateTime(2024, 12, 30, 05, 00, 00) },

				new Event { Name = "Cycling Trip", Price = 10.00M, ShortDescription = "The cycling trip", LongDescription = "Our organised cycling trip through the valley. Coordinated group trip following the famous cycle routes, hence 6 hours of fun. All cycles are adventure specs with 8 gear and disc brakes", Category = Categories["Novice"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/cycling.jpg", EventDateTime = new DateTime(2025, 01, 06, 12, 30, 00) },

				new Event { Name = "Hot Air Balloon", Price = 120.95M, ShortDescription = "Cruise through the skies", LongDescription = "Romantic trip, as you navigate the skies with our driver, taking in the breathtaking views that come with it. Be prepared for a life changing experience.", Category = Categories["Novice"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/hotair.jpg", EventDateTime = new DateTime(2025, 02, 10, 18, 20, 00) },

				new Event { Name = "Kayaking", Price = 82.95M, ShortDescription = "Fight off the waves", LongDescription = "Feel the adrenaline rush as you fight to keep aboard in kayak, in the tremendous river of yellowstone, to gain the title of the river rider. Conquer the craft of river riding with our expert tutors.", Category = Categories["Experienced"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/kayaking.jpg", EventDateTime = new DateTime(2025, 06, 12, 13, 45, 00) },

				new Event { Name = "Paragliding", Price = 133.95M, ShortDescription = "Fly today", LongDescription = "Soar like a bird and embrace the freedom of the skies with our thrilling paragliding adventures! Glide over breathtaking landscapes and feel the rush of wind as you take in panoramic views like never before. Perfect for adrenaline seekers and nature lovers alike.", Category = Categories["Experienced"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/paragliding.jpeg", EventDateTime = new DateTime(2025, 02, 10, 15, 20, 00) },

				new Event { Name = "Rafting", Price = 99.50M, ShortDescription = "Fight the nature", LongDescription="Conquer roaring rapids and navigate the twists and turns of wild rivers with our action-packed rafting experiences. Whether you’re a beginner or an expert, dive into the heart-pounding excitement of white-water adventure.", Category = Categories["Experienced"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/rafting.jpg", EventDateTime = new DateTime(2025, 09, 10, 11, 25, 00) },

				new Event { Name = "Safari", Price = 99.99M, ShortDescription = "Welcome to the Jungle", LongDescription = "Embark on a wild safari and explore untamed landscapes brimming with exotic wildlife. Whether on foot, by jeep, or boat, uncover the magic of the wilderness and create memories to last a lifetime.", Category = Categories["Novice"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/safari.jpg", EventDateTime = new DateTime(2025, 11, 10, 20, 10, 00) },

				new Event { Name = "Rock Climbing", Price = 39.99M, ShortDescription = "Climb to your sucess", LongDescription = "Challenge your limits and climb to new heights with our rock-climbing expeditions. From beginners to seasoned climbers, tackle rugged cliffs and experience the ultimate test of strength and determination.", Category = Categories["Experienced"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/rock.jpg", EventDateTime = new DateTime(2024, 12, 10, 14, 00, 00) },

				new Event { Name = "Sky Diving", Price = 450.00M, ShortDescription = "Please seek help!", LongDescription = "Feel the ultimate adrenaline rush as you leap from thousands of feet above the ground! Our expert-led skydiving adventures offer unparalleled views and an unforgettable freefall that’ll leave you exhilarated.", Category = Categories["Sucidal"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/skydiving.png", EventDateTime = new DateTime(2025, 05, 10, 10, 15, 00) },

				new Event { Name = "Trekking", Price = 50.00M, ShortDescription = "Trek to your success", LongDescription = "Discover stunning trails and hidden natural gems with our immersive trekking adventures. From serene landscapes to challenging peaks, reconnect with nature and enjoy the journey of a lifetime.", Category = Categories["Experienced"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/trekking.jpg", EventDateTime = new DateTime(2025, 11, 29, 05, 30, 00) },

				new Event { Name = "Ultra Flight", Price = 79.40M, ShortDescription = "Check in now", LongDescription = "Experience the thrill of ultraflight, where you’ll soar above stunning vistas in a lightweight aircraft. Enjoy unmatched views, speed, and excitement in this high-flying adventure.", Category = Categories["Novice"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/ultraflight.jpg", EventDateTime = new DateTime(2025, 12, 06, 11, 45, 00) },

				new Event { Name = "Zipline", Price = 21.99M, ShortDescription = "Zip across town, literally", LongDescription = "Fly through the air on our exhilarating zipline adventures! Feel the rush of adrenaline as you speed through forests, over canyons, or across valleys, enjoying the thrill of heights and breathtaking scenery.", Category = Categories["Experienced"], ImageUrl = "https://adventureblob.blob.core.windows.net/eventblobs/zipline.jpg", EventDateTime = new DateTime(2025, 07, 18, 16, 46, 00) }
				);
			}

			context.SaveChanges();
		}

		private static Dictionary<string, Category>? categories;

		public static Dictionary<string, Category> Categories
		{
			get
			{
				if (categories == null)
				{
					var genresList = new Category[]
					{
						new Category { CategoryName = "Novice" },
						new Category { CategoryName = "Experienced" },
						new Category { CategoryName = "Sucidal" }
					};

					categories = new Dictionary<string, Category>();

					foreach (Category genre in genresList)
					{
						categories.Add(genre.CategoryName, genre);
					}
				}

				return categories;
			}
		}
	}
}
