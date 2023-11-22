using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class PhoneDialerPage : ContentPage
{
    public PhoneDialerPage()
    {
        InitializeComponent();
        BindingContext = new PhoneDialerViewModel();
    }
    
}