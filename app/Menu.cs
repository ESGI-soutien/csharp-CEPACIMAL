using CEPACIMAL.challenger;

namespace CEPACIMAL.app;

public class Menu
{
  private Player _Player { get; set; }
  private IChallenger _Challenger { get; set; }

  private static Menu? _instance;

  public static Menu GetInstance()
  {
    return _instance ??= new();
  }
  
  private void BeginFight() {
    new Fight(_Player,_Challenger).Play();
  }

  public void BeginGame()
  {
    ChoosePlayerName();

    var currentDifficulty = ChooseDifficulty();
    while (true)
    {
      _Player = new Player(_Player);
      _Challenger = BotFactory.Create(currentDifficulty);
      
      if (_Player != null && _Challenger != null)
      {
        BeginFight();
      }
      
      if (UI.UI.GetInstance().WantToEndGame())
      {
        break;
      }

      if (UI.UI.GetInstance().WantToUpdateDifficulty(currentDifficulty))
      {
        _Challenger = null;
        currentDifficulty = ChooseDifficulty();
      }
    }
  }
  
  private string ChooseDifficulty() {
    string difficulty = "";
    
    while (_Challenger == null) {
      difficulty = UI.UI.GetInstance().ChooseDifficulty();
      _Challenger = BotFactory.Create(difficulty);
      
      if (_Challenger == null) {
        UI.UI.GetInstance().WrongInput();
      }
    }
    
    return difficulty;
  }
  
  private void ChoosePlayerName() {
    var playerName = UI.UI.GetInstance().GetPlayerName();
    _Player = new Player(playerName, 1.5, 0.5, 1);
  }
}