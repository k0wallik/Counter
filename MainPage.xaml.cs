using System.Text.Json; 
using Microsoft.Maui.Controls; 
using System.IO;

using Counter.ViewModels;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Ustawienie ViewModelu jako kontekstu danych
            BindingContext = new MainPageViewModel();
        }
    }
}
