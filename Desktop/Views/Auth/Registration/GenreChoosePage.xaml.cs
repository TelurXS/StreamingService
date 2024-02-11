using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;
using VideoDemos.Core.Auth;

namespace VideoDemos.Views.Auth.Registration;

public partial class GenreChoosePage : ContentPage
{
    private List<string> _tempGenreNamesArray = new()
    {
        "Біографія",
        "Бойовик",
        "Вестерн",
        "Військовий",
        "Детектив",
        "Документальний",
        "Аніме",
        "Комедия",
        "Драма",
        "Криминал",
        "Мюзикл",
        "Пригоди",
        "Сімейні",
        "Спорт",
        "Жахи",
        "Триллер",
        "Жахи",
        "Фантастика",
        "Фентезі",
        "Історія",
        "Мультфільми",
    };

    public GenreChoosePage()
    {
        InitializeComponent();
    }

    private void GenreButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string genre = button.Text;
        if (Equals(button.BackgroundColor, Colors.Transparent))
        {
            button.BackgroundColor = Color.FromArgb("#0042E2");
            RegisterService.AccountModel.Genres.Add(new Genre(genre));
        }
        else
        {
            button.BackgroundColor = Colors.Transparent;
            RegisterService.AccountModel.Genres.Remove(new Genre(genre));
        }
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel.Genres.Count > 3)
        {
            await Shell.Current.GoToAsync($"/{nameof(ChoosePlanPage)}");
        }
    }

    async void LoginButton_OnClick(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel != null) RegisterService.AccountModel = null;
        await Shell.Current.GoToAsync($"///{nameof(AuthPage)}");
    }

    private void GenreChoosePage_OnLoaded(object sender, EventArgs e)
    {
        int totalWidth = 300;
        int minWidth = 110;
        int maxWidth = 221;
        bool _isFirst = true;
        int fistButtonWidth = 0;
        Random random = new Random();
        for (int i = 0; i < _tempGenreNamesArray.Count(); i++)
        {
            Button button = new Button();

            if (_isFirst)
            {
                fistButtonWidth = random.Next(minWidth, maxWidth);
                button.WidthRequest = fistButtonWidth;
            }
            else
            {
                button.WidthRequest = totalWidth - fistButtonWidth;
            }

            button.Text = _tempGenreNamesArray[i];
            button.Clicked += GenreButtonClicked;


            GenreFlexLayout.Children.Add(button);
            _isFirst = !_isFirst;
        }
    }
}