namespace CoffeeMachine
{
    /// <summary>
    /// Enum to store the reason for error
    /// </summary>
    public enum ERROR_REASON
    {
        NO_FREE_OUTLETS,
        INGREDIENT_ERROR
    }

    /// <summary>
    /// Reason to store the error in ingredient
    /// </summary>
   public enum ERROR
   {
       NO_ERROR,
       INSUFFICIENT,
       UNAVAILABLE
   }
}
