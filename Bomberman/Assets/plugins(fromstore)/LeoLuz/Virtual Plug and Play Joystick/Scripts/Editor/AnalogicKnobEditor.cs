using UnityEditor;
using UnityEngine;

namespace LeoLuz
{
    [CustomEditor(typeof(AnalogicKnob))]
    class AnalogicKnobEditor : Editor
    {
        void OnSceneGUI()
        {
            AnalogicKnob analogicKnob = target as AnalogicKnob;
            if (analogicKnob == null)
                return;

            Canvas canvas = analogicKnob.GetComponent<Canvas>();
            RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
            float fullWidthX = CanvasRect.sizeDelta.x * CanvasRect.localScale.x;
            float fullWidthY = CanvasRect.sizeDelta.y * CanvasRect.localScale.y;
            Vector3 InitialPoint = CanvasRect.transform.position-new Vector3(fullWidthX*0.5f, fullWidthY * 0.5f,0f);
            Handles.color = Color.cyan;
            Vector3 HandlePoint = InitialPoint + Vector3.up * (fullWidthY * analogicKnob.YNormalScope) + Vector3.right * (fullWidthX * analogicKnob.XNormalScope);
            Handles.DrawLine(HandlePoint, HandlePoint +  Vector3.down* fullWidthY*analogicKnob.YNormalScope);
            Handles.DrawLine(HandlePoint, HandlePoint + Vector3.left * fullWidthX * analogicKnob.XNormalScope);
            HandlePoint = Handles.FreeMoveHandle(HandlePoint, Quaternion.identity, 5f, Vector3.zero, Handles.CubeHandleCap);
            Vector3 Difference = HandlePoint - InitialPoint;
            Vector3 normal = new Vector2(Difference.x / fullWidthX, Difference.y / fullWidthY);
            analogicKnob.XNormalScope = normal.x;
            analogicKnob.YNormalScope = normal.y;
        }
    }
}