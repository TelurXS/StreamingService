using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;
using VideoDemos.Views.Auth.Registration;

namespace VideoDemos.Views.Profile;

public partial class GenreChangePage : ContentPage
{
    private List<DB_Genre> _genreNamesArray = new();
    private List<DB_Genre> _profileModel;

    public GenreChangePage()
    {
        InitializeComponent();

        _genreNamesArray =
            JsonConvert.DeserializeObject<List<DB_Genre>>(APIExecutor.ExecuteGet(Config.API_LINK + "/genres"));

        string pJson = APIExecutor.ExecuteGet(Config.API_LINK + "/manage/genres");
        _profileModel = JsonConvert.DeserializeObject<List<DB_Genre>>(pJson);
    }

    private void GenreButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string genre = button.Text;
        if (Equals(button.BackgroundColor, Colors.Transparent))
        {
            button.BackgroundColor = Color.FromArgb("#0042E2");
            _profileModel.Add(_genreNamesArray.Find(x => x.Name == genre));
        }
        else
        {
            button.BackgroundColor = Colors.Transparent;

            _profileModel.Remove(_profileModel.Find(x => x.Name == genre));
        }
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        GenreArray genreArray = new GenreArray();
        foreach (DB_Genre genre in _profileModel)
        {
            genreArray.GenresArray.Add(genre.Name);
        }
        APIExecutor.ExecutePost(Config.API_LINK + "/manage/genres", JsonConvert.SerializeObject(genreArray));

        await Shell.Current.GoToAsync($"..");
    }

    private void GenreChoosePage_OnLoaded(object sender, EventArgs e)
    {
        int totalWidth = 300;
        int minWidth = 110;
        int maxWidth = 221;
        bool _isFirst = true;
        int fistButtonWidth = 0;
        Random random = new Random();
        for (int i = 0; i < _genreNamesArray.Count(); i++)
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

            button.Text = _genreNamesArray[i].Name;
            button.Clicked += GenreButtonClicked;

            if (_profileModel.Any(x => x.Name == _genreNamesArray[i].Name))
            {
                button.BackgroundColor = Color.FromArgb("#0042E2");
            }

            GenreFlexLayout.Children.Add(button);
            _isFirst = !_isFirst;
        }
    }
}