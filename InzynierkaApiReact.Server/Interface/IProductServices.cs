using InzynierkaApiReact.Server.Models;

namespace InzynierkaApiReact.Server.Interface
{
    public interface IProductServices
    {
        Task<Product> SearchByEan(string eanCode);
        Task<Product> SearchByCastoCode(string castoCode);

        Task<Tuple<Stream,string>> DownloadPlanogramById(int planogramid);
    }
}