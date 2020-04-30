using AutoMapper;
using My_App.Dtos;
using My_App.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Helpers
{
    public class AutoMapperprofiles :Profile
    {
        public AutoMapperprofiles()
        {
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.Photourl, opt => opt.MapFrom(
                     src => src.Photos.FirstOrDefault(p => p.Ismain).Url))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(d => d.DateOfBirth.calculatage()));

            CreateMap<User, Userforlistdto>()
                .ForMember(dest => dest.PohtoUrl, opt => opt.MapFrom(
                     src => src.Photos.FirstOrDefault(p => p.Ismain).Url))
                     .ForMember(dest => dest.Age, opt => opt.MapFrom(d => d.DateOfBirth.calculatage()));
            CreateMap<photo, PhotosFOrDEtailDTO>();

            CreateMap<UserforUpdateDto, User>();

            CreateMap<photo, PhotoForReturnDto>();

            CreateMap<PhotoForCreationDto,photo>();

            CreateMap<UserForRegisterDto, User>();

            CreateMap<MessageForCreationDto,Message>().ReverseMap();

            CreateMap<Message, MessageToReturnDto>()
                .ForMember(M => M.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(P => P.Ismain).Url))
                .ForMember(M => M.RecipientPhotoUrl, opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(P => P.Ismain).Url));

        }
    }
}
