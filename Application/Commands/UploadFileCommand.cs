using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UploadFileCommand : IRequest<List<BookResponse>>
    {
        public IFormFile File { get; set; }
    }
}
