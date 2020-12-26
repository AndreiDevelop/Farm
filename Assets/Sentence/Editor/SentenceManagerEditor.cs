using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SentenceManager))]
public class SentenceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SentenceManager myScript = (SentenceManager)target;

        if (GUILayout.Button("Generate Sentences"))
        {
            myScript.GenerateSentences();
        }

        if (GUILayout.Button("Clear Sentences"))
        {
            myScript.ClearSentences();
        }
    }
}
