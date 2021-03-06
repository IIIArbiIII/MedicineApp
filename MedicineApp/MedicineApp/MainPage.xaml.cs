﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net;
using System.Windows;
using Windows.UI.ViewManagement;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MedicineApp
{
    // TODO: Ce je baza prazna naj se prikaze obvestilo v listvievu da naj uporabnik doda zdravila

    //Sprobavam
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string defaultMelodie = "ms - winsoundevent:Notification.Default";

        DispatcherTimer timer = new DispatcherTimer();
        //Task<List<Zdravilo>> seznamVsehZdravil;
        //bool deleteZdravila = false;
        //bool updateSkrbnik = false;

        //Testni podatki
        //Zdravilo z1 = new Zdravilo("Lekadol", new DateTime(2016, 5, 26), 10, "Tablet");
        //Zdravilo z2 = new Zdravilo("Aspirin", new DateTime(2017, 9, 22), 20, "Tablet");
        //Zdravilo z3 = new Zdravilo("Ventolin", new DateTime(2017, 12, 16), 120, "Vpihov");
        //Zdravilo z4 = new Zdravilo("Avamys", new DateTime(2018, 6, 14), 60, "Vpihov");
        //Zdravilo z5 = new Zdravilo("Bronhobol", new DateTime(2016, 2, 5), 10, "Tablet");
        //Zdravilo z6 = new Zdravilo("ProbaBrisi", new DateTime(2018, 5, 5), 20, "Tablet");

        //Skrbnik s1 = new Skrbnik("Joze", "Pepic", "031111222", 1234);
        List<Zdravilo> seznamVsehZdravilIzBaze = new List<Zdravilo>();
        List<Opomnik> seznamVsehOpomnikov = new List<Opomnik>();
        //List<Zdravilo> zdravilaZaBox = new List<Zdravilo>();
        //List<Skrbnik> skrbnikiList = new List<Skrbnik>();
        //-------------------------------------------------------------------
        public MainPage()
        {
          var sss =  Baza.GetAllOpomnikIds();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            //DownloadDb();
            //DownloadDb();
            this.InitializeComponent();
        }


        //private void ZapolniBazo()
        //{
        //    Baza.AddZdravilo(z1);
        //    Baza.AddZdravilo(z2);
        //    Baza.AddZdravilo(z3);
        //    Baza.AddZdravilo(z4);
        //    Baza.AddZdravilo(z5);
        //    Baza.AddZdravilo(z6);

        //    Baza.AddSkrbnik(s1);

        //}


        void timer_Tick(object sender, object e)
        {
            KaziUro();
            /* ANALOGNA URA
            secondHand.Angle = DateTime.Now.Second * 6;
            minuteHand.Angle = DateTime.Now.Minute * 6;
            hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5); */
        }

        private void btnOdpriOknoDodajZdravilo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pogledi.ZdraviloDodaj));
        }

        private void lstbZdravila_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //string s = "";
            // TODO: Naredi stran za info o zdravilu ter jo prikazi
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

        private async void ZapolniListView()
        {
            //TODO: Ce je baza prazna daj obvestilo
            if (seznamVsehZdravilIzBaze.Count() == 0)
                listviewZravilo.ItemsSource = await Baza.GetAllDZdraviloAsync();
            else if (seznamVsehZdravilIzBaze.Count() > 0)
                listviewZravilo.ItemsSource = await Baza.GetAllDZdraviloAsync();
            else
                listviewZravilo.ItemsSource = null;

        }
        //toto sem jaz napisal pa kličem tvojo. pravim ti da te metode jaz niam v projektu how the fuck :O magic
        private async void ZapolniListOpomnik()
        {

            if(seznamVsehOpomnikov.Count() == 0)
            {
                listViewOpomnik.ItemsSource = await Baza.GetAllOpomnikAsync();
            }
            else if (seznamVsehOpomnikov.Count() > 0)
            {
                listViewOpomnik.ItemsSource = await Baza.GetAllOpomnikAsync();
            }
            else
            {
                listViewOpomnik.ItemsSource = null;
            }
        }

        private void Btn_newAlarm_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pogledi.AlarmView), seznamVsehZdravilIzBaze);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            seznamVsehZdravilIzBaze = await Baza.GetAllDZdraviloAsync();
            stopwatch.Stop();
            progressBar_getDB.ShowPaused = true;
            progressBar_getDB.Visibility = Visibility.Collapsed;
            ZapolniListView();
            ZapolniListOpomnik();

            #region BackgroundTask
            //var messageDialog = new MessageDialog("Čas potreben za pridobitev baze {0}.", stopwatch.Elapsed.ToString());
            //messageDialog.ShowAsync();
            ////Moja koda - brisi ce bojo problemi; 
            //await BackgroundExecutionManager.RequestAccessAsync();

            //RegisterBackgroundTask("MyTask.FirstTask", "FirstTask", new TimeTrigger(20, false),
            //new SystemCondition(SystemConditionType.InternetAvailable));
            //;
            //string myTaskName = "FirstTask";

            //// check if task is already registered
            //foreach (var cur in BackgroundTaskRegistration.AllTasks)
            //    if (cur.Value.Name == myTaskName)
            //    {
            //        await (new MessageDialog("Task already registered")).ShowAsync();
            //        return;
            //    }

            //// Windows Phone app must call this to use trigger types (see MSDN)
            //await BackgroundExecutionManager.RequestAccessAsync();

            //// register a new task
            //BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder { Name = "First Task", TaskEntryPoint = "MyTask.FirstTask" };
            //taskBuilder.SetTrigger(new TimeTrigger(15, true));
            //BackgroundTaskRegistration myFirstTask = taskBuilder.Register();

            //await (new MessageDialog("Task registered")).ShowAsync();
            #endregion
        }

        #region BackgroundTaskImplementation
        // Register a background task with the specified taskEntryPoint, name, trigger,
        // and condition (optional).
        //
        // taskEntryPoint: Task entry point for the background task.
        // taskName: A name for the background task.
        // trigger: The trigger for the background task.
        // condition: Optional parameter. A conditional event that must be true for the task to fire.
        //
        //public static BackgroundTaskRegistration RegisterBackgroundTask(string taskEntryPoint,
        //                                                                string taskName,
        //                                                                IBackgroundTrigger trigger,
        //                                                                IBackgroundCondition condition)
        //{
        //    foreach (var cur in BackgroundTaskRegistration.AllTasks)
        //    {

        //        if (cur.Value.Name == taskName)
        //        {
        //            return (BackgroundTaskRegistration)(cur.Value);
        //        }
        //    }

        //    //
        //    // Register the background task.
        //    //

        //    var builder = new BackgroundTaskBuilder();

        //    builder.Name = taskName;
        //    builder.TaskEntryPoint = taskEntryPoint;
        //    builder.SetTrigger(trigger);

        //    if (condition != null)
        //    {

        //        builder.AddCondition(condition);
        //    }
        //    BackgroundTaskRegistration task = builder.Register();

        //    return task;
        //}
        #endregion


        private void listviewZravilo_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Baza.DeleteZdravilo((Zdravilo)listviewZravilo.SelectedValue);
            ZapolniListView();
        }


        public void KaziUro()
        {
            DigitalnaUraApp.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void PolniListZdravil()
        {
            Zdravilo z = new Zdravilo();
            listviewZravilo.Items.Add(z);
        }

        private void pivotTab_Loaded(object sender, RoutedEventArgs e)
        {
            PolniListZdravil();
        }

        private void MenuFlyout_Opening(object sender, object e)
        {
            Zdravilo z = (Zdravilo)listviewZravilo.SelectedItem;
            podrobnostiFlyout.Text = "Naziv: " + z.Naziv + "\n" + "Rok Trajanja: " + z.RokTrajanja.ToString("MM.dd.yyyy") + "\n" + "Koloičina: " + z.Kolicina.ToString();
        }
      
    }

}
