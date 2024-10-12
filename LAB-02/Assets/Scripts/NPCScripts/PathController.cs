using System;
using System.Collections.Generic;
using UnityEngine;
using WaypointScripts;

namespace NPCScripts
{
    public class PathController : MonoBehaviour
    {
        [SerializeField] private PathManager pathManager;
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;

        private List<Waypoint> _thePath;
        private Waypoint _target;

        private void Start()
        {
            _thePath = pathManager.GetPath();
            if (_thePath != null && _thePath.Count > 0)
            {
                // set starting target to the first waypoint
                _target = _thePath[0];
            }
        }

        private void RotateTowardsTarget()
        {
            float stepSize = rotateSpeed * Time.deltaTime;

            Vector3 targetDir = _target.GetPos() - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        private void MoveForward()
        {
            float stepSize = Time.deltaTime * moveSpeed;
            float distanceToTarget = Vector3.Distance(transform.position, _target.GetPos());
            if (distanceToTarget < stepSize)
            {
                // we will overshoot the target
                // so we should do something smarter here
                return;
            }
            // take a step forward
            transform.Translate(Vector3.forward * stepSize);
        }

        private void Update()
        {
            RotateTowardsTarget();
            MoveForward();
        }

        private void OnTriggerEnter(Collider other)
        {
            _target = pathManager.GetNextTarget();
        }
    }
}
