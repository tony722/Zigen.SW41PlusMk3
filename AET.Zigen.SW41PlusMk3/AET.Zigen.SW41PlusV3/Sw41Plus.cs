using System.Net;
using AET.Zigen.SW41PlusV3.Api;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;

namespace AET.Zigen.SW41PlusV3 {
  public class Sw41Plus : RestClient {

    public Sw41Plus() : base(new CrestronHttpClient(4)){
    }

    public Sw41Plus(IHttpClient httpClient) : base(httpClient) {
      SetVideoOutF = delegate { };
    }

    public void Initialize() {
      AudioSettings = new AudioSettings(this);
      AudioSettings.Initialize();
      ActiveSource = new ActiveSource(this);
    }

    public ushort Debug {
      set { HttpClient.Debug = value; }
    }


    public AudioSettings AudioSettings { get; set; }
    public ActiveSource ActiveSource { get; set; }

    public void Poll() {
      ActiveSource.Poll();
      AudioSettings.Poll();
    }

    public SetUshortOutputDelegate SetVideoOutF { get; set; }

  }
}