using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TopicsController : ControllerBase
    {
        public TopicsController(IChatService chatService)
        {
            this.ChatService = chatService;
        }

        public IChatService ChatService { get; }

        [HttpGet]
        public Task<IEnumerable<Topic>> Get()
        {
            return ChatService.GetTopicsAsync();
        }

        [HttpGet("{id}")]
        public Task<Topic> Get(int id)
        {
            return ChatService.GetTopicAsync(id);
        }

        // create new
        [HttpPost]
        public async Task Post(Topic topic)
        {
            ChatService.Add(topic);

            await ChatService.SaveChangesAsync();
        }

        // update existing
        [HttpPut]
        public async Task Put(Topic topic)
        {
            Topic existingTopic = await ChatService.GetTopicAsync(topic.Id);

            if (existingTopic == null)
                throw new ArgumentOutOfRangeException(nameof(topic.Id));

            existingTopic.Name = topic.Name;
            existingTopic.Description = topic.Description;

            await ChatService.SaveChangesAsync();
        }

        [HttpGet("/Topics/{topicId}/Messages/Latest")]
        public async Task<List<Message>> GetLatestMessages(int topicId, int count)
        {
            List<Message> messages = await this.ChatService.GetLatestMessagesForTopicAsync(topicId, count);

            return messages;
        }
    }
}
