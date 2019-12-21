using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat
{
    public interface IChatService
    {
        Task<IEnumerable<Topic>> GetTopicsAsync();
        Task<IEnumerable<Topic>> GetTopicsAsync(CancellationToken cancellationToken);

        Task SaveChangesAsync();
        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task AddAsync(Topic topic);
        Task<Topic> GetTopicAsync(int id);
        Task AddAsync(Author author);
        Task<List<Message>> GetLatestMessagesForTopicAsync(int topicId, int count);
        Task AddAsync(Message message);
        Task<Author> GetAuthorAsync(Guid id);
    }
}
