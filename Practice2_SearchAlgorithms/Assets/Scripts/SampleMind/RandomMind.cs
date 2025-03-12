using Assets.Scripts.DataStructures;
using UnityEngine;

namespace Assets.Scripts.SampleMind
{
    public class RandomMind : AbstractPathMind {
       
        public override Locomotion.MoveDirection GetNextMove(BoardInfo boardInfo, CellInfo currentPos, CellInfo[] goals)
        {

            int val;

            CellInfo[] neighbours = currentPos.WalkableNeighbours(boardInfo);

            do
            {
                val = Random.Range(0, 4);
            } while (neighbours[val] == null);

            if (val == 0) return Locomotion.MoveDirection.Up;
            if (val == 1) return Locomotion.MoveDirection.Right;
            if (val == 2) return Locomotion.MoveDirection.Down;
            return Locomotion.MoveDirection.Left;
        }
    }
}
