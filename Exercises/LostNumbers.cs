public class LostNumbers
{
    public int[] avaliableNumbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public List<int> missingNumbers = new();

    public LostNumbers()
    {
        // Read the file and store the numbers in an array
        string text = File.ReadAllText("data/numeros.txt");
        foreach (char c in text)
        {
            int number = int.Parse(c.ToString());
            avaliableNumbers[number]++;
        }

        Console.WriteLine("Available numbers: ");
        for (int i = 0; i < avaliableNumbers.Length; i++)
        {
            Console.WriteLine(i + ": " + avaliableNumbers[i]);
        }

        //Try compose numbers
        int startNumber = 5000;

        for (int i = startNumber; i > 0; i--)
        {
            var t = i.ToString();
            Console.WriteLine("Trying to compose: " + t);
            Console.WriteLine("Available numbers: ");
            for (int j = 0; j < avaliableNumbers.Length; j++)
            {
                Console.WriteLine(j + ": " + avaliableNumbers[j]);
            }
            //Store used numbers
            Boolean canCompose = true;
            int[] requiredNumbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (char c in t)
            {
                var number = int.Parse(c.ToString());
                requiredNumbers[number]++;
            }

            if (
                avaliableNumbers[0] < requiredNumbers[0] ||
                avaliableNumbers[1] < requiredNumbers[1] ||
                avaliableNumbers[2] < requiredNumbers[2] ||
                avaliableNumbers[3] < requiredNumbers[3] ||
                avaliableNumbers[4] < requiredNumbers[4] ||
                avaliableNumbers[5] < requiredNumbers[5] ||
                avaliableNumbers[6] < requiredNumbers[6] ||
                avaliableNumbers[7] < requiredNumbers[7] ||
                avaliableNumbers[8] < requiredNumbers[8] ||
                avaliableNumbers[9] < requiredNumbers[9]
            )
            {
                canCompose = false;
            }

            if (!canCompose)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Not possible to compose: " + t);
                Console.ResetColor();
                Console.Write(" ");
                missingNumbers.Add(int.Parse(t));

            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Possible to compose: " + t);
                Console.ResetColor();
                Console.Write(" ");

                //Remove used numbers
                avaliableNumbers[0] -= requiredNumbers[0];
                avaliableNumbers[1] -= requiredNumbers[1];
                avaliableNumbers[2] -= requiredNumbers[2];
                avaliableNumbers[3] -= requiredNumbers[3];
                avaliableNumbers[4] -= requiredNumbers[4];
                avaliableNumbers[5] -= requiredNumbers[5];
                avaliableNumbers[6] -= requiredNumbers[6];
                avaliableNumbers[7] -= requiredNumbers[7];
                avaliableNumbers[8] -= requiredNumbers[8];
                avaliableNumbers[9] -= requiredNumbers[9];
            }
        }

        missingNumbers.Sort();
        string Result = "";

        foreach (int number in missingNumbers)
        {
            Result += number.ToString() + ", ";
        }

        Console.WriteLine("Missing numbers: " + Result);
    }

}