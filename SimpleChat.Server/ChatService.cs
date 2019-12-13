using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat.Server
{
    public class ChatService : IChatService
    {
        public ChatService(ChatContext context)
        {
            this.Context = context;
        }

        public ChatContext Context { get; }

        public void Add(Topic topic)
        {
            Context.Topics.Add(topic);
        }

        public void Add(Author author)
        {
            Context.Authors.Add(author);
        }

        public void Add(Message message)
        {
            Context.Messages.Add(message);
        }


        public Task<List<Message>> GetLatestMessagesForTopicAsync(int topicId, int count)
        {
            return GetLatestMessagesForTopicAsync(topicId, count, CancellationToken.None);
        }
        public Task<List<Message>> GetLatestMessagesForTopicAsync(int topicId, int count, CancellationToken cancellationToken)
        {
            return this.Context.Messages.Where(x => x.TopicId == topicId)
                               .OrderByDescending(x => x.Date)
                               .Take(count)
                               .ToListAsync(cancellationToken);
        }

        public Task<Topic> GetTopicAsync(int id)
        {
            return Context.Topics.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            return GetTopicsAsync(CancellationToken.None);
        }

        public Task<IEnumerable<Topic>> GetTopicsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<Topic>>(Context.Topics);
        }

        public Task SaveChangesAsync()
        {
            return SaveChangesAsync(CancellationToken.None);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}
