using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleChat.App.Mobile
{
    public partial class App : Application
    {
        public static App Instance { get; private set; }


        public App()
        {
            Instance = this;

            InitializeComponent();

            Uri uri = new Uri("https://simplechatserver.azurewebsites.net/");
            ChatService chatService = new ChatService(uri);

            Guid? userId = getUserId();

            if (userId.HasValue)
            {
                CreateTopicsListPage(chatService, userId.Value);
            }
            else
            {
                CreateRegisterPage(chatService);
            }


        }

        private void CreateTopicsListPage(ChatService chatService, Guid authorId)
        {
            var page = new TopicsListPage();

            var navPage = new NavigationPage(page);

            page.BindingContext = new Models.TopicsListPageViewModel(navPage.Navigation, chatService, authorId);

            this.MainPage = navPage;
        }

        private void CreateRegisterPage(ChatService chatService)
        {
            var page = new CreateProfilePage();
            var navPage = new NavigationPage(page);

            var vm = new Models.CreateProfilePageViewModel(chatService, navPage.Navigation);

            vm.CompletionTask = async () =>
            {
                await SetUserIdAsync(vm.Author.Id);

                CreateTopicsListPage(chatService, vm.Author.Id);
            };

            page.BindingContext = vm;


            this.MainPage = navPage;
        }

        public const string USER_ID = "user_id";

        private Guid? getUserId()
        {
            if (Properties.ContainsKey(USER_ID))
            {
                object userIdObject = Properties[USER_ID];

                if (userIdObject is Guid g)
                {
                    return g;
                }
            }
            return null;
        }

        public async Task SetUserIdAsync(Guid id)
        {
            Properties[USER_ID] = id;
            await this.SavePropertiesAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
