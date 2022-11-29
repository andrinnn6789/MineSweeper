namespace MineSweeper.Core;

public class Program
{
    public static void Main(string[] args)
    {
        var field = new Field();
        PrintField(field);
        Console.WriteLine();
        Console.WriteLine("MineSweeper!!!!!!!!! Enter x and y to sweep or mark a mine. If you can mark all mines you win.\nPress enter to start");
        Console.WriteLine();
        var cheat = Console.ReadKey();
        if (cheat.Key == ConsoleKey.F9) PrintRevealedField(field);

        while (!field.GameOver || field.GameIsWinner)
        {
            Console.Write("Enter x:\t");
            var height = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Enter y:\t");
            var length = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine("mark: m, sweep: s");
            var input = Console.ReadLine();
            if (input == "m")
            {
                field.MarkOneFieldAsAMine(length, height);
            }
            else if (input == "s")
            {
                field.SweepOneField(length, height);
            }

            PrintField(field);
            Console.WriteLine();
        }

        if (field.GameOver) Console.WriteLine("You lose :(");
        else if (field.GameIsWinner) Console.WriteLine("You win!");

        PrintRevealedField(field);
    }

    private static void PrintRevealedField(Field field)
    {
        for (var i = 0; i < Field.LengthOfField; i++)
        {
            Console.Write("y" + (i + 1) + "\t");
            for (var j = 0; j < Field.HeightOfField; j++)
            {
                Console.Write(field.RevealedMineSweeperField[i, j]);
            }

            Console.WriteLine();
        }
    }
    private static void PrintField(Field field)
    {
        for (var i = 0; i < Field.LengthOfField; i++)
        {
            Console.Write("y" + (i + 1) + "\t");
            for (var j = 0; j < Field.HeightOfField; j++)
            {
                Console.Write(field.MineSweeperField[i, j]);
            }

            Console.WriteLine("");
        }
    }
}