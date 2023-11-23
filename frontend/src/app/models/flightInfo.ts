export interface FlightInfo {
    id: number;
    flightCode: string;
    airliner: string;
    isActive: boolean;
    departureDateHour: Date;
    arivalDateHour: Date;
    arivalAirportCode: string;
    departureAirportCode: string;
  }