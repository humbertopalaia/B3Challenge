﻿using AutoMapper;
using B3Challenge.Domain.Dtos.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Domain
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TaskInsertDto, Domain.Entities.Task> ();
        }

    }
}
