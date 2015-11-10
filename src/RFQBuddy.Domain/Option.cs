namespace RFQBuddy.Domain
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Xml.Linq;

  public class Option
  {
    private readonly List<string> _texts;

    public Option(XElement sourceElement, Service service)
    {
      Service = service;
      _texts = sourceElement
        .DescendantNodes()
        .OfType<XText>()
        .Where(t => !string.IsNullOrWhiteSpace(t.Value))
        .Select(t => t.Value.Trim())
        .ToList();
    }

    public Service Service { get; private set; }

    public string Name
    {
      get { return _texts.First(); }
    }

    public string Value
    {
      get
      {
        var value = string.Join(", ", _texts.Skip(1));
        return Regex.Replace(value, @" {2,}", " ");
      }
    }
  }
}