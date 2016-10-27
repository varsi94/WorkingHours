using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkingHours.Desktop.Interfaces;

namespace WorkingHours.Desktop.Dialogs
{
    public class DialogWrapper<TControl, TViewModel> : IDialogWrapper where TControl : FrameworkElement, new()
    {
        private readonly TaskCompletionSource<TViewModel> Tcs;

        private readonly DialogWindow dialogWindow;

        public DialogWrapper(string title, double width, double height)
        {
            dialogWindow = new DialogWindow
            {
                Title = title,
                DialogWrapper = this,
                Width = width,
                Height = height
            };
            dialogWindow.titleTextBlock.Text = title;
            dialogWindow.content.Content = new TControl();

            Tcs = new TaskCompletionSource<TViewModel>();
        }
        
        public Task<TViewModel> ShowDialogAsync()
        {
            dialogWindow.Show();
            return Tcs.Task;
        }

        public void Cancel()
        {
            Tcs.SetResult(default(TViewModel));
        }

        public void Ok(object input)
        {
            Tcs.SetResult((TViewModel) input);
        }
    }
}
