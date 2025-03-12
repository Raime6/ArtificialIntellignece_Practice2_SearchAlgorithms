using Assets.Scripts.Algorithms;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Assets.Scripts.SampleMind
{
    public class AStarOnlineMind : AbstractPathMind
    {

        private AStarOnline aStarOnline;
        private CellNode    currentNode;
        private Loader      loader;

        public void Initialize(Loader loader, BoardInfo boardInfo, CellNode startNode)
        {
            aStarOnline = new AStarOnline();

            currentNode = startNode;
            this.loader = loader;
        }

        public override Locomotion.MoveDirection GetNextMove(BoardInfo boardInfo, CellInfo currentPos, CellInfo[] goals)
        {
            currentNode = aStarOnline.Behaviour(loader, boardInfo, currentNode);

            Vector2 val = currentNode.cellInfo.GetPosition - currentPos.GetPosition;

            if (val.Equals(Vector2.up)) return Locomotion.MoveDirection.Up;
            if (val.Equals(Vector2.down)) return Locomotion.MoveDirection.Down;
            if (val.Equals(Vector2.left)) return Locomotion.MoveDirection.Left;
            return Locomotion.MoveDirection.Right;
        }
    }
}
