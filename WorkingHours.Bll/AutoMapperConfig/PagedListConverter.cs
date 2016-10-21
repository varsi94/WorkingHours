using System;
using AutoMapper;
using PagedList;
using System.Linq;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.AutoMapperConfig
{
    internal class PagedListConverter<TIn, TOut> : ITypeConverter<IPagedList<TIn>, PagedResult<TOut>>
    {
        public PagedResult<TOut> Convert(IPagedList<TIn> source, PagedResult<TOut> destination, ResolutionContext context)
        {
            destination = new PagedResult<TOut>
            {
                PageCount = source.PageCount,
                PageIndex = source.PageNumber,
                PageSize = source.PageSize
            };
            destination.Items = source.Select(x => context.Mapper.Map<TOut>(x)).ToList();
            return destination;
        }
    }
}