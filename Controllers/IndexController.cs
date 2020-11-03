using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class IndexController : BaseApiController
    {
        [HttpGet("/")]
        public ActionResult<IndexDTO> index()
        {
            return new IndexDTO
            {
                ApiVersion = 1.0
            };
        }
    }
}