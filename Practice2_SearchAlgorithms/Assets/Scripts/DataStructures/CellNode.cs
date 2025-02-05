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
        public CellInfo cellInfo  { get; private set; }
            
        private CellNode parent   { get; set; }

        private int      G        { get; set; }
        private int      H        { get; set; }
        private int      F        { get; set; }



        public CellNode(CellInfo _cellInfo, int _G, int _H)
        {
            this.cellInfo = _cellInfo;
            this.G        = _G;
            this.H        = _H;
            this.F        = this.G + this.H;
        }

        public object Clone()
        {
            var result = new CellNode(this.cellInfo, this.G, this.F);

            return result;
        }



        /*public List<CellNode> Expand()
        {
            List < CellNode > childs = new List<CellNode>();
            CellInfo[] neighbours = cellInfo.WalkableNeighbours();


        }*/
    }
}
