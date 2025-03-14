
using Assets.Scripts;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.DataStructures.PlaceableItem;



namespace Assets.Scripts.Algorithms
{
    public class AStar
    {
        private List<CellNode> openList    = new List<CellNode>();
        private List<CellNode> closeList   = new List<CellNode>();
        private List<CellNode> successors  = new List<CellNode>();

        private List<CellNode> goalPath    = new List<CellNode>();

        private CellNode node;

        public List<CellNode> Behaviour(Loader loader, BoardInfo boardInfo, CellNode startNode, ref int openListLength, ref int closeListLength)
        {
            openList .Add(startNode);

            while (true)
            {
                if (openList.Count <= 0)
                    return null;

                node = openList[0];

                openList .RemoveAt(0);
                closeList.Add(node);

                if (node.cellInfo.CellId == boardInfo.Exit.CellId)
                    break;

                successors = node.Expand(boardInfo);
                for (int i = 0; i < successors.Count; i++)
                    Insert(loader.algorithmOptimized, openList, successors[i]);
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
            if (optimization == Loader.Optimization.SIMPLE_LOOP && node.parent != null)
            {
                if (nodeSuccessor.cellInfo.CellId != node.parent.cellInfo.CellId)
                    InsertOrd(nodeList, nodeSuccessor);
            }
            else if (optimization == Loader.Optimization.COMPLEX_LOOP)
            {
                if (!nodeSuccessor.IsInList(closeList) && !nodeSuccessor.IsInList(openList))
                    InsertOrd(nodeList, nodeSuccessor);
            }
            else
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
