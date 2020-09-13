using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace AET.Zigen.SW41PlusV3 {
  public delegate void TriggerDelegate();
  public delegate void SetUshortOutputArrayDelegate(ushort index, ushort value);
  public delegate void SetAllUshortOutputArrayDelegate(ushort value);
  public delegate void SetUshortOutputDelegate(ushort value);

  public delegate void SetShortOutputDelegate(short value);

  public delegate void SetStringOutputArrayDelegate(ushort index, SimplSharpString value);
  public delegate void SetAllStringOutputArrayDelegate(SimplSharpString value);
  public delegate void SetStringOutputDelegate(SimplSharpString value);

  public delegate void PulseBooleanOutputDelegate();
  public delegate void PulseBooleanOutputArrayDelegate(ushort index);

  public delegate void PulseSequenceOutputDelegate(SimplSharpString value);

  public delegate ushort GetUshortInputDelegate();
  public delegate ushort GetUshortOutputDelegate();
  public delegate ushort GetUshortOutputArrayDelegate(ushort index);

  public delegate void UshortInputChangedDelegate(ushort value);
}
