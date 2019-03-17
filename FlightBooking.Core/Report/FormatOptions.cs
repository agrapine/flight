using System;
namespace FlightBooking.Core.Report
{
  public class FormatOptions
  {
    public static FormatOptions Default = new FormatOptions
    {
      VerticalWhiteSpace = Environment.NewLine + Environment.NewLine,
      NewLine = Environment.NewLine,
      Identation = "    "
    };

    public string VerticalWhiteSpace { get; private set; }
    public string NewLine { get; private set; }
    public string Identation { get; private set; }
  }
}
