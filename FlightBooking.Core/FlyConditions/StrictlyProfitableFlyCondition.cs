namespace FlightBooking.Core.FlyConditions
{
  /// <summary>
  /// Strictly profitable flight rule.
  /// </summary>
  public class StrictlyProfitableFlyCondition : IFlyCondition
  {
    public bool CanFly(ScheduledFlightSummary summary)
    {
      return summary.ProfitSurplus > 0
      && summary.SeatsTaken < summary.AvailableSeats
      && summary.SeatsTaken / (double)summary.AvailableSeats > summary.MinimumTakeOffPercentage;
    }
  }
}
