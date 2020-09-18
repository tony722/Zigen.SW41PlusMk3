using AET.Unity.RestClient;

namespace AET.Zigen.SW41PlusV3 {
  public abstract class Sw41PlusApiObject : ApiObject {
    protected Sw41PlusApiObject(string setUrl, string getUrl) : base(setUrl, getUrl) { }

    protected Sw41Plus Sw41 {
      get { return RestClient as Sw41Plus; }
    }
  }
}
