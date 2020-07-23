using System.Collections.Generic;   //Dictionary

namespace CoffeeMachine
{
    public class Beverage
    {
        /// <summary>
        /// The name of beverage
        /// </summary>
        private string beverageName;

        /// <summary>
        /// A map of ingredients to be used for preparation
        /// </summary>
        private Dictionary<Ingredient, int> ingredientList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="beverageName">The name of beverage</param>
        /// <param name="ingredientList"> A map of ingredients to be used for preparation</param>
        public Beverage(string beverageName, Dictionary<Ingredient, int> ingredientList)
        {
            this.beverageName = beverageName;
            this.ingredientList = ingredientList;
        }

        /// <summary>
        /// Property to expose the beverage name
        /// </summary>
        public string BeverageName
        {
            get 
            {
                return beverageName;
            }
        }

        /// <summary>
        /// Property to expose the map of ingredients
        /// </summary>
        public Dictionary<Ingredient, int> IngredientList
        {
            get
            {
                return ingredientList;
            }
        }

        /// <summary>
        /// Checks whether the beverage can be prepared with the available quantity of ingredients
        /// </summary>
        /// <param name="availList">Map of available ingredients witth their repsective quantities</param>
        /// <param name="errorList">Map of error in case it is present as out parameter</param>
        /// <returns>Whether the beverage was prepared successfully or not</returns>
        public bool Prepare(Dictionary<Ingredient, int> availList, out Dictionary<Ingredient, ERROR> errorList)
        {
            bool canMake = true;
            errorList = new Dictionary<Ingredient, ERROR>();

            //For each ingredient in the recipe of beverage, check whether there is sufficient quantity in the coffe machine
            foreach (Ingredient i in IngredientList.Keys)
            {
                // If coffee machine doesn't contain the ingredient, set error as unavailable ingredient
                if (!availList.ContainsKey(i))
                {
                    canMake = false;
                    errorList[i] = ERROR.UNAVAILABLE;
                }
                // If coffee machine doesn't contain sufficient amount ingredient, set error as insufficient ingredient
                else if (availList[i] < ingredientList[i])
                {
                    canMake = false;
                    errorList[i] = ERROR.INSUFFICIENT;
                }
            }
            return canMake;
        }
    }
}
