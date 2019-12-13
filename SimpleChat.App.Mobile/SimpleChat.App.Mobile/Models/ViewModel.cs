using System;
using System.Threading.Tasks;

namespace SimpleChat.App.Mobile.Models
{

    public class ViewModel : Model
    {
        public Func<Task> CompletionTask { get; set; }
    }
}