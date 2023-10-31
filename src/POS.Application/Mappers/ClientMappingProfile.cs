﻿using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientResponseDto>()
                .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.DocumentType, x => x.MapFrom(y => y.DocumentType.Abbreviation))
                .ForMember(x => x.StateClient,
                    x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<Client, ClientByIdResponseDto>()
                .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<ClientRequestDto, Client>();
        }


    }
}
