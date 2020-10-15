using System;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.SW41PlusV3.Api {
  public class AudioSettings : Sw41PlusObject {
    private JObject json;
    public AudioSettings(Sw41Plus sw41) : this() {
      Sw41Plus = sw41;
    }

    public AudioSettings() : base("/SetAudioSettings","/GetAudioSettings") { }

    internal void Initialize() {
      AddEmptyDelegatesToSplusOutputs();
      Band115 = new EqSetting(this, "band0");
      Band330 = new EqSetting(this, "band1");
      Band990 = new EqSetting(this, "band2");
      Band3000 = new EqSetting(this, "band3");
      Band9900 = new EqSetting(this, "band4");
      Treble = new EqSetting(this, "treble");
      Bass = new EqSetting(this, "basstone");
    }

    #region AudioSelect

    private string audioSelect;
    internal string AudioSelect {
      get { return audioSelect; }
      set {
        if (audioSelect == value) return;
        Post("audiosel", value.ToLower());
        AudioSelectF = value;
      }
    }

    internal string AudioSelectF {
      set {
        audioSelect = value;
        ShowFeedback(value == "local", SetAudioSelectLocalF);
        ShowFeedback(value == "arc", SetAudioSelectArcF);
      }
    }

    public void AudioSelectLocal() { AudioSelect = "local"; }
    public void AudioSelectArc() { AudioSelect = "arc"; }
    #endregion

    #region Mute

    private ushort mute;
    public ushort Mute {
      get { return mute; }
      set {
        if (mute == value) return;
        PostString("mute", value.ToBool());
        MuteF = value;
      }
    }

    public ushort MuteF {
      set {
        mute = value;
        ShowFeedback(value, SetMuteF);
      }
    }

    public void MuteToggle() { Mute = (ushort)(Mute == 0 ? 1 : 0); }

    #endregion

    #region Volume

    private ushort volume, volumeScaled;
    public ushort Volume {
      get { return volume; }
      set {
        var valueScaled = value.ConvertFrom16Bit(100);
        if (volumeScaled == valueScaled) return;
        Post("volume", valueScaled);
        UpdateVolumeF(value, valueScaled);
      }
    }

    public void UpdateVolumeF(ushort value, ushort valueScaled) {
      volume = value;
      volumeScaled = valueScaled;
      ShowFeedback(value, SetVolumeF);
    }

    #endregion

    #region TuneMode

    private string tuneMode;
    internal string TuneMode {
      get { return tuneMode; }
      set {
        if (tuneMode == value) return;
        Post("tune mode", value);
        TuneModeF = value;
      }
    }

    internal string TuneModeF {
      set {
        tuneMode = value;
        ShowFeedback(value == "disabled", SetTuneModeDisabledF);
        ShowFeedback(value == "presets", SetTuneModePresetsF);
        ShowFeedback(value == "equalizer", SetTuneModeEqualizerF);
        ShowFeedback(value == "tonecontrol", SetTuneModeToneControlF);
      }
    }

    public void TuneModeDisabled() { TuneMode = "disabled"; }
    public void TuneModePresets() { TuneMode = "presets"; }
    public void TuneModeEqualizer() { TuneMode = "equalizer"; }
    public void TuneModeToneControl() { TuneMode = "tonecontrol"; }
    #endregion

    #region Preset

    private string preset;
    internal string Preset {
      get { return preset; }
      set {
        if (preset == value) return;
        Post("preset", value);
        PresetF = value;
      }
    }

    internal string PresetF {
      set {
        preset = value;
        ShowFeedback(value == "flat", SetPresetFlatF);
        ShowFeedback(value == "rock", SetPresetRockF);
        ShowFeedback(value == "classical", SetPresetClassicalF);
        ShowFeedback(value == "dance", SetPresetDanceF);
        ShowFeedback(value == "acoustic", SetPresetAcousticF);
      }
    }

    public void PresetFlat() { Preset = "flat"; }
    public void PresetRock() { Preset = "rock"; }
    public void PresetClassical() { Preset = "classical"; }
    public void PresetDance() { Preset = "dance"; }
    public void PresetAcoustic() { Preset = "acoustic"; }
    #endregion

    #region EQ Bands
    public EqSetting Band115 { get; private set; }
    public EqSetting Band330 { get; private set; }
    public EqSetting Band990 { get; private set; }
    public EqSetting Band3000 { get; private set; }
    public EqSetting Band9900 { get; private set; }
    public EqSetting Bass { get; private set; }
    public EqSetting Treble { get; private set; }
    #endregion

    #region Surround

    private ushort surround;
    public ushort Surround {
      get { return surround; }
      set {
        if (surround == value) return;
        PostString("surround", value.ToBool());
        SurroundF = value;
      }
    }

    public ushort SurroundF {
      set {
        surround = value;
        ShowFeedback(value, SetSurroundF);
      }
    }

    public void SurroundToggle() { Surround = (ushort)(Surround == 0 ? 1 : 0); }

    private ushort surroundLevel, surroundLevelScaled;
    public ushort SurroundLevel {
      get { return surroundLevel; }
      set {
        var valueScaled = value.ConvertFrom16Bit(7);
        if (surroundLevelScaled == valueScaled) return;
        Post("surrlevel", valueScaled);
        UpdateSurroundLevelF(value, valueScaled);
      }
    }

    public void UpdateSurroundLevelF(ushort value, ushort valueScaled) {
      surroundLevel = value;
      surroundLevelScaled = valueScaled;
      ShowFeedback(value, SetSurroundLevelF);
    }
    #endregion

    #region BassEnhancement

    private ushort bassEnhancement;
    public ushort BassEnhancement {
      get { return bassEnhancement; }
      set {
        if (bassEnhancement == value) return;
        PostString("bass", value.ToBool());
        BassEnhancementF = value;
      }
    }

    public ushort BassEnhancementF {
      set {
        bassEnhancement = value;
        ShowFeedback(value, SetBassEnhancementF);
      }
    }

    public void BassEnhancementToggle() { BassEnhancement = (ushort)(BassEnhancement == 0 ? 1 : 0); }

    private ushort bassLevel, bassLevelScaled;
    public ushort BassLevel {
      get { return bassLevel; }
      set {
        var valueScaled = value.ConvertFrom16Bit(127);
        if (bassLevelScaled == valueScaled) return;
        Post("basslevel", valueScaled);
        UpdateBassLevelF(value, valueScaled);
      }
    }

    public void UpdateBassLevelF(ushort value, ushort valueScaled) {
      bassLevel = value;
      bassLevelScaled = valueScaled;
      ShowFeedback(value, SetBassLevelF);
    }

    private ushort bassCutoff;
    internal ushort BassCutoff {
      get { return bassCutoff; }
      set {
        if (bassCutoff == value) return;
        Post("bassfreq", value);
        BassCutoffF = value;
      }
    }

    internal ushort BassCutoffF {
      set {
        bassCutoff = value;
        ShowFeedback(value == 80, SetBassCutFreq80F);
        ShowFeedback(value == 100, SetBassCutFreq100F);
        ShowFeedback(value == 125, SetBassCutFreq125F);
        ShowFeedback(value == 150, SetBassCutFreq150F);
        ShowFeedback(value == 175, SetBassCutFreq175F);
        ShowFeedback(value == 200, SetBassCutFreq200F);
        ShowFeedback(value == 225, SetBassCutFreq225F);
      }
    }

    public void BassCutFreq80() { BassCutoff = 80; }
    public void BassCutFreq100() { BassCutoff = 100; }
    public void BassCutFreq125() { BassCutoff = 125; }
    public void BassCutFreq150() { BassCutoff = 150; }
    public void BassCutFreq175() { BassCutoff = 175; }
    public void BassCutFreq200() { BassCutoff = 200; }
    public void BassCutFreq225() { BassCutoff = 225; }


    private ushort highPass;
    public ushort HighPass {
      get { return highPass; }
      set {
        if (highPass == value) return;
        PostString("highpass", value.ToBool());
        HighPassF = value;
      }
    }

    public ushort HighPassF {
      set {
        highPass = value;
        ShowFeedback(value, SetHighPassF);
      }
    }

    public void HighPassToggle() { HighPass = (ushort)(HighPass == 0 ? 1 : 0); }
    #endregion

    public void Poll() {
      var response = Sw41Plus.HttpGet(GetUrl);
      try {
        json = JObject.Parse(response);
        json = json["audioInfo"] as JObject;
        FillFromJsonObject();
      } catch (Exception ex) {
        ErrorMessage.Error("Sw41Plus.AudioSettings.Poll: Error handling Poll() response: {0}", ex.Message);
      }
    }

    private void FillFromJsonObject() {
      AudioSelectF = json["audiosel"].Value<string>();
      MuteF = BoolFromJson("mute");
      ScaledFromJson("volume", 100, UpdateVolumeF);
      TuneModeF = json["tune mode"].Value<string>();
      PresetF = json["presets"].Value<string>();
      Band115.UpdateFeedback(json);
      Band330.UpdateFeedback(json);
      Band990.UpdateFeedback(json);
      Band3000.UpdateFeedback(json);
      Band9900.UpdateFeedback(json);
      Bass.UpdateFeedback(json);
      Treble.UpdateFeedback(json);
      SurroundF = BoolFromJson("surround");
      ScaledFromJson("surrlevel", 7, UpdateSurroundLevelF);
      BassEnhancementF = BoolFromJson("bass");
      ScaledFromJson("basslevel", 127, UpdateBassLevelF);
      BassCutoffF = json["bassfreq"].Value<ushort>();
      HighPassF = BoolFromJson("highpass");
    }

    #region Conversion Routines
    private short ConvertEqTo16Bit(double? nullableValue) {
      double value = nullableValue ?? 0;
      return (short)(value * 10);
    }
    private double ConvertEqFrom16Bit(short value) {
      double o = value;
      o /= 10;
      o = Math.Round(o * 4) / 4;
      return o;
    }

    private void EqFromJson(string key, Action<short, double> updateFeedback) {
      var valueScaled = json[key].Value<double>();
      var value = ConvertEqTo16Bit(valueScaled);
      updateFeedback(value, valueScaled);
    }

    private void ScaledFromJson(string key, int scale, Action<ushort, ushort> updateFeedback) {
      var valueScaled = json[key].Value<int>();
      var value = valueScaled.ConvertTo16Bit(scale);
      updateFeedback(value, (ushort)valueScaled);
    }

    private ushort BoolFromJson(string key) {
      return (ushort)(json[key].Value<bool>() ? 1 : 0);
    }
    #endregion

    #region Json Post Builders
    internal void Post(string key, string value) {
      if (value == null) PostObject(key, "null");
      PostFormatted(@"{{""{0}"":""{1}""}}", key, value);
    }

    internal void Post(string key, ushort value) {
      PostObject(key, value);
    }

    internal void Post(string key, double value) {
      PostObject(key, value);
    }

    internal void PostString(string key, bool value) {
      PostObject(key, value.ToString().ToLower());
    }

    private void PostObject(string key, object value) {
      PostFormatted(@"{{""{0}"":{1}}}", key, value);
    }

    #endregion

    #region Feedback routines
    private void ShowFeedback(ushort value, SetUshortOutputDelegate localDelegate) {
      localDelegate(value);
    }

    private void ShowFeedback(bool value, SetUshortOutputDelegate localDelegate) {
      ShowFeedback(value.ToUshort(), localDelegate);
    }
    #endregion

    #region SPlus Feedback Delegates
    public void AddEmptyDelegatesToSplusOutputs() {
      SetAudioSelectLocalF = delegate { };
      SetAudioSelectArcF = delegate { };
      SetMuteF = delegate { };
      SetVolumeF = delegate { };
      SetTuneModeDisabledF = delegate { };
      SetTuneModePresetsF = delegate { };
      SetTuneModeEqualizerF = delegate { };
      SetTuneModeToneControlF = delegate { };
      SetPresetFlatF = delegate { };
      SetPresetRockF = delegate { };
      SetPresetClassicalF = delegate { };
      SetPresetDanceF = delegate { };
      SetPresetAcousticF = delegate { };
      SetSurroundF = delegate { };
      SetSurroundLevelF = delegate { };
      SetBassEnhancementF = delegate { };
      SetBassLevelF = delegate { };
      SetBassCutFreq80F = delegate { };
      SetBassCutFreq100F = delegate { };
      SetBassCutFreq125F = delegate { };
      SetBassCutFreq150F = delegate { };
      SetBassCutFreq175F = delegate { };
      SetBassCutFreq200F = delegate { };
      SetBassCutFreq225F = delegate { };
      SetHighPassF = delegate { };
      SetMuteF = delegate { };
      SetVolumeF = delegate { };
    }
    public SetUshortOutputDelegate SetAudioSelectLocalF { get; set; }
    public SetUshortOutputDelegate SetAudioSelectArcF { get; set; }
    public SetUshortOutputDelegate SetTuneModeDisabledF { get; set; }
    public SetUshortOutputDelegate SetTuneModePresetsF { get; set; }
    public SetUshortOutputDelegate SetTuneModeEqualizerF { get; set; }
    public SetUshortOutputDelegate SetTuneModeToneControlF { get; set; }
    public SetUshortOutputDelegate SetPresetFlatF { get; set; }
    public SetUshortOutputDelegate SetPresetRockF { get; set; }
    public SetUshortOutputDelegate SetPresetClassicalF { get; set; }
    public SetUshortOutputDelegate SetPresetDanceF { get; set; }
    public SetUshortOutputDelegate SetPresetAcousticF { get; set; }
    public SetUshortOutputDelegate SetSurroundF { get; set; }
    public SetUshortOutputDelegate SetSurroundLevelF { get; set; }
    public SetUshortOutputDelegate SetBassEnhancementF { get; set; }
    public SetUshortOutputDelegate SetBassLevelF { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq80F { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq100F { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq125F { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq150F { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq175F { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq200F { get; set; }
    public SetUshortOutputDelegate SetBassCutFreq225F { get; set; }
    public SetUshortOutputDelegate SetHighPassF { get; set; }
    public SetUshortOutputDelegate SetMuteF { get; set; }
    public SetUshortOutputDelegate SetVolumeF { get; set; }
    #endregion
  }
}
