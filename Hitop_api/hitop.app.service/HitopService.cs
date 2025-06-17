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


        public async Task<String> Test()
        {

            return "aaaaa";
        }

        public async Task<IEnumerable<String>> GetProduct(string search = null)
        {
        
            IEnumerable<String> iens = null;
      
            return iens;
        }
    }

}
