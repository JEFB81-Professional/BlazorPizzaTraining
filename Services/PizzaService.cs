using BlazingPizza.Data;

namespace BlazingPizza.Services
{
    public class PizzaService
    {
        public async Task<List<PizzaModel>> GetPizzasAsync()
        {
            // In a real app, this method would get data from a database or a web API.
            PizzaModel[] pizzas =
            {   
                new() { PizzaId = 1, Name = "Margherita", Description = "Tomato sauce, mozzarella, and basil", Price = 8.50m, Vegetarian = true, Vegan = false, ImageUrl = "img/pizzas/margherita.jpg"  },
                new() { PizzaId = 2, Name = "Pepperoni", Description = "Tomato sauce, mozzarella, and pepperoni", Price = 9.50m, Vegetarian = false, Vegan = false, ImageUrl = "img/pizzas/pepperoni.jpg" },
                new() { PizzaId = 3, Name = "Veggie Delight", Description = "Tomato sauce, mozzarella, bell peppers, onions, mushrooms, and olives", Price = 10.00m, Vegetarian = true, Vegan = false , ImageUrl = "img/pizzas/salad.jpg"  },
                new() { PizzaId = 4, Name = "Vegan Special", Description = "Tomato sauce, vegan cheese, spinach, artichokes, and sun-dried tomatoes", Price = 11.00m, Vegetarian = true, Vegan = true, ImageUrl = "img/pizzas/mushroom.jpg"  }
            };

            return pizzas.ToList();
        }
    }
}   