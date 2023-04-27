namespace CEPACIMAL.challenger;

public interface IChallenger
{
  string Name { get; }
  int HealthPoints { get; set; }
  int Speed { get; }
  bool IsEscaping { get; }
  double Defense { get; }

  abstract void DoAction(IChallenger enemy);
}