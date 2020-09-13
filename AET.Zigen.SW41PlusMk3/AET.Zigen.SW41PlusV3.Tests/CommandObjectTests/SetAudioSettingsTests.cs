using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.SW41PlusV3.ApiObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests.CommandObjectTests {
  [TestClass]
  public class SetAudioSettingsTests {

    private SplusAudioSettings api;
    private Sw41Plus sw41Plus;

    [TestInitialize]
    public void TestInit() {
      sw41Plus = new Sw41Plus {HttpClient = Test.HttpClient, HostName = "http://testhost"};
      api = sw41Plus.AudioSettings;
      ErrorMessage.Clear();
    }

    [DataTestMethod]
    [DataRow("", true)]
    [DataRow(null, true)]
    [DataRow("oops", false)]
    [DataRow("local", true)]
    [DataRow("loCaL", true)]
    [DataRow("arc", true)]
    public void RequiredFieldsAreValid_AudioSelect(string value, bool result) {
      api.AudioSelect = value;
      CheckResult(result, "SW41PlusV3.AudioSettings: AudioSelect must be 'local' or 'arc'.");
    }

    [TestMethod]
    public void AudioSelect_SendsCorrectCommend() {
      api.AudioSelect = "Local";
      api.Send();
      ErrorMessage.LastErrorMessage.Should().Be("");
      TestHttpClient.Url.Should().Be("http://testhost/SetAudioSettings");
      TestHttpClient.RequestContents.Should().Be(@"{""audiosel"":""local""}");
    }

    [TestMethod]
    public void AudioSelect_SplusDelegates_CorrectAtStartup() {
    }

    [DataTestMethod]
    [DataRow(0,"false")]
    [DataRow(1,"true")]
    [DataRow(2, "true")]
    public void Mute_SendsCorrectCommand(int value, string results) {
      api.Mute = (ushort)value;
      api.Send();
      TestHttpClient.RequestContents.Should().Be($"{{\"mute\":{results}}}");
    }

    [TestMethod]
    public void Volume_SendsCorrectCommand() {
      api.Volume = 32768;
      api.Send();
      TestHttpClient.RequestContents.Should().Be(@"{""volume"":50}");
    }

    [DataTestMethod]
    [DataRow("", true)]
    [DataRow(null, true)]
    [DataRow("oops", false)]
    [DataRow("disabled", true)]
    [DataRow("diSablEd", true)]
    [DataRow("presets", true)]
    [DataRow("equalizer", true)]
    [DataRow("tonecontrol", true)]
    public void RequiredFieldsAreValid_TuneMode(string value, bool result) {
      api.TuneMode = value;
      CheckResult(result, "SW41PlusV3.AudioSettings: TuneMode must be disabled, presets, equalizer, or tonecontrol.");
    }

    [TestMethod]
    public void TuneMode_SendsCorrectCommand() {
      api.TuneMode = "equalizer";
      api.Send();
      TestHttpClient.RequestContents.Should().Be(@"{""tune mode"":""equalizer""}");

      api.TuneMode = "";
      api.Send();
      TestHttpClient.RequestContents.Should().Be("{}");
    }

    [DataTestMethod]
    [DataRow("", true)]
    [DataRow(null, true)]
    [DataRow("oops",false)]
    [DataRow("flat", true)]
    [DataRow("fLaT", true)]
    [DataRow("rock", true)]
    [DataRow("classical", true)]
    [DataRow("dance", true)]
    [DataRow("acoustic", true)]
    public void RequiredFieldsAreValid_Presets(string value, bool result) {
      api.Preset = value;
      CheckResult(result, "SW41PlusV3.AudioSettings: Preset must be flat, rock");
    }

    [DataTestMethod]
    [DataRow(0, "false")]
    [DataRow(1, "true")]
    [DataRow(2, "true")]
    public void Surround_SendsCorrectCommand(int value, string results) {
      api.Surround = (ushort)value;
      api.Send();
      TestHttpClient.RequestContents.Should().Be($"{{\"surround\":{results}}}");
    }

    [TestMethod]
    public void EQBands_SendsCorrectCommand() {
      api.Band115 = -118;
      api.Band330 = -30;
      api.Band990 = 0;
      api.Band3000 = 30;
      api.Band9900 = 120;
      api.Send(); 
      TestHttpClient.RequestContents.Should().Be(@"{""band0"":-11.75,""band1"":-3.0,""band2"":0.0,""band3"":3.0,""band4"":12.0}");
    }

    [DataTestMethod]
    [DataRow(0, false)]
    [DataRow(20, false)]
    [DataRow(80, true)]
    [DataRow(85, false)]
    [DataRow(100, true)]
    [DataRow(125, true)]
    [DataRow(150, true)]
    [DataRow(175, true)]
    [DataRow(200, true)]
    [DataRow(225, true)]
    [DataRow(250, false)]
    public void RequiredFieldsAreValid_BassCutoff(int value, bool result) {
      api.BassCutoff = (ushort)value;
      CheckResult(result, "SW41PlusV3.AudioSettings: BassCutoff must be 80, 100, 125, 150, 175, 200, or 225");
    }

    [DataTestMethod]
    [DataRow(0, 0)]
    [DataRow(32768, 63)]
    [DataRow(65535, 127)]
    public void BassLevel_ConvertsFrom16BitTo127(int value, int result) {
      api.BassLevel = (ushort)value;
      api.Send();
      TestHttpClient.RequestContents.Should().Be($"{{\"basslevel\":{result}}}");
    }

    [TestMethod]
    public void Execute_AllValues_SendsCorrectValues() {
      api.AudioSelect = "local";
      api.Mute = 0;
      api.Volume = 32768;
      api.TuneMode = "disabled";
      api.Preset = "flat";
      api.Band115 = 0;
      api.Band330 = 0;
      api.Band990 = 0;
      api.Band3000 = 0;
      api.Band9900 = 0;
      api.Bass = 0;
      api.Treble = 0;
      api.Surround = 0;
      api.SurroundLevel = 9363;
      api.BassLevel = 16384;
      api.BassEnhancement = 0;
      api.BassCutoff = 100;
      api.HighPass = 0;
      api.Send();
      TestHttpClient.RequestContents.Should().Be(
        @"{""audiosel"":""local"",""mute"":false,""volume"":50,""tune mode"":""disabled"",""presets"":""flat"",""band0"":0.0,""band1"":0.0,""band2"":0.0,""band3"":0.0,""band4"":0.0,""basstone"":0.0,""treble"":0.0,""surround"":false,""surrlevel"":1,""bass"":false,""basslevel"":31,""bassfreq"":100,""highpass"":false}");
    }

    [TestMethod]
    public void BassEnhancement_SendsCorrectCommand() {
      api.BassEnhancement = 1;
      api.BassLevel = 65535;
      api.BassCutoff = 80;
      api.HighPass = 1;
      api.Send();
      ErrorMessage.LastErrorMessage.Should().Be("");
      TestHttpClient.RequestContents.Should().Be(@"{""bass"":true,""basslevel"":127,""bassfreq"":80,""highpass"":true}");
    }

    #region Helper Methods
    private void CheckResult(bool result, string errrorMessageStartsWith) {
      api.RequiredFieldsAreValid().Should().Be(result);
      if (!result) ErrorMessage.LastErrorMessage.Should().StartWith(errrorMessageStartsWith);
    }
    #endregion
  }
}
