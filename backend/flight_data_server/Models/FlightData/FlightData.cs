using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flight_data_server.Models.FlightData
    {
    public class FlightData
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("FlightInfo")]
        public String FlightCode { get; set; }

        public DateTime LoggingTime { get; set; }

        public double TrueAirSpeed { get; set; }

        public double GroundSpeed { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double Throttle { get; set; }

        public double Heading { get; set; }

        public double Throttle1 { get; set; }

        public double Throttle2 { get; set; }

        public double AOA { get; set; }

        public double TotalFuel { get; set; }

        public double OutsideTemperature { get; set; }

        public int LandingGear { get; set; }


        }
    }
