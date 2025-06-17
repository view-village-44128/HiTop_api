using hitop.model;
using System.Text;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using app.config;
using System.Collections.Immutable;
using System.Xml.Linq;
using System.Collections.Generic;

namespace hitop.app.service
{
    public class HitopService : IHitopService
    {
        private readonly IConfiguration _config;
        private readonly string xConnString;
        private readonly string gDatabase;
        private readonly ConfigReader _reader;

        public HitopService(IConfiguration config, ConfigReader reader)
        {
            _config = config;
            xConnString = _config.GetConnectionString("hitopAppCon");
            gDatabase = _config["Database"];
            _reader = reader;
        }

        private string ErrorMessage = "";
        public string getErrorMessage()
        {
            return this.ErrorMessage;
        }
        productModel[] testProduct = new productModel[]
{
            new productModel { id = 1, code = "1", name = "product1" },
            new productModel { id = 2, code = "2", name = "product2" },
            new productModel { id = 3, code = "3", name = "product3" },
            new productModel { id = 4, code = "4", name = "product4" },
            new productModel { id = 5, code = "5", name = "product5" },
            new productModel { id = 6, code = "6", name = "product6" },
            new productModel { id = 7, code = "7", name = "product7" }
        };


        public async Task<string> Test()
        {

            return "aaaaa";
        }

        public async Task<IEnumerable<productModel>> GetProduct(string search = null)
        {
        
            IEnumerable<productModel> iens = null;
            iens = testProduct;
            return iens;
        }

        public async Task<IEnumerable<productModel>> GetProductDatabase(string search = null)
        {

            IEnumerable<productModel> iens = null;
            Object xobj = null;
            if (gDatabase == "Pg")
            {
                try
                {
                    using (var db = new NpgsqlConnection(xConnString))
                    {
                        string xsql = $@"SELECT * FROM product where name = @search";
                        xobj = new { search };

                        await db.OpenAsync().ConfigureAwait(false);
                        iens = await db.QueryAsync<productModel>(xsql, xobj).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = "GetProductDatabase " + ex.Message;
                }
            }
            return iens;
        }
    }

}
