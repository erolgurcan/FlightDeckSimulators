using flight_data_server.Database;
using flight_data_server.Interface;
using flight_data_server.Models.FlightData;

namespace flight_data_server.Services.DBFunctions
    {
    public class FlightDataInfoFunctions : DatabaseFunctions<FlightInfo>, IFlightDataInfoFunctions
        {
        private readonly FlightDataDBContext _db;

        public FlightDataInfoFunctions(FlightDataDBContext db) : base(db)
            {
            this._db = db;
            }

        }
    }
