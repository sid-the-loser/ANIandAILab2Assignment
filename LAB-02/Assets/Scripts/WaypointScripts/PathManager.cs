using System.Collections.Generic;
using UnityEngine;

namespace WaypointScripts
{
    public class PathManager : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private List<Waypoint> path;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<GameObject> prefabPoints;
        
        private int _currentPointIndex;

        public List<Waypoint> GetPath()
        {
            if (path == null)
            {
                path = new List<Waypoint>();
            }

            return path;
        }

        public void CreateAddPoint()
        {
            Waypoint go = new Waypoint();
            path.Add(go);
        }

        public Waypoint GetNextTarget()
        {
            int nextPointIndex = (_currentPointIndex + 1) % path.Count;
            _currentPointIndex = nextPointIndex;
            
            return path[nextPointIndex];
        }
    }
}
