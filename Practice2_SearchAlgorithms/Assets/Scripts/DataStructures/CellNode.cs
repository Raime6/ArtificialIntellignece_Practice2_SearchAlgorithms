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

        public void Reset()
        {
            this.parent = null;
            this.G = 0;
            this.H = 0;
            this.F = 0;
        }



        public List<CellNode> Expand(BoardInfo boardInfo)
        {
            List<CellNode> childs     = new List<CellNode>();
            CellInfo[]     neighbours = cellInfo.WalkableNeighbours(boardInfo);

            for (int i = 0; i < neighbours.Length; i++)
            {
                if(neighbours[i] != null)
                {
                    CellNode node = new CellNode(neighbours[i], this, boardInfo.currentGoalPosition);

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

        public float CalculateManhattanDistance(Vector2 node, Vector2 nodeGoal)
        {
            return Math.Abs(node.x - nodeGoal.x) + Math.Abs(node.y - nodeGoal.y);
        }
    }
}
