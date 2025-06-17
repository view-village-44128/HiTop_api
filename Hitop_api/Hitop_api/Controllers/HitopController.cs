using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hitop.model;
using hitop.app.service;

namespace hitop.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class HitopController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly IHitopService sv;
        public HitopController(IHitopService AppService, IConfiguration configuration)
        {
            sv = AppService;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<String> Test()
        {
            String ien = null;
            ien = await sv.Test();
            if (ien == null && !string.IsNullOrEmpty(sv.getErrorMessage()))
            {
                throw new ApplicationException(sv.getErrorMessage());
            }
            return ien;
        }


        [HttpGet]
        public async Task<IEnumerable<String>> GetProduct(string search = null)
        {
            IEnumerable<String> ien = null;
            ien = await sv.GetProduct(search);
            if (ien == null && !string.IsNullOrEmpty(sv.getErrorMessage()))
            {
                throw new ApplicationException(sv.getErrorMessage());
            }
            return ien;
        }

    }
}
