using MauiAppDemoLive.Models;
using MauiAppDemoLive.PageModels;

namespace MauiAppDemoLive.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}