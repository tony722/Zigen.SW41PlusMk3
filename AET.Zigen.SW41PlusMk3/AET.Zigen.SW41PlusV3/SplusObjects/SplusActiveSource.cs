using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;
using AET.Zigen.SW41PlusV3.ApiObjects;
using AET.Zigen.SW41PlusV3.SplusObjects;

namespace AET.Zigen.SW41PlusV3 {
  public class SplusActiveSource : SplusObject<ActiveSource> {
    private ushort source;

    public SplusActiveSource() : base (null) {
      //This constructor is to allow Simpl+ visibility and should not be used
    }

    public SplusActiveSource(RestClient restClient) : base(restClient) { }

    public ushort Source {
      get { return source; }
      set {
        source = value;
        ObjectToSend.Source = (ushort)(value - 1);
      }
    }
    public override void Poll() { }
  }
}
