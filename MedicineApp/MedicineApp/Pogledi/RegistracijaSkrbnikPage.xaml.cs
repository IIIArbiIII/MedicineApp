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
    public sealed partial class RegistracijaSkrbnikPage : Page
    {
        public RegistracijaSkrbnikPage()
        {
            this.InitializeComponent();
        }

        private void btnSharniNovegaSkrbnika_Click(object sender, RoutedEventArgs e)
        {
            //test
            Skrbnik s = new Skrbnik();

            s.Ime = txtImeSkrbnikaRegister.Text;
            s.Priimek = txtPriimekSkrbnikaRegister.Text;
            s.TelStevilka = txtTelefonskaSkrbnikaRegister.Text;
            int pin2 = int.Parse(txtPinSkrbnikaRegister.Text);
            pin2 = s.Pin;

            Baza.AddSkrbnik(s);

        }
    }
}
