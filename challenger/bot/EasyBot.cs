namespace CEPACIMAL.challenger;
using CEPACIMAL.UI;

public class EasyBot : Challenger
{
  public EasyBot() : base("Easy Bot", 1, 0.01, 0)
  {
  }

  public override void DoAction(IChallenger enemy)
  {
    UI.GetInstance().Pass(this);
  }
}