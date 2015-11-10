namespace RFQBuddy.Acquisition.Contracts
{
  using System;
  using System.Xml.Linq;

  public interface IJobDataAcquisitionSource
  {
    XElement AcquireJobData(string url);
  }
}