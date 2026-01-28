namespace BlazingPizza.Data
{
    /// <summary>
    /// The class defines the pizza's properties and data types. 
    /// </summary>
        public class PizzaModel
    {
        public int PizzaId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Vegetarian { get; set; }

        public bool Vegan { get; set; }
        public string ImageUrl { get; set; }
    }
}