// See https://aka.ms/new-console-template for more information

using CEPACIMAL.app;

namespace CEPACIMAL;

static class Program
{
  public static void Main(string[] args)
  {
    Menu.GetInstance().BeginGame();
  }
}