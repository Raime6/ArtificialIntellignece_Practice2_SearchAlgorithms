using Assets.Scripts.Algorithms;
using UnityEngine;

namespace Assets.Scripts
{
    public class Loader : MonoBehaviour
    {
        public enum Algorithm
        {
            BFS,
            ASTAR,
            ASTAR_ONLINE
        }

        public enum Optimization
        {
            NONE,
            SIMPLE_LOOP,
            COMPLEX_LOOP
        }

        public GameObject   gameManager;          //GameManager prefab to instantiate.
        public bool         Planner             = false;
        public int          seed                = 2016;
        public int          numEnemies          = 0;
        public Algorithm    characterAlgorithm  = Algorithm.BFS;
        public int          deepness            = 0;
        public Optimization algorithmOptimized  = Optimization.NONE;

        void Awake()
        {
            //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
            if (GameManager.instance == null)
            {
                //Instantiate gameManager prefab
                var obj = Instantiate(gameManager) as GameObject;
                obj.name = "GameManager";
                var manager=obj.GetComponent<GameManager>();
                manager.ForPlanner = Planner;
                manager.seed = seed;
                manager.numEnemies = numEnemies;
                manager.InitGame();
            }


        }
    }
}