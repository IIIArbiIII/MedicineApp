using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        List<Interval> seznamIntervalov = new List<Interval>();

        Dictionary<int, string> seznamZaDneve = new Dictionary<int, string>();
        Dictionary<int, string> seznamZaUre = new Dictionary<int, string>();
        Dictionary<int, string> seznamZaDoze = new Dictionary<int, string>();
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
        }

        private void Btn_NovaNavodila_OnClick(object sender, RoutedEventArgs e)
        {
            string imeComboBoxaZaDan = "comboBox_interval_dan" + rowCount;
            string imeComboBoxaZaUro = "comboBox_interval_ura" + rowCount;
            string imeComboBoxaZaDozo = "comboBox_interval_doza" + rowCount;


            //dodaj novo vrsto
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
            cb3.ItemsSource = seznamZaDoze;
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

            //shrani interval

        }

        private void comboBoxZdravilo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set cboxes
            #region
            Zdravilo z = (sender as ComboBox).SelectedItem as Zdravilo;

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

        private void comboBox_interval_dan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Zbrisi ta event
            Dictionary<int,string> text = (sender as ComboBox).SelectedItem as Dictionary<int, string>;
            string s = "";
        }

        private void ButtonSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Zakleni gumbe da se ne sprozi predcasno

            IEnumerable<ComboBox> collection = grid_instruction.Children.OfType<ComboBox>();
            Interval interval = new Interval();
            List<Interval> seznamIntervalov = new List<Interval>();

            List<ComboBox> coll = collection.ToList();

            for (int i = 0; i < coll.Count(); i++)
            {
                interval.Dan = int.Parse(coll[i].SelectedValue.ToString());
                interval.Ure = int.Parse(coll[i + 1].SelectedValue.ToString());
                interval.Doza = int.Parse(coll[i + 2].SelectedValue.ToString());

                seznamIntervalov.Add(interval);

                interval = new Interval();
                i = i + 2;
            }
            string s = "";

        }
    }
}
