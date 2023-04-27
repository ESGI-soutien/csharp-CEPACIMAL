namespace CEPACIMAL.challenger;

public class HardBot : Challenger
{
  public HardBot() : base("Hard bot", 1.8, 0.8, 2)
  {
  }


  public override void DoAction(IChallenger enemy)
  {
    var canHeal = this.HealthPoints < 25 && this.Potions.Count >= 1;
    if (canHeal)
    {
      var currentHeal = this.Heal();
      UI.UI.GetInstance().Heal(this, currentHeal);

      return;
    }

    Attack(enemy, GetMostPowerfulAttack());
  }

  private Attack GetMostPowerfulAttack()
  {
    return Attacks.MaxBy(attack => attack.Damage)!;
  }
}