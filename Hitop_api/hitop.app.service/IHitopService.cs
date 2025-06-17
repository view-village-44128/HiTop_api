using hitop.model;

namespace hitop.app.service
{
    public interface IHitopService
    {
        string getErrorMessage();

        Task<string> Test();
        Task<IEnumerable<productModel>> GetProduct(string search = null);
        Task<IEnumerable<productModel>> GetProductDatabase(string search = null);
    }
}
