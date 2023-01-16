using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day06MidExamTravel
{
    public class Trip
    {
        public Trip() { }

        public Trip(string destination, string travellerName, string travellerPassportNo, 
            DateTime departureDate, DateTime returnDate)
        {
            Destination = destination;
            TravellerName = travellerName;
            TravellerPassportNo = travellerPassportNo;
            SetDepartureReturnDates(departureDate, returnDate);
        }

        public int Id { get; set; }

        private string _destination;

        [Required]
        [StringLength(50)]
        public string Destination
        {
            get { return _destination; }
            set
            {
                if (value.Length < 2 || value.Length > 30)
                {
                    throw new ArgumentException("Destination length must be 2-30 characters long");
                }
                _destination = value;
            }
        }

        private string _travellerName;

        [Required]
        [StringLength(50)]
        public string TravellerName
        {
            get { return _travellerName; }
            set 
            { 
                if (value.Length < 2 || value.Length > 30)
                {
                    throw new ArgumentException("Name length must be 2-30 characters long");
                }
                _travellerName = value; 
            }
        }

        private string _travellerPassportNo;

        [Required]
        [StringLength(50)]
        public string TravellerPassportNo
        {
            get { return _travellerPassportNo; }
            set 
            {
                Regex regex = new Regex(@"^[A-Z]{2}\d{6}$");
                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("Passport Number must start capital character and must be only charaters and numbers");
                }
                _travellerPassportNo = value;
            }
        }


        private DateTime _departureDate;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureDate 
        {
            get { return _departureDate; }
        }

        private DateTime _returnDate;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReturnDate 
        { 
            get { return _returnDate; }
        }

        public void SetDepartureReturnDates(DateTime dep, DateTime ret)
        {
            if (dep > ret)
            {
                throw new ArgumentException("Rerutn date must be greater than return date.");
            }
            _departureDate = dep;
            _returnDate = ret;
        }
    }
}
