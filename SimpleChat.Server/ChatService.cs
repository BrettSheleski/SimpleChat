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

        public async Task AddAsync(Topic topic)
        {
            await Context.Topics.AddAsync(topic);
        }

        public async Task AddAsync(Author author)
        {
            await Context.Authors.AddAsync(author);
        }

        public async Task AddAsync(Message message)
        {
            await Context.Messages.AddAsync(message);
        }

        public async  Task AddAsync(AuthorImage authorImage)
        {
            await Context.AuthorImages.AddAsync(authorImage);
        }
        public Task<Author> GetAuthorAsync(Guid id)
        {
            return this.Context.Authors.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<AuthorImage> GetAuthorImageAsync(Guid id)
        {
            return this.Context.AuthorImages.FirstOrDefaultAsync(x => x.Id == id);
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
