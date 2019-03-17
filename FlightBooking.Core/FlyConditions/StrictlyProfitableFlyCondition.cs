namespace FlightBooking.Core.FlyConditions
{
  /// <summary>
  /// Strictly profitable flight rule.
  /// </summary>
  public class StrictlyProfitableFlyCondition : IFlyCondition
  {
    public bool CanFly(FlightSummary summary, Plane plane)
    {
      return summary.ProfitSurplus > 0
      && summary.SeatsTaken < plane.NumberOfSeats
      && summary.SeatsTaken / (double)plane.NumberOfSeats > summary.MinimumTakeOffPercentage;
    }
  }
}
