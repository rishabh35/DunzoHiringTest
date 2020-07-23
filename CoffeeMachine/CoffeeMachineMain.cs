using System;                       //Int32, Exception
using System.Collections.Generic;   //Dictionary, List
using Newtonsoft.Json.Linq;         //JObject

namespace CoffeeMachine
{
    class CoffeeMachineMain
    {

        private int number_of_outlets = 0;
        private Dictionary<Ingredient, int> total_items_quantity;
        private List<Beverage> beverage_list;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonString">Input</param>
        public CoffeeMachineMain(string jsonString)
        {
            // Initialise and parse the JSON
            Initialise(jsonString);
        }

        /// <summary>
        /// Parses the JSON string input
        /// </summary>
        /// <param name="jsonString">The input</param>
        private void Initialise(string jsonString) 
        {
            // Generate JSON Object
            JObject blogPostArray = JObject.Parse(jsonString);

            try
            {
                //Get free outlets
                Int32.TryParse((string)blogPostArray["machine"]["outlets"]["count_n"], out number_of_outlets);

                //Get JSON Object for total available items
                JObject ingre = (JObject)blogPostArray["machine"]["total_items_quantity"];

                //Get JSON Object for list of beverages
                JObject bev = (JObject)blogPostArray["machine"]["beverages"]; 

                //Parse the available ingredients and quantity list
                total_items_quantity = InitialiseIngredients(ingre);

                //Parse beverages order required
                InitialiseBeverages(bev);
            }
            catch (Exception ex)
            {
                throw new Exception("Error initialising JSON: " + ex.Message);
            }
        }

        /// <summary>
        /// Initialise the list of ingredients from JSON Object
        /// </summary>
        /// <param name="ingre">The JSON objects contaiining the list of ingredients with their quantity</param>
        /// <returns>Map of ingredients, with their corresponding quantities</returns>
        private Dictionary<Ingredient, int> InitialiseIngredients(JObject ingre)
        {
            //Iterate over all the properties of JSON tree and get ingredients
            Dictionary<Ingredient, int> ingList = new Dictionary<Ingredient, int>();
            foreach (JProperty property in ingre.Properties())
            {
                Ingredient ing = IngredientFactory.GetIngredient(property.Name);
                if (!ingList.ContainsKey(ing))
                    ingList[ing] = 0;
                //Handling case if ingredients is already available (maybe multiple slots for same ingredients)
                ingList[ing] += (int)property.Value;
            }
            return ingList;
        }

        /// <summary>
        /// Initilaise the list of beverages from JSON
        /// </summary>
        /// <param name="bev">The JSON Object containing the list of beverages</param>
        private void InitialiseBeverages(JObject bev)
        {
            beverage_list = new List<Beverage>();
            foreach (JProperty property in bev.Properties())
            {
                JObject beverage = (JObject)bev[property.Name];  //TODO: Handle Exception
                if (beverage == null)
                    continue;
                beverage_list.Add(new Beverage(property.Name, InitialiseIngredients(beverage)));
            }
        }

        /// <summary>
        /// Function to calculate what all can be made in the given scenario.
        /// It returns only one of the possible outputs.
        /// </summary>
        /// <returns>The list of status of each beverage requested</returns>
        public List<PreparationStatus> CalculateWhatCanBeMade() 
        {
            List<PreparationStatus> prepStatus = new List<PreparationStatus>();
            int freeOutlets = number_of_outlets;

            // Assuming the orders come as they are placed and are served on first come first serve basis
            foreach (Beverage b in beverage_list)
            {
                PreparationStatus prep = new PreparationStatus(b);
                Dictionary<Ingredient, ERROR> errorStatus;
                if (freeOutlets == 0)
                {
                    prep.isPrepared = false;
                    prep.reasonForError = ERROR_REASON.NO_FREE_OUTLETS;
                }
                // For each beverage check if it can be prepared
                prep.isPrepared = b.Prepare(total_items_quantity, out errorStatus);
                if (prep.isPrepared)
                {
                    // If the beverage can be prepared update the quantity list
                    UpdateAvailList(b);
                    --freeOutlets;
                }
                else
                {
                    // If the beverage cannot be prepared update the error status
                    prep.statusList = errorStatus;
                }

                //Add the status of this beverage 
                prepStatus.Add(prep);
            }

            return prepStatus;
        }

        /// <summary>
        /// Update the available quantities of ingredients
        /// </summary>
        /// <param name="b">Beverage which was prepared</param>
        private void UpdateAvailList(Beverage b)
        {
            foreach (Ingredient i in b.IngredientList.Keys)
            {
                total_items_quantity[i] -= b.IngredientList[i];
            }
        }

    }
}
