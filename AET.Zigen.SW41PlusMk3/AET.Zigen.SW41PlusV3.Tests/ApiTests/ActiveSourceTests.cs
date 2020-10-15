using AET.Unity.SimplSharp.HttpClient;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests.CommandObjectTests {
  [TestClass]
  public class ActiveSourceTests {
    private ActiveSource api;
    private Sw41Plus sw41;

    [TestInitialize]
    public void TestInit() {
      sw41 = Test.Sw41;
      api = sw41.ActiveSourceApi;
    }


    [TestMethod]
    public void InputIsValid_SourceIsInvalid_ReturnsFalse() {
      using (new AssertionScope()) {
        api.Source = 0;
        api.InputIsValid().Should().BeFalse();

        api.Source = 5;
        api.InputIsValid().Should().BeFalse();

      }
    }

    [TestMethod]
    public void InputIsValid_SourceIsValid_ReturnsTrue() {
      using (new AssertionScope()) {
        api.Source = 1;
        api.InputIsValid().Should().BeTrue();

        api.Source = 4;
        api.InputIsValid().Should().BeTrue();
      }
    }

    [TestMethod]
    public void Execute_SendsCorrectCommend() {
      api.Source = 1;
      TestHttpClient.Url.Should().Be("http://testhost/SetActiveSource");
      TestHttpClient.RequestContents.Should().Be(@"{""source"":0}");
    }
  }
}
