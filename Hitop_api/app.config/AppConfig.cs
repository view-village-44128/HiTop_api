using Microsoft.Extensions.Configuration;
namespace app.config
{
    public class ConfigReader
    {
        private readonly IConfiguration _config;

        public ConfigReader(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
            return _config.GetConnectionString("hitopAppCon");
        }

    }
}
