using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.SW41PlusV3.Api;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests.CommandObjectTests {
  [TestClass]
  public class ActiveSourceTests {
    private ActiveSource api;
    private Sw41Plus sw41Plus;

    [TestInitialize]
    public void TestInit() {
      sw41Plus = new Sw41Plus {HttpClient = Test.HttpClient, HostName = "http://testhost"};
      api = sw41Plus.ActiveSource;
    }


    [TestMethod]
    public void RequiredFieldsAreValid_SourceIsInvalid_ReturnsFalse() {
      using (new AssertionScope()) {
        api.Source = 0;
        api.RequiredFieldsAreValid().Should().BeFalse();

        api.Source = 5;
        api.RequiredFieldsAreValid().Should().BeFalse();

      }
    }

    [TestMethod]
    public void RequiredFieldsAreValid_SourceIsValid_ReturnsTrue() {
      using (new AssertionScope()) {
        api.Source = 1;
        api.RequiredFieldsAreValid().Should().BeTrue();

        api.Source = 4;
        api.RequiredFieldsAreValid().Should().BeTrue();
      }
    }

    [TestMethod]
    public void Execute_SendsCorrectCommend() {
      api.Source = 1;
      api.Send();
      TestHttpClient.Url.Should().Be("http://testhost/SetActiveSource");
      TestHttpClient.RequestContents.Should().Be(@"{""source"":0}");
    }
  }
}
