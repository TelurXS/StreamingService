using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metflix.Core.Models;
using Newtonsoft.Json;
using VideoDemos.Core.Auth;
using VideoDemos.Core.Backend;

namespace VideoDemos.Views.Auth.Registration;

public partial class GenreChoosePage : ContentPage
{
    private List<DB_Genre> _genreNamesArray = new();

    public GenreChoosePage()
    {
        InitializeComponent();
        _genreNamesArray =
            JsonConvert.DeserializeObject<List<DB_Genre>>(APIExecutor.ExecuteGet(Config.API_LINK + "/genres"));
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
            RegisterService.AccountModel.Genres.GenresArray.Remove(genre);
        }
    }

    async void OnContinueClicked(object sender, EventArgs e)
    {
        if (RegisterService.AccountModel.Genres.GenresArray.Count > 3)
        {
            List<string> dbQ = new List<string>();
            GenreArray genreArray = new GenreArray();
            foreach (var data in RegisterService.AccountModel.Genres.GenresArray)
            {
                dbQ.Add(data);
            }
            genreArray.GenresArray = dbQ;
            APIExecutor.ExecutePost(Config.API_LINK + "/manage/genres", JsonConvert.SerializeObject(genreArray));
            
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


            GenreFlexLayout.Children.Add(button);
            _isFirst = !_isFirst;
        }
    }
}

public class GenreArray
{
    [JsonProperty("genres")] public List<string> GenresArray { get; set; }
    public GenreArray()
    {
        GenresArray = new List<string>();
    }
}