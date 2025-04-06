using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEditor;

namespace UzGameDev.TinyScripts
{
    [CustomEditor(typeof(Camera_Bounds_2D))]
    [CanEditMultipleObjects]
    public class Camera_Bounds_2D_Editor : Editor
    {
        private Camera_Bounds_2D bounds;
        private readonly BoxBoundsHandle m_BoundsHandle = new();
        
        //============================================================
        public void OnSceneGUI()
        {
            bounds = (Camera_Bounds_2D)target;
            var handleMatrix = bounds.transform.localToWorldMatrix;
            handleMatrix.SetRow(0, Vector4.Scale(handleMatrix.GetRow(0), new Vector4(1f, 1f, 0f, 1f)));
            handleMatrix.SetRow(1, Vector4.Scale(handleMatrix.GetRow(1), new Vector4(1f, 1f, 0f, 1f)));
            handleMatrix.SetRow(2, new Vector4(0f, 0f, 1f, bounds.transform.position.z));
            
            using (new Handles.DrawingScope(handleMatrix))
            {
                m_BoundsHandle.center = bounds.offset;
                m_BoundsHandle.size = bounds.bound;
                m_BoundsHandle.SetColor(Color.white);
                EditorGUI.BeginChangeCheck();
                m_BoundsHandle.DrawHandle();
                
                var rect = new Rect(m_BoundsHandle.center.x - (m_BoundsHandle.size.x / 2),
                    m_BoundsHandle.center.y - (m_BoundsHandle.size.y / 2), m_BoundsHandle.size.x, m_BoundsHandle.size.y);
                
                Handles.DrawSolidRectangleWithOutline(rect, new Color(1, 1, 1, 0.1f), Color.yellow);
                
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(bounds, $"Modify {ObjectNames.NicifyVariableName(bounds.GetType().Name)}");
                    
                    var oldSize = bounds.bound;
                    bounds.bound = m_BoundsHandle.size;
                    
                    if (bounds.bound != oldSize)
                    {
                        bounds.offset = m_BoundsHandle.center;
                    }
                }
            }
        }
        //============================================================
    }
}