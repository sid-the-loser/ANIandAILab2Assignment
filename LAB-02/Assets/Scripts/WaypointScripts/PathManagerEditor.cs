using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WaypointScripts
{
    [CustomEditor(typeof(PathManager))]
    public class PathManagerEditor : Editor
    {
        [SerializeField] private PathManager pathManager;

        [SerializeField] private List<Waypoint> thePath;
        private List<int> _toDelete;

        private Waypoint _selectedPoint = null;
        private bool _doRepaint = true;

        private void OnScreenGUI()
        {
            thePath = pathManager.GetPath();
            DrawPath(thePath);
        }

        private void OnEnable()
        {
            pathManager = target as PathManager;
            _toDelete = new List<int>();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            thePath = pathManager.GetPath();
            
            base.OnInspectorGUI();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Path");
            
            DrawGUIForPoints();
            
            // button for adding a point to the path
            if (GUILayout.Button("Add Point to Path"))
            {
                pathManager.CreateAddPoint();
            }
            
            EditorGUILayout.EndVertical();
            SceneView.RepaintAll();
        }

        private void DrawGUIForPoints()
        {
            if (thePath != null && thePath.Count > 0)
            {
                for (int i = 0; i < thePath.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    Waypoint p = thePath[i];

                    Vector3 oldPos = p.GetPos();
                    Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);
                    
                    // the delete button
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        _toDelete.Add(i);
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (_toDelete.Count > 0)
            {
                foreach (var i in _toDelete)
                {
                    thePath.RemoveAt(i);
                }
                _toDelete.Clear();
            }
        }

        public void DrawPathLine(Waypoint p1, Waypoint p2)
        {
            // draw a line between current and next point
            Color c = Handles.color;
            
        }

        private void DrawPath(List<Waypoint> path)
        {
            if (path != null)
            {
                int current = 0;
                foreach (var wp in path)
                {
                    // draw current point
                    _doRepaint = DrawPoint(wp);
                    int next = (current + 1) % path.Count;
                    Waypoint wpNext = path[next];

                    DrawPathLine(wp, wpNext);
                    
                    // advance counter
                    current++;
                }
            }
            if (_doRepaint) Repaint();
        }
    }
}
