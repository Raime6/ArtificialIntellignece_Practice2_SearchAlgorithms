
using Assets.Scripts;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Assets.Scripts.DataStructures.PlaceableItem;



namespace Assets.Scripts.Algorithms
{
    public class BFS
    {
        List<CellNode> openList    = new List<CellNode>();
        List<string>   closeList   = new List<string>();
        List<CellNode> successors  = new List<CellNode>();

        List<CellNode> goalPath    = new List<CellNode>();

        CellNode       node;



        public List<CellNode> Behaviour(Loader loader, BoardInfo boardInfo, CellNode startNode, ref int openListLength, ref int closeListLength)
        {
            openList.Add(startNode);

            while (true)
            {

                if (openList.Count <= 0)
                    return null;

                node = openList[0];

                openList.RemoveAt(0);
                closeList.Add(node.cellInfo.CellId);

                if (node.cellInfo.CellId == boardInfo.Exit.CellId)
                    break;

                successors = node.Expand(boardInfo);
                for (int i = 0; i < successors.Count; i++)
                {
                    Insert(loader.algorithmOptimized, openList, successors[i]);
                }
            }

            openListLength  = openList .Count;
            closeListLength = closeList.Count;

            do
            {
                goalPath.Add(node);
                node = node.parent;
            } while (node.parent != null);

            goalPath.Reverse();

            return goalPath;
        }


        private void Insert(Loader.Optimization optimization, List<CellNode> nodeList, CellNode nodeSuccessor)
        {
            if (optimization == Loader.Optimization.SIMPLE_LOOP)
            {
                if (nodeSuccessor.cellInfo.CellId != node.cellInfo.CellId)
                    nodeList.Add(nodeSuccessor);
            }
            else if (optimization == Loader.Optimization.COMPLEX_LOOP)
            {
                if (!closeList.Contains(nodeSuccessor.cellInfo.CellId))
                    nodeList.Add(nodeSuccessor);
            }
            else
                nodeList.Add(nodeSuccessor);
        }
    }
}
