﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_App.Data;
using My_App.Dtos;
using My_App.Helpers;

namespace My_App.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;

        private readonly IMapper _Mapper;

        public UsersController(IDatingRepository repo,IMapper mapper)
        {
            _repo = repo;
            _Mapper = mapper;
        }

       [HttpGet]
       public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await _repo.GetUsers(userParams);

            var userToretuern = _Mapper.Map<IEnumerable<UserForDetailDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotlaCount, users.TotalPages);


            return Ok(userToretuern);
        }




     
        [HttpGet("{id}",Name ="GetUser")]
        public async Task<IActionResult> Getuser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToretuern = _Mapper.Map<UserForDetailDto>(user);

            return Ok(userToretuern);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id , UserforUpdateDto userforUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromrepo = await _repo.GetUser(id);
            _Mapper.Map(userforUpdateDto, userFromrepo);
            if (await _repo.Saveall())
                return NoContent();

            throw new Exception("update user" + id + "failed on save");
        }
    }
}