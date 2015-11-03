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

        /// <summary>
        /// Creates an empty board object given dimensions of board.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public Board(int rows, int cols)
        {
            board = new int[rows, cols];
            numLit = 0;
        }

        /// <summary>
        /// Creates a board object when given a 2D array of intgers, and how many lit tiles.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="numLit"></param>
        public Board(int[,] board, int numLit)
        {
            this.board = board;
            this.numLit = numLit;
        }

        /// <summary>
        /// Creates a board object when only given a 2D array
        /// </summary>
        /// <param name="board"></param>
        public Board(int[,] board)
        {
            this.board = board;
            this.numLit = findNumLit();
        }

        /// <summary>
        /// Performs an action on the board at the given coordinates. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
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

        /// <summary>
        /// Determines the amount of lit tiles on the board. Useful for when
        /// pre-made boards without light data is sent. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Flips the value of the board at given coordiates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
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

        /// <summary>
        /// Checks if the puzzle has been solved
        /// </summary>
        /// <returns></returns>
        public bool solved()
        {
            if (numRemainingLights() == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares two boards to see if they are equal.
        /// </summary>
        /// <param name="compareTo">Board to be comapred to</param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks how many tiles are still unlit
        /// </summary>
        /// <returns>Number of unlit tiles</returns>
        public int numRemainingLights()
        {
            int numTiles = board.GetLength(0) * board.GetLength(1);
            return numTiles - numLit;
        }

        /// <summary>
        /// Creates a copy of the this board.
        /// </summary>
        /// <returns>Copy Board Object</returns>
        public Board copyBoard()
        {
            int[,] newBoard = (int[,])board.Clone();
            Board copy = new Board(newBoard, numLit);

            return copy;
        }

        /// <summary>
        /// Gets the tile state at given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int getValueAt(int x, int y)
        {
            return board[x, y];
        }

        /// <summary>
        /// Creates a string representation of te board.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Set the state of a single tile on the board.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="value">True = ON, False = OFF</param>
        public void setPosition(int x, int y, bool state)
        {
            if(state)
            {
                board[x, y] = 1;
            }
            else
            {
                board[x, y] = 0;
            }
        }
    }
}
