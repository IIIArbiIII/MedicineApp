using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
        public ZdraviloDodaj()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            this.InitializeComponent();
        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
          //  this.Frame.Navigate(typeof(Pogledi.ZdraviloDodaj);
            /*
            try
            {
                Zdravilo z = new Zdravilo();
                z.Naziv = inputZdravilo.ToString();
                double inputKolicina2 = double.Parse(inputKolicina.Text);
                z.Kolicina = inputKolicina2;
                z.RokTrajanja = inputRokTrajanja.Date.DateTime;
                z.Enota = comboBoxVrstaZdravila.ToString();


                Baza.AddZdravilo(z);
            }
            catch 
            {
                throw new Exception("Napaka pri vnosu v bazo");
            }
            */
            
        }
    }
}
