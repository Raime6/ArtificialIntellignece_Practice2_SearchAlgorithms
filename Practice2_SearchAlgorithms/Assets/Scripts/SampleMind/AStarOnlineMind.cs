using Assets.Scripts.Algorithms;
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SampleMind
{
    public class AStarOnlineMind : AbstractPathMind
    {

        private AStarOnline aStarOnline;
        private CellNode    currentNode;
        private Loader      loader;
        private Stopwatch   stopWatch;
        private int         openListLength;
        private int         closeListLength;
        private int         goalPathLength;

        private int         deepness;

        public void Initialize(Loader loader, BoardInfo boardInfo, CellNode startNode, int deepness)
        {
            aStarOnline     = new AStarOnline();
            stopWatch       = new Stopwatch();
            openListLength  = 0;
            closeListLength = 0;

            currentNode = startNode;
            this.loader = loader;

            this.deepness = deepness;
        }

        public override Locomotion.MoveDirection GetNextMove(BoardInfo boardInfo, CellInfo currentPos, CellInfo[] goals)
        {         
            if (boardInfo.Enemies.Count > 0)
            {
                boardInfo.currentGoalPosition = GetNearestEnemy(boardInfo.Enemies, currentNode);
            }
            else
                boardInfo.currentGoalPosition = new Vector2(boardInfo.Exit.ColumnId, boardInfo.Exit.RowId);

            stopWatch.Start();
            currentNode = aStarOnline.Behaviour(loader, boardInfo, currentNode, deepness, ref openListLength, ref closeListLength, ref goalPathLength);
            stopWatch.Stop();

            UnityEngine.Debug.Log("A* Online - Average Time: "        + stopWatch.ElapsedMilliseconds + " ms");
            UnityEngine.Debug.Log("A* Online - Open List nº nodes: "  + openListLength                + " nodes");
            UnityEngine.Debug.Log("A* Online - Close List nº nodes: " + closeListLength               + " nodes");
            UnityEngine.Debug.Log("A* Online - Path Length: "         + deepness                      + " cells");

            Vector2 val = currentNode.cellInfo.GetPosition - currentPos.GetPosition;

            currentNode.Reset();

            if (val.Equals(Vector2.up)) return Locomotion.MoveDirection.Up;
            if (val.Equals(Vector2.down)) return Locomotion.MoveDirection.Down;
            if (val.Equals(Vector2.left)) return Locomotion.MoveDirection.Left;
            return Locomotion.MoveDirection.Right;
        }

        private Vector2 GetNearestEnemy(List<EnemyBehaviour> enemies, CellNode currentNode)
        {
            Vector2 nearestEnemyPosition;
            Vector2 enemyPosition;

            if (enemies.Count == 1)
                return enemies[0].CurrentPosition().GetPosition;

            nearestEnemyPosition = enemies[0].CurrentPosition().GetPosition;

            for (int i = 1; i < enemies.Count; i++)
            {
                enemyPosition = enemies[i].CurrentPosition().GetPosition;

                if (currentNode.CalculateManhattanDistance(currentNode.cellInfo.GetPosition, nearestEnemyPosition) > currentNode.CalculateManhattanDistance(currentNode.cellInfo.GetPosition, enemyPosition))
                    nearestEnemyPosition = enemyPosition;
            }

            return nearestEnemyPosition;
        }
    }
}
