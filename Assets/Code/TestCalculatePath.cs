using UnityEngine;
using UnityEngine.AI;

namespace Code
{
    public class TestCalculatePath : MonoBehaviour
    {

        public Transform target;
        private NavMeshPath path = new NavMeshPath();
        void Update()
        {
            // Update the way to the goal every second.
            NavMesh.CalculatePath(new Vector3(2.0f, 0.3f, 2.0f), new Vector3(-2.0f, 0.3f, -2.0f), NavMesh.AllAreas, path);
        }
    }
}
