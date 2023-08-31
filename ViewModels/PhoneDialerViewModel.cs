using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace MauiApp1.ViewModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

internal class PhoneDialerViewModel : ObservableObject
{

    public ICommand TranslatePhoneNum { get; private set; }
    public ICommand CallPhoneNum { get; private set; }
    public string EnteredNumber { get; private set; }
    public bool IsCallBtnEnabled { get; private set; }
    public string CallBtnText { get; private set; }


    internal string ToNumber(string raw)
    {
       return PhoneDialerUtilities.ToNumber(raw);

    }

    public PhoneDialerViewModel()
    {
        TranslatePhoneNum = new AsyncRelayCommand(Translate);
        CallPhoneNum = new AsyncRelayCommand(Call);
    }


    private async Task Translate()
    {
        
        string translatedNumber = ToNumber(EnteredNumber); ;
        Console.WriteLine("funcao executada");

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            Console.WriteLine("a string ta cheia");
            IsCallBtnEnabled = true;
            CallBtnText = "Call " + translatedNumber;
        }
        else
        {
            Console.WriteLine("a string ta vazia");
            IsCallBtnEnabled = false;
            CallBtnText = "Call";
        }

    }



    private async Task Call()
    {
        
        if (!string.IsNullOrEmpty(EnteredNumber))
        {
            string translatedNumber = ToNumber(EnteredNumber);

            bool callConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No");

            if (callConfirmed)
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported) //talvez tenha que mudar para PhoneDialerPage
                    {
                        PhoneDialer.Default.Open(translatedNumber);
                    }
                }
                catch (ArgumentNullException)
                {
                    await Application.Current.MainPage.DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
        

    }


    

}
