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
    [WebService(Namespace = "http://testdomain.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class BoardHandler : System.Web.Services.WebService
    {

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object[] BoardHandle(string board)
        {
            int[,] test = JsonConvert.DeserializeObject<int[,]>(board);
            
            Board b = new Board(test);
            BoardNode root = new BoardNode(b, -1, -1, null);
            BoardNode solution = Search.AStar(root);

            ArrayList actionList = Search.buildActionList(solution);
            return actionList.ToArray();  
        }
    }
}
