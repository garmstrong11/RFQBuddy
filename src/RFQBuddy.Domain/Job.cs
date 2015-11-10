namespace RFQBuddy.Domain
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Xml.Linq;

  public class Job
  {
    private readonly List<string> _texts;
    private readonly XNamespace _ns = "http://www.w3.org/1999/xhtml";

    public Job(XElement element)
    {
      Products = new List<Product>();
      var spans = element.Descendants(_ns + "span");
      _texts = spans.DescendantNodes().OfType<XText>().Select(s => s.Value).ToList();
    }

    public string Name
    {
      get { return _texts[0]; }
    }

    public string Description
    {
      get { return string.Join("", _texts.Skip(1)); }
    }

    public List<Product> Products { get; set; }

    public override string ToString()
    {
      return string.Format("{0} - {1}", Name, Description);
    }
  }
}