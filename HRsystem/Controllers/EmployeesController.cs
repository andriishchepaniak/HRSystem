using HR.DAL.Interfaces;
using HR.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository db;
        public EmployeesController(IEmployeeRepository context)
        {
            db = context;
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.GetAll());
        }
        [Route("getByPages")]
        [HttpGet]
        public async Task<IActionResult> Get(int offset=0, int count=10)
        {
            return Ok(await db.GetAllWithPage(offset, count));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await db.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee value)
        {
            return Ok(await db.Add(value));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Employee value)
        {
            return Ok(await db.Update(value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await db.DeleteAsync(id));
        }
    }
}
