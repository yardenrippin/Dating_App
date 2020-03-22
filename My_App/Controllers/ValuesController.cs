﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_App.Data;

namespace My_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


       
        private readonly DataContext _context;
        public ValuesController(DataContext context )
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Getvalues()
        {
            var values = _context.values.ToList();
            return Ok(values);

        }

       [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Getvalues(int id)
        {
            var values = _context.values.FirstOrDefault(x=>x.Id==id);
            return Ok(values);
        }
        

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
