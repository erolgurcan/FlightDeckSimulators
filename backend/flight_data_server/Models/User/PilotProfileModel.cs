using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace flight_data_server.Models.User
    {
    public class PilotProfileModel
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PilotId { get; set; }
        public int UserProfileID { get; set; }
        public string? PilotRating { get; set; }

        public string? PilotProfileName { get; set; }

        }
    }
