using System;
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
using My_App.Modles;

namespace My_App.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private readonly IDatingRepository _repo ;
        private readonly IMapper _mapper;
          
        public MessagesController(IDatingRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
       
        [HttpGet("{id}",Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userid, int id)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repo.GetMessage(id);
            if (messageFromRepo == null)
                return NotFound();
            return Ok(messageFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesforUser(int userid, [FromQuery]MessageParams messageParams)
        {
             if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            messageParams.UserId = userid;

            var MessageFromRepo = await _repo.GetMessagesForUser(messageParams);

            var messages = _mapper.Map<IEnumerable<MessageToReturnDto>>(MessageFromRepo);

            Response.AddPagination(MessageFromRepo.CurrentPage, MessageFromRepo.PageSize
                , MessageFromRepo.TotlaCount, MessageFromRepo.TotalPages);

            return Ok(messages);
        }
        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userid,int recipientid)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var mesagessfrorepo = await _repo.GetMessangerThread(userid, recipientid);

            var messagethread = _mapper.Map<IEnumerable<MessageToReturnDto>>(mesagessfrorepo);

            return Ok(messagethread);


        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userid, MessageForCreationDto messageForCreationDto)
        {
            var sender = await _repo.GetUser(userid);
            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            messageForCreationDto.SenderId = userid;

            var recipient = await _repo.GetUser(messageForCreationDto.RecipientId);

            if (recipient == null)
                return BadRequest("Could Not Finde User");

            var message = _mapper.Map<Message>(messageForCreationDto);

            _repo.Add(message);
           

            if (await _repo.Saveall())
            {
                var messagetoReturn = _mapper.Map<MessageToReturnDto>(message);
                return CreatedAtRoute("GetMessage", new { id = message.Id }, messagetoReturn);
            }
             
            throw new Exception("Create the Message faile on save");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userid)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messagefromrepo = await _repo.GetMessage(id);

            if (messagefromrepo.SenderId == userid) 
            messagefromrepo.SenderDeleted = true;

            if (messagefromrepo.RecipientId == userid)
                messagefromrepo.RecipientDeleted= true;

            if (messagefromrepo.SenderDeleted == true && messagefromrepo.RecipientDeleted == true)
                _repo.Delete(messagefromrepo);

            if (await _repo.Saveall())
            return NoContent();

            throw new Exception("error deleting the message");

        }
        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkeMessagesRed(int userid,int id)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var message = await _repo.GetMessage(id);

            if (message.RecipientId != userid) 
            return Unauthorized();

            message.IsRead = true;
            message.DateRead = DateTime.Now;

            await _repo.Saveall();

            return NoContent();



        }

    }
}