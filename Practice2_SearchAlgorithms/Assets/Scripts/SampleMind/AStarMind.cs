﻿using Assets.Scripts.Algorithms;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.SampleMind
{
    public class AStarMind : AbstractPathMind {

        private AStar          aStar;
        private List<CellNode> goalPath;
        private Stopwatch      stopWatch;
        private int            openListLength;
        private int            closeListLength;

        public void Initialize(Loader loader, BoardInfo boardInfo, CellNode startNode)
        {
            aStar           = new AStar();
            stopWatch       = new Stopwatch();
            openListLength  = 0;
            closeListLength = 0;

            stopWatch.Start();
            goalPath = aStar.Behaviour(loader, boardInfo, startNode, ref openListLength, ref closeListLength);
            stopWatch.Stop();

            UnityEngine.Debug.Log("A* - Average Time: "        + stopWatch.ElapsedMilliseconds + " ms");
            UnityEngine.Debug.Log("A* - Open List nº nodes: "  + openListLength                + " nodes");
            UnityEngine.Debug.Log("A* - Close List nº nodes: " + closeListLength               + " nodes");
            UnityEngine.Debug.Log("A* - Path Length: "         + goalPath.Count                + " cells");
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
