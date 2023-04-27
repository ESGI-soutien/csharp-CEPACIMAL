using CEPACIMAL.challenger;

namespace CEPACIMAL.app;

public class Fight
{
  public IChallenger _Challenger1 { get; set; }
  public IChallenger _Challenger2 { get; set; }

  public Fight(IChallenger c1, IChallenger c2)
  {
    _Challenger1 = c1;
    _Challenger2 = c2;
  }

  public void Play()
  {
    UI.UI.GetInstance().BeginFight(_Challenger1, _Challenger2);
    UpdateChallengerOrder();

    while (CanContinue())
    {
      UI.UI.GetInstance().BeginTurn(_Challenger1, _Challenger2);
      _Challenger1.DoAction(_Challenger2);

      if (CanContinue())
      {
        UI.UI.GetInstance().BeginTurn(_Challenger2,_Challenger1);
        _Challenger2.DoAction(_Challenger1);
      }
    }
    
    UI.UI.GetInstance().DisplayWinner(GetWinner());
  }

  public void UpdateChallengerOrder()
  {
    if (_Challenger2.Speed > _Challenger1.Speed)
    {
      (_Challenger1, _Challenger2) = (_Challenger2, _Challenger1);
    }
  }

  private IChallenger GetWinner()
  {
    if (_Challenger1.HealthPoints == 0 || _Challenger1.IsEscaping)
    {
      return _Challenger2;
    }

    return _Challenger1;
  }

  private bool CanContinue()
  {
    return
      !_Challenger1.IsEscaping
      && !_Challenger2.IsEscaping
      && _Challenger1.HealthPoints > 0
      && _Challenger2.HealthPoints > 0;
  }
}