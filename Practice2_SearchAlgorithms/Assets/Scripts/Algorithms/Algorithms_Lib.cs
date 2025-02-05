
using Assets.Scripts.DataStructures;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.DataStructures.PlaceableItem;

namespace Assets.Scripts.Algorithms
{
    public CellNode AStar_Behaviour(CellNode startNode, NodeTree tree)
    {
        List< CellNode > openList = new List< CellNode >();
        List< CellNode > sucesors = new List< CellNode >();

        CellNode node;



        // Behaviour
        openList.Add(startNode);

        while(true)
        {

            if (openList.Count <= 0)
                return null;

            node = openList[0];

            if (node.cellInfo.ItemInCell.Type == ItemType.Goal)
                return node;

            sucesors = node.;

        }
    }
}
