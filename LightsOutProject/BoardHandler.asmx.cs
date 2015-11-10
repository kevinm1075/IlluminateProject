using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections;
using LightsOut;
using Newtonsoft.Json;

namespace LightsOutProject
{
    [WebService(Namespace = "http://http://illuminateai.azurewebsites.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class BoardHandler : System.Web.Services.WebService
    {

        /// <summary>
        /// BoardHandle - Attempts to obtain the solution for a given board
        /// </summary>
        /// <param name="board">JSON stringified board</param>
        /// <returns></returns>
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object[] BoardHandle(string board)
        {
            int[,] webBoard = JsonConvert.DeserializeObject<int[,]>(board);

            Board boardObj = new Board(webBoard);
            ArrayList clickedObj = new ArrayList();
            BoardNode root = new BoardNode(boardObj, -1, -1, null, clickedObj);

            BoardNode solution;

            try
            {
                solution = Search.AStar(root);
            }
            catch(OutOfMemoryException ex)
            {
                solution = null;
            }

            ArrayList actionList = Search.buildActionList(solution);
            return actionList.ToArray();  
        }
    }
}
