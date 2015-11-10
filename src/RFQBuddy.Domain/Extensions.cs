namespace RFQBuddy.Domain
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Xml.Linq;

  public static class Extensions
  {
    public static IEnumerable<XElement> ByDivClassName(this XElement element, string className)
    {
      XNamespace ns = "http://www.w3.org/1999/xhtml";
      return element.Descendants(ns + "div")
        .Where(d => d.Attribute("class").Value == className);
    }

    public static string GetClassName(this XElement element)
    {
      XNamespace ns = "http://www.w3.org/1999/xhtml";
      var noClass = element.Attributes().All(a => a.Name != (ns + "class"));

      return noClass ? string.Empty : element.Attribute("class").Value;
    }
  }
}