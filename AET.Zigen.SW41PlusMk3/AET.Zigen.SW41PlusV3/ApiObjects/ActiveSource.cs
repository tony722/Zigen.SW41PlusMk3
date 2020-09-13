using AET.Unity.RestClient;
using Newtonsoft.Json;

namespace AET.Zigen.SW41PlusV3.ApiObjects {

  /// <summary> Sets the desired source to route to the sink. </summary>
  public class ActiveSource : ApiCommandObject<ActiveSource> {

    public ActiveSource() : base("/SetActiveSource", null) {   }

    [JsonProperty("source")] public ushort Source { get; set; }
   
    public override bool RequiredFieldsAreValid() {
      return Source >= 0 && Source <= 3;
    }
  }
}

