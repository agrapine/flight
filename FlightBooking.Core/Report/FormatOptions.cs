using System;
namespace FlightBooking.Core.Report
{
  public class FormatOptions
  {
    public static FormatOptions Default = new FormatOptions
    {
      VerticalSpace = Environment.NewLine + Environment.NewLine,
      NewLine = Environment.NewLine,
      Ident = "    "
    };

    public string VerticalSpace { get; private set; }
    public string NewLine { get; private set; }
    public string Ident { get; private set; }
  }
}
