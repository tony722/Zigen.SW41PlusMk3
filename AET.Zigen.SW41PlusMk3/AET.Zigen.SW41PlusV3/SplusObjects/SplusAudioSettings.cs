using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using AET.Zigen.SW41PlusV3.ApiObjects;
using AET.Zigen.SW41PlusV3.SplusObjects;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.SW41PlusV3 {
  public class SplusAudioSettings : SplusObject<AudioSettings> {

    private string audioSelect;
    private ushort mute, volume;
    private string tuneMode, preset;
    private short band115, band330, band990, band3000, band9900;
    private short bass, treble;
    private ushort surround, surroundLevel;
    private ushort bassEnhancement, bassLevel, bassCutoff, highPass;

    public SplusAudioSettings() : base(new RestClient()) {
      //This constructor is to allow Simpl+ visibility and should not be used
    }

    public SplusAudioSettings(RestClient restClient) : base (restClient) {
      AddEmptyDelegatesToSplusOutputs();
    }

    public void AudioSelectArc() { AudioSelect = "arc"; }
    public void AudioSelectLocal() { AudioSelect = "local"; }

    internal string AudioSelect {
      get { return audioSelect; }
      set {
        value = Clean(value);
        audioSelect = value;
        SetLocalAudioF((value == "local").ToUshort());
        SetArcAudioF((value == "arc").ToUshort());
        ObjectToSend.AudioSelect = value;
      }
    } 



    public void MuteToggle() { Mute = (ushort)(Mute == 0 ? 1 : 0);  }
    public ushort Mute {
      get { return mute; }
      set {
        mute = value;
        SetMuteF(value);
        ObjectToSend.Mute = value.ToBool();
      }
    }
    
    public ushort Volume {
      get { return volume; }
      set {
        volume = value;
        SetVolumeF(value);
        ObjectToSend.Volume = ConvertFrom16Bit(value, 100);
      }
    }

    public void TuneModeDisabled() { TuneMode = "disabled"; }
    public void TuneModePresets() { TuneMode = "presets"; }
    public void TuneModeEqualizer() { TuneMode = "equalizer"; }
    public void TuneModeToneControl() { TuneMode = "tonecontrol"; }

    internal string TuneMode {
      get { return tuneMode; }
      set {
        value = Clean(value);
        tuneMode = value;
        SetTuneModeDisabledF((value == "disabled").ToUshort());
        SetTuneModePresetsF((value == "presets").ToUshort());
        SetTuneModeEqualizerF((value == "equalizer").ToUshort());
        SetTuneModeToneControlF((value == "tonecontrol").ToUshort());
        ObjectToSend.TuneMode = value;
      }
    }

    public void PresetFlat() { Preset = "flat"; }
    public void PresetRock() { Preset = "rock"; }
    public void PresetClassical() { Preset = "classical"; }
    public void PresetDance() { Preset = "dance"; }
    public void PresetAcoustic() { Preset = "acoustic"; }

    internal string Preset {
      get { return preset; }
      set {
        value = Clean(value);
        preset = value;
        SetPresetFlatF((value == "flat").ToUshort());
        SetPresetRockF((value == "rock").ToUshort());
        SetPresetClassicalF((value == "classical").ToUshort());
        SetPresetDanceF((value == "dance").ToUshort());
        SetPresetAcousticF((value == "acoustic").ToUshort());
        ObjectToSend.Preset = value;
      }
    }

    #region EQ Bands

    public short Band115 {
      get { return band115; }
      set {
        band115 = value;
        SetBand115F(value);
        ObjectToSend.Band115 = ConvertEqFrom16Bit(value);
      }
    }

    public short Band330 {
      get { return band330; }
      set {
        band330 = value;
        SetBand330F(value);
        ObjectToSend.Band330 = ConvertEqFrom16Bit(value);
      }
    }

    public short Band990 {
      get { return band990; }
      set {
        band990 = value;
        SetBand990F(value);
        ObjectToSend.Band990 = ConvertEqFrom16Bit(value);
      }
    }

    public short Band3000 {
      get { return band3000; }
      set {
        band3000 = value;
        SetBand3000F(value);
        ObjectToSend.Band3000 = ConvertEqFrom16Bit(value);
      }
    }

    public short Band9900 {
      get { return band9900; }
      set {
        band9900 = value;
        SetBand9900F(value);
        ObjectToSend.Band9900 = ConvertEqFrom16Bit(value);
      }
    }

    #endregion

    #region Bass/Treble

    public short Bass {
      get { return bass; }
      set {
        bass = value;
        SetBassF(value);
        ObjectToSend.Bass = ConvertEqFrom16Bit(value);
      }
    }

    public short Treble {
      get { return treble; }
      set {
        treble = value;
        SetTrebleF(value);
        ObjectToSend.Treble = ConvertEqFrom16Bit(value);
      }
    }

    #endregion 

    #region Surround

    public void SurroundToggle() { Surround = (ushort)(Surround == 0 ? 1 : 0); }
    public ushort Surround {
      get { return surround; }
      set {
        surround = value;
        SetSurroundF(value);
        ObjectToSend.Surround = value.ToBool();
      }
    }

    public ushort SurroundLevel {
      get { return surroundLevel; }
      set {
        surroundLevel = value;
        SetSurroundLevelF(value);
        ObjectToSend.SurroundLevel = ConvertFrom16Bit(value, 7);
      }
    }

    #endregion

    #region BassEnhancement

    public void BassEnhancementToggle() { BassEnhancement = (ushort)(BassEnhancement == 0 ? 1 : 0); }
    public ushort BassEnhancement {
      get { return bassEnhancement; }
      set {
        bassEnhancement = value;
        SetBassEnhancementF(value);
        ObjectToSend.BassEnhancement = value.ToBool();
      }
    }

    public ushort BassLevel {
      get { return bassLevel; }
      set {
        bassLevel = value;
        SetBassLevelF(value);
        ObjectToSend.BassLevel = ConvertFrom16Bit(value, 127);
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
      get { return bassCutoff; }
      set {
        bassCutoff = value;
        SetBassCFreq80F((value == 80).ToUshort());
        SetBassCFreq100F((value == 100).ToUshort());
        SetBassCFreq125F((value == 125).ToUshort());
        SetBassCFreq150F((value == 150).ToUshort());
        SetBassCFreq175F((value == 175).ToUshort());
        SetBassCFreq200F((value == 200).ToUshort());
        SetBassCFreq225F((value == 225).ToUshort());
        ObjectToSend.BassCutoff = value;
      }
    }

    public void HighPassToggle() { HighPass = (ushort)(HighPass == 0 ? 1 : 0); }
    public ushort HighPass {
      get { return highPass; }
      set {
        highPass = value;
        SetHighPassF(value);
        ObjectToSend.HighPass = value.ToBool();
      }
    }

    private ushort ConvertTo16Bit(int value, int scale) {
      return (ushort)(value * 65535 / scale);
    }

    private ushort ConvertTo16Bit(ushort? value, int scale) {
      return ConvertTo16Bit((int)(value ?? 0), scale);
    }


    #endregion

    public override void Poll() {
      var audioSettings = HttpGet("audioInfo");
      FillFromJsonObject(audioSettings);
      ResetObjectToSend();
    }

    protected void FillFromJsonObject(AudioSettings audioSettings) {
      AudioSelect = audioSettings.AudioSelect;
      Mute = audioSettings.Mute.ToUshort();
      Volume = ConvertTo16Bit(audioSettings.Volume, 100);
      TuneMode = audioSettings.TuneMode;
      Preset = audioSettings.Preset;
      Band115 = ConvertEqTo16Bit(audioSettings.Band115);
      Band330 = ConvertEqTo16Bit(audioSettings.Band330);
      Band990 = ConvertEqTo16Bit(audioSettings.Band990);
      Band3000 = ConvertEqTo16Bit(audioSettings.Band3000);
      Band9900 = ConvertEqTo16Bit(audioSettings.Band9900);
      Bass = ConvertEqTo16Bit(audioSettings.Bass);
      Treble = ConvertEqTo16Bit(audioSettings.Treble);
      Surround = audioSettings.Surround.ToUshort();
      SurroundLevel = ConvertTo16Bit(audioSettings.SurroundLevel, 7);
      BassEnhancement = audioSettings.BassEnhancement.ToUshort();
      BassLevel = ConvertTo16Bit(audioSettings.BassLevel, 127);
      BassCutoff = audioSettings.BassCutoff ?? 0;
      HighPass = audioSettings.HighPass.ToUshort();
    }

    #region Conversion Routines
    private short ConvertEqTo16Bit(double? nullableValue) {
      double value = nullableValue ?? 0;
      return (short) (value * 10);
    }
    private double ConvertEqFrom16Bit(short value) {
      double o = value;
      o /= 10;
      o = Math.Round(o * 4) / 4;
      return o;
    }

    private ushort ConvertFrom16Bit(ushort value, int scale) {
      return (ushort)((value * scale) / 65535);
    }

    #endregion 

    #region Splus Feedback Delegates
    public void AddEmptyDelegatesToSplusOutputs() {
      SetLocalAudioF = delegate { };
      SetArcAudioF = delegate { };
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
      SetBand115F = delegate { };
      SetBand330F = delegate { };
      SetBand990F = delegate { };
      SetBand3000F = delegate { };
      SetBand9900F = delegate { };
      SetBassF = delegate { };
      SetTrebleF = delegate { };
      SetSurroundF = delegate { };
      SetSurroundLevelF = delegate { };
      SetBassEnhancementF = delegate { };
      SetBassLevelF = delegate { };
      SetBassCFreq80F = delegate { };
      SetBassCFreq100F = delegate { };
      SetBassCFreq125F = delegate { };
      SetBassCFreq150F = delegate { };
      SetBassCFreq175F = delegate { };
      SetBassCFreq200F = delegate { };
      SetBassCFreq225F = delegate { };
      SetHighPassF = delegate { };
    }
    public SetUshortOutputDelegate SetLocalAudioF { get; set; }
    public SetUshortOutputDelegate SetArcAudioF { get; set; }
    public SetUshortOutputDelegate SetMuteF { get; set; }
    public SetUshortOutputDelegate SetVolumeF { get; set; }
    public SetUshortOutputDelegate SetTuneModeDisabledF { get; set; }
    public SetUshortOutputDelegate SetTuneModePresetsF { get; set; }
    public SetUshortOutputDelegate SetTuneModeEqualizerF { get; set; }
    public SetUshortOutputDelegate SetTuneModeToneControlF { get; set; }
    public SetUshortOutputDelegate SetPresetFlatF { get; set; }
    public SetUshortOutputDelegate SetPresetRockF { get; set; }
    public SetUshortOutputDelegate SetPresetClassicalF { get; set; }
    public SetUshortOutputDelegate SetPresetDanceF { get; set; }
    public SetUshortOutputDelegate SetPresetAcousticF { get; set; }
    public SetShortOutputDelegate SetBand115F { get; set; }
    public SetShortOutputDelegate SetBand330F { get; set; }
    public SetShortOutputDelegate SetBand990F { get; set; }
    public SetShortOutputDelegate SetBand3000F { get; set; }
    public SetShortOutputDelegate SetBand9900F { get; set; }
    public SetShortOutputDelegate SetBassF { get; set; }
    public SetShortOutputDelegate SetTrebleF { get; set; }
    public SetUshortOutputDelegate SetSurroundF { get; set; }
    public SetUshortOutputDelegate SetSurroundLevelF { get; set; }
    public SetUshortOutputDelegate SetBassEnhancementF { get; set; }
    public SetUshortOutputDelegate SetBassLevelF { get; set; }
    public SetUshortOutputDelegate SetBassCFreq80F { get; set; }
    public SetUshortOutputDelegate SetBassCFreq100F { get; set; }
    public SetUshortOutputDelegate SetBassCFreq125F { get; set; }
    public SetUshortOutputDelegate SetBassCFreq150F { get; set; }
    public SetUshortOutputDelegate SetBassCFreq175F { get; set; }
    public SetUshortOutputDelegate SetBassCFreq200F { get; set; }
    public SetUshortOutputDelegate SetBassCFreq225F { get; set; }
    public SetUshortOutputDelegate SetHighPassF { get; set; }

    #endregion
  }
}
