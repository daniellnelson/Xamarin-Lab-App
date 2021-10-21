using Newtonsoft.Json;
using RestSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using techy.Models;
using techy.Utils;
using techy.Views;
using Xamarin.Forms;

namespace techy.ViewModels
{
    [QueryProperty(nameof(Games), nameof(Games))]
    public class AddGameListViewModel : BaseViewModel
    {
        //Button 
        //Text box to search
        //search function that pops AddGameList Page
        private int index = 0;
        private string gameName;

        public Command<Game> GameTapped { get; }

        private Game _selectedGame;
        public string GameName
        {
            get => gameName;
            set => SetProperty(ref gameName, value);
        }

        public Command SearchCommand { get; }
        public ObservableCollection<Game> Games { get; set; }

        private async void SearchGame()
        {
            index++;
            if(index < 2)
            {
                var authToken = "";
                var gameName = GameName;
                var clientID = "9afdbcy5ef0pfzq18h1s01s0pa1ryo";
                var clientSecret = "tfookxc5opog564079tfkvnjuvi39i";
                try
                {
                    var client = new RestClient("https://id.twitch.tv/");
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest("/oauth2/token");
                    request.AddParameter("client_id", clientID);
                    request.AddParameter("client_secret", clientSecret);
                    request.AddParameter("grant_type", "client_credentials");
                    request.AddHeader("Connection", "keep-alive");

                    //  request.AddHeader("header", "value");
                    //request.AddFile("file", path);
                    var response = client.Post(request);
                    var content = response.Content; // Raw content as string
                    string[] words = content.Split(':');
                    authToken = words[1];
                    var ttl = words[2];
                    string[] ttlwords = ttl.Split(',');
                    ttl = ttlwords[0];
                    ttl = ttl.Replace('"', ' ');
                    ttl = ttl.Trim();
                    int time = int.Parse(ttl);
                    string[] phrases = authToken.Split('"');
                    authToken = phrases[1];
                    authToken = authToken.Replace('"', ' ');
                    authToken = authToken.Trim();
                }
                catch (Exception ex)
                {
                    DialogService.ShowMessage(ex.Message, "Error");
                }
                try
                {
                    var client = new RestClient("https://api.igdb.com/v4/");
                    string url = "games?fields=name,id&limit=10&search=" + gameName;
                    string bearerToken = "Bearer " + authToken;
                    var request = new RestRequest(url);

                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("Client-ID", clientID);
                    request.AddHeader("Authorization", bearerToken);

                    var response = client.Post(request);
                    var content = response.Content; // Raw content as string

                    var game = JsonConvert.DeserializeObject<IEnumerable<Game>>(content);
                    //Games.Clear();
                    getGames(game);
                    await Shell.Current.GoToAsync($"{nameof(SearchedGamePage)}?{nameof(SearchedGameViewModel.Items)}={game}");

                    // string json = JsonConvert.SerializeObject(obj);
                }
                catch (Exception ex)
                {
                    DialogService.ShowMessage(ex.Message, "Error");
                    ListFilled = false;
                }

                index = 0;

            }
            else
            {
                ListFilled = false;
            }
        }

        public async void getGames(IEnumerable<Game> game)
        {
            var db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await db.DropTableAsync<Game>().ConfigureAwait(false);
            await db.CreateTableAsync<Game>().ConfigureAwait(false);
            await db.RunInTransactionAsync(trans =>
            {
                try
                {
                    foreach(var item in game)
                    {
                        trans.Insert(new Game
                        {
                            id = item.id,
                            name = item.name
                        });
                    }
                   
                }
                catch (Exception ex)
                {

                }
            }).ConfigureAwait(false);
        }

        public bool listFilled = false;

        public bool ListFilled
        {
            get => listFilled;
            set => SetProperty(ref listFilled, value);
        }


        public AddGameListViewModel()
        {
            SearchCommand = new Command(SearchGame);
        }

    }
}
