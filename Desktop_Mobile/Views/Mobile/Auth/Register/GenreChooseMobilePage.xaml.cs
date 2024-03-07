using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Mobile.Auth.Register;

public partial class GenreChooseMobilePage : ContentPage
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



    public GenreChooseMobilePage()
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
            RegisterService.AccountModel.Genres.GenresArray.Add(genre);
        }
        else
        {
            button.BackgroundColor = Colors.Transparent;
            RegisterService.AccountModel.Genres.GenresArray.Remove((genre));
        }
    }
    private async void Button_OnClicked(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel.Genres.GenresArray.Count >= 3)
        {
            List<string> dbQ = new List<string>();
            GenreArray genreArray = new GenreArray();
            foreach (var data in RegisterService.AccountModel.Genres.GenresArray)
            {
                dbQ.Add(data);
            }

            genreArray.GenresArray = dbQ;
            APIExecutor.ExecutePost(Config.API_LINK + "/manage/genres", JsonConvert.SerializeObject(genreArray));

            await Shell.Current.GoToAsync($"/{nameof(ChoosePlanMobilePage)}");
        }
    }

    private void GenreChooseMobilePage_OnLoaded(object sender, EventArgs e)
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