using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class VectorXZ
{
    public float x = 0, z = 0;
}

[CreateAssetMenu(fileName = "LevelEditor", menuName = "Create Level Editor", order = 1)]
public class LevelEditorCreator : ScriptableObject 
{
    public GameObject Object;
    public VectorXZ spawnPoint;

    public void BuildObject()
    {
        Vector3 bufSpawnPoint = new Vector3(spawnPoint.x * 3, 1.3f, spawnPoint.z * 3);
        if (!Mathf.Approximately(Mathf.Abs(spawnPoint.x) - Mathf.Floor((Mathf.Abs(spawnPoint.x))), 0.5f))
            bufSpawnPoint.x = Mathf.Round(bufSpawnPoint.x);
        if (!Mathf.Approximately(Mathf.Abs(spawnPoint.z) - Mathf.Floor((Mathf.Abs(spawnPoint.z))), 0.5f))
            bufSpawnPoint.z = Mathf.Round(bufSpawnPoint.z);

        Instantiate(Object, bufSpawnPoint, Quaternion.identity);
    }
}

[CustomEditor(typeof(LevelEditorCreator))]
public class UnityEditorChanger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelEditorCreator myScript = (LevelEditorCreator)target;
        if(GUILayout.Button("Build Object"))
            myScript.BuildObject();
    }
}