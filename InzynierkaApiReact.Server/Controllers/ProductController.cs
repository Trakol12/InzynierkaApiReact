using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InzynierkaApiReact.Server.Models;
using InzynierkaApiReact.Server.Data;
using InzynierkaApiReact.Server.Interface;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace InzynierkaApiReact.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }



        //Endpoint do wyszukiwania produktów po kodzie Ean
        //Wywołuje się tą metodę za pomocą żądania GET na adresie /Product/search.ean/{eanCode}
        [HttpGet("search/ean/{eanCode}")]
        //Metoda zwraca wynik w formacie HTTP i oczekuje kodu Ean jako aprametru
        public async Task<IActionResult> SearchByEan(string eanCode)
        {
            if (!Regex.IsMatch(eanCode, "^\\d{13}$"))
            {
                return BadRequest(new { message = "Zły format kodu EAN" });
            }

            Product product = await _productServices.SearchByEan(eanCode);

            //Ewentualnie można tak: //return NotFound("Nie znaleziono produktu"); 
            //lecz nie daje to możliwości rozbudowy błędu o np. kodzie błędu dacie lub czegoś jeszcze co się chce
            if (product == null) { return NotFound(new { message = "Nie znaleziono produktu" }); }

            return Ok((ProductDTO)product);
        }

        //Endpoint do wyszukiwania produktów po kodzie Sklepowym
        [HttpGet("search/Casto/{castoCode}")]
        public async Task<IActionResult> SearchByCastoCode(string castoCode)
        {

            if (!Regex.IsMatch(castoCode, "^\\d{9}EA$"))
            {
                return BadRequest(new { message = "Zły format kodu casto" });
            }
            Product product = await _productServices.SearchByCastoCode(castoCode);

            //Ewentualnie można tak: //return NotFound("Nie znaleziono produktu"); 
            //lecz nie daje to możliwości rozbudowy błędu o np. kodzie błędu dacie lub czegoś jeszcze co się chce
            if (product == null) { return NotFound(new { message = "Nie znaleziono produktu" }); }

            return Ok((ProductDTO)product);

        }

        [HttpGet("download/{planogramId}")]
        public async Task<IActionResult> DownloadPlanogramById(int planogramID)
        {
            try
            {
                var pdf = await _productServices.DownloadPlanogramById(planogramID);
                if (pdf == null)
                {
                    return NotFound();
                }

                var (fileStream, fileName) = pdf;  // Poprawna dekonstrukcja krotki
                // Zakodowanie nazwy pliku dla nagłówka HTTP
                var encodedFileName = Uri.EscapeDataString(fileName);
                // Ustawienie nagłówka z zakodowaną nazwą pliku
                Response.Headers["Content-Disposition"] = $"inline; filename*=UTF-8''{encodedFileName}";
                return File(fileStream, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //if(fileStream == null) { return NotFound(new { message = "Nie znaleziono pliku" }); }

        }

        [HttpGet("scrape/{ProductPageLink}")]
        public async Task<IActionResult> GetImageAndDescryption(string ProductPageLink)
        {          
            try
            {

                //Dekoduje url aby zadziałało
                string decodedUrl = Uri.UnescapeDataString(ProductPageLink);
                var web = new HtmlWeb();
                // Load the HTML document from the URL
                var document = await web.LoadFromWebAsync(decodedUrl);



                // XPath to extract the image URL
                var imageNode = document.DocumentNode.SelectSingleNode("(//div[@id='main-content']//picture/img)[1]"); //[1] jest po to aby wynierało tylko 1 znalezione
                string imageUrl = imageNode?.GetAttributeValue("src", string.Empty);

                // XPath to extract the product description
                var descriptionNode = document.DocumentNode.SelectSingleNode("(//*[@id='product-details']//p)[2]");
                string description = !string.IsNullOrWhiteSpace(descriptionNode.InnerText.Trim())
                                        ? descriptionNode.InnerText.Trim()
                                        : "Brak opisu.";

                // Check if both elements were found
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return NotFound(new { message = "Brak obrazu" });
                }


                // Return the image URL and description in the response
                return Ok(new { ImageUrl = imageUrl, Description = description });


            }
            catch (System.Exception ex)
            {
                // Return an error message if an exception occurs
                return BadRequest(new { message = ex.Message });
            }
        }



}
}
