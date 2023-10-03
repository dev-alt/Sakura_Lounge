using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using Windows.Media.Audio;

namespace SakuraLounge.Classes
{
    public class Drink
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Recipe { get; set; }
        public string Mix { get; set; }
        public string Price { get; set; }
        public Drink()
        {
            Name = "";
            ImageName = "";
            Recipe = "";
            Mix = "";
            Price = "";
        }
        public Drink(string inputName, string inputImageName, string inputRecipe, string inputMix, string price)
        {
            Name = inputName;
            ImageName = inputImageName;
            Recipe = inputRecipe;
            Mix = inputMix;
            Price = price;

        }
        public static Dictionary<string, Drink> DrinkDictionary = new Dictionary<string, Drink>()
        {

            {
                "Matcha Zen Latte",
                new Drink
                {
                    Name = "Matcha Zen Latte",
                    Recipe = "A soothing green tea latte made with premium matcha powder, steamed milk, and a touch of honey. Garnished with a sprinkle of matcha for an authentic and serene experience.",
                    Mix = "Put all the ingredients in a shaker with ice and shake. Strain into a martini glass and garnish with a lime wheel or zest.",
                    Price = "10"
                }
            },
            {
                "Sakura Sparkler",
                new Drink
                {
                    Name = "Sakura Sparkler",
                    Recipe = "A refreshing sparkling beverage infused with the delicate flavor of cherry blossoms. It combines sparkling water, cherry blossom syrup, and a splash of lemon juice for a floral and bubbly treat.",
                    Mix = "In a glass, combine sparkling water, cherry blossom syrup, and lemon juice. Stir gently and serve chilled.",
                    Price = "10"
}
            },
            {
                "Dragon's Fire Sake",
                new Drink
                {
                    Name = "Dragon's Fire Sake",
                    ImageName = "dragons_fire_sake.png",
                    Recipe = "A fiery and daring sake cocktail crafted with premium sake, chili-infused honey, and a hint of ginger. Served in a traditional sake cup with a garnish of red chili for a touch of spice.",
                    Mix = "In a cocktail shaker, combine sake, chili-infused honey, and ginger. Shake well and strain into a sake cup. Garnish with a red chili pepper.",
                    Price = "10"
}
            },
            {
                "Bamboo Forest Mojito",
                new Drink
                {
                    Name = "Bamboo Forest Mojito",
                    Recipe = "A tropical twist on the classic mojito. This cocktail features rum, fresh mint, lime, and coconut water, topped with bamboo shoots for a unique presentation.",
                    Mix = "In a glass, muddle fresh mint leaves and lime juice. Add rum and coconut water. Stir and garnish with bamboo shoots.",
                    Price = "10"
}
            },
            {
                "Fortune Teller's Fizz",
                new Drink
                {
                    Name = "Fortune Teller's Fizz",
                    Recipe = "A mystical and visually captivating cocktail that changes colors as you sip. Made with butterfly pea flower tea, gin, and elderflower liqueur, it's a fortune worth sipping.",
                    Mix = "Brew butterfly pea flower tea and let it cool. In a glass, combine gin and elderflower liqueur. Add butterfly pea flower tea and watch the colors change.",
                    Price = "10"
}
            },
            {
                "Lucky Lotus Martini",
                new Drink
                {
                    Name = "Lucky Lotus Martini",
                    Recipe = "A sophisticated martini with a hint of floral essence. It combines vodka, lychee liqueur, and lotus blossom syrup, garnished with an edible lotus flower petal.",
                    Mix = "In a cocktail shaker, combine vodka, lychee liqueur, and lotus blossom syrup. Shake and strain into a martini glass. Garnish with an edible lotus flower petal.",
                    Price = "10"
}
            }

        };


    };
}
