﻿using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile()
        {
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.StateCategory, x => x.MapFrom(
                    y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();

            CreateMap<CategoryRequestDto, Category>();

            CreateMap<Category, CategorySelectResponseDto>()
                .ReverseMap();
        }
    }
}
