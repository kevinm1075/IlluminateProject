﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Wintellect.PowerCollections;

namespace LightsOut
{
    class Search
    {
        /// <summary>
        /// AStar - Performs the A* algorithm search to find a
        ///         solution of a given BoardNode.
        /// </summary>
        /// <param name="root">BoardNode to find a solution for</param>
        /// <returns>BoardNode containing the solution. NULL if no solution found</returns>
        public static BoardNode AStar(BoardNode root)
        {
            BoardNode current = root;
            Comparison<BoardNode> compare = new Comparison<BoardNode>(compareFScore);
            OrderedBag<BoardNode> openList = new OrderedBag<BoardNode>(compare);
            OrderedBag<BoardNode> closedList = new OrderedBag<BoardNode>(compare);
            int curGScore;
            int heuristic;

            curGScore = 0;
            heuristic = current.board.numRemainingLights();
            current.gScore = curGScore;
            current.fScore = (curGScore + heuristic);

            openList.Add(current);

            while(openList.Count != 0)
            {
                current = openList.GetFirst();

                if(current.clicked.Capacity >= 100000)
                {
                    return null;
                }

                if(current.board.solved())
                {
                    return current;
                }

                openList.Remove(current);
                closedList.Add(current);

                addChildren(current);

                foreach (BoardNode child in current.children)
                {
                    if (closedList.Contains(child))
                    {
                        ;
                    }
                    else
                    {
                        curGScore = current.gScore + 1;

                        if (openList.Contains(child) == false || curGScore < child.gScore)
                        {
                            child.gScore = (curGScore);
                            heuristic = child.board.numRemainingLights();
                            child.fScore = (curGScore + heuristic);

                            if (openList.Contains(child) == false)
                            {
                                openList.Add(child);
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// buildActionList - Creates the list of actions to arrive at a
        ///                   solution of a board. Returns up to parent node,
        ///                   storing actions along the path.
        /// </summary>
        /// <param name="answer">BoardNode containing solution</param>
        /// <returns>Returns - List of actions to solve board.</returns>
        public static ArrayList buildActionList(BoardNode answer)
        {
            BoardNode current = answer;
            int[] curAction;
            ArrayList actionList = new ArrayList();
            
            if (answer != null)
            {
                // Follows path up to the root, storing actions as it goes.
                while (current.prev != null)
                {
                    curAction = current.getAction();
                    actionList.Add(curAction);
                    current = current.prev;
                }
            }
            else
            { 
                actionList.Add(-1); //Unsolvable
            }

            return actionList;
        }

        /// <summary>
        /// checkVisited - Checks a visited list to see if a board state has already
        ///                been added. 
        /// </summary>
        /// <param name="curBoard">Board to check</param>
        /// <param name="visited">Visted list to check</param>
        /// <returns>True- Board is in visited. False- Board is not in visited.</returns>
        private static bool checkVisited(Board curBoard, ArrayList visited)
        {
            Board compareBoard;

            // Compares each board in the visited list to
            // the board sent as argument.
            for(int i = 0; i < visited.Count; i++)
            {
                compareBoard = (Board)visited[i];

                if(compareBoard.compare(curBoard))
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// compareFScore - Compares the F(n) value of
        ///                 two different nodes
        /// </summary>
        /// <param name="boardA">Board to be compared</param>
        /// <param name="boardB">Board to be compared</param>
        /// <returns>If value is less than 0, board A is less than board B
        ///          If value is greater than 0, board A is greater than board b
        ///          If value is equal to 0, then board a equals board b</returns>
        public static int compareFScore(BoardNode boardA, BoardNode boardB)
        {
            int fScoreA = boardA.fScore;
            int fScoreB = boardB.fScore;
            int difference = fScoreA - fScoreB;

            return difference;
        }

        /// <summary>
        /// addChildren - adds all possible children to the given
        ///               BoardNode.
        /// </summary>
        /// <param name="current">BoardNode to create children for</param>
        private static void addChildren(BoardNode current)
        {
            Board currentBoard = current.board;
            Board child = currentBoard.copyBoard();
            int currentCost = current.gScore;
            int numRows = child.board.GetLength(0);
            int numCols = child.board.GetLength(1);
            int[] action;

            for (int curRow = 0; curRow < numRows; curRow++)
            {
                for (int curCol = 0; curCol < numCols; curCol++)
                {
                    action = new int[2] { curRow, curCol };

                    if(!current.clicked.Contains(action))  // Checks if we have made this move previosly on this branch
                    {
                        child = currentBoard.copyBoard();
                        child.action(curRow, curCol);
                        current.addChild(new BoardNode(child, curRow, curCol, current, current.clicked));
                    }

                }
            }
        }
    }
}
