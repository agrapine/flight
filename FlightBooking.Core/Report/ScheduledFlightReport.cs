using System;
using System.Text;

namespace FlightBooking.Core.Report
{
  public static class ScheduledFlightReport
  {
    public static string BuildReport(this ScheduledFlightSummary summary, FormatOptions formatOptions = default)
    {
      var fmt = formatOptions ?? FormatOptions.Default;

      var str = new StringBuilder();

      str.Append($"Flight summary for {summary.Title}");
      str.Append(fmt.VerticalSpace);
      str.Append($"Total passengers: {summary.SeatsTaken}{fmt.NewLine}");
      str.Append($"{fmt.Ident}General sales: {summary.GeneralPassengers}{fmt.NewLine}");
      str.Append($"{fmt.Ident}Discounted sales: {summary.DiscountedPassengers}{fmt.NewLine}");
      str.Append($"{fmt.Ident}Loyalty member sales: {summary.LoyaltyPassengers}{fmt.NewLine}");
      str.Append($"{fmt.Ident}Airline employee sales: {summary.AirlinePassengers}{fmt.VerticalSpace}");

      str.Append($"Total expected baggage: {summary.TotalExpectedBaggage}{fmt.VerticalSpace}");
      str.Append($"Total revenue from flight: {summary.ProfitFromFlight}{fmt.NewLine}");
      str.Append($"Total costs from flight: {summary.CostOfFlight}{fmt.NewLine}");


      if (summary.ProfitSurplus > 0)
        str.Append($"Flight generating profit of: {summary.ProfitSurplus}");
      else
        str.Append($"Flight losing money of: {summary.ProfitSurplus}");
      str.Append(fmt.VerticalSpace);

      str.Append($"Total loyalty points given away: {summary.TotalLoyaltyPointsAcrrued}{fmt.NewLine}");
      str.Append($"Total loyalty points redeemed: {summary.TotalLoyaltyPointsRedeemed}{fmt.NewLine}");

      str.Append(fmt.VerticalSpace);

      if(summary.CanFly)
        str.Append($"THIS FLIGHT MAY PROCEED");
      else
        str.Append($"FLIGHT MAY NOT PROCEED");
        

      return str.ToString();
    }
  }
}
