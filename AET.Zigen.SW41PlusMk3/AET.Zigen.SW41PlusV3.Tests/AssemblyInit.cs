using System.Collections.Generic;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.SW41PlusV3.Tests {
  [TestClass]
  public static class AssemblyInit {

    [AssemblyInitialize]
    public static void Init(TestContext _) {
      ErrorMessage.ErrorMessageHandler = new TestErrorMessageHandler();
    }
  }
}
