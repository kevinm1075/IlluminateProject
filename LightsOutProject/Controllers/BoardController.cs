using LightsOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;

namespace LightsOutProject.Controllers
{
    public class BoardController : ApiController
    {
 [HttpPost]
        public IHttpActionResult PostTable(int[,] table)
        {

            Board b = new Board(table);
            BoardNode root = new BoardNode(b, -1, -1, null);
            BoardNode solution = Search.AStar(root);

            ArrayList actionList = Search.buildActionList(solution);
            return Ok(actionList.ToArray());
        }
    }
}
