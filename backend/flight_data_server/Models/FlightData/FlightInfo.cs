using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flight_data_server.Models.FlightData
    {
    public class FlightInfo
        {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        public String FlightCode { get; set; }

        public int Airliner { get; set; }

        public Boolean isActive { get; set; }

        public DateTime DepartedDate { get; set; }


        public String ArivalAirportCode { get; set; }

        public String DepartureAirportCode { get; set; }

        public String AirplaneModel { get; set; }

        public String TailNumber { get; set; }

        public int PilotID { get; set; }

        }
    }
