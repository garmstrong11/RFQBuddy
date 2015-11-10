namespace RFQBuddy.Domain
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Xml.Linq;

  public class Service
  {
    private readonly XText _nameAndRev;
    private readonly List<XText> _amountSpan;
    private readonly XNamespace _ns = "http://www.w3.org/1999/xhtml";
    private readonly List<Option> _options;

    public Service() { }

    public Service(XElement baseElement, Product parent)
    {
      _nameAndRev = baseElement.Elements(_ns + "span").Nodes().OfType<XText>().FirstOrDefault();
      _amountSpan = baseElement.Elements(_ns + "span").Elements(_ns + "span").Nodes().OfType<XText>().ToList();
      _options = new List<Option>();
      Product = parent;
    }

    public Product Product { get; private set; }

    public string Name
    {
      get { return Regex.Replace(_nameAndRev.Value, @"\(R\d+\)", "").Trim(); }
    }

    public string Revision
    {
      get
      {
        var trimmed = _nameAndRev.Value.Trim();
        return trimmed.Substring(trimmed.Length - 4).Trim('(', ')');
      }
    }

    public string BidAmount
    {
      get { return _amountSpan[1].Value.Trim(); }
    }

    public void AddSpecs(XElement element)
    {
      var opts = element.ByDivClassName("opts");
      foreach (var opt in opts)
      {
        _options.Add(new Option(opt, this));
      }
    }

    public IReadOnlyCollection<Option> Options
    {
      get { return _options.AsReadOnly(); }
    } 
  }
}