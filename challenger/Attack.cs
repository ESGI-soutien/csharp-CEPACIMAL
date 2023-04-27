namespace CEPACIMAL.challenger;

public sealed class Attack
{
  public string Name { get; }
  public int Damage { get; }

  public Attack(string name, int damage)
  {
    Name = name;
    Damage = damage;
  }

  public override string ToString()
  {
    return $"{Name}: {Damage}";
  }
}