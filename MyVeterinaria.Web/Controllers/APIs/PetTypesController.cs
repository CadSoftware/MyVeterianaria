using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MyVeterinaria.Web.Controllers.APIs
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Data;
    using Data.Entities;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PetTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public PetTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<PetType> GetPetTypes()
        {
            return _context.PetTypes;
        }
    }
}