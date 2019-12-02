using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            DataGrid.Visibility = Visibility.Visible;
            List<Character> characters = new List<Character>();
            JObject json = new JObject();
            WebClient client = new WebClient();
            int pageNumber;
            if (Int32.TryParse(PageTextBlock.Text, out pageNumber) == false)
            {
                MessageBox.Show("Поля должны быть целочисленными!");
            }
            else
            {
                PageHasher<Character> hash = new PageHasher<Character>();
                if (hash.HasThisPage(pageNumber))
                    DataGrid.ItemsSource = hash.GetPage(pageNumber);
                else
                {
                    try
                    {
                        json = JObject.Parse(client.DownloadString(new Uri($"https://swapi.co/api/people/?page={pageNumber}")));
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при получение данных,\n возможно вы ввели несуществующую страницу");
                        return;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        Character character = new Character
                        {
                            Name = json["results"][i]["name"].ToString(),
                            Height = json["results"][i]["height"].ToString(),
                            Mass = json["results"][i]["mass"].ToString(),
                            HairColor = json["results"][i]["hair_color"].ToString(),
                            SkinColor = json["results"][i]["skin_color"].ToString(),
                            EyeColor = json["results"][i]["eye_color"].ToString(),
                            BirthYear = json["results"][i]["birth_year"].ToString(),
                            Gender = json["results"][i]["gender"].ToString(),
                            Homeworld = json["results"][i]["homeworld"].ToString(),
                        };
                        characters.Add(character);
                    }
                    hash.AddPageInfo(characters, pageNumber);
                    DataGrid.ItemsSource = characters;
                }
            }
        }
    }
}
