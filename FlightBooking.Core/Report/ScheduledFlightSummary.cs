using System;
using System.Text;

namespace FlightBooking.Core.Report
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

    public int GeneralPassengers { get; set; }
    public int LoyaltyPassengers { get; set; }
    public int EmployeePassengers { get; set; }

    public double ProfitSurplus { get; set; }

    public int AvailableSeats { get; set; }

    public bool CanProceed { get; set; }

    public double MinimumTakeOffPercentage { get; set; }
  }
}
