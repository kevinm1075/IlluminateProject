using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LightsOut
{
    class BoardNode
    {
        #region Data Members

        private Board board;
        private int x; // X position of move
        private int y; // Y position of move
        private int gScore; // g(n)
        private int fScore; // f(n)
        private BoardNode prev;
        private ArrayList children;

        #endregion

        #region Constructors

        /// <summary>
        /// BoardNode Constructor - Instantiates a BoardNode object
        ///                         which contains a board state,
        ///                         the action which caused the current state,
        ///                         the parent of the node, and the nodes children.
        /// </summary>
        /// <param name="board">Current board state</param>
        /// <param name="x">X position of move</param>
        /// <param name="y">Y position of move</param>
        /// <param name="prev">Parent of node</param>
        public BoardNode(Board board, int x, int y, BoardNode prev)
        {
            this.board = board;
            this.x = x;
            this.y = y;
            this.prev = prev;
            children = new ArrayList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// addChild - Adds a child to a node.
        /// </summary>
        /// <param name="child">Child to add</param>
        public void addChild(BoardNode child)
        {
            children.Add(child); // Added to the array list of children
        }

        /// <summary>
        /// getAction - Returns the action/move used to arrive
        ///             at the current board state.
        /// </summary>
        /// <returns>array containing x,y position of move</returns>
        public int[] getAction()
        {
            int[] action = { x, y };

            return action;
        }

        #endregion

        #region Accessors

        /// <summary>
        /// getBoard
        /// </summary>
        /// <returns>Returns the Board object of the node</returns>
        public Board getBoard()
        {
            return board;
        }

        /// <summary>
        /// getX
        /// </summary>
        /// <returns>Returns the X coordinate of action</returns>
        public int getX()
        {
            return x;
        }

        /// <summary>
        /// getY
        /// </summary>
        /// <returns>Returns the Y coordinate of action</returns>
        public int getY()
        {
            return y;
        }

        /// <summary>
        /// getGScore
        /// </summary>
        /// <returns>Returns the g(n) value of node</returns>
        public int getGScore()
        {
            return gScore;
        }

        /// <summary>
        /// getFScore
        /// </summary>
        /// <returns>Returns the f(n) value of node</returns>
        public int getFScore()
        {
            return fScore;
        }

        /// <summary>
        /// getPrev
        /// </summary>
        /// <returns>Returns the parent of node</returns>
        public BoardNode getPrev()
        {
            return prev;
        }

        /// <summary>
        /// getChildren
        /// </summary>
        /// <returns>Returns all the children of the current node</returns>
        public ArrayList getChildren()
        {
            return children;
        }

        /// <summary>
        /// setGScore
        /// </summary>
        /// <param name="score">g(n) value of node</param>
        public void setGScore(int score)
        {
            gScore = score;
        }

        /// <summary>
        /// setFScore
        /// </summary>
        /// <param name="score">f(n) value of node</param>
        public void setFScore(int score)
        {
            fScore = score;
        }
        #endregion
    }
}
