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
    public sealed partial class AlarmView : Page
    {
        // TODO: Popravi razporeditev elementov
        List<Zdravilo> seznamZdravil = new List<Zdravilo>();

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
        }

        private void Btn_NovaNavodila_OnClick(object sender, RoutedEventArgs e)
        {
            ComboBox cb1 = new ComboBox();
            ComboBox cb2 = new ComboBox();
            ComboBox cb3 = new ComboBox();

            grid_instruction.Children.Add(cb1);
            Grid.SetRow(cb1, 1);
            Grid.SetColumn(cb1, 0);

            grid_instruction.Children.Add(cb2);
            Grid.SetRow(cb2, 1);
            Grid.SetColumn(cb2, 1);

            grid_instruction.Children.Add(cb3);
            Grid.SetRow(cb3, 1);
            Grid.SetColumn(cb3, 2);
        }

        private void comboBoxZdravilo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set cboxes
            #region
            Zdravilo z = (sender as ComboBox).SelectedItem as Zdravilo;
            Dictionary<int, string> seznamZaDneve = new Dictionary<int, string>();
            Dictionary<int, string> seznamZaUre = new Dictionary<int, string>();
            Dictionary<int, string> seznamZaDoze = new Dictionary<int, string>();
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

#endregion
        }

        private void comboBox_interval_dan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Zbrisi ta event
            Dictionary<int,string> text = (sender as ComboBox).SelectedItem as Dictionary<int, string>;
            string s = "";
        }
    }
}
