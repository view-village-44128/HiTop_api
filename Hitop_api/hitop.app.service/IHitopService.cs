using hitop.model;

namespace hitop.app.service
{
    public interface IHitopService
    {
        string getErrorMessage();

        Task<String> Test();
        Task<IEnumerable<String>> GetProduct(string search = null);
    }
}
