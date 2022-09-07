using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WM.Api.DB;
using WM.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalUniqueIdentifierController : ControllerBase
    {
        private readonly CoreDbContext _context;
        private readonly ILogger<GlobalUniqueIdentifierController> _logger;

        public GlobalUniqueIdentifierController(CoreDbContext context, ILogger<GlobalUniqueIdentifierController> logger)
        {
            _context = context;
            _logger = logger;
        }
    
        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            if (string.IsNullOrEmpty(guid) || !ValidateGuid(guid))
                return BadRequest($"InValid GUID <{guid}>");

            var existingGuid = GetGlobalUniqueIdentifier(guid);
            if (existingGuid == null)
                return BadRequest($"GUID <{guid}> No Record exists");
            return Ok(existingGuid);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewGlobalUniqueIdentifierModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newGuid = new GlobalUniqueIdentifier
            {
                Usr = value.Usr,
                Guid = string.IsNullOrEmpty(value.Guid) ? Guid.NewGuid().ToString("N").ToUpper() : value.Guid,
                Expire = DateTime.Now.AddDays(value.ExpiryDays == null ? 30 : value.ExpiryDays.Value)
            };
            var existingGuid = GetGlobalUniqueIdentifier(newGuid.Guid);
            
            if(existingGuid != null)
                return BadRequest($"GUID <{newGuid.Guid}> Record already exists");

            _context.GlobalUniqueIdentifiers.Add(newGuid);
            _context.SaveChanges();
            return Ok(GetGlobalUniqueIdentifier(newGuid.Guid));
        }

        [HttpPut("{guid}")]
        public IActionResult Put(string guid, [FromBody] UpdateGlobalUniqueIdentifierModel value)
        {
            if (string.IsNullOrEmpty(guid) || !ValidateGuid(guid))
                return BadRequest($"InValid GUID <{guid}>");

            var existingGuid = GetGlobalUniqueIdentifier(guid);
            if (existingGuid == null)
                return BadRequest($"GUID <{guid}> No Record exists");
            existingGuid.Expire = DateTime.Now.AddDays(value.ExpiryDays);
            _context.GlobalUniqueIdentifiers.Update(existingGuid);
            _context.SaveChanges();
            return Ok(Get(existingGuid.Guid));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(string guid)
        {
            if (string.IsNullOrEmpty(guid) || !ValidateGuid(guid))
                return BadRequest($"InValid GUID <{guid}>");

            var existingGuid = GetGlobalUniqueIdentifier(guid);
            if (existingGuid == null)
                return BadRequest($"GUID <{guid}> No Record exists");
            _context.GlobalUniqueIdentifiers.Remove(existingGuid);
            _context.SaveChanges();
            return Ok();
        }

        private GlobalUniqueIdentifier GetGlobalUniqueIdentifier(string guid)
        {   
            return _context.GlobalUniqueIdentifiers.Where(x => x.Guid == guid).FirstOrDefault();
        }

        private bool ValidateGuid(String input)
        {
            Regex regex = new Regex("^[A-Z0-9]{32}$");
            return regex.IsMatch(input);
        }
    }
}
