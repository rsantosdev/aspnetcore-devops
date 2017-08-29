using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Api.Controllers
{
    [Route("api/contacts")]
    public class ContactController : Controller
    {
        readonly DataContext _db;

        public ContactController(DataContext dataContext)
        {
            _db = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _db.Contacts.ToListAsync();
        }
    }
}
