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
            ChatService.Add(author);

            await ChatService.SaveChangesAsync();

            return author;
        }

    }
}