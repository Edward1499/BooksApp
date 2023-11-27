using Application.Queries;
using Application.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetCachedBookListQueryHandler : IRequestHandler<GetCachedBookListQuery, List<BookResponse>>
    {
        private readonly IMemoryCache _memoryCache;

        public GetCachedBookListQueryHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<List<BookResponse>> Handle(GetCachedBookListQuery request, CancellationToken cancellationToken)
        {
            return (List<BookResponse>)_memoryCache.Get("books") ?? new List<BookResponse>();
        }
    }
}
