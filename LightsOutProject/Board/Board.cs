using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{
    public class Board
    {
        public int[,] board { get; set; }
        public int numLit { get; set; }

        public Board(int[,] board, int numLit)
        {
            this.board = board;
            this.numLit = numLit;
        }

        public Board(int rows, int cols)
        {
            board = new int[rows, cols];
            numLit = 0;
        }

        public Board(int[,] board)
        {
            this.board = board;
            this.numLit = findNumLit();
        }

        public void action(int x, int y)
        {
            // Get positions of neighbors
            int top = x - 1;
            int bot = x + 1;
            int left = y - 1;
            int right = y + 1;

            // Flip value of initial tile
            flipValue(x, y);

            // If neighbor exists, flip tile
            if (top > -1)
            {
                flipValue(top, y);
            }

            if (bot < board.GetLength(0))
            {
                flipValue(bot, y);
            }

            if (left > -1)
            {
                flipValue(x, left);
            }

            if (right < board.GetLength(1))
            {
                flipValue(x, right);
            }
        }

        public int findNumLit()
        {
            int lit = 0;

            for (int curRow = 0; curRow < board.GetLength(0); curRow++)
            {
                for (int curCol = 0; curCol < board.GetLength(1); curCol++)
                {
                    if (this.board[curRow, curCol] == 1)
                    {
                        lit++;
                    }
                }
            }

            return lit;
        }

        public void flipValue(int x, int y)
        {
            if (board[x, y] == 1)
            {
                board[x, y] = 0;
                numLit--;
            }
            else
            {
                board[x, y] = 1;
                numLit++;
            }
        }

        public bool solved()
        {
            if (numRemainingLights() == 0)
            {
                return true;
            }

            return false;
        }

        public bool compare(Board compareTo)
        {
            int current;

            // Compare each board position, if a difference is found then boards are not the same.
            for (int curRow = 0; curRow < board.GetLength(0); curRow++)
            {
                for (int curCol = 0; curCol < board.GetLength(1); curCol++)
                {
                    current = this.board[curRow, curCol];
                    int compareMe = compareTo.board[curRow, curCol];
                    if (current != compareMe)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int numRemainingLights()
        {
            int numTiles = board.GetLength(0) * board.GetLength(1);
            return numTiles - numLit;
        }

        public Board copyBoard()
        {
            int[,] newBoard = (int[,])board.Clone();
            Board copy = new Board(newBoard, numLit);

            return copy;
        }

        public int getValueAt(int x, int y)
        {
            return board[x, y];
        }

        public string toString()
        {
            string result = "";

            for (int curRow = 0; curRow < board.GetLength(0); curRow++)
            {
                for (int curCol = 0; curCol < board.GetLength(1); curCol++)
                {
                    int current = board[curRow, curCol];

                    result += current + " ";
                }

                result += ("\n\n");
            }

            return result;
        }

        public void setPosition(int x, int y, int value)
        {
            board[x, y] = value;
        }
    }
}
