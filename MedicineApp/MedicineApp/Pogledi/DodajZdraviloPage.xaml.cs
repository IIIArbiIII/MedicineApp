using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MedicineApp.Pogledi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class ZdraviloDodaj : Page
    {
        static bool zdraviloIsValidated = false;

        void set_boxes()
        {
            //set asb_tipEnote
            // TODO: Dodaj vec enot
            List<string> seznamEnot = new List<string>();
            seznamEnot.Add("Tablet");
            seznamEnot.Add("Vpihov");
            seznamEnot.Add("mL");
            seznamEnot.Add("g");

            asb_tipEnote.ItemsSource = seznamEnot;
            //


        }

        public ZdraviloDodaj()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            // TODO: Lepo porazporedi elemente
            this.InitializeComponent();

            set_boxes();
        }

        bool inputValidation()
        {
            // TODO: Dodaj msg za uspeh/neuspeh
            
            return true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
                Zdravilo z = new Zdravilo();
                z.Naziv = inputZdravilo.Text;
                z.Kolicina = double.Parse(txtbox_stEnot.Text);
                z.RokTrajanja = inputRokTrajanja.Date.DateTime;
                z.Enota = asb_tipEnote.Text;

            try
            {
                Baza.AddZdravilo(z);
                Frame.Navigate(typeof (MainPage));
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void inputZdravilo_TextChanged(object sender, TextChangedEventArgs e)
        {
            //inputZdravilo
            string naziv = inputZdravilo.Text;
            bool naziv_result = naziv.All(Char.IsLetter);
            string errorMsg = "Dovoljene so samo črke";

            if (!naziv_result || naziv == "")
            {
                inputZdravilo.BorderBrush = new SolidColorBrush(Colors.Red);
                
                inputZdravilo.PlaceholderText = errorMsg;
                btn_dodajZdravilo.IsEnabled = false;
            }
            else
            {
                inputZdravilo.BorderBrush = new SolidColorBrush(Colors.Green);
                btn_dodajZdravilo.IsEnabled = true;

            }
        }

        private void txtbox_stEnot_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtbox_stEnot
            string steviloEnotZdravila = txtbox_stEnot.Text;
            bool steviloEnotZdravila_result = steviloEnotZdravila.All(Char.IsNumber);
            string errorMsg = "Dovoljene so samo številke";

            if (!steviloEnotZdravila_result)
            {
                txtbox_stEnot.BorderBrush = new SolidColorBrush(Colors.Red);
                txtbox_stEnot.PlaceholderText = errorMsg;
                btn_dodajZdravilo.IsEnabled = false;
            }
            else
            {
                txtbox_stEnot.BorderBrush = new SolidColorBrush(Colors.Green);
                btn_dodajZdravilo.IsEnabled = true;
            }
        }

        private void asb_tipEnote_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //asb_tipEnote
            string tipZdravila = asb_tipEnote.Text;
            bool tipZdravila_result = tipZdravila.All(Char.IsLetter);
            string errorMsg = "Dovoljene so samo črke";

            if (!tipZdravila_result)
            {
                asb_tipEnote.BorderBrush = new SolidColorBrush(Colors.Red);
                asb_tipEnote.PlaceholderText = errorMsg;
                btn_dodajZdravilo.IsEnabled = false;
            }
            else
            {
                asb_tipEnote.BorderBrush = new SolidColorBrush(Colors.Green);
                btn_dodajZdravilo.IsEnabled = true;
            }
        }

        private void inputRokTrajanja_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            //inputRokTrajanja
            DateTime rokTrajanja = inputRokTrajanja.Date.Date;
            DateTime danes = DateTime.Now;

            int rokTrajanja_result = DateTime.Compare(rokTrajanja, danes);

            if (rokTrajanja_result <= 0)
            {
                inputRokTrajanja.Foreground = new SolidColorBrush(Colors.Red);            
                btn_dodajZdravilo.IsEnabled = false;
            }
            else
            {
                inputRokTrajanja.Foreground = new SolidColorBrush(Colors.Green);
                btn_dodajZdravilo.IsEnabled = true;
            }


        }
    }
}
