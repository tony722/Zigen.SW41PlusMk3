using System.Configuration;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests.CommandObjectTests {
  [TestClass]
  public class AudioSettingsTests {

    private AudioSettings api;
    private Sw41Plus sw41;

    [TestInitialize]
    public void TestInit() {
      ErrorMessage.Clear();
      TestHttpClient.Clear();
      sw41 = Test.Sw41;
      api = sw41.AudioSettingsApi;
    }

    #region Serialization Tests
    [TestMethod]
    public void AudioSelect_SendsCorrectCommend() {
      api.AudioSelect = "Local";
      ErrorMessage.LastErrorMessage.Should().Be("");
      TestHttpClient.Url.Should().Be("http://testhost/SetAudioSettings");
      TestHttpClient.RequestContents.Should().Be(@"{""audiosel"":""local""}");
    }

    [TestMethod]
    public void AudioSelect_SplusDelegates_CorrectAtStartup() {
    }

    [TestMethod] 
    public void Mute_SendsCorrectCommand() {
      api.Mute = 1;
      TestHttpClient.RequestContents.Should().Be($"{{\"mute\":true}}");
      api.MuteToggle();
      TestHttpClient.RequestContents.Should().Be($"{{\"mute\":false}}");
    }

    [TestMethod]
    public void Volume_SendsCorrectCommand() {
      api.Volume = 32768;
      
      TestHttpClient.RequestContents.Should().Be(@"{""volume"":50}");
    }

    [TestMethod]
    public void TuneMode_SendsCorrectCommand() {
      api.TuneMode = "equalizer";
      
      TestHttpClient.RequestContents.Should().Be(@"{""tune mode"":""equalizer""}");
    }

    public void Surround_SendsCorrectCommand(int value, string results) {
      api.Surround = 1;
      TestHttpClient.RequestContents.Should().Be($"{{\"surround\":true}}");
      api.SurroundToggle();
      TestHttpClient.RequestContents.Should().Be($"{{\"surround\":false}}");
    }

    [TestMethod]
    public void EQBands_SendsCorrectCommand() {
      ValidateEQBand(api.Band115, -118, -11.75);
      ValidateEQBand(api.Band330, -30, -3);
      ValidateEQBand(api.Band990, 5, .5);
      ValidateEQBand(api.Band3000, 30, 3);
      ValidateEQBand(api.Band9900, 120, 12);
    }

    private void ValidateEQBand(EqSetting eq, short value, double scaledValue) {
      eq.Value = value;
      TestHttpClient.RequestContents.Should().Be(string.Format(@"{{""{0}"":{1}}}", eq.JsonName, scaledValue));
    }

    [DataTestMethod]
    [DataRow(32768, 63)]
    [DataRow(65535, 127)]
    public void BassLevel_ConvertsFrom16BitTo127(int value, int result) {
      api.BassLevel = (ushort)value;
      
      TestHttpClient.RequestContents.Should().Be($"{{\"basslevel\":{result}}}");
    }


    [TestMethod]
    public void BassEnhancement_SendsCorrectCommand() {
      using (new AssertionScope()) {
        api.BassEnhancement = 1;
        ErrorMessage.LastErrorMessage.Should().Be("");
        TestHttpClient.RequestContents.Should().Be(@"{""bass"":true}");

        api.BassLevel = 65535;
        ErrorMessage.LastErrorMessage.Should().Be("");
        TestHttpClient.RequestContents.Should().Be(@"{""basslevel"":127}");

        api.BassCutoff = 80;
        ErrorMessage.LastErrorMessage.Should().Be("");
        TestHttpClient.RequestContents.Should().Be(@"{""bassfreq"":80}");

        api.HighPass = 1;
        ErrorMessage.LastErrorMessage.Should().Be("");
        TestHttpClient.RequestContents.Should().Be(@"{""highpass"":true}");
      }
    }

    #endregion

    #region Deserialization Tests
    [TestMethod]
    public void Deserialize_ValidData_ReturnsCorrectlyPopulatedObject() {
      var responseString =
        @"{""status"":""success"",""audioInfo"":{""audiosel"":""local"",""mute"":true,""volume"":50,""tune mode"":""presets"",""presets"":""flat"",""band0"":5,""band1"":6,""band2"":7,""band3"":8,""band4"":9,""basstone"":10,""treble"":11,""surround"":true,""surrlevel"":1,""basslevel"":31,""bass"":true,""bassfreq"":100,""highpass"":true}}";
      TestHttpClient.ResponseContents = responseString;
      sw41.AudioSettingsApi.Poll();
      using (new AssertionScope()) {
        ErrorMessage.LastErrorMessage.Should().Be("");
        api.AudioSelect.Should().Be("local");
        api.Mute.Should().Be(1);
        api.Volume.Should().Be(32767);
        api.TuneMode.Should().Be("presets");
        api.Band115.Value.Should().Be(50);
        api.Band330.Value.Should().Be(60);
        api.Band990.Value.Should().Be(70);
        api.Band3000.Value.Should().Be(80);
        api.Band9900.Value.Should().Be(90);
        api.Bass.Value.Should().Be(100);
        api.Treble.Value.Should().Be(110);
        api.Surround.Should().Be(1);
        api.SurroundLevel.Should().Be(9362);
        api.BassEnhancement.Should().Be(1);
        api.BassCutoff.Should().Be(100);
        api.HighPass.Should().Be(1);
      }
    }

    #endregion
  }
}
