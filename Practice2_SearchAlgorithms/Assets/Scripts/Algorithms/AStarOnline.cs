
using Assets.Scripts;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Assets.Scripts.DataStructures.PlaceableItem;



namespace Assets.Scripts.Algorithms
{
    public class AStarOnline
    {
        private List<CellNode> openList   = new List<CellNode>();
        private List<CellNode> successors = new List<CellNode>();

        private CellNode node;



        public CellNode Behaviour(BoardInfo boardInfo, CellNode currentNode)
        {
            openList.Add(currentNode);

            if (openList.Count <= 0)
                return null;

            node = openList[0];

            openList.RemoveAt(0);

            if (node.cellInfo.CellId == boardInfo.currentGoalPosition)
                return node;

            successors = node.Expand(boardInfo);
            for (int i = 0; i < successors.Count; i++)
                Insert(openList, successors[i]);

            return openList[0];
        }



        private void Insert(List<CellNode> nodeList, CellNode nodeSuccessor)
        {
            InsertOrd(nodeList, nodeSuccessor);
        }

        private void InsertOrd(List<CellNode> nodeList, CellNode nodeSuccessor)
        {
            int index = BinarySearch(nodeList, nodeSuccessor, 0, nodeList.Count - 1);

            nodeList.Insert(index, nodeSuccessor);
        }

        private int BinarySearch(List<CellNode> nodeList, CellNode nodeSuccessor, int initPos, int lastPos)
        {
            if (initPos > lastPos)
                return initPos;

            int middle = initPos + (lastPos - initPos) / 2;

            if (nodeList[middle].F == nodeSuccessor.F)
            {
                if (nodeList[middle].H > nodeSuccessor.H)
                    return BinarySearch(nodeList, nodeSuccessor, initPos, middle - 1);
                else
                    return BinarySearch(nodeList, nodeSuccessor, middle + 1, lastPos);
            }

            if (nodeList[middle].F > nodeSuccessor.F)
                return BinarySearch(nodeList, nodeSuccessor, initPos, middle - 1);

            return BinarySearch(nodeList, nodeSuccessor, middle + 1, lastPos);
        }
    }
}
