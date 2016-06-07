using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Contacts;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MedicineApp.Pogledi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SkrbnikPage : Page
    {
        public SkrbnikPage()
        {
            this.InitializeComponent();
            
            
            cbox_defaultMelodie.ItemsSource = Baza.SeznamMelodij;
            cbox_defaultMelodie.SelectedValuePath = "Value";
            cbox_defaultMelodie.DisplayMemberPath = "Key";

        }

        private void Btn_Shrani_OnClick(object sender, RoutedEventArgs e)
        {
            Skrbnik s = new Skrbnik(
                "Ime",
                "Priimek",
                "telefon",
                1234);
            throw new NotImplementedException();
        }

        private async void PickAContactButton_Click(object sender, RoutedEventArgs e)
        {
            ContactPicker contactPicker = new ContactPicker();
            contactPicker.CommitButtonText = "SelectThis";
           

            contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Email);
            contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Address);
            contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);
            Contact contact = await contactPicker.PickContactAsync();

            if (contact != null)
            {
                //OutputFields.Visibility = Visibility.Visible;
                //OutputEmpty.Visibility = Visibility.Collapsed;

                OutputName.Text = contact.DisplayName;

                AppendContactFieldValues(OutputEmails, contact.Emails);
                AppendContactFieldValues(OutputPhoneNumbers, contact.Phones);
                AppendContactFieldValues(OutputAddresses, contact.Addresses);
            }
            else
            {
                //OutputEmpty.Visibility = Visibility.Visible;
                //OutputFields.Visibility = Visibility.Collapsed;
            }
        }

        private void AppendContactFieldValues<T>(TextBlock content, IList<T> fields)
        {
            if (fields.Count > 0)
            {
                StringBuilder output = new StringBuilder();

                if (fields[0].GetType() == typeof(ContactEmail))
                {
                    foreach (ContactEmail email in fields as IList<ContactEmail>)
                    {
                        output.AppendFormat("Email: {0} ({1})\n", email.Address, email.Kind);
                    }
                }
                else if (fields[0].GetType() == typeof(ContactPhone))
                {
                    foreach (ContactPhone phone in fields as IList<ContactPhone>)
                    {
                        output.AppendFormat("Phone: {0} ({1})\n", phone.Number, phone.Kind);
                    }
                }
                else if (fields[0].GetType() == typeof(ContactAddress))
                {
                    List<String> addressParts = null;
                    string unstructuredAddress = "";

                    foreach (ContactAddress address in fields as IList<ContactAddress>)
                    {
                        addressParts = (new List<string> { address.StreetAddress, address.Locality, address.Region, address.PostalCode });
                        unstructuredAddress = string.Join(", ", addressParts.FindAll(s => !string.IsNullOrEmpty(s)));
                        output.AppendFormat("Address: {0} ({1})\n", unstructuredAddress, address.Kind);
                    }
                }

                content.Visibility = Visibility.Visible;
                content.Text = output.ToString();
            }
            else
            {
                content.Visibility = Visibility.Collapsed;
            }
        }
    }
}
