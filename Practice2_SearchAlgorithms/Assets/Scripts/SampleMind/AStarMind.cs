using Assets.Scripts.Algorithms;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SampleMind
{
    public class AStarMind : AbstractPathMind {

        private AStar aStar;
        private List<CellNode> goalPath;

        public void Initialize(BoardInfo boardInfo, CellNode startNode)
        {
            aStar = new AStar();

            goalPath = aStar.Behaviour(boardInfo, startNode);
        }

        public override Locomotion.MoveDirection GetNextMove(BoardInfo boardInfo, CellInfo currentPos, CellInfo[] goals)
        {
            Vector2 val = goalPath[0].cellInfo.GetPosition - currentPos.GetPosition;
            goalPath.RemoveAt(0);

            if (val.Equals(Vector2.up))   return Locomotion.MoveDirection.Up;
            if (val.Equals(Vector2.down)) return Locomotion.MoveDirection.Down;
            if (val.Equals(Vector2.left)) return Locomotion.MoveDirection.Left;
            return Locomotion.MoveDirection.Right;
        }
    }
}
