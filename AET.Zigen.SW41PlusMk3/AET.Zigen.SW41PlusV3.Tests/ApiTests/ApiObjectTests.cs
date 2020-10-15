using System;
using System.Diagnostics;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp.HttpClient;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests {
  [TestClass]
  public class ApiObjectTests {
    private Sw41Plus sw41;
    private AudioSettings api;

    [TestInitialize]
    public void TestInit() {
      sw41 = Test.Sw41;
      api = sw41.AudioSettingsApi;
    }

    [TestMethod]
    public void ObjectChanged_SendsOnlyChangedPieces() {
      api.Volume = 655;
      api.Mute = 1;
      TestHttpClient.Clear();
      api.Volume = 7209;
      TestHttpClient.RequestContents.Should().Be(@"{""volume"":11}");
    }
  }
}
