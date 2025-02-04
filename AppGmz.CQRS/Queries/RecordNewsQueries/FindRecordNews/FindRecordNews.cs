﻿using AppGmz.Models.DtoModels;
using MediatR;
using System;

namespace AppGmz.CQRS.Queries.RecordNewsQueries.FindRecordNews
{
    public class FindRecordNews : IRequest<FullRecordNewsDto>
    {
        public Guid Id { get; }

        public FindRecordNews(Guid id)
        {
            Id = id;
        }
    }
}