namespace RFQBuddy.Acquisition
{
  using System;
  using System.Runtime.Serialization;

  [Serializable]
  public class AcquisitionFailedException : Exception
  {
    public AcquisitionFailedException() {}
    public AcquisitionFailedException(string message) : base(message) {}
    public AcquisitionFailedException(string message, Exception inner) : base(message, inner) {}

    protected AcquisitionFailedException(
      SerializationInfo info,
      StreamingContext context) : base(info, context) {}
  }
}