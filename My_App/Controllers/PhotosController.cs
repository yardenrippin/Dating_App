using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using My_App.Data;
using My_App.Dtos;
using My_App.Helpers;
using My_App.Modles;

namespace My_App.Controllers
{   [Authorize]
    //this route set by defult
    //[Route("api/[controller]")]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinartSettings> _cloudinaryconfig;
        private Cloudinary _cloudinary;
        public PhotosController(IDatingRepository repo, IMapper mapper, IOptions<CloudinartSettings> cloudinaryconfig)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryconfig = cloudinaryconfig;

            Account acc = new Account(
                _cloudinaryconfig.Value.CloudName,
                _cloudinaryconfig.Value.ApiKey,
                _cloudinaryconfig.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }


        [HttpGet("{id}", Name = "GetPhotot")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photofromrepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photofromrepo);
            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForuser(int userid,
            [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userid);

            var file = photoForCreationDto.File;

            var UploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500)
                        .Crop("fill").Gravity("face")
                    };

                    UploadResult = _cloudinary.Upload(uploadParams);
                }

            }
            photoForCreationDto.Url = UploadResult.Uri.ToString();
            photoForCreationDto.PublicID = UploadResult.PublicId;

            var myphoto = _mapper.Map<photo>(photoForCreationDto);

            if (userFromRepo.Photos.Any(u => u.Ismain))
                myphoto.Ismain = true;

            userFromRepo.Photos.Add(myphoto);



            if (await _repo.Saveall())
            {
                var phototoreturn = _mapper.Map<PhotoForReturnDto>(myphoto);
                return CreatedAtRoute("GetPhotot", new { id = myphoto.Id }, phototoreturn);
            }
            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMaimPhoto(int userid,int id,object o)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userid);

            if (!user.Photos.Any(p=>p.Id==id))
                return Unauthorized();

            var photofromrepo = await _repo.GetPhoto(id);

            if (photofromrepo.Ismain)
                return BadRequest("this is already the main photo");

            var currentMainPhotot = await _repo.GetmainPhotoOfUser(userid);
            currentMainPhotot.Ismain = false;
            photofromrepo.Ismain = true;

            if (await _repo.Saveall())
                return NoContent();
            return BadRequest("Could not set photo to main");
        }

       [HttpDelete("{id}")]
       public async Task <IActionResult>Deletphoto(int userid, int id)
        {
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var user = await _repo.GetUser(userid);

            var photoFromrepo = await _repo.GetPhoto(id);

            if (photoFromrepo.Ismain)
                return BadRequest("you canot delete your main photo");

            if (photoFromrepo.PublicID != null)
            {
                var deleteparams = new DeletionParams(photoFromrepo.PublicID);

                var result = _cloudinary.Destroy(deleteparams);
                if (result.Result == "ok")
                {
                    _repo.Delete(photoFromrepo);
                }

                    
            }
            if (photoFromrepo == null)
            {
                _repo.Delete(photoFromrepo);
            }

            if(await _repo.Saveall())
            {
                return Ok();
            }
            return BadRequest("failed to delete the photo");

        }
    }
}