namespace CEPACIMAL.equipment;

public class Potion
{
  public int HealthPointsToRestore { get; }

  public Potion(int healthPointsToRestore)
  {
    this.HealthPointsToRestore = healthPointsToRestore;
  }
}