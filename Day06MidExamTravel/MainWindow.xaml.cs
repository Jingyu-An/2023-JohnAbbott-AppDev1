using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Day06MidExamTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new JingyuTripDbContext();
                LvTravelList.ItemsSource = Globals.dbContext.Trips.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string destination = TbxDestination.Text;
                string name = TbxTravellerName.Text;
                string passportNo = TbxPassportNo.Text;

                if (DatepickerDeparture.SelectedDate == null || DatepickerReturn.SelectedDate == null)
                {
                    throw new ArgumentException("Please select a date");
                }
                DateTime departureDate = DatepickerDeparture.SelectedDate.Value;
                DateTime returnDate = DatepickerReturn.SelectedDate.Value;

                Globals.dbContext.Trips.Add(new Trip(destination, name, passportNo, departureDate, returnDate));
                Globals.dbContext.SaveChanges();
                LvTravelList.ItemsSource = Globals.dbContext.Trips.ToList();
                ResetFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Trip trip = LvTravelList.SelectedItem as Trip;

            if (trip == null) return;

            var result = MessageBox.Show(this, "Do you want to delete this trip?\n" + 
                $"[Destination: {trip.Destination}, Traveller Name:{trip.TravellerName}]",
                "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            Globals.dbContext.Trips.Remove(trip);
            Globals.dbContext.SaveChanges();
            LvTravelList.ItemsSource = Globals.dbContext.Trips.ToList();
            ResetFields();
        }

        private void LvTravelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trip trip = LvTravelList.SelectedItem as Trip;
            if (trip != null)
            {
                TbxDestination.Text = trip.Destination;
                TbxTravellerName.Text = trip.TravellerName;
                TbxPassportNo.Text = trip.TravellerPassportNo;
                DatepickerDeparture.SelectedDate = trip.DepartureDate;
                DatepickerReturn.SelectedDate = trip.ReturnDate;
            }
            else
            {
                ResetFields();
            }
        }

        private void ResetFields()
        {
            TbxDestination.Text = "";
            TbxTravellerName.Text = "";
            TbxPassportNo.Text = "";
            DatepickerDeparture.SelectedDate = DateTime.Today;
            DatepickerReturn.SelectedDate = DateTime.Today;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Trip trip = LvTravelList.SelectedItems[0] as Trip;
            try
            {
                string destination = TbxDestination.Text;
                string name = TbxTravellerName.Text;
                string passportNo = TbxPassportNo.Text;

                if (DatepickerDeparture.SelectedDate == null || DatepickerReturn.SelectedDate == null)
                {
                    throw new ArgumentException("Please select a date");
                }
                DateTime departureDate = DatepickerDeparture.SelectedDate.Value;
                DateTime returnDate = DatepickerReturn.SelectedDate.Value;

                Trip updateTrip = Globals.dbContext.Trips.Find(keyValues: trip.Id);

                updateTrip.Destination = destination;
                updateTrip.TravellerName = name;
                updateTrip.TravellerPassportNo = passportNo;
                updateTrip.SetDepartureReturnDates(departureDate, returnDate);

                Globals.dbContext.SaveChanges();
                LvTravelList.ItemsSource = Globals.dbContext.Trips.ToList();
                ResetFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Data File (*.data)|*.data|All File (*.*)|*.*|Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int i = 0; i < LvTravelList.SelectedItems.Count; i++)
                        {
                            Trip trips = LvTravelList.SelectedItems[i] as Trip;
                            writer.WriteLine($"{trips.Destination};{trips.TravellerName};" +
                                $"{trips.TravellerPassportNo};{trips.DepartureDate};{trips.ReturnDate}");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
