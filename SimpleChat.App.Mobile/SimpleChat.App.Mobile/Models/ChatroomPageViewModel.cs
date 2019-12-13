using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.App.Mobile.Models
{
    public class ChatroomPageViewModel : ViewModel
    {
        public ChatroomPageViewModel(ChatService chatService, Topic topic, Guid authorId)
        {
            this.ChatService = chatService;
            this.Topic = topic;
            this.GetLatestMessagesCommand = new AsyncCommand(GetLatestMessagesAsync);
            this.SendCommand = new AsyncCommand(SendAsync);
            this.AuthorId = authorId;
        }

        public ChatService ChatService { get; }
        public Topic Topic { get; }
        public AsyncCommand GetLatestMessagesCommand { get; }
        public AsyncCommand SendCommand { get; }
        public Guid AuthorId { get; }
        public ObservableCollection<MessageModel> Messages { get; } = new ObservableCollection<MessageModel>();
        public string Message { get => _message; set => Set(ref _message, value, UpdateCanSendMessage); }

        void UpdateCanSendMessage()
        {
            this.SendCommand.Enabled = !string.IsNullOrEmpty(Message);
        }

        public async Task GetLatestMessagesAsync()
        {
            this.Messages.Clear();

            var messages = await ChatService.GetLatestMessagesForTopicAsync(Topic, 100);

            foreach (var msg in messages)
            {
                this.Messages.Add(new MessageModel(msg, new AuthorModel { AuthorId = msg.AuthorId }));
            }
        }

        private string _message;

        public async Task SendAsync()
        {
            var msg = await this.ChatService.CreateMessageAsync(this.Topic.Id, this.Message, this.AuthorId);

            var model = new MessageModel(msg, new AuthorModel { AuthorId = this.AuthorId });

            this.Messages.Add(model);
        }

        public class MessageModel : Model
        {
            public MessageModel(Message message, AuthorModel author)
            {
                this.Message = message;
                this.Author = author;
            }

            public Message Message { get; }
            public AuthorModel Author { get; }
        }

        public class AuthorModel : Model
        {
            public Guid AuthorId { get; set; }
            public string Name { get => _name; set => Set(ref _name, value); }

            private string _name;
        }
    }
}
