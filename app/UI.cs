using CEPACIMAL.challenger;

namespace CEPACIMAL.UI;

public class UI
{
  private static UI? _instance;

  public static UI GetInstance()
  {
    return _instance ??= new UI();
  }

  public string GetPlayerName()
  {
    string name = "";
    bool isNameCorrect = false;

    while (!isNameCorrect)
    {
      Console.WriteLine("\nEnter your name: ");
      Console.Write("> ");

      name = Console.ReadLine() ?? "";
      string yesOrNoInput = YesOrNoQuestion($"\nIs the name {name} correct ? [yes, no]");

      isNameCorrect = yesOrNoInput.Equals("yes", StringComparison.OrdinalIgnoreCase);
    }

    return name;
  }

  public string ChooseDifficulty()
  {
    Console.WriteLine("\nEnter the difficulties [Easy, Normal, Hard] :");
    Console.Write("> ");

    string? difficulty = null;
    while (difficulty == null)
    {
      difficulty = Console.ReadLine();
    }
    
    return difficulty;
  }

  public bool WantToUpdateDifficulty(string currentDifficulty)
  {
    Console.WriteLine($@"The current difficulty is {currentDifficulty}.");

    return this.YesOrNoQuestion("\nDo you want to change it ? [No, Yes]")
      .Equals("yes", StringComparison.OrdinalIgnoreCase);
  }

  public bool WantToEndGame()
  {
    return YesOrNoQuestion("\nDo you want to stop playing [No, Yes] :")
      .Equals("yes", StringComparison.OrdinalIgnoreCase);
  }

  private string YesOrNoQuestion(string question)
  {
    string yesOrNoInput = "";
    bool isInputCorrect = false;

    while (!isInputCorrect)
    {
      Console.WriteLine(question);
      Console.Write("> ");
      yesOrNoInput = Console.ReadLine();

      isInputCorrect = yesOrNoInput.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                       yesOrNoInput.Equals("no", StringComparison.OrdinalIgnoreCase);
      if (!isInputCorrect)
      {
        Console.WriteLine("You didn't write correctly...\n");
      }
    }

    return yesOrNoInput;
  }

  public void BeginFight(IChallenger c1, IChallenger c2)
  {
    Console.WriteLine($"\nThe fight between {c1.Name} and {c2.Name} has started!");
  }

  public void BeginTurn(IChallenger c1, IChallenger c2)
  {
    Console.WriteLine($"{c1.Name} has {c1.HealthPoints} HP");
    Console.WriteLine($"{c2.Name} has {c2.HealthPoints} HP\n");

    Console.WriteLine($"{c1.Name} it's your turn !");
  }

  public void DisplayWinner(IChallenger winner)
  {
    Console.WriteLine($"{winner.Name} has won!");
  }

  public string? ChooseAction()
  {
    Console.WriteLine("\nAnalyse, attack, heal or escape ?");
    Console.Write("> ");

    string? action = null;
    while (action == null)
    {
      action = Console.ReadLine();
    }

    return action;
  }

  public void AnalyseEnemy(IChallenger enemy)
  {
    Console.WriteLine($"\n{enemy}");
  }

  public void Heal(IChallenger challenger, int healthRecover)
  {
    Console.WriteLine($"\n{challenger.Name} took a potion!");
    Console.WriteLine($"He has recovered {healthRecover} HP!\n");
  }

  public void FailToHeal(IChallenger challenger)
  {
    Console.WriteLine($"\n{challenger.Name} does not have potion!");
  }

  public void NoNeedToHeal(IChallenger challenger)
  {
    Console.WriteLine($"{challenger.Name} has already full life!");
  }

  public void Escape(IChallenger challenger)
  {
    Console.WriteLine($"{challenger.Name} escaped...\n");
  }

  public void WrongInput()
  {
    Console.WriteLine("You did not write correctly...\n");
  }
  
  public string ChooseAttack(challenger.Player player) {
    foreach (var attack in player.Attacks)
    {
      Console.WriteLine(attack);
    }
    Console.Write("> ");

    string? attackName = null;
    while (attackName == null)
    {
      attackName = Console.ReadLine();
    }
    
    return attackName;
  }

  public void Attack(IChallenger challenger, IChallenger enemy, challenger.Attack chosenAttack, int damage)
  {
    Console.WriteLine($"\n{challenger.Name} attacks {enemy.Name} with {chosenAttack.Name}");
    Console.WriteLine($"It deals {damage} damage!\n");
  }

  public void CriticalStrike()
  {
    Console.WriteLine("\nCritical hit!");
  }

  public void FailedStrike()
  {
    Console.WriteLine("\nYour attack failed...\n");
  }

  public void Pass(IChallenger challenger)
  {
    Console.WriteLine($"\n{challenger.Name} did nothing ¯\\_(ツ)_/¯\n");
  }
}