using System.Collections.Generic;   //Dictionary

namespace CoffeeMachine
{
    /// <summary>
    /// preparation status for a beverage
    /// </summary>
    public class PreparationStatus
    {
        /// <summary>
        /// The beverage
        /// </summary>
        public Beverage beverage;

        /// <summary>
        /// Whether the beverge has been prepared ot not
        /// </summary>
        public bool isPrepared;

        /// <summary>
        /// Reason for not preparing the beverage
        /// </summary>
        public ERROR_REASON reasonForError = ERROR_REASON.INGREDIENT_ERROR;

        /// <summary>
        /// List of insufficient/unavailable ingridients and their reaso
        /// </summary>
        public Dictionary<Ingredient, ERROR> statusList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="beverage">The beverage for which preparation status is needed</param>
        public PreparationStatus(Beverage beverage)
        {
            this.beverage = beverage;
        }
    }
}
