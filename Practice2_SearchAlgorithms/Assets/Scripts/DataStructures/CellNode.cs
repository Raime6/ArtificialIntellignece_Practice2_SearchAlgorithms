using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.DataStructures
{
    public class CellNode : ICloneable
    {
        public   CellInfo cellInfo { get; private set; }
            
        public   CellNode parent   { get; set; }

        public   float    G        { get; set; }
        public   float    H        { get; set; }
        public   float    F        { get; set; }



        public CellNode(CellInfo _cellInfo, CellNode _parent, Vector2 cellNodeGoalPosition)
        {
            this.cellInfo = _cellInfo;
            this.parent   = _parent;

            if (parent == null)
                G = 0f;
            else
                G = parent.G + cellInfo.WalkCost;

            this.H = CalulateH(cellNodeGoalPosition);

            this.F = this.G + this.H;
        }

        public CellNode(CellInfo _cellInfo, CellNode _parent, float _G, float _H)
        {
            this.cellInfo = _cellInfo;
            this.parent   = _parent;
            this.G        = _G;
            this.H        = _H;
            this.F        = this.G + this.H;
        }

        public object Clone()
        {
            var result = new CellNode(this.cellInfo, this.parent, this.G, this.H);

            return result;
        }



        public List<CellNode> Expand(BoardInfo boardInfo)
        {
            List<CellNode> childs     = new List<CellNode>();
            CellInfo[]     neighbours = cellInfo.WalkableNeighbours(boardInfo);

            Vector2 goalPosition;

            if (SceneManager.GetActiveScene().name == "Enemies")
            {
                if(boardInfo.Enemies.Count > 0)
                {
                    goalPosition = GetNearestEnemy(boardInfo.Enemies);
                }
                else
                    goalPosition = new Vector2(boardInfo.Exit.ColumnId, boardInfo.Exit.RowId);
            }
            else
                goalPosition = new Vector2(boardInfo.Exit.ColumnId, boardInfo.Exit.RowId);

            boardInfo.currentGoalPosition = goalPosition.x + "," + goalPosition.y;

            for (int i = 0; i < neighbours.Length; i++)
            {
                if(neighbours[i] != null)
                {
                    CellNode node = new CellNode(neighbours[i], this, goalPosition);

                    childs.Add(node);
                }
            }

            return childs;
        }

        private float CalulateH(Vector2 cellNodeGoalPosistion)
        {
            Vector2 cellNodePosition = cellInfo.GetPosition;

            return CalculateManhattanDistance(cellNodePosition, cellNodeGoalPosistion);
        }

        private float CalculateManhattanDistance(Vector2 node, Vector2 nodeGoal)
        {
            return Math.Abs(node.x - nodeGoal.x) + Math.Abs(node.y - nodeGoal.y);
        }

        private Vector2 GetNearestEnemy(List<EnemyBehaviour> enemies)
        {
            Vector2 nearestEnemyPosition;
            Vector2 enemyPosition;
            
            if (enemies.Count == 1)
                return enemies[0].CurrentPosition().GetPosition;

            nearestEnemyPosition = enemies[0].CurrentPosition().GetPosition;

            for (int i = 1; i < enemies.Count; i++)
            {
                enemyPosition = enemies[i].CurrentPosition().GetPosition;

                if (CalculateManhattanDistance(this.cellInfo.GetPosition, nearestEnemyPosition) > CalculateManhattanDistance(this.cellInfo.GetPosition, enemyPosition))
                    nearestEnemyPosition = enemyPosition;
            }

            return nearestEnemyPosition;
        }
    }
}
