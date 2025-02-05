using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataStructures
{
    public class NodeTree : ICloneable
    {
        private CellNode root;
        private List<CellNode> childrens;



        public NodeTree(CellNode root)
        {
            this.root = root;
            childrens = new List<CellNode>();
        }



        public void addChild (CellNode newChild)
        {
            childrens.Add(newChild);
        }

        public object Clone()
        {
            var result = new NodeTree(this.root);

            return result;
        }
    }
}
