namespace AET.Zigen.SW41PlusV3.Api {
  public class ActiveSource : Sw41PlusApiObject {
    public ActiveSource() : base("/SetActiveSource", "/GetActiveSource") { }

    public ushort Source {
      get { return (ushort)(GetInt("source") + 1); }
      set {
        Json["source"] = value - 1;
        Sw41.SetVideoOutF(value);
      }
    }

    public override bool RequiredFieldsAreValid() {
      if (Source < 1 || Source > 4) return FalseWithErrorMessage("SW41PlusV3.Source({0}): Must be between 1 and 4", Source);
      return true;
    }

    public override void Poll() {
      FillJson(() => Sw41.SetVideoOutF(Source));
    }
  }
}
