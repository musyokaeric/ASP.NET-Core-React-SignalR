using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Reactivities.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}
