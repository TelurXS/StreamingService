using System.Net;
using System.Text;
using VideoDemos.Core.Auth;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace VideoDemos.ViewModels;

public partial class LoadingViewModel
{
    private INavigation _navigation;
    private AuthService _authService;

    public LoadingViewModel(INavigation navigation, AuthService authService)
    {
        _navigation = navigation;
        _authService = authService;
    }

}