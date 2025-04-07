using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace InzynierkaApiReact.Server.Models
{
    public class Product
    {
        [Key]
        public string EanCode { get; set; }
        public string Name { get; set; }
        public string CastoCode { get; set; }
        public string Category { get; set; }
        public string ProductPageLink { get; set; }


        //Klucz obcy do Planogramu
        //Entity Framework automatycznie ustala jakiego typu relacje są między tabelami (w tym przypadku 1 do wielu(product do planogramu))
                    //więc nie ma potrzeby ręcznego ustawiania 
        public int? PlanogramId { get; set; }

        //Nawigacja do tabeli Planogram
        public Planogram Planogram { get; set; }

        //Jest to potrzebne dla EntityFrameWork aby móc łatwiej uzyskać dostęp do lokalizacji produktu bez robienia dwóch zapytań(Pozwala na użycie Include w serwisie z lokalizacją)
        public ProductLocalization ProductLocalization { get; set; }

        
        
        public static explicit operator ProductDTO(Product model)
        {
            if (model == null) return null;

            return new ProductDTO
            {
                EanCode = model.EanCode ,
                CastoCode = model.CastoCode ,
                Alley = model.ProductLocalization.Alley,
                NumerOnTheExposition = model.ProductLocalization.NumerOnTheExposition,
                NumberOnTheShelf = model.ProductLocalization.NumberOnTheShelf,
                Name = model.Name,
                Category = model.Category,
                ProductPageLink = model.ProductPageLink,
                PlanogramId = model.Planogram.Id

            };
        }

    }
}
