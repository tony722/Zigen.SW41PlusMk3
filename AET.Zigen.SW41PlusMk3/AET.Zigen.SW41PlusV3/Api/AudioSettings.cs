using System;
using AET.Unity.SimplSharp;

namespace AET.Zigen.SW41PlusV3.Api {
  public class AudioSettings : Sw41PlusApiObject {

    public AudioSettings() : base("/SetAudioSettings", "/GetAudioSettings") { }


    #region AudioSelect
    internal string AudioSelect {
      get { return (string)Json["audiosel"]; }
      set {
        value = Clean(value);
        Json["audiosel"] = value;
        UpdateAudioSelectFb(value);
      }
    }

    private void UpdateAudioSelectFb(string value) {
      Sw41.SetLocalAudioF((value == "local").ToUshort());
      Sw41.SetArcAudioF((value == "arc").ToUshort());
    }

    public void AudioSelectArc() { AudioSelect = "arc"; }
    public void AudioSelectLocal() { AudioSelect = "local"; }
    #endregion

    #region Mute
    public ushort Mute {
      get { return GetBool("mute").ToUshort(); }
      set {
        Json["mute"] = value.ToBool();
        Sw41.SetMuteF(value);
      }
    }

    public void MuteToggle() { Mute = (ushort)(Mute == 0 ? 1 : 0); }

    #endregion

    public ushort Volume {
      get { return ConvertTo16Bit((long?)Json["volume"], 100); }
      set {
        Json["volume"] = ConvertFrom16Bit(value, 100);
        Sw41.SetVolumeF(value);
      }
    }

    #region TuneMode
    internal string TuneMode {
      get { return (string)Json["tune mode"]; }
      set {
        value = Clean(value);
        Json["tune mode"] = value;
        UpdateTuneModeFb(value);
      }
    }

    private void UpdateTuneModeFb(string value) {
      Sw41.SetTuneModeDisabledF((value == "disabled").ToUshort());
      Sw41.SetTuneModePresetsF((value == "presets").ToUshort());
      Sw41.SetTuneModeEqualizerF((value == "equalizer").ToUshort());
      Sw41.SetTuneModeToneControlF((value == "tonecontrol").ToUshort());
    }

    public void TuneModeDisabled() { TuneMode = "disabled"; }
    public void TuneModePresets() { TuneMode = "presets"; }
    public void TuneModeEqualizer() { TuneMode = "equalizer"; }
    public void TuneModeToneControl() { TuneMode = "tonecontrol"; }
    #endregion

    #region Preset
    internal string Preset {
      get { return (string)Json["preset"]; }
      set {
        value = Clean(value);
        Json["preset"] = value;
        UpdatePresetFb(value);
      }
    }

    private void UpdatePresetFb(string value) {
      Sw41.SetPresetFlatF((value == "flat").ToUshort());
      Sw41.SetPresetRockF((value == "rock").ToUshort());
      Sw41.SetPresetClassicalF((value == "classical").ToUshort());
      Sw41.SetPresetDanceF((value == "dance").ToUshort());
      Sw41.SetPresetAcousticF((value == "acoustic").ToUshort());
    }

    public void PresetFlat() { Preset = "flat"; }
    public void PresetRock() { Preset = "rock"; }
    public void PresetClassical() { Preset = "classical"; }
    public void PresetDance() { Preset = "dance"; }
    public void PresetAcoustic() { Preset = "acoustic"; }
    #endregion
    
    #region EQ Bands

    public short Band115 {
      get { return ConvertEqTo16Bit(GetDouble("band0")); }
      set {
        Json["band0"] = ConvertEqFrom16Bit(value);
        Sw41.SetBand115F(value);
        Sw41.SetBand115Text(Json["band0"].ToString());
      }
    }


    public short Band330 {
      get { return ConvertEqTo16Bit(GetDouble("band1")); }
      set {
        Json["band1"] = ConvertEqFrom16Bit(value);
        Sw41.SetBand330F(value);
        Sw41.SetBand330Text(Json["band1"].ToString());
      }
    }



    public short Band990 {
      get { return ConvertEqTo16Bit(GetDouble("band2")); }
      set {
        Json["band2"] = ConvertEqFrom16Bit(value);
        Sw41.SetBand990F(value);
        Sw41.SetBand990Text(Json["band2"].ToString());
      }
    }


    public short Band3000 {
      get { return ConvertEqTo16Bit(GetDouble("band3")); }
      set {
        Json["band3"] = ConvertEqFrom16Bit(value);
        Sw41.SetBand3000F(value);
        Sw41.SetBand3000Text(Json["band3"].ToString());
      }
    }

    public short Band9900 {
      get { return ConvertEqTo16Bit(GetDouble("band4")); }
      set {
        Json["band4"] = ConvertEqFrom16Bit(value);
        Sw41.SetBand9900F(value);
        Sw41.SetBand9900Text(Json["band4"].ToString());
      }
    }


    #endregion

    #region Bass/Treble

    public short Bass {
      get { return ConvertEqTo16Bit(GetDouble("basstone")); }
      set {
        Json["basstone"] = ConvertEqFrom16Bit(value);
        Sw41.SetBassF(value);
        Sw41.SetBassText(Json["basstone"].ToString());
      }

    }

    public short Treble {
      get { return ConvertEqTo16Bit(GetDouble("treble")); }
      set {
        Json["treble"] = ConvertEqFrom16Bit(value);
        Sw41.SetTrebleF(value);
        Sw41.SetTrebleText(Json["treble"].ToString());
      }
    }

    #endregion 

    #region Surround

    public ushort Surround {
      get { return GetBool("surround").ToUshort(); }
      set {
        Json["surround"] = value.ToBool();
        Sw41.SetSurroundF(value);
      }
    }
    public void SurroundToggle() { Surround = (ushort)(Surround == 0 ? 1 : 0); }

    public ushort SurroundLevel {
      get { return ConvertTo16Bit(GetInt("surrlevel"), 7); }
      set {
        Json["surrlevel"] = ConvertFrom16Bit(value, 7);
        Sw41.SetSurroundLevelF(value);
      }
    }

    #endregion

    #region BassEnhancement

    public void BassEnhancementToggle() { BassEnhancement = (ushort)(BassEnhancement == 0 ? 1 : 0); }
    public ushort BassEnhancement {
      get { return GetBool("bass").ToUshort(); }
      set {
        Json["bass"] = value.ToBool();
        Sw41.SetBassEnhancementF(value);
      }
    }

    public ushort BassLevel {
      get { return ConvertTo16Bit(GetInt("basslevel"), 127); }
      set {
        Json["basslevel"] = ConvertFrom16Bit(value, 127);
        Sw41.SetBassLevelF(value);
      }
    }

    public void BassCutFreq80() { BassCutoff = 80; }
    public void BassCutFreq100() { BassCutoff = 100; }
    public void BassCutFreq125() { BassCutoff = 125; }
    public void BassCutFreq150() { BassCutoff = 150; }
    public void BassCutFreq175() { BassCutoff = 175; }
    public void BassCutFreq200() { BassCutoff = 200; }
    public void BassCutFreq225() { BassCutoff = 225; }

    internal ushort BassCutoff {
      get { return (ushort)GetInt("bassfreq"); }
      set {
        Json["bassfreq"] = value;
        UpdateBassCutFreqFb(value);
      }
    }

    private void UpdateBassCutFreqFb(ushort value) {
      Sw41.SetBassCFreq80F((value == 80).ToUshort());
      Sw41.SetBassCFreq100F((value == 100).ToUshort());
      Sw41.SetBassCFreq125F((value == 125).ToUshort());
      Sw41.SetBassCFreq150F((value == 150).ToUshort());
      Sw41.SetBassCFreq175F((value == 175).ToUshort());
      Sw41.SetBassCFreq200F((value == 200).ToUshort());
      Sw41.SetBassCFreq225F((value == 225).ToUshort());
    }

    public void HighPassToggle() { HighPass = (ushort)(HighPass == 0 ? 1 : 0); }
    public ushort HighPass {
      get { return GetBool("highpass").ToUshort(); }
      set {
        Json["highpass"] = value.ToBool();
        Sw41.SetHighPassF(value);
      }
    }
    #endregion

    public override void Poll() {
      FillJson("audioInfo", () => {
        Json["preset"] = Json["presets"];
        Json.Remove("presets");
        FillFromJsonObject();
      });
    }

    private void FillFromJsonObject() {
      UpdateAudioSelectFb(AudioSelect);
      Sw41.SetMuteF(Mute);
      Sw41.SetVolumeF(Volume);
      UpdateTuneModeFb(TuneMode);
      UpdatePresetFb(Preset);
      Sw41.SetBand115F(Band115);
      Sw41.SetBand330F(Band330);
      Sw41.SetBand990F(Band990);
      Sw41.SetBand3000F(Band3000);
      Sw41.SetBand9900F(Band9900);
      Sw41.SetBassF(Bass);
      Sw41.SetTrebleF(Treble);
      Sw41.SetSurroundF(Surround);
      Sw41.SetSurroundLevelF(SurroundLevel);
      Sw41.SetBassEnhancementF(BassEnhancement);
      Sw41.SetBassLevelF(BassLevel);
      UpdateBassCutFreqFb(BassCutoff);
      Sw41.SetHighPassF(HighPass);
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
    #endregion

    #region RequiredFieldsAreValid
    public override bool RequiredFieldsAreValid() {
      if (!AudioSelectIsValid()) return false;
      if (!TuneModeIsValid()) return false;
      if (!PresetIsValid()) return false;
      if (!ValueIsValid((ushort?)(long?)Json["bassfreq"], "BassCutoff", new ushort?[] { 80, 100, 125, 150, 175, 200, 225 })) return false;
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
    #endregion
  }
}
