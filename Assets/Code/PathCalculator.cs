using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Code
{
    public class PathCalculator : MonoBehaviour
    {

        public NavMeshAgent spawnPosition;
        public bool pathAvailable;
        //private bool hasPath = false;
        private bool _display = false;
        private string blockingNotification;

        void Start()
        {
            spawnPosition = GetComponent<NavMeshAgent>();
        }


        public bool CalculateNewPath()
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            spawnPosition.CalculatePath(new Vector3(-2.0f, 0f, -2.0f), navMeshPath);

            if (navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public bool getdisplayNotification()
        //{
        //    return _display;
        //}
        //public string getblockingNotification()
        //{
        //    return blockingNotification;
        //}

    }
}
