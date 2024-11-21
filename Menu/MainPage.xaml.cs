using System.Collections.Generic;
using System.Linq;

namespace Menu;

public partial class MainPage : ContentPage
{
    private List<MenuItem> items;

    public MainPage()
    {
        InitializeComponent();
        InitializeMenu();
        UpdateMenuList(items); // Показать все блюда при запуске
    }

    private void InitializeMenu()
    {
        items = new List<MenuItem>
        {
            new MenuItem("Салат", 350m, 150, 200, new List<string> { "орехи" }, new List<string> { "овощи", "салат", "орехи", "масло" }),
            new MenuItem("Бургер", 450m, 500, 600, new List<string> { "глютен", "молочные продукты" }, new List<string> { "булка", "котлета", "сыр", "соус" }),
            new MenuItem("Пицца", 600m, 700, 800, new List<string> { "глютен", "молочные продукты" }, new List<string> { "тесто", "сыр", "томат", "пепперони" }),
            new MenuItem("Паста", 300m, 600, 700, new List<string> { "глютен" }, new List<string> { "макароны", "сливки", "сыр", "специи" }),
            new MenuItem("Суши", 800m, 300, 350, new List<string> { "рыба", "соевый соус" }, new List<string> { "рис", "рыба", "нори", "соус" }),
            new MenuItem("Суп", 250m, 150, 180, new List<string>(), new List<string> { "вода", "овощи", "мясо" }),
            new MenuItem("Стейк", 1200m, 700, 750, new List<string>(), new List<string> { "мясо", "специи", "масло" }),
            new MenuItem("Десерт", 200m, 400, 500, new List<string> { "орехи", "молочные продукты" }, new List<string> { "сахар", "молоко", "шоколад", "орехи" }),
            new MenuItem("Кофе", 150m, 0, 100, new List<string>(), new List<string> { "кофе", "вода" }),
            new MenuItem("Сок", 100m, 50, 60, new List<string>(), new List<string> { "фрукты", "вода" })
        };
    }

    private void UpdateMenuList(IEnumerable<MenuItem> menuItems)
    {
        var displayItems = menuItems.Select(item => new
        {
            Name = item.Name,
            Details = $"{item.Price} руб | {item.Calories} ккал | {item.EnergyValue} кДж | {string.Join(", ", item.Ingredients)}"
        }).ToList();

        MenuList.ItemsSource = displayItems;
    }

    private void OnFilterByPriceClicked(object sender, EventArgs e)
    {
        if (decimal.TryParse(PriceEntry.Text, out var maxPrice))
        {
            var filteredItems = items.Where(i => i.Price <= maxPrice).ToList();
            UpdateMenuList(filteredItems);
        }
    }

    private void OnFilterByCaloriesClicked(object sender, EventArgs e)
    {
        if (int.TryParse(CaloriesEntry.Text, out var maxCalories))
        {
            var filteredItems = items.Where(i => i.Calories <= maxCalories).ToList();
            UpdateMenuList(filteredItems);
        }
    }

    private void OnFilterByAllergenClicked(object sender, EventArgs e)
    {
        var allergen = AllergenEntry.Text?.Trim();
        if (!string.IsNullOrEmpty(allergen))
        {
            var filteredItems = items.Where(i => !i.Allergens.Contains(allergen, StringComparer.OrdinalIgnoreCase)).ToList();
            UpdateMenuList(filteredItems);
        }
    }

    private void OnFilterByIngredientsClicked(object sender, EventArgs e)
    {
        var ingredientsInput = IngredientsEntry.Text;
        if (!string.IsNullOrEmpty(ingredientsInput))
        {
            var ingredients = ingredientsInput.Split(',').Select(i => i.Trim().ToLower()).ToList();
            var filteredItems = items.Where(item => item.Ingredients.Any(ingredient => ingredients.Contains(ingredient.ToLower()))).ToList();
            UpdateMenuList(filteredItems);
        }
    }
}

public class MenuItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Calories { get; set; }
    public int EnergyValue { get; set; }
    public List<string> Allergens { get; set; }
    public List<string> Ingredients { get; set; }

    public MenuItem(string name, decimal price, int calories, int energyValue, List<string> allergens, List<string> ingredients)
    {
        Name = name;
        Price = price;
        Calories = calories;
        EnergyValue = energyValue;
        Allergens = allergens;
        Ingredients = ingredients;
    }
}
