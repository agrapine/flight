using System;
using System.Text;

namespace FlightBooking.Core
{
  public class ScheduledFlightSummary
  {
    public string Title { get; set; }
    public double CostOfFlight { get; set; }
    public double ProfitFromFlight { get; set; }

    public int TotalLoyaltyPointsAcrrued { get; set; }
    public int TotalLoyaltyPointsRedeemed { get; set; }
    public int TotalExpectedBaggage { get; set; }
    public int SeatsTaken { get; set; }

    /* a Dictionary<PassengetType, Count> would be nicer ...*/
    public int AirlinePassengers { get; set; }
    public int LoyaltyPassengers { get; set; }
    public int GeneralPassengers { get; set; }
    public int DiscountedPassengers { get; set; }

    public double ProfitSurplus => ProfitFromFlight - CostOfFlight;

    public int AvailableSeats { get; set; }

    public double MinimumTakeOffPercentage { get; set; }

    public bool CanFly { get; set; }
  }
}
