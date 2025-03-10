﻿using Assets.Scripts.DataStructures;
using Assets.Scripts.SampleMind;
using System.Dynamic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Locomotion))]
    public class CharacterBehaviour: MonoBehaviour
    {
        
        protected Locomotion       LocomotionController;
        protected AbstractPathMind PathController;
        public    BoardManager     BoardManager { get; set; }
        protected CellInfo         currentTarget;

        private   bool             algorithmInitialized;
       
        void Awake()
        {

            algorithmInitialized = false;
            PathController       = GetComponentInChildren<AStarMind>();
            PathController      .SetCharacter(this);
            LocomotionController = GetComponent<Locomotion>();
            LocomotionController.SetCharacter(this);

        }

        void Update()
        {
            if (BoardManager == null) return;
            if (LocomotionController.MoveNeed && algorithmInitialized)
            {

                var boardClone = (BoardInfo)BoardManager.boardInfo.Clone();
                LocomotionController.SetNewDirection(PathController.GetNextMove(boardClone,LocomotionController.CurrentEndPosition(),new [] {this.currentTarget}));
            }
        }

       

        public void SetCurrentTarget(CellInfo newTargetCell)
        {
            this.currentTarget = newTargetCell;
        }

        public void InitializeAlgorithm()
        {
            CellNode startNode = new CellNode(BoardManager.boardInfo.CellInfos[0, 0], null, Vector2.zero);
            PathController.GetComponent<AStarMind>().Initialize(BoardManager.boardInfo, startNode);

            algorithmInitialized = true;
        }
    }
}

