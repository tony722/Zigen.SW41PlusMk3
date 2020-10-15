using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AET.Unity.SimplSharp.HttpClient;

namespace AET.Zigen.SW41PlusV3.Tests {
  static class Test {
    public static Sw41Plus Sw41 {
      get {
        var sw41 = new Sw41Plus(new TestHttpClient()) {HostName = "http://testhost"};
        sw41.Initialize();
        return sw41;
      }
    }
  }
}
