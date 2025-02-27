
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
            List<CellNode> closeList = new List<CellNode>();
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
                closeList.Add(node);

                if (node.cellInfo.ItemInCell.Type == ItemType.Goal)
                    break;

                sucesors = node.Expand(boardInfo);
                for (int i = 0; i < sucesors.Count; i++)
                {
                    if (!closeList.Contains(sucesors[i]))
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
            if (initPos >= lastPos)
            {
                if (node.F < nodeList[initPos].F || (node.F == nodeList[initPos].F && node.G < nodeList[initPos].G))
                    return initPos;
                else
                    return initPos + 1;
            }

            int middle = initPos + (lastPos - initPos) / 2;

            if (node.F < nodeList[middle].F || (node.F == nodeList[initPos].F && node.G < nodeList[initPos].G))
                return BinarySearch(nodeList, node, initPos, middle);
            else
                return BinarySearch(nodeList, node, middle + 1, lastPos);
        }
    }
}
