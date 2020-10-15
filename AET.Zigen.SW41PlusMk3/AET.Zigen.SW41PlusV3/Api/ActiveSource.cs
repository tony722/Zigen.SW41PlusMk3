using System.Diagnostics;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.SW41PlusV3.Api {
  public class ActiveSource : Sw41PlusObject {
    private ushort source;

    public ActiveSource(Sw41Plus sw41) : this() {
      Sw41Plus = sw41;
    }
    public ActiveSource() : base("/SetActiveSource", "/GetActiveSource") { }

    public ushort Source {
      get { return source; }
      set {
        if (source == value) return;
        SourceF = value;
        Switch(value);
      }
    }

    public ushort SourceF {
      set {
        source = value;
        Sw41Plus.SetVideoOutF(value);
      }
    }

    private void Switch(int input) {
      if (InputIsValid()) {
        string json = string.Format(@"{{""source"":{0}}}", input - 1);
        Sw41Plus.HttpPost(SetUrl, json);
      }
    }

    internal bool InputIsValid() {
      if (Source < 1 || Source > 4) return ApiObject.FalseWithErrorMessage("SW41PlusV3.Source({0}): Must be between 1 and 4", Source);
      return true;
    }

    public void Poll() {
      var response = Sw41Plus.HttpGet(GetUrl);
      ParseMatrix(response);
    }

    private void ParseMatrix(string response) {
      var json = JObject.Parse(response);
      var value = json["source"].Value<ushort>();
      SourceF = (ushort)(value + 1);
    }
  }
}
