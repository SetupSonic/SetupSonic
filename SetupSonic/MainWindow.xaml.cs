using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace SetupSonic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<SonicProfile> ProfileCollection { get; set; }
        public ObservableCollection<RealmChoice> RealmChoiceList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            RealmChoiceList = new ObservableCollection<RealmChoice>()
            {
                new RealmChoice() { Value = "uswest", DisplayName = "U.S West" },
                new RealmChoice() { Value = "useast", DisplayName = "U.S East" },
                new RealmChoice() { Value = "europe", DisplayName = "Europe" },
                new RealmChoice() { Value = "asia", DisplayName = "Asia" }
            };

            ProfileCollection = JsonConvert.DeserializeObject<ObservableCollection<SonicProfile>>(File.ReadAllText(Constants.SonicSettings));
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            File.WriteAllText(Constants.SonicSettings, JsonConvert.SerializeObject(ProfileCollection, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            }));
        }

        private void OnQuitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.ReadMeUrl);
            e.Handled = true;
        }
    }
}
