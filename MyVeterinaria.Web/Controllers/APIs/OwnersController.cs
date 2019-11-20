using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVeterinaria.Common.Models;
using MyVeterinaria.Web.Data;

namespace MyVeterinaria.Web.Controllers.APIs
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OwnersController : ControllerBase
    {
        private readonly DataContext _context;

        public OwnersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/GetOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var owner = await _context.Owners
                .Include(x => x.User)
                .Include(x => x.Pets)
                    .ThenInclude(p => p.PetType)
                .Include(x => x.Pets)
                    .ThenInclude(p => p.Histories)
                    .ThenInclude(h => h.ServiceType)
                .Select(x => new OwnerResponse
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Address = x.User.Address,
                    Document = x.User.Document,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    Pets = x.Pets.Select(p => new PetResponse
                    {
                        Id = p.Id,
                        PetType = p.PetType.Name,
                        Born = p.Born,
                        Name = p.Name,
                        Race = p.Race,
                        Remarks = p.Remarks,
                        ImagenUrl = p.ImageFullPath,
                        Histories = p.Histories.Select(h => new HistoryResponse
                        {
                            Id = h.Id,
                            Date = h.Date,
                            ServiceType = h.ServiceType.Name,
                            Remarks = h.Remarks,
                            Description = h.Description
                        }).ToList()
                    }).ToList(),
                }).FirstOrDefaultAsync(o => o.Email.ToLower() == emailRequest.Email.ToLower());

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }
    }
}