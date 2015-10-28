using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LightsOut
{
    class LightsOut
    {
        public static void Main()
        {
            /* The following function allows the user to play the game, or have
               the program solve the game from their current state. If they input
               'b', then Breadth first seach will solve the board, 'a', A* algorithm
               will solve the board, or just input a valid X coordinate, enter key, then Y
               coordinate to make a manual move. Inputting b or a afterwards, will solve
               the board for the user, and display the moves needed to reach the solution
               from their current state */
            //int[,] temp = new int[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            Board board = new Board(4,4);
            //board.setPosition(2, 2, 1);
            string input = null;

            while(!board.solved())
            {
                Console.Clear();
                Console.Out.WriteLine(board.toString()); // Display Board

                Console.Write("Solve with? b = Breadth, a = A*  : ");
                input = Console.ReadLine(); // Take user input

                if (String.Equals(input, "b")) // Solve board with Breadth
                {
                    //solve(board, "b");
                }
                else if (String.Equals(input, "a")) // Solve Board with A*
                {
                    //solve(board, "a");
                }
                else // Play the game
                {
                    int x = Convert.ToInt32(input);
                    input = Console.ReadLine();
                    int y = Convert.ToInt32(input);

                    board.action(x, y);
                }

            }

            Console.Clear();
            Console.Out.WriteLine(board.toString() + "\n");
            Console.Out.WriteLine("Solved!\n");
            Console.Read();
        }

        /* Calls the specified search methods, and displays the action list
        public static void solve(Board board, string search)
        {
            BoardNode root = new BoardNode(board, -1, -1, null);
            BoardNode solution;

            if (search == "b")
            {
                solution = Search.Breadth(root);
            }
            else
            {
                solution = Search.AStar(root);
            }
            
            ArrayList actionList = Search.buildActionList(solution);

            for (int i = 0; i < actionList.Count; i++)
            {
                int[] temp = (int[])actionList[i];

                for (int j = 0; j < 2; j++)
                {
                    Console.Out.Write(temp[j]);

                    if (j == 0)
                    {
                        Console.Out.Write(", ");
                    }
                }

                Console.Out.Write("\n");
            }
            
            Console.ReadLine();
        }
        */
    }
}
