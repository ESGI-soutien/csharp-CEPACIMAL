using System.Text;
using CEPACIMAL.equipment;

namespace CEPACIMAL.challenger;

public abstract class Challenger : IChallenger
{
  public const int MaxHealthPoints = 100;

  public string Name { get; set; }
  public int HealthPoints { get; set; }
  public double Power { get; set; }
  public double Defense { get; }
  public int Speed { get; set; }
  public bool IsEscaping { get; set; }
  public List<Potion> Potions { get; }
  public List<Attack> Attacks { get; }

  public abstract void DoAction(IChallenger enemy);

  protected Challenger(string name, double power, double defense, int speed)
  {
    Name = name;
    HealthPoints = MaxHealthPoints;
    Power = power;
    Defense = defense;
    Speed = speed;
    IsEscaping = false;
    Potions = new List<Potion> { new(25) };
    Attacks = new List<Attack>() { new("Punch", 10), new("Fireball", 20), new("Flash", 60) };
  }

  protected Challenger(Challenger challenger)
  {
    Name = challenger.Name;
    HealthPoints = MaxHealthPoints;
    Power = challenger.Power;
    Defense = challenger.Defense;
    Speed = challenger.Speed;
    IsEscaping = false;
    Potions = new List<Potion>() { new(25) };
    Attacks = new List<Attack>() { new("Punch", 10), new("Fireball", 20), new("Flash", 60) };
  }


  private int GetDamage(IChallenger enemy, Attack attack)
  {
    var defense = attack.Damage * enemy.Defense;
    
    return (int)Math.Round(attack.Damage * Power - defense);
  }

  protected void Attack(IChallenger enemy, Attack attack)
  {
    var damage = GetDamage(enemy, attack);

    if (HasFailed())
    {
      UI.UI.GetInstance().FailedStrike();
    }
    else if (IsCriticalStrike())
    {
      damage *= 2;
      UI.UI.GetInstance().CriticalStrike();
    }

    InflictDamage(enemy, damage);
    UI.UI.GetInstance().Attack(this, enemy, attack, damage);
  }

  private bool HasFailed()
  {
    return new Random().Next(0, 101) < 10;
  }

  protected virtual int Heal()
  {
    var hpToRestore = 0;

    var isHealingImpossible = Potions.Count == 0 || HealthPoints == Challenger.MaxHealthPoints;
    if (isHealingImpossible)
    {
      return hpToRestore;
    }

    var currentPotion = Potions[0];
    bool isFullLifeRestored = HealthPoints + currentPotion.HealthPointsToRestore > Challenger.MaxHealthPoints;
    if (!isFullLifeRestored)
    {
      hpToRestore = currentPotion.HealthPointsToRestore;
      HealthPoints += hpToRestore;
    }
    else
    {
      hpToRestore = Challenger.MaxHealthPoints - currentPotion.HealthPointsToRestore;
      HealthPoints += hpToRestore;
    }

    Potions.Remove(currentPotion);

    return hpToRestore;
  }

  private void InflictDamage(IChallenger enemy, int damage)
  {
    if (enemy.HealthPoints - damage < 0)
    {
      damage = enemy.HealthPoints;
      enemy.HealthPoints = 0;
    }

    enemy.HealthPoints -= damage;
  }

  private bool IsCriticalStrike()
  {
    return new Random().Next(0, 101) < 20;
  }

  public override string ToString()
  {
    var sb = new StringBuilder();
    foreach (var attack in Attacks)
    {
      sb.Append(attack.Name);
      sb.Append(", ");
    }
    sb.Length -= 2;
    
    return $"Name: {Name}\nHP: {HealthPoints}\nAttacks: {sb}";
  }
}