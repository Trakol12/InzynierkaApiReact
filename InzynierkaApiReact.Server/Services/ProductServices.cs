using InzynierkaApiReact.Server.Data;
using InzynierkaApiReact.Server.Interface;
using InzynierkaApiReact.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InzynierkaApiReact.Server.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _context; //To jest z folderu Data. Używa się do wykonywania operacji na bazie danych, np: Select Instert Update Delete
                                                //Jest jako readonly żeby być pewnym że nie zostanie zmieniona w trakcie działa aplikacji
                                                //Pozwala kontrollerowi na dostęp do bazy danych
        public ProductServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> SearchByEan(string eanCode)
        {
            var product = await _context.Product
                .Include(p => p.Planogram)
                .Include(p => p.ProductLocalization)
                .FirstOrDefaultAsync(p => p.EanCode == eanCode);
            //Jeśli nie znajdzie produkltu o zadanym kodzie Ean to zwraca odpowiedź z kodem HTTP 404 i wiadomością 

            //Ewentualnie można tak: //return NotFound("Nie znaleziono produktu"); 
            //lecz nie daje to możliwości rozbudowy błędu o np. kodzie błędu dacie lub czegoś jeszcze co się chce
            return product;

        }
        public async Task<Product> SearchByCastoCode(string castoCode)
        {
            var product = await _context.Product
                .Include(p => p.Planogram)
                .Include(p => p.ProductLocalization)
                .FirstOrDefaultAsync(p => p.CastoCode == castoCode);
            //Jeśli nie znajdzie produkltu o zadanym kodzie Casto to zwraca odpowiedź z kodem HTTP 404 i wiadomością 
            return product;
        }

        public async Task<Tuple<Stream, string>> DownloadPlanogramById (int planogramID)
        {

            var planogram = await _context.Planogram.FindAsync(planogramID);
            if (planogram == null || !File.Exists(planogram.PdfPath))
            {
                return null;
            }

            var stream = new FileStream(planogram.PdfPath, FileMode.Open, FileAccess.Read);
            return new Tuple<Stream, string>(stream, Path.GetFileName(planogram.PdfPath));

        }
    }
    }
