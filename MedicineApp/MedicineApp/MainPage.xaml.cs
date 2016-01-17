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
using SQLite.Net;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MedicineApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Zdravilo zd = new Zdravilo("Aspirin",new DateTime(2016,5,10),10);
            Baza b = new Baza();
            if (b.CreateDB())
            {
                 Baza.AddZdravilo(zd);
            }

            Zdravilo k;
            string imeZdravila = "lekadol";
            //k = Baza.GetFirstZdraviloByName(imeZdravila.ToLower());


           

            this.InitializeComponent();
        }

        private void ZapolniBazo()
        {
            Zdravilo z1 = new Zdravilo("Lekadol", new DateTime(2016, 5, 26), 10);
            Zdravilo z2 = new Zdravilo("Aspirin", new DateTime(2017, 9, 22), 10);
            Zdravilo z3 = new Zdravilo("Ventolin", new DateTime(2017, 12, 16), 10);
            Zdravilo z4 = new Zdravilo("Avamys", new DateTime(2018, 6, 14), 10);
            Zdravilo z5 = new Zdravilo("Bronhobol", new DateTime(2016, 2, 5), 10);

            Skrbnik s1 = new Skrbnik("Janez","Novak","030356152",4562);


           

        }
    }
}
