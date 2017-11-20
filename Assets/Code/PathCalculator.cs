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

                //Game.Ctx.Notification.display = true;
                //Game.Ctx.towerNotification = "Unable to build new tower: Blocking Path!";
                return false;
            }
            else
            {
                //for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
                //    Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.red);
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
