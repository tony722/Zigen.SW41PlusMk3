using System;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp.HttpClient;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests {
  [TestClass]
  public class Sw41PlusTests {
    private readonly Sw41Plus sw41 = new Sw41Plus();
    private ushort? LocalAudioF, ArcAudioF, TuneModeDisabledF, TuneModePresetsF, TuneModeEqualizerF, TuneModeToneControlF, PresetFlatF, PresetRockF, PresetClassicalF, PresetDanceF, PresetAcousticF, BassCFreq80F, BassCFreq100F, BassCFreq125F, BassCFreq150F, BassCFreq175F, BassCFreq200F, BassCFreq225F;
    private ushort? MuteF, VolumeF, SurroundF, SurroundLevelF, BassEnhancementF, BassLevelF, HighPassF;
    private short Band115F, Band330F, Band990F, Band3000F, Band9900F, BassF, TrebleF;

    [TestInitialize]
    public void TestInit() {
      sw41.HttpClient = Test.HttpClient;
      sw41.HostName = "http://testhost";
      SetupDelegates();

    }

    [DataTestMethod]
    [DataRow(@"{""audioInfo"":{""audiosel"":""local""}}",1,0)]
    [DataRow(@"{""audioInfo"":{""audiosel"":""arc""}}", 0,1)]
    public void PollAudioSettings_AudioSelect_TriggersSPlusDelegatesCorrectly(string responseText, int v1, int v2) {
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      LocalAudioF.Should().Be((ushort)v1);
      ArcAudioF.Should().Be((ushort)v2);
    }

    [DataTestMethod]
    [DataRow(@"{""audioInfo"":{""tune mode"":""disabled""}}", 1, 0, 0, 0)]
    [DataRow(@"{""audioInfo"":{""tune mode"":""presets""}}", 0, 1, 0, 0)]
    [DataRow(@"{""audioInfo"":{""tune mode"":""equalizer""}}", 0, 0, 1, 0)]
    [DataRow(@"{""audioInfo"":{""tune mode"":""tonecontrol""}}", 0, 0, 0, 1)]
    public void PollAudioSettings_TuneMode_TriggersSPlusDelegatesCorrectly(string responseText, int v1, int v2, int v3, int v4) {
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      TuneModeDisabledF.Should().Be((ushort)v1);
      TuneModePresetsF.Should().Be((ushort)v2);
      TuneModeEqualizerF.Should().Be((ushort)v3);
      TuneModeToneControlF.Should().Be((ushort)v4);
    }

    [DataTestMethod]
    [DataRow(@"{""audioInfo"":{""presets"":""flat""}}", 1, 0, 0, 0,0)]
    [DataRow(@"{""audioInfo"":{""presets"":""rock""}}", 0, 1, 0, 0, 0)]
    [DataRow(@"{""audioInfo"":{""presets"":""classical""}}", 0, 0, 1, 0, 0)]
    [DataRow(@"{""audioInfo"":{""presets"":""dance""}}", 0, 0, 0, 1, 0)]
    [DataRow(@"{""audioInfo"":{""presets"":""acoustic""}}", 0, 0, 0, 0, 1)]
    public void PollAudioSettings_Preset_TriggersSPlusDelegatesCorrectly(string responseText, int v1, int v2, int v3, int v4, int v5) {
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      PresetFlatF.Should().Be((ushort)v1);
      PresetRockF.Should().Be((ushort)v2);
      PresetClassicalF.Should().Be((ushort)v3);
      PresetDanceF.Should().Be((ushort)v4);
      PresetAcousticF.Should().Be((ushort)v5);
    }

    [DataTestMethod]
    [DataRow(@"{""audioInfo"":{""bassfreq"":80}}", 1, 0, 0, 0, 0, 0, 0)]
    [DataRow(@"{""audioInfo"":{""bassfreq"":100}}", 0, 1, 0, 0, 0, 0, 0)]
    [DataRow(@"{""audioInfo"":{""bassfreq"":125}}", 0, 0, 1, 0, 0, 0, 0)]
    [DataRow(@"{""audioInfo"":{""bassfreq"":150}}", 0, 0, 0, 1, 0, 0, 0)]
    [DataRow(@"{""audioInfo"":{""bassfreq"":175}}", 0, 0, 0, 0, 1, 0, 0)]
    [DataRow(@"{""audioInfo"":{""bassfreq"":200}}", 0, 0, 0, 0, 0, 1, 0)]
    [DataRow(@"{""audioInfo"":{""bassfreq"":225}}", 0, 0, 0, 0, 0, 0, 1)]
    public void PollAudioSettings_BassFreq_TriggersSPlusDelegatesCorrectly(string responseText, int v1, int v2, int v3, int v4, int v5, int v6, int v7) {
      TestHttpClient.ResponseContents = responseText;
      sw41.AudioSettings.Poll();
      BassCFreq80F.Should().Be((ushort) v1);
      BassCFreq100F.Should().Be((ushort)v2);
      BassCFreq125F.Should().Be((ushort)v3);
      BassCFreq150F.Should().Be((ushort)v4);
      BassCFreq175F.Should().Be((ushort)v5);
      BassCFreq200F.Should().Be((ushort)v6);
      BassCFreq225F.Should().Be((ushort)v7);
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
        BassCFreq175F.Should().Be(1);
        HighPassF.Should().Be(1);
      }
    }

    private void SetupDelegates() {
      sw41.SetLocalAudioF = value => LocalAudioF = value;
      sw41.SetArcAudioF = value => ArcAudioF = value;
      sw41.SetMuteF = value => MuteF = value;
      sw41.SetVolumeF = value => VolumeF = value;
      sw41.SetTuneModeDisabledF = value => TuneModeDisabledF = value;
      sw41.SetTuneModePresetsF = value => TuneModePresetsF = value;
      sw41.SetTuneModeEqualizerF = value => TuneModeEqualizerF = value;
      sw41.SetTuneModeToneControlF = value => TuneModeToneControlF = value;
      sw41.SetPresetFlatF = value => PresetFlatF = value;
      sw41.SetPresetRockF = value => PresetRockF = value;
      sw41.SetPresetClassicalF = value => PresetClassicalF = value;
      sw41.SetPresetDanceF = value => PresetDanceF = value;
      sw41.SetPresetAcousticF = value => PresetAcousticF = value;
      sw41.SetBand115F = value => Band115F = value;
      sw41.SetBand330F = value => Band330F = value;
      sw41.SetBand990F = value => Band990F = value;
      sw41.SetBand3000F = value => Band3000F = value;
      sw41.SetBand9900F = value => Band9900F = value;
      sw41.SetBassF = value => BassF = value;
      sw41.SetTrebleF = value => TrebleF = value;
      sw41.SetBassEnhancementF = value => BassEnhancementF = value;
      sw41.SetSurroundF = value => SurroundF = value;
      sw41.SetSurroundLevelF = value => SurroundLevelF = value;
      sw41.SetBassLevelF = value => BassLevelF = value;
      sw41.SetBassCFreq80F = value => BassCFreq80F = value;
      sw41.SetBassCFreq100F = value => BassCFreq100F = value;
      sw41.SetBassCFreq125F = value => BassCFreq125F = value;
      sw41.SetBassCFreq150F = value => BassCFreq150F = value;
      sw41.SetBassCFreq175F = value => BassCFreq175F = value;
      sw41.SetBassCFreq200F = value => BassCFreq200F = value;
      sw41.SetBassCFreq225F = value => BassCFreq225F = value;
      sw41.SetHighPassF = value => HighPassF = value;

    }
  }
}
