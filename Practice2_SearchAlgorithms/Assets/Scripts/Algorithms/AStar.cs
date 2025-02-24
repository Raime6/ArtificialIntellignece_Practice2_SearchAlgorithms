
using Assets.Scripts;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.DataStructures.PlaceableItem;



namespace Assets.Scripts.Algorithms
{
    public class AStar
    {
        public CellNode Behaviour(BoardManager boardManager, CellNode startNode, NodeTree tree)
        {
            List<CellNode> openList = new List<CellNode>();
            List<CellNode> sucesors = new List<CellNode>();

            CellNode node;



            // Behaviour
            tree.AddChild(startNode);
            openList.Add(startNode);

            while (true)
            {

                if (openList.Count <= 0)
                    return null;

                node = openList[0];
                openList.RemoveAt(0);

                if (node.cellInfo.ItemInCell.Type == ItemType.Goal)
                    return node;

                sucesors = node.Expand(boardManager);
                for (int i = 0; i < sucesors.Count; i++)
                {
                    tree.AddChild(startNode);
                    InsertOrd(openList, sucesors[i]);
                }
            }
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
