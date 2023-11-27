using Application.Commands;
using Application.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Mime;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _mediator.Send(new GetCachedBookListQuery());
            dynamic mymodel = new ExpandoObject();
            mymodel.Books = books;
            return View(mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadFileCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }

        [HttpPost("DownloadFile")]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile()
        {        
            var fileData = await _mediator.Send(new DownloadFileCommand());
            return File(fileData, MediaTypeNames.Application.Octet, "ISBN_Output_File.txt");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}