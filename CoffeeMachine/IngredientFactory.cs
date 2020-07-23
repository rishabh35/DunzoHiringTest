using System.Collections.Generic;   //Dictionary

namespace CoffeeMachine
{
    /// <summary>
    /// Factory which returns one instance of object
    /// </summary>
    public static class IngredientFactory
    {
        private static Dictionary<string, Ingredient> ingredientList = new Dictionary<string,Ingredient>();

        /// <summary>
        /// Returns the instance of ingredient
        /// </summary>
        /// <param name="ingName">The name of ingredient to be found</param>
        /// <returns>The instance of ingredientt with the name passed as parameter</returns>
        public static Ingredient GetIngredient(string ingName)
        {
            Ingredient retIng;
            //If ingredient is already present, return that particular instance
            if (ingredientList.ContainsKey(ingName))
                retIng = ingredientList[ingName];
            else
            {
                //Else create new instance and return the newly created instance and store its reference in map
                retIng = new Ingredient(ingName);
                ingredientList[ingName] = retIng;
            }
            return retIng;
        }
    }
}
