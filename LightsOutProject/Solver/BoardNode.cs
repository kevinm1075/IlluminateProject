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

        public Board board { get; set; }
        public int x { get; set; } // X position of action
        public int y { get; set; } // Y position of action
        public int gScore { get; set; } // g(n)
        public int fScore { get; set; } // f(n)
        public BoardNode prev { get; set; }
        public ArrayList clicked { get; set; } // Tracks tiles which have been clicked already
        public ArrayList children { get; set; }

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
        public BoardNode(Board board, int x, int y, BoardNode prev, ArrayList clicked)
        {
            this.board = board;
            this.x = x;
            this.y = y;
            this.prev = prev;
            this.clicked = new ArrayList(clicked);
            this.clicked.Add(getAction());
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
    }
}
