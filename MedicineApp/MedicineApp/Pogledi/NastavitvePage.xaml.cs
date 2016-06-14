using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class NastavitvePage : Page
    {

        public NastavitvePage()
        {
            this.InitializeComponent();

            if (Baza.IsSkrbnikDefault())
            {
                
            }
                //this.Frame.Navigate(typeof(SkrbnikPage));

        }

        private void btnSkrbniGesloVstopi_Click(object sender, RoutedEventArgs e)
        {
            int pin;
            Skrbnik skrbnik;
            if (int.TryParse(passBoxSkrbnik.Password, out pin))
            {
                skrbnik = Baza.GetSkrbnik(pin);
            }
            else
            {
                return;
            }

            if (skrbnik != null)
            {
                this.Frame.Navigate(typeof(SkrbnikPage));
            }
            else
            {
                napakaGeslo.Text = "Vnešeno napačno geslo !";
            }
        }
    }
}
