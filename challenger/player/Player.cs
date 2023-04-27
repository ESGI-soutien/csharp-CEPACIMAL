namespace CEPACIMAL.challenger;

public class Player : Challenger
{
  const string analyse = "analyse";
  const string attack = "attack";
  const string heal = "heal";
  const string escape = "escape";

  private List<string> actions = new() { analyse, attack, heal, escape };

  public Player(string name, double power, double defense, int speed) : base(name, power, defense, speed)
  {
  }

  public Player(Player player) : base(player)
  {
  }

  public override void DoAction(IChallenger enemy)
  {
    var isActionCompleted = false;

    while (!isActionCompleted)
    {
      var choice = UI.UI.GetInstance().ChooseAction();

      if (!actions.Contains(choice.ToLower()))
      {
        UI.UI.GetInstance().WrongInput();
      }

      else if (choice.Equals(analyse, StringComparison.OrdinalIgnoreCase))
      {
        UI.UI.GetInstance().AnalyseEnemy(enemy);
      }
      else if (choice.Equals(heal, StringComparison.OrdinalIgnoreCase))
      {
        if (this.Heal() > 0)
        {
          isActionCompleted = true;
        }
      }

      if (choice.Equals(attack, StringComparison.OrdinalIgnoreCase))
      {
        this.ChooseAttack(enemy);
        isActionCompleted = true;
      }

      if (choice.Equals(escape, StringComparison.OrdinalIgnoreCase))
      {
        UI.UI.GetInstance().Escape(this);
        this.IsEscaping = true;
        isActionCompleted = true;
      }
    }
  }

  private void ChooseAttack(IChallenger enemy)
  {
    var isInputValid = false;

    while (!isInputValid)
    {
      var choice = UI.UI.GetInstance().ChooseAttack(this);
      
      var attack = FindAttack(choice);
      if (attack == null )
      {
        UI.UI.GetInstance().WrongInput();
        continue;
      }

      isInputValid = true;
      Attack(enemy, attack);
    }
  }

  public Attack? FindAttack(string attackName)
  {
    return Attacks.Find(attack => attack.Name.ToUpper().Equals(attackName.Trim().ToUpper()));
  }

  protected override int Heal()
  {
    var currentHeal = base.Heal();

    if (Potions.Count == 0)
    {
      UI.UI.GetInstance().FailToHeal(this);
    }
    else if (HealthPoints == Challenger.MaxHealthPoints)
    {
      UI.UI.GetInstance().NoNeedToHeal(this);
    }
    else
    {
      UI.UI.GetInstance().Heal(this, currentHeal);
    }

    return currentHeal;
  }
}