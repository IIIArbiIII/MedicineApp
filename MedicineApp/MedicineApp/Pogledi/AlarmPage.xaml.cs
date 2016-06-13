using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Calls;
using Windows.Storage;
using Windows.Storage.BulkAccess;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MedicineApp.Pogledi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlarmView : Page
    {
        // TODO: Popravi razporeditev elementov
        List<Zdravilo> seznamZdravil = new List<Zdravilo>();
        List<Interval> seznamIntervalov = new List<Interval>();
        //List<DateTime> seznamUrZaToastNotificatione;

        Dictionary<int, string> seznamZaDneve = new Dictionary<int, string>();
        Dictionary<int, string> seznamZaUre = new Dictionary<int, string>();
        Dictionary<int, string> seznamZaDoze = new Dictionary<int, string>();

        Zdravilo z = new Zdravilo();
        //od vključno 0 pa navzgor
        int rowCount = 1;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            seznamZdravil = e.Parameter as List<Zdravilo>;
            if (seznamZdravil.Count != 0)
            {
                comboBoxZdravilo.ItemsSource = seznamZdravil;
            }
        }

        public AlarmView()
        {
            this.InitializeComponent();
            var k = ToastNotificationManager.History;
        }

        private async void Btn_NovaNavodila_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Poglej da ni mogoce vzet vec zdravila kot pa ga je na voljo

            var collection = grid_instruction.Children.OfType<ComboBox>().ToList();
            var btn_collection = grid_instruction.Children.OfType<Button>().ToList();

            bool izpolnjeniComboboxi = false;
            //pregled ce so izpolnjeni vsi 3 cboxi
            for (int i = collection.Count(); i > collection.Count()-3; i--)
            {
                if (collection[i-1].SelectedItem != null)
                {
                    izpolnjeniComboboxi = true;
                    continue;
                }
                izpolnjeniComboboxi = false;
                break;
            }

            //zablokiraj prejsni gumb
            if (izpolnjeniComboboxi)
            {

                // TODO:Preveri da vneseni podatki ne presegajo maximumov
                string imeComboBoxaZaDan = "comboBox_interval_dan" + rowCount;
                string imeComboBoxaZaUro = "comboBox_interval_ura" + rowCount;
                string imeComboBoxaZaDozo = "comboBox_interval_doza" + rowCount;

                int stDni = int.Parse(collection[collection.Count() - 3].SelectedValue.ToString());
                int stUr = int.Parse(collection[collection.Count() - 2].SelectedValue.ToString());
                int stEnot = int.Parse(collection[collection.Count()-1].SelectedValue.ToString());

                Dictionary<int, string> noviseznamZaDoze = new Dictionary<int, string>();
                int steviloEnotZdravila = (stDni * (24 / stUr)) * stEnot;
                //int counter = 0;
                int vmesni = seznamZaDoze.Count() - steviloEnotZdravila;

                if (steviloEnotZdravila > seznamZaDoze.Count())
                {
                    //TODO: naredi popup pa mu tu nastavi lastnosi
                    var messageDialog = new MessageDialog("Zdravila je premalo.");
                    await messageDialog.ShowAsync();

                    return;
                }

                //blokiraj gumb
                for (int i = 0; i < btn_collection.Count(); i++)
                {
                    if (i == btn_collection.Count() - 1)
                    {
                        btn_collection[i].IsEnabled = false;
                    }
                }

                for (int i = 0; i < vmesni; i++)
                {
                    noviseznamZaDoze.Add(seznamZaDoze.ElementAt(i).Key, seznamZaDoze.ElementAt(i).Value);
                }
                seznamZaDoze = noviseznamZaDoze;

                // TODO: dodaj novo vrsto
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(1, GridUnitType.Star);

                grid_instruction.RowDefinitions.Add(r);

                //vstavi comboboxe
                ComboBox cb1 = new ComboBox();
                cb1.Name = imeComboBoxaZaDan;

                cb1.ItemsSource = seznamZaDneve;
                cb1.DisplayMemberPath = "Value";
                cb1.SelectedValuePath = "Key";

                ComboBox cb2 = new ComboBox();
                cb2.Name = imeComboBoxaZaUro;
                cb2.ItemsSource = seznamZaUre;
                cb2.DisplayMemberPath = "Value";
                cb2.SelectedValuePath = "Key";

                ComboBox cb3 = new ComboBox();
                cb3.Name = imeComboBoxaZaDozo;
                cb3.ItemsSource = noviseznamZaDoze;
                //cb3.ItemsSource = seznamZaDoze;
                cb3.DisplayMemberPath = "Value";
                cb3.SelectedValuePath = "Key";

                Button btn = new Button();
                btn.Click += Btn_NovaNavodila_OnClick;
                btn.Content = "+";

                grid_instruction.Children.Add(cb1);
                Grid.SetRow(cb1, rowCount);
                Grid.SetColumn(cb1, 0);

                grid_instruction.Children.Add(cb2);
                Grid.SetRow(cb2, rowCount);
                Grid.SetColumn(cb2, 1);

                grid_instruction.Children.Add(cb3);
                Grid.SetRow(cb3, rowCount);
                Grid.SetColumn(cb3, 2);

                grid_instruction.Children.Add(btn);
                Grid.SetRow(btn, rowCount);
                Grid.SetColumn(btn, 3);

                rowCount++;

                
            }

            //--------
           

        }

        private void comboBoxZdravilo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            btn_NovaNavodila.IsEnabled = true;

            //set cboxes
            #region
            z = (sender as ComboBox).SelectedItem as Zdravilo;

            seznamZaDneve = new Dictionary<int, string>();
            seznamZaUre = new Dictionary<int, string>();
            seznamZaDoze = new Dictionary<int, string>();
            //dan
            for (int i = 1; i <= 31; i++)
            {
                switch (i)
                {
                    case 1:
                        string a = i + " dan";
                        seznamZaDneve.Add(i, a);
                        break;

                    case 2:
                        string b = i + " dneva";
                        seznamZaDneve.Add(i, b);
                        break;

                    default:
                        string c = i + " dni";
                        seznamZaDneve.Add(i, c);
                        break;
                }
            }
            comboBox_interval_dan.ItemsSource = seznamZaDneve;
            comboBox_interval_dan.DisplayMemberPath = "Value";
            comboBox_interval_dan.SelectedValuePath = "Key";

            //ura
            for (int i = 1; i <= 24; i++)
            {
                switch (i)
                {
                    case 1:
                        string a = "Na " + i + " uro";
                        seznamZaUre.Add(i, a);
                        break;

                    case 2:
                        string b = "Na " + i + " uri";
                        seznamZaUre.Add(i, b);
                        break;

                    default:
                        string c = "Na " + i + " ure";
                        seznamZaUre.Add(i, c);
                        break;
                }
            }

            comboBox_interval_ura.ItemsSource = seznamZaUre;
            comboBox_interval_ura.DisplayMemberPath = "Value";
            comboBox_interval_ura.SelectedValuePath = "Key";

            //doze
            for (int i = 1; i <= z.Kolicina; i++)
            {
                string a = i + " " + z.Enota;
                seznamZaDoze.Add(i, a);
            }

            comboBox_interval_doza.ItemsSource = seznamZaDoze;
            comboBox_interval_doza.DisplayMemberPath = "Value";
            comboBox_interval_doza.SelectedValuePath = "Key";

            //pobrisi prejsne intervale
            var children = grid_instruction.Children.ToList();
            if (children.Count > 5)
            {
                for (int i = 5; i < children.Count; i++)
                {
                    grid_instruction.Children.Remove(children[i]);
                }
            }
            


            #endregion
        }

        private async void ButtonSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Gumb za pocistis intervale
            // TODO: preglej da je vse vpisano sele pol idi dalje. Fali za datum

            if (!IsEverythingValid())
                return;

            Interval interval = new Interval();
            //List<Interval> seznamIntervalov = new List<Interval>();

            var coll = grid_instruction.Children.OfType<ComboBox>().ToList();

            for (int i = 0; i < coll.Count(); i++)
            {
                interval.Dan = int.Parse(coll[i].SelectedValue.ToString());
                interval.Ure = int.Parse(coll[i + 1].SelectedValue.ToString());
                interval.Doza = int.Parse(coll[i + 2].SelectedValue.ToString());

                seznamIntervalov.Add(interval);

                interval = new Interval();
                i = i + 2;
            }

            Opomnik opomnik = new Opomnik();
            int IdZdravila = int.Parse(comboBoxZdravilo.SelectedValue.ToString());
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timePickZdravilo.Time.Hours, timePickZdravilo.Time.Minutes, timePickZdravilo.Time.Seconds);
            //alarm_n1.Zdravilo1 = Baza.GetFirstZdraviloById(IdZdravila);
            //IzracunajKonecJemanja(seznamIntervalov, dt);
            //alarm_n1.KonecJemanja = seznamUrZaToastNotificatione[seznamUrZaToastNotificatione.Count() - 1];

            opomnik.IdZdravilo = IdZdravila;
            opomnik.ZacetekJemanja = dt;
            opomnik.Intervali = seznamIntervalov;
            opomnik.IzracunajCasovneTermine();


            // TODO: preglej melodijo ce je default
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            opomnik.Melodija = localSettings.Values["DefaultMelodie"].ToString();

            //TODO:this here is a fuckfest
            //int OpomnikId = await Baza.AddOpomnikAsync(opomnik);
            int idOpomnika = Baza.ShraniOpomnik(opomnik);
            var k = await Baza.GetOpomnikById(idOpomnika);
            MakeToastNotifications(k);
            Frame.Navigate(typeof (MainPage));

        }
        static void MakeToastNotifications(Opomnik opomnik)
        {
            foreach (var x in opomnik.Intervali)
            {
                foreach (var y in x.SeznamTerminovZaAlarm)
                {
                    Windows.Data.Xml.Dom.XmlDocument toastXml = new Windows.Data.Xml.Dom.XmlDocument();
                    string toastXmlTemplate = "<toast scenario=\'alarm\' launch=\'app-defined-string\'>" +
                                              "<visual>" +
                                              "<binding template =\'ToastGeneric\'>" +
                                              "<text>" + opomnik.Zdravilo1.Naziv + "</text>" +
                                              "<text>" +
                                              "Vzeti je potrebno: " + x.Doza + " " + opomnik.Zdravilo1.Enota +
                                              "</text>" +
                                              "</binding>" +
                                              "</visual>" +
                                              "<audio src=\'" + opomnik.Melodija + "\'" + "/>" + 
                                              "<actions>" +
                                              "<action activationType=\'foreground\' content =\'yes\' arguments=\'" + y.Id + "\'" +
                                              "/>" +
                                              "</actions>" +
                                              "</toast>";

                    toastXml.LoadXml(toastXmlTemplate);
                    //y.TerminZazvonenje = DateTime.Now.AddSeconds(10);
                    var toast = new Windows.UI.Notifications.ScheduledToastNotification(toastXml, y.TerminZazvonenje);
                    Random rnd = new Random();
                    toast.Id = rnd.Next(10000).ToString();
                    if (Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().Setting ==
                        NotificationSetting.Enabled)
                    {
                        Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
                    }
                }
            }
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }
            
        private bool IsEverythingValid()
        {
            int stPrazniElementov = 0;

            Interval interval = new Interval();
            //List<Interval> seznamIntervalov = new List<Interval>();
            int steviloEnotZdravila = 0;
            var coll = grid_instruction.Children.OfType<ComboBox>().ToList();

            for (int i = 0; i < coll.Count(); i++)
            {
                interval.Dan = int.Parse(coll[i].SelectedValue.ToString());
                interval.Ure = int.Parse(coll[i + 1].SelectedValue.ToString());
                interval.Doza = int.Parse(coll[i + 2].SelectedValue.ToString());

                steviloEnotZdravila += (interval.Dan * (24 / interval.Ure)) * interval.Doza;
                
                i = i + 2;
            }

            if (steviloEnotZdravila > z.Kolicina)
            {
                //TODO: Popup za prevec izbranega zdravila
                //var messageDialog = new MessageDialog("Zdravilo vam bo zmanjkalo.");
                return false;
            }

            foreach (var x in coll)
            {
                if (x.SelectedValue == null)
                {
                    x.BorderBrush = new SolidColorBrush(Colors.Red);
                    stPrazniElementov++;
                }
                else
                {
                    x.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 133, 133, 133));
                }
            }
            return stPrazniElementov == 0;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
