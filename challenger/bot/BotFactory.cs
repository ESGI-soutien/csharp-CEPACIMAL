namespace CEPACIMAL.challenger;

public static class BotFactory
{
  public static IChallenger? Create(string difficulty)
  {
    if (difficulty.Equals("easy", StringComparison.OrdinalIgnoreCase))
    {
      return new EasyBot();
    }

    if (difficulty.Equals("normal", StringComparison.OrdinalIgnoreCase))
    {
      return new NormalBot();
    }

    if (difficulty.Equals("hard", StringComparison.OrdinalIgnoreCase))
    {
      return new HardBot();
    }

    return null;
  }
}