namespace ChangeTasks
{
    class Program
    {
        static void Main()
        {
            string text = GetFileContents();
            var coins = GetCoins(text);

            Console.WriteLine("coins/banknotes - " + string.Join(", ", coins) + "\n");

            Dictionary<int, int> change = new Dictionary<int, int>();

            int invalidInputCount = 0;

            while (coins != null) 
            {
                Console.Write("Write balance here: ");
                var input = Console.ReadLine();
                Console.Write("\n");

                int balance = 0;

                //Check if it is a valid balance
                bool isNumber = int.TryParse(input, out balance);
                if (!isNumber)
                {
                    invalidInputCount++;

                    Console.WriteLine("Not a valid number input.");
                    Console.WriteLine("If empty/invalid input is entered twice the program will close.\n");

                    // If enter is pressed twice in a row without a valid input it will exit the program
                    if (invalidInputCount >= 2)
                    {
                        Environment.Exit(1);
                    }

                    continue;
                } 
                else
                {
                    invalidInputCount = 0;
                }

                //Calculate the balance
                for (int i = 0; i < coins.Count; i++)
                {
                    int coinCount = balance / coins[i];
                    balance %= coins[i];
                    if (coinCount > 0)
                    {
                        change.Add(coins[i], coinCount);
                    }
                }

                PrintResult(change);
                change.Clear();
            }
        }

        static string GetFileContents()
        {
            string text = "";

            try
            {
                //Adjust path for file that needs to be read.
                using (var sr = new StreamReader("C:\\Projects\\ChangeTask\\Coins.txt"))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return text;
        }

        static List<int> GetCoins(string text) 
        {
            List<int> coins = text
                .Split(';')
                .Where(x => int.TryParse(x, out _))
                .Select(int.Parse)
                .OrderBy(x => -x)
                .ToList();

            return coins;
        }

        static void PrintResult(Dictionary<int, int> change)
        {
            foreach (var item in change)
            {
                Console.WriteLine(item.Value.ToString() + " x " + item.Key.ToString() + " coins/currency");
            }

            Console.WriteLine();
        }
    }
}
