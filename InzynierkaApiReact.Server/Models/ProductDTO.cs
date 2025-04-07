namespace InzynierkaApiReact.Server.Models
{
    public class ProductDTO
    {
        public string EanCode { get; set; }
        public string Name { get; set; }
        public string CastoCode { get; set; }
        public string Category { get; set; }
        public string ProductPageLink { get; set; }
        public int Alley { get; set; }
        public int? NumberOnTheShelf { get; set; }
        public int? NumerOnTheExposition { get; set; }
        public int PlanogramId { get; set; }


        
    }
}
