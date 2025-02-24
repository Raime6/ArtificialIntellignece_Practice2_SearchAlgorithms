using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataStructures
{
    public class CellNode : ICloneable
    {
        public CellInfo  cellInfo { get; private set; }
            
        private CellNode parent   { get; set; }

        public  float    G        { get; set; }
        private float    H        { get; set; }
        public  float    F        { get; set; }



        public CellNode(CellInfo _cellInfo, CellNode _parent)
        {
            this.cellInfo = _cellInfo;
            this.parent   = _parent;

            if (parent != null)
                G = 0f;
            else
                G = parent.G + cellInfo.WalkCost;

            this.H        = 0;

            this.F        = this.G + this.H;
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



        public void setParent(CellNode parent)
        {
            this.parent = parent;
        }



        public List<CellNode> Expand(BoardManager boardManager)
        {
            List<CellNode> childs     = new List<CellNode>();
            CellInfo[]     neighbours = cellInfo.WalkableNeighbours(boardManager.boardInfo);

            for (int i = 0; i < neighbours.Length; i++)
            {
                CellNode node = new CellNode(neighbours[i], this);

                childs.Add(node);
            }

            return childs;
        }
    }
}
