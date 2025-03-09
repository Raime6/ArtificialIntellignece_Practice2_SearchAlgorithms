
using Assets.Scripts;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Assets.Scripts.DataStructures.PlaceableItem;



namespace Assets.Scripts.Algorithms
{
    public class AStar
    {
        public List<CellNode> Behaviour(BoardInfo boardInfo, CellNode startNode)
        {
            List<CellNode> openList  = new List<CellNode>();
            List<string>   closeList = new List<string>();
            List<CellNode> sucesors  = new List<CellNode>();

            List<CellNode> goalPath  = new List<CellNode>();

            CellNode node;



            // Behaviour
            openList .Add(startNode);

            while (true)
            {

                if (openList.Count <= 0)
                    return null;

                node = openList[0];

                openList .RemoveAt(0);
                closeList.Add(node.cellInfo.CellId);

                if (node.cellInfo.CellId == boardInfo.Exit.CellId)
                    break;

                sucesors = node.Expand(boardInfo);
                for (int i = 0; i < sucesors.Count; i++)
                {
                    if (!closeList.Contains(sucesors[i].cellInfo.CellId))
                        InsertOrd(openList, sucesors[i]);
                }
            }

            do
            {
                goalPath.Add(node);
                node = node.parent;
            } while (node.parent != null);

            goalPath.Reverse();

            return goalPath;
        }



        private void InsertOrd(List<CellNode> nodeList, CellNode node)
        {
            int index = BinarySearch(nodeList, node, 0, nodeList.Count - 1);

            nodeList.Insert(index, node);
        }

        private int BinarySearch(List<CellNode> nodeList, CellNode node, int initPos, int lastPos)
        {
            if (initPos > lastPos)
                return initPos;

            int middle = initPos + (lastPos - initPos) / 2;

            if (nodeList[middle].F == node.F)
            {
                if (nodeList[middle].H > node.H)
                    return BinarySearch(nodeList, node, initPos, middle - 1);
                else
                    return BinarySearch(nodeList, node, middle + 1, lastPos);
            }

            if (nodeList[middle].F > node.F)
                return BinarySearch(nodeList, node, initPos, middle - 1);

            return BinarySearch(nodeList, node, middle + 1, lastPos);
        }
    }
}
