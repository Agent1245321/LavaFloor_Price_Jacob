using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
[CustomEditor(typeof(PowerUp))]
public class UIEditor : Editor
{
    public int Status;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PowerUp manager = (PowerUp)target;

        if(GUILayout.Button("Set PowerUp Apperance"))
        {
            Debug.Log("THIS IS A WORKING BUTTON IN THE EDITOR HAHAHAH");
            manager.UpdateButtons();
        }
    }
}
