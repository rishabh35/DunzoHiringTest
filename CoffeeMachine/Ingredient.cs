namespace CoffeeMachine
{
    /// <summary>
    /// Class that define an ingredient
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// The name of ingredient
        /// </summary>
        private string ingredientName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ingredientName">The name of constructor</param>
        public Ingredient(string ingredientName)
        {
            this.ingredientName = ingredientName;
        }

        /// <summary>
        /// Property to expose the ingredient name
        /// </summary>
        public string IngredientName
        {
            get 
            {
                return ingredientName;
            }
        }
    }
}
