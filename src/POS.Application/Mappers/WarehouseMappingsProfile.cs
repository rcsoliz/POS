﻿using AutoMapper;
using POS.Application.Dtos.Warehouse.Request;
using POS.Application.Dtos.Warehouse.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class WarehouseMappingsProfile: Profile
    {
        public WarehouseMappingsProfile()
        {
            CreateMap<Warehouse, WareHouseResponseDto>()
                .ForMember(x => x.WarehouseId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateWarehouse, x => x.MapFrom(
                y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<Warehouse, WarehoseByIdResponseDto>()
                .ForMember(x => x.WarehouseId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<WarehouseRequestDto, Warehouse>().ReverseMap();
        }
    }
}
