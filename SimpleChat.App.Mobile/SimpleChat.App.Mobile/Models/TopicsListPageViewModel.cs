using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleChat.App.Mobile.Models
{
    public class TopicsListPageViewModel : ViewModel
    {
        public TopicsListPageViewModel(INavigation navigation, ChatService chatService, Guid authorId)
        {
            this.Navigation = navigation;
            this.ChatService = chatService;
            this.AuthorId = authorId;


            this.UpdateTopicsCommand = new AsyncCommand(UpdateTopicsAsync);
        }

        public INavigation Navigation { get; }
        public ChatService ChatService { get; }
        public Guid AuthorId { get; }
        public AsyncCommand UpdateTopicsCommand { get; }
        public ObservableCollection<TopicModel> Topics { get; } = new ObservableCollection<TopicModel>();


        public async Task UpdateTopicsAsync()
        {
            Dictionary<int, TopicModel> existingTopics = Topics.ToDictionary(x => x.Topic.Id);

            foreach(var topic in await ChatService.GetTopicsAsync())
            {
                Topic nonClosureTopic = topic;

                if (existingTopics.ContainsKey(topic.Id))
                {
                    existingTopics[topic.Id].Topic = topic;
                    existingTopics[topic.Id].SelectCommand = new AsyncCommand(async () => await SelectTopicAsync(nonClosureTopic));

                    existingTopics.Remove(topic.Id);
                }
                else
                {
                    Topics.Add(new TopicModel(topic, new AsyncCommand(async () => await SelectTopicAsync(nonClosureTopic))));
                }
            }

            foreach(var removedTopic in existingTopics.Values)
            {
                Topics.Remove(removedTopic);
            }
        }

        private async Task SelectTopicAsync(Topic topic)
        {
            var page = new ChatroomPage();

            var vm = new ChatroomPageViewModel(this.ChatService, topic, this.AuthorId);

            page.BindingContext = vm;

            // do both at the same time
            await Task.WhenAll(vm.GetLatestMessagesAsync(), Navigation.PushAsync(page));
        }

        public class TopicModel : Model
        {
            public TopicModel(Topic topic, ICommand selectCommand)
            {
                this.SelectCommand = selectCommand;
                this.Topic = topic;
            }

            public ICommand SelectCommand { get; set; }
            public Topic Topic { get; set; }
        }
    }
}
