using System;
using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.SW41PlusV3.ApiObjects;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests.ResponseObjectTests {
  [TestClass]
  public class GetAudioSettingsTests {
    [TestMethod]
    public void Deserialize_Null_ReturnsEmptyObject() {
      var api = AudioSettings.Deserialize(null);
      api.Should().Be(null);
    }

    [TestMethod]
    public void Deserialize_ValidData_ReturnsCorrectlyPopulatedObject() {
      var responseString =
        @"{""status"":""success"",""audioInfo"":{""audiosel"":""local"",""mute"":true,""volume"":50,""tune mode"":""presets"",""presets"":""flat"",""band0"":5,""band1"":6,""band2"":7,""band3"":8,""band4"":9,""basstone"":10,""treble"":11,""surround"":true,""surrlevel"":1,""basslevel"":31,""bass"":true,""bassfreq"":100,""highpass"":true}}";
      var sw41 = new Sw41Plus {HttpClient = Test.HttpClient};
      TestHttpClient.ResponseContents = responseString;
      sw41.AudioSettings.Poll();
      var api = sw41.AudioSettings;
      using (new AssertionScope()) {
        api.AudioSelect.Should().Be("local");
        api.Mute.Should().Be(1);
        api.Volume.Should().Be(32767);
        api.TuneMode.Should().Be("presets");
        api.Band115.Should().Be(50);
        api.Band330.Should().Be(60);
        api.Band990.Should().Be(70);
        api.Band3000.Should().Be(80);
        api.Band9900.Should().Be(90);
        api.Bass.Should().Be(100);
        api.Treble.Should().Be(110);
        api.Surround.Should().Be(1);
        api.SurroundLevel.Should().Be(9362);
        api.BassEnhancement.Should().Be(1);
        api.BassCutoff.Should().Be(100);
        api.HighPass.Should().Be(1);
      }
    }
  }
}
