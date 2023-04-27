namespace CEPACIMAL.challenger;

public class NormalBot : Challenger
{
  public NormalBot() : base("Normal bot", 1.5, 0.5, 1)
  {
  }

  public override void DoAction(IChallenger enemy)
  {
    var randomIndex = new Random().Next(0,this.Attacks.Count);
    var randomAttack = this.Attacks[randomIndex];
    
    this.Attack(enemy, randomAttack);
  }
}