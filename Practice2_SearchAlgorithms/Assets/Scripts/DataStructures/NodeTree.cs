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
        private List<CellNode> childrens;



        public NodeTree()
        {
            childrens = new List<CellNode>();
        }



        public void AddChild (CellNode newChild)
        {
            childrens.Add(newChild);
        }

        public object Clone()
        {
            var result = new NodeTree();

            return result;
        }
    }
}
