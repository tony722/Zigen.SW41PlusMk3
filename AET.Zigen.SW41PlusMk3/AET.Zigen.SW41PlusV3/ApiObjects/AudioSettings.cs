using System.Linq;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json;

namespace AET.Zigen.SW41PlusV3.ApiObjects {

  public class AudioSettings : ApiCommandObject<AudioSettings> {
    public AudioSettings() : base("/SetAudioSettings", "/GetAudioSettings") { }

    [JsonProperty("audiosel")]
    public string AudioSelect { get; set; }

    [JsonProperty("mute")]
    public bool? Mute { get; set; }

    [JsonProperty("volume")]
    public ushort? Volume { get; set; }

    [JsonProperty("tune mode")]
    public string TuneMode { get; set; }

    [JsonProperty("presets")]
    public string Preset { get; set; }

    #region EQ Bands

    [JsonProperty("band0")]
    public double? Band115 { get; set; }

    [JsonProperty("band1")]
    public double? Band330 { get; set; }

    [JsonProperty("band2")]
    public double? Band990 { get; set; }

    [JsonProperty("band3")]
    public double? Band3000 { get; set; }

    [JsonProperty("band4")]
    public double? Band9900 { get; set; }
    #endregion

    #region Bass/Treble

    [JsonProperty("basstone")]
    public double? Bass { get; set; }

    [JsonProperty("treble")]
    public double? Treble { get; set; }
    #endregion 

    #region Surround
    [JsonProperty("surround")]
    public bool? Surround { get; set; }

    [JsonProperty("surrlevel")]
    public ushort? SurroundLevel { get; set; }
    #endregion

    #region BassEnhancement
    [JsonProperty("bass")]
    public bool? BassEnhancement { get; set; }

    [JsonProperty("basslevel")]
    public ushort? BassLevel { get; set; }

    [JsonProperty("bassfreq")]
    public ushort? BassCutoff { get; set; }


    [JsonProperty("highpass")]
    public bool? HighPass { get; set; }
    #endregion

    #region RequiredFieldsAreValid
    public override bool RequiredFieldsAreValid() {
      if (!AudioSelectIsValid()) return false;
      if (!TuneModeIsValid()) return false;
      if (!PresetIsValid()) return false;
      if (!ValueIsValid(BassCutoff, "BassCutoff", new ushort?[] { 80, 100, 125, 150, 175, 200, 225 })) return false;
      return true;
    }

    private bool AudioSelectIsValid() {
      if (AudioSelect == "") AudioSelect = null;
      else if (AudioSelect != null) {
        if (AudioSelect != "local" && AudioSelect != "arc") {
          return FalseWithErrorMessage("SW41PlusV3.AudioSettings: AudioSelect must be 'local' or 'arc'.");
        }
      }
      return true;
    }

    private bool TuneModeIsValid() {
      return ValueIsValid(TuneMode, "TuneMode", new[] { "disabled", "presets", "equalizer", "tonecontrol" });
    }

    private bool PresetIsValid() {
      return ValueIsValid(Preset, "Preset", new[] { "flat", "rock", "classical", "dance", "acoustic" });
    }

    private bool ValueIsValid<T>(T value, string name, T[] allowedValues) {
      if (value == null) return true;
      if (allowedValues.Contains(value)) return true;
      return FalseWithErrorMessage("SW41PlusV3.AudioSettings: {0} must be {1}.", name, allowedValues.FormatAsList());
    }

    private bool ValueIsValid(ushort? value, string name, ushort minValue, ushort maxValue) {
      if (value == null) return true;
      if (value >= minValue && value <= maxValue) return true;
      return FalseWithErrorMessage("SW41PlusV3.AudioSettings: {0} must be {1} to {2}.", name, minValue, maxValue);
    }
    #endregion
  }
}
