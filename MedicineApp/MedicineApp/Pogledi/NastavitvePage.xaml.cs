﻿using System;
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

        }

        private void btnSkrbniGesloVstopi_Click(object sender, RoutedEventArgs e)
        {
            //test
            Skrbnik s = new Skrbnik("Janez", "Novak", "030356152", 4545);
        
            if (s.Pin.ToString() == passBoxSkrbnik.ToString())
            {
                this.Frame.Navigate(typeof(SkrbnikPage));
            }
            else
            {
                napakaGeslo.Text = "Vnsešeno napačno geslo !";
            }
        }

        private void btnNovSkrbnik_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistracijaSkrbnikPage));
        }
    }
}
