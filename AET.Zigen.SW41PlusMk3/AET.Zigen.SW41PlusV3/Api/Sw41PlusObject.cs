using AET.Unity.RestClient;
using AET.Unity.SimplSharp;

namespace AET.Zigen.SW41PlusV3.Api {
  public abstract class Sw41PlusObject  {
    protected Sw41PlusObject (string setUrl, string getUrl) {
      SetUrl = setUrl;
      GetUrl = getUrl;
    }

    protected string GetUrl { get; private set; }
    protected string SetUrl { get; private set; }

    public Sw41Plus Sw41Plus { get; set; }

    internal string Post(string contents) {
      return Sw41Plus.HttpPost(SetUrl, contents);
    }

    internal string PostFormatted(string contents, params object[] args) {
      var postContents = string.Format(contents, args);
      return Post(postContents);
    }
  }
}
