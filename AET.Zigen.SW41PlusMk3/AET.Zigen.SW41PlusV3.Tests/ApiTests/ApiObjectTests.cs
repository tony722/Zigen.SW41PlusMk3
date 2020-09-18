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
      sw41 = new Sw41Plus {HttpClient = Test.HttpClient};
      api = sw41.AudioSettings;
    }

    [TestMethod]
    public void ObjectChanged_SendsOnlyChangedPieces() {
      api.Volume = 655;
      api.Mute = 1;
      api.Send();
      TestHttpClient.Clear();
      api.Volume = 7209;
      api.Send();
      TestHttpClient.RequestContents.Should().Be(@"{""volume"":11}");
    }
  }
}
