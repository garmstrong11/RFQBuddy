namespace RFQBuddy.Domain
{
  using System.Collections.Generic;
  using System.Xml.Linq;

  public class Product
  {
    private readonly XElement _sourceElement;

    public Product(XElement sourceElement)
    {
      _sourceElement = sourceElement;
      Services = new List<Service>();
    }

    public string Name
    {
      get { return _sourceElement.Value; }
    }

    public override string ToString()
    {
      return string.Format("{0} - {1} services", Name, Services.Count);
    }

    public List<Service> Services { get; set; }
  }
}