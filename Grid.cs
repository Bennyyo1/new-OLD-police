using ThiefAndPolice;

public class Grid
{
    public int X { get; set; }
    public int Y { get; set; }
    

    public static void Print(int x, int y, Person[,]matrix)
    {

        for (int row = 0; row < x; row++) //loop x
        {
            for (int col = 0; col < y; col++) //loop y
            {
                if (matrix[row, col] == null)
                {
                    Console.Write(" ");
                }
                else if (matrix[row, col] is Thief)
                {
                    Console.Write("T");
                }
                else if (matrix[row, col] is Police)
                {
                    Console.Write("P");
                }
                else if (matrix[row, col] is Citizen)
                {
                    Console.Write("C");
                }

            }
            Console.WriteLine();
        }

    }



}