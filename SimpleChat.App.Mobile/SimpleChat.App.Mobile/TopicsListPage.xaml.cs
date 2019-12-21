using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleChat.App.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopicsListPage : ContentPage
    {
        public TopicsListPage()
        {
            InitializeComponent();
        }

        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is Models.TopicsListPageViewModel vm)
            {
                await vm.UpdateTopicsAsync();
            }
        }
    }
}