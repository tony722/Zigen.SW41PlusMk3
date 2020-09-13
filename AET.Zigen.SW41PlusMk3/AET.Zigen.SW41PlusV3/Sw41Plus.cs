using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.SW41PlusV3.ApiObjects;

namespace AET.Zigen.SW41PlusV3 {
  public class Sw41Plus  {
    private readonly RestClient restClient = new RestClient();

    public ushort Debug {
      set { restClient.HttpClient.Debug = value;  }
    }

    public Sw41Plus() {
      AudioSettings = new SplusAudioSettings(restClient);
      ActiveSource = new SplusActiveSource(restClient);
    }

    public SplusAudioSettings AudioSettings { get; set; }
    public SplusActiveSource ActiveSource { get; set; }

    public IHttpClient HttpClient {
      set { restClient.HttpClient = value; }
    }

    public string HostName {
      set { restClient.HostName = value; }
    }
  }
}
