using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleChat.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        public AuthorsController(IChatService chatService)
        {
            this.ChatService = chatService;
        }

        public IChatService ChatService { get; }


        // create new
        [HttpPost]
        public async Task<Author> Post([FromBody] Author author)
        {
            await ChatService.AddAsync(author);

            await ChatService.SaveChangesAsync();

            return author;
        }

        [HttpGet("{id}")]
        public async Task<Author> Get(Guid id)
        {
            return await this.ChatService.GetAuthorAsync(id);
        }


        [HttpGet("/Authors/{id}/Image")]
        public async Task<AuthorImage> GetImage(Guid id)
        {
            return await this.ChatService.GetAuthorImageAsync(id);
        }

        [HttpPost("/Authors/{id}/Image")]
        public async Task PostImage([FromBody]AuthorImage authorImage)
        {
            await this.ChatService.AddAsync(authorImage);

            await this.ChatService.SaveChangesAsync();
        }
    }
}