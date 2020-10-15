using System;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.SW41PlusV3.Tests {
  [TestClass]
  public class Sw41PlusTests {
    private Sw41Plus sw41;
    private ushort LocalAudioF, ArcAudioF, TuneModeDisabledF, TuneModePresetsF, TuneModeEqualizerF, TuneModeToneControlF, PresetFlatF, PresetRockF, PresetClassicalF, PresetDanceF, PresetAcousticF, BassCutFreq80F, BassCutFreq100F, BassCutFreq125F, BassCutFreq150F, BassCutFreq175F, BassCutFreq200F, BassCutFreq225F;
    private ushort MuteF, VolumeF, SurroundF, SurroundLevelF, BassEnhancementF, BassLevelF, HighPassF;
    private short Band115F, Band330F, Band990F, Band3000F, Band9900F, BassF, TrebleF;

    [TestInitialize]
    public void TestInit() {
      sw41 = Test.Sw41;
      SetupDelegates();

    }

    [DataTestMethod]
    [DataRow("local",1,0)]
    [DataRow("arc", 0,1)]
    public void PollAudioSettings_AudioSelect_TriggersSPlusDelegatesCorrectly(string value, int v1, int v2) {
      var responseText = string.Format(@"{{""status"": ""success"", ""audioInfo"": {{""audiosel"": ""{0}"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""bassfreq"": 0, ""highpass"": false}}}}", value);
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      LocalAudioF.Should().Be((ushort)v1);
      ArcAudioF.Should().Be((ushort)v2);
    }

    [DataTestMethod]
    [DataRow("disabled", 1, 0, 0, 0)]
    [DataRow("presets", 0, 1, 0, 0)]
    [DataRow("equalizer", 0, 0, 1, 0)]
    [DataRow("tonecontrol", 0, 0, 0, 1)]
    public void PollAudioSettings_TuneMode_TriggersSPlusDelegatesCorrectly(string value, int v1, int v2, int v3, int v4) {
      var responseText = string.Format(@"{{""status"": ""success"", ""audioInfo"": {{""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""{0}"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""bassfreq"": 0, ""highpass"": false}}}}", value);
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      ErrorMessage.LastErrorMessage.Should().BeNullOrEmpty();
      TuneModeDisabledF.Should().Be((ushort)v1);
      TuneModePresetsF.Should().Be((ushort)v2);
      TuneModeEqualizerF.Should().Be((ushort)v3);
      TuneModeToneControlF.Should().Be((ushort)v4);
    }

    [DataTestMethod]
    [DataRow("flat", 1, 0, 0, 0 ,0)]
    [DataRow("rock", 0, 1, 0, 0, 0)]
    [DataRow("classical",0, 0, 1, 0, 0)]
    [DataRow("dance", 0, 0, 0, 1, 0)]
    [DataRow("acoustic", 0, 0, 0, 0, 1)]
    public void PollAudioSettings_Preset_TriggersSPlusDelegatesCorrectly(string value, int v1, int v2, int v3, int v4, int v5) {
      var responseText = string.Format(@"{{""status"": ""success"", ""audioInfo"": {{""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""{0}"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""bassfreq"": 0, ""highpass"": false}}}}", value);
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      PresetFlatF.Should().Be((ushort)v1);
      PresetRockF.Should().Be((ushort)v2);
      PresetClassicalF.Should().Be((ushort)v3);
      PresetDanceF.Should().Be((ushort)v4);
      PresetAcousticF.Should().Be((ushort)v5);
    }

    [DataTestMethod]
    [DataRow(1, 0, 0, 0, 0, 0, 0, @"{""audioInfo"":{""bassfreq"":80,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    [DataRow(0, 1, 0, 0, 0, 0, 0, @"{""audioInfo"":{""bassfreq"":100,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    [DataRow(0, 0, 1, 0, 0, 0, 0, @"{""audioInfo"":{""bassfreq"":125,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    [DataRow(0, 0, 0, 1, 0, 0, 0, @"{""audioInfo"":{""bassfreq"":150,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    [DataRow(0, 0, 0, 0, 1, 0, 0, @"{""audioInfo"":{""bassfreq"":175,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    [DataRow(0, 0, 0, 0, 0, 1, 0, @"{""audioInfo"":{""bassfreq"":200,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    [DataRow(0, 0, 0, 0, 0, 0, 1, @"{""audioInfo"":{""bassfreq"":225,""audiosel"": ""local"", ""mute"": false, ""volume"": 0, ""tune mode"": ""tonecontrol"", ""presets"": ""acoustic"", ""band0"": 0.0, ""band1"": 0.0, ""band2"": 0.0, ""band3"": 0.0, ""band4"": 0.0, ""basstone"": 0.0, ""treble"": 0.0, ""surround"": false, ""surrlevel"": 0, ""basslevel"": 0, ""bass"": false, ""highpass"": false}}")]
    public void PollAudioSettings_BassFreq_TriggersSPlusDelegatesCorrectly(int v1, int v2, int v3, int v4, int v5, int v6, int v7, string responseText) {
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      using (new AssertionScope()) {
        ErrorMessage.LastErrorMessage.Should().BeNullOrEmpty();
        BassCutFreq80F.Should().Be((ushort) v1);
        BassCutFreq100F.Should().Be((ushort) v2);
        BassCutFreq125F.Should().Be((ushort) v3);
        BassCutFreq150F.Should().Be((ushort) v4);
        BassCutFreq175F.Should().Be((ushort) v5);
        BassCutFreq200F.Should().Be((ushort) v6);
        BassCutFreq225F.Should().Be((ushort) v7);
      }
    }

    [TestMethod]
    public void GetAudioSettings_SetEverything_TriggersSPlusDelegatesCorrectly() {
      TestHttpClient.ResponseContents =
        @"{""status"":""success"",""audioInfo"":{ ""audiosel"":""local"",""mute"":true,""volume"":10,""tune mode"":""equalizer"",""presets"":""classical"",""band0"":-11.75,""band1"":-8,""band2"":-2,""band3"":0,""band4"":5,""basstone"":12,""treble"":-10,""surround"":true,""surrlevel"":3,""basslevel"":31,""bass"":true,""bassfreq"":175,""highpass"":true}}";
      using (new AssertionScope()) {
        sw41.AudioSettings.Poll();
        LocalAudioF.Should().Be(1);
        MuteF.Should().Be(1);
        VolumeF.Should().Be(6553);
        TuneModeEqualizerF.Should().Be(1);
        PresetClassicalF.Should().Be(1);
        Band115F.Should().Be(-117);
        Band330F.Should().Be(-80);
        Band990F.Should().Be(-20);
        Band3000F.Should().Be(0);
        Band9900F.Should().Be(50);
        BassF.Should().Be(120);
        TrebleF.Should().Be(-100);
        SurroundF.Should().Be(1);
        SurroundLevelF.Should().Be(28086);
        BassLevelF.Should().Be(15996);
        BassEnhancementF.Should().Be(1);
        BassCutFreq175F.Should().Be(1);
        HighPassF.Should().Be(1);
      }
    }

    private void SetupDelegates() {
      sw41.AudioSettings.SetAudioSelectLocalF = value => LocalAudioF = value;
      sw41.AudioSettings.SetAudioSelectArcF = value => ArcAudioF = value;
      sw41.AudioSettings.SetMuteF = value => MuteF = value;
      sw41.AudioSettings.SetVolumeF = value => VolumeF = value;
      sw41.AudioSettings.SetTuneModeDisabledF = value => TuneModeDisabledF = value;
      sw41.AudioSettings.SetTuneModePresetsF = value => TuneModePresetsF = value;
      sw41.AudioSettings.SetTuneModeEqualizerF = value => TuneModeEqualizerF = value;
      sw41.AudioSettings.SetTuneModeToneControlF = value => TuneModeToneControlF = value;
      sw41.AudioSettings.SetPresetFlatF = value => PresetFlatF = value;
      sw41.AudioSettings.SetPresetRockF = value => PresetRockF = value;
      sw41.AudioSettings.SetPresetClassicalF = value => PresetClassicalF = value;
      sw41.AudioSettings.SetPresetDanceF = value => PresetDanceF = value;
      sw41.AudioSettings.SetPresetAcousticF = value => PresetAcousticF = value;
      sw41.AudioSettings.Band115.FeedbackDelegate = value => Band115F = value;
      sw41.AudioSettings.Band330.FeedbackDelegate = value => Band330F = value;
      sw41.AudioSettings.Band990.FeedbackDelegate = value => Band990F = value;
      sw41.AudioSettings.Band3000.FeedbackDelegate = value => Band3000F = value;
      sw41.AudioSettings.Band9900.FeedbackDelegate = value => Band9900F = value;
      sw41.AudioSettings.Bass.FeedbackDelegate = value => BassF = value;
      sw41.AudioSettings.Treble.FeedbackDelegate = value => TrebleF = value;
      sw41.AudioSettings.SetBassEnhancementF = value => BassEnhancementF = value;
      sw41.AudioSettings.SetSurroundF = value => SurroundF = value;
      sw41.AudioSettings.SetSurroundLevelF = value => SurroundLevelF = value;
      sw41.AudioSettings.SetBassLevelF = value => BassLevelF = value;
      sw41.AudioSettings.SetBassCutFreq80F = value => BassCutFreq80F = value;
      sw41.AudioSettings.SetBassCutFreq100F = value => BassCutFreq100F = value;
      sw41.AudioSettings.SetBassCutFreq125F = value => BassCutFreq125F = value;
      sw41.AudioSettings.SetBassCutFreq150F = value => BassCutFreq150F = value;
      sw41.AudioSettings.SetBassCutFreq175F = value => BassCutFreq175F = value;
      sw41.AudioSettings.SetBassCutFreq200F = value => BassCutFreq200F = value;
      sw41.AudioSettings.SetBassCutFreq225F = value => BassCutFreq225F = value;
      sw41.AudioSettings.SetHighPassF = value => HighPassF = value;

    }
  }
}
