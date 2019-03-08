using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(WaveManager))]
public class WaveManager_Editor : Editor
{
    private WaveManager wave;

    private void OnEnable()
    {
        wave = target as WaveManager;
    }

    private void OnSceneGUI()
    {
        DrawHandles();
        DrawLines();
    }

    private void DrawHandles()
    {
        for (int i = 0; i < wave.m_corners.Count; i++)
        {
            wave.m_corners[i] = Handles.PositionHandle(wave.m_corners[i], Quaternion.identity);
        }
        EditorUtility.SetDirty(target);
    }

    private void DrawLines()
    {
        Handles.color = Color.red;
        if (wave.m_corners.Count > 0)
        {
            Vector3 start = wave.m_corners[0];
            for (int i = 1; i < wave.m_corners.Count; i++)
            {
                Vector3 end = wave.m_corners[i];

                Handles.DrawLine(start, end);

                start = end;
            }
            Handles.DrawLine(wave.m_corners[0], wave.m_corners[wave.m_corners.Count - 1]);
        }
    }
}
#endif