using System.Text.Json.Serialization;

namespace KGRC_Evaluacion.Models
{
    public class ViewResponseFind
    { 
        public string  ?strDrink {  get; set; }
        public int ?idDrink  {  get; set; }
        public string ?strDrinkThumb {  get; set; }
    }
    public class DrinksResponseFind
    {
        [JsonPropertyName("drinks")]
        public List<ViewResponseFind> ?Drinks { get; set; }
    }
}
