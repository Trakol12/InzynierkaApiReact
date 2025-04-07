using System.ComponentModel.DataAnnotations;

namespace InzynierkaApiReact.Server.Models
{
    public class ProductLocalization
    {
        [Key]
        public int Id { get; set; }

        //Klucz obcy
        public string ProductEanCode { get;set; }
        //Nawigacja do tabeli Product
        public Product Product { get; set; }

        public int Alley { get; set; }
        public int? NumberOnTheShelf { get; set; }
        public int? NumerOnTheExposition { get; set; }

    }
}
