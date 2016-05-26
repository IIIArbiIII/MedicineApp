using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MedicineApp
{
    // TODO: Popravi nalaganje v listview. Asinhrono dobi bazo, nato pa zapuni listview. Brisi oni asynch iz event handlerja
    //Sprobavam
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();

        bool deleteZdravila = false;
        bool updateSkrbnik = false;

        //Testni podatki
        Zdravilo z1 = new Zdravilo("Lekadol", new DateTime(2016, 5, 26), 10, "Tablet");
        Zdravilo z2 = new Zdravilo("Aspirin", new DateTime(2017, 9, 22), 20, "Tablet");
        Zdravilo z3 = new Zdravilo("Ventolin", new DateTime(2017, 12, 16), 120, "Vpihov");
        Zdravilo z4 = new Zdravilo("Avamys", new DateTime(2018, 6, 14), 60, "Vpihov");
        Zdravilo z5 = new Zdravilo("Bronhobol", new DateTime(2016, 2, 5), 10, "Tablet");

        Skrbnik s1 = new Skrbnik("Janez", "Novak", "030356152", 4562);
        Skrbnik s2 = new Skrbnik("Miha", "Podgorelec", "040356152", 4562);

        List<Zdravilo> seznamVsehZdravilIzBaze = new List<Zdravilo>();
        //List<Zdravilo> zdravilaZaBox = new List<Zdravilo>();
        //List<Skrbnik> skrbnikiList = new List<Skrbnik>();
        //-------------------------------------------------------------------
        public MainPage()
        {

            // this.InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            Baza b = new Baza();

            /*
            if (b.CreateDb())
                ZapolniBazo();
            */       
            if (deleteZdravila)
            {
                Baza.DeleteZdravilo(z1);
                Baza.DeleteZdravilo(z2);                
            }
            if (updateSkrbnik)
            {
                Baza.UpdateSkrbnik(s2);
            }
            
            this.InitializeComponent();
            //zapolnilistbox();
        }

        private void ZapolniBazo()
        {
            Baza.AddZdravilo(z1);
            Baza.AddZdravilo(z2);
            Baza.AddZdravilo(z3);
            Baza.AddZdravilo(z4);
            Baza.AddZdravilo(z5);

        }

        void timer_Tick(object sender, object e)
        {
            secondHand.Angle = DateTime.Now.Second * 6;
            minuteHand.Angle = DateTime.Now.Minute * 6;
            hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
        }

        async Task<bool> zapolnilistbox()
        {
            try
            {
                //testing toaster
                OnScheduleToast();
                //seznamVsehZdravilIzBaze = await Baza.GetAllDZdraviloAsync();
                //listviewZravilo.ItemsSource = seznamVsehZdravilIzBaze;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void OnScheduleToast()
        {
            string xml = @"<toast>
            <visual>
            <binding template=""ToastGeneric"">
                <text>Hello!</text>
                <text>This is a scheduled toast!</text>
            </binding>
            </visual>
        </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            ScheduledToastNotification toast = new ScheduledToastNotification(doc, DateTimeOffset.Now.AddSeconds(1));
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }

        
        //tvoj del
        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOdpriOknoDodajZdravilo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pogledi.ZdraviloDodaj));
        }

        private void lstbZdravila_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // Zdravilo z = new Zdravilo();

            

            // TO-DO novi view za zdravila lstbZdravila.SelectedItem 

        }

        private void txtFiltriraj_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            List<Zdravilo> neka = new List<Zdravilo>();

            foreach (var l in seznamVsehZdravilIzBaze)
            {
                if (l.Naziv.ToLower().Contains(txtFiltriraj.Text.ToLower()))
                {
                    neka.Add(l);
                }
            }

            listviewZravilo.ItemsSource = neka;
        }

        private void txtFiltriraj_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pogledi.NastavitvePage));
        }

        private async void listviewZravilo_Loaded(object sender, RoutedEventArgs e)
        {
            
            // TODO: kaj ce traje vec ko 5sek?
            Task<List<Zdravilo>> seznamVsehZdravil = Baza.GetAllDZdraviloAsync();
            await Task.Delay(TimeSpan.FromSeconds(2));
            progressBar_getDB.ShowPaused = true;
            progressBar_getDB.Visibility = Visibility.Collapsed;
            listviewZravilo.ItemsSource = await seznamVsehZdravil;
            seznamVsehZdravilIzBaze = await seznamVsehZdravil;
        }

        private void Btn_newAlarm_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Pogledi.AlarmView), seznamVsehZdravilIzBaze);
        }
    }
}
