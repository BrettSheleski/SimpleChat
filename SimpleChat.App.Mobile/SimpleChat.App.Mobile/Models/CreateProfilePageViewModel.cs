using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleChat.App.Mobile.Models
{
    public class CreateProfilePageViewModel : ViewModel
    {
        public CreateProfilePageViewModel(ChatService chatService, INavigation navigation)
        {
            this.ChatService = chatService;
            this.Navigation = navigation;
            this.CreateProfileCommand = new AsyncCommand(CreateProfileAsync);
            UpdateCommandEnabled();
        }

        public ChatService ChatService { get; }
        public INavigation Navigation { get; }
        public AsyncCommand CreateProfileCommand { get; }
        public string Name { get => _name; set => Set(ref _name , value, UpdateCommandEnabled); }
        public Author Author { get; private set; }

        private void UpdateCommandEnabled()
        {
            CreateProfileCommand.Enabled = !string.IsNullOrWhiteSpace(Name);
        }

        private string _name;

        private async Task CreateProfileAsync()
        {
            Author author = new Author
            {
                Name = this.Name
            };


            await this.ChatService.AddAuthor(author);

            this.Author = author;

            await CompletionTask?.Invoke();
        }
    }
}
