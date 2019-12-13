using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleChat.App.Mobile.Models
{
    public class AsyncCommand : Model, ICommand
    {
        public AsyncCommand(Func<Task> func)
        {
            this.Func = func;
        }

        public Func<Task> Func { get; }
        public bool Enabled { get => _enabled; set => Set(ref _enabled, value, OnCanExecuteChanged); }

        public bool Executing { get => _executing; set => Set(ref _executing, value); }

        public event EventHandler CanExecuteChanged;


        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool _enabled = true;
        private bool _executing;

        bool ICommand.CanExecute(object parameter)
        {
            return Enabled;
        }

        async void ICommand.Execute(object parameter)
        {
            Executing = true;
            await Func?.Invoke();
            Executing = false;
        }
    }
}