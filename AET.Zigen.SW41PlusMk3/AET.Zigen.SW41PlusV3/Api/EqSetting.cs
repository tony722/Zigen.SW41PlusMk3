using System;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.SW41PlusV3.Api {
  public class EqSetting {
    private short currentValue;
    private double currentValueScaled;

    public EqSetting() {
      FeedbackDelegate = delegate { };
      TextFeedbackDelegate = delegate { };
    }

    public EqSetting(AudioSettings audioSettings, string jsonName) : this() {
      AudioSettings = audioSettings;
      JsonName = jsonName;
    }

    public AudioSettings AudioSettings { get; set; }
    public string JsonName { get; set; }
    
    public SetShortOutputDelegate FeedbackDelegate { get; set; }

    public SetStringOutputDelegate TextFeedbackDelegate { get; set; }
    
    public short Value {
      get { return currentValue; }
      set {
        var valueScaled = ConvertEqFrom16Bit(value);
        if (currentValueScaled == valueScaled) return;
        AudioSettings.Post(JsonName, valueScaled);
        UpdateFeedback(value, valueScaled);
      }
    }

    public void UpdateFeedback(JObject json) {
      var valueScaled = json[JsonName].Value<double>();
      var value = ConvertEqTo16Bit(valueScaled);
      UpdateFeedback(value, valueScaled);
    }

    public void UpdateFeedback(short value, double valueScaled) {
      currentValue = value;
      currentValueScaled = valueScaled;

      FeedbackDelegate(value);
      TextFeedbackDelegate(valueScaled.ToString());
    }

    private double ConvertEqFrom16Bit(short value) {
      double o = value;
      o /= 10;
      o = Math.Round(o * 4) / 4;
      return o;
    }
    private short ConvertEqTo16Bit(double? nullableValue) {
      double value = nullableValue ?? 0;
      return (short)(value * 10);
    }
  }
}
