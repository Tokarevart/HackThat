using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ObjectDestructionData
{
    public string name;
    public GameObject gameObject;
    public AudioSource soundEffect;
}

[CreateAssetMenu(fileName = "ObjectsDestructionData", menuName = "Create Objects Destruction Data", order = 1)]
public class ObjectsDestructionDataClass : ScriptableObject
{
    public List<ObjectDestructionData> ObjectsDestructionDataList = new List<ObjectDestructionData>();

    public ObjectDestructionData FindObjDestrData(string name)
    {
        foreach (var data in ObjectsDestructionDataList)
            if (data.name == name)
                return data;

        return null;
    }

    public void RefreshSoundEffects()
    {
        foreach (var data in ObjectsDestructionDataList)
            if (data.gameObject)
                data.soundEffect = data.gameObject.GetComponent<AudioSource>();
    }
}

[CustomEditor(typeof(ObjectsDestructionDataClass))]
public class UnityEditorChanger0 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectsDestructionDataClass myScript = (ObjectsDestructionDataClass)target;
        if(GUILayout.Button("Refresh Sound Effects"))
            myScript.RefreshSoundEffects();
    }
}