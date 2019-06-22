using System;
using System.Reflection;
using UnityEditor;

[InitializeOnLoad]
public static class GameViewScaleFixer
{
    private static Assembly m_assembly = Assembly.Load("UnityEditor.dll");
    private static Type m_type = m_assembly.GetType("UnityEditor.GameView");
    private static BindingFlags m_bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
    private static MethodInfo m_snapZoomMethod = m_type.GetMethod("SnapZoom", m_bindingAttr);
    private static object[] m_parameters = new object[] { 0.21f };

    static GameViewScaleFixer()
    {
        //EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        //var gameView = EditorWindow.GetWindow(m_type);
        //if (gameView == null) return;
        //m_snapZoomMethod.Invoke(gameView, m_parameters);
    }
}