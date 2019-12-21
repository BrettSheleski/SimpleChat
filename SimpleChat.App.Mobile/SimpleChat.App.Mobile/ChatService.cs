using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat.App.Mobile
{
    public class ChatService
    {
        public ChatService(Uri uri)
        {
            this.ServiceUri = uri;
        }

        public Uri ServiceUri { get; }

        public async Task AddAuthor(Author author)
        {
            UriBuilder builder = new UriBuilder(ServiceUri);

            builder.Path = "/Authors";

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(author);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                json = await client.UploadStringTaskAsync(builder.Uri, json);
            }

            Author responseAuthor = Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(json);

            author.Id = responseAuthor.Id;
        }

        public async Task<IEnumerable<Message>> GetLatestMessagesForTopicAsync(Topic topic, int count)
        {
            try
            {
                UriBuilder builder = new UriBuilder(ServiceUri);
                builder.Path = $"/Topics/{topic.Id}/Messages/Latest";
                builder.Query = $"count={count}";
                string json;

                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Accept] = "application/json";

                    json = await client.DownloadStringTaskAsync(builder.Uri);
                }

                List<Message> messages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Message>>(json);

                return messages;
            }
            catch
            {
                return new List<Message>();
            }
        }

        public async Task<Message> CreateMessageAsync(int topicId, string text, Guid authorId)
        {
            UriBuilder builder = new UriBuilder(ServiceUri);
            builder.Path = $"/Topics/{topicId}/Messages";
            Message msg = new Message
            {
                TopicId = topicId,
                Text = text,
                AuthorId = authorId
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(msg);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                json = await client.UploadStringTaskAsync(builder.Uri, json);
            }

            msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(json);

            return msg;
        }

        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            UriBuilder builder = new UriBuilder(ServiceUri);

            builder.Path = $"/Authors/{authorId}";
            string json;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "application/json";

                json = await client.DownloadStringTaskAsync(builder.Uri);
            }

            Author author = Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(json);

            return author;
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            UriBuilder builder = new UriBuilder(ServiceUri);

            builder.Path = "/Topics";
            string json;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                
                json = await client.DownloadStringTaskAsync(builder.Uri);
            }

            List<Topic> topics = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Topic>>(json);

            return topics;
        }
    }
}