using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class VectorXZ
{
    public float x = 0, z = 0;
}

[CreateAssetMenu(fileName = "LevelEditor", menuName = "Level Editor", order = 1)]
public class LevelEditor : ScriptableObject 
{
    public GameObject environmentContainer;
    public SpawnPoints[] spawnPointsLists;
    [Space(10)]
    public GameObject standartObject;
    public VectorXZ spawnPoint;

    public void CreateEmptyEnvironmentContainer()
    {
        GameObject instanedObj = (GameObject)PrefabUtility.InstantiatePrefab(environmentContainer);
        instanedObj.transform.position = Vector3.zero;
        instanedObj.transform.rotation = Quaternion.identity;

        Transform[] children = instanedObj.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child != instanedObj.transform) 
                DestroyImmediate(child.gameObject);
        //for (int i = 1; i < children.Length; i++)
            //DestroyImmediate(children[i].gameObject);
    }

    public void BuildObject(GameObject obj, VectorXZ inputPoint)
    {
        if (!obj)
            return;

        if (!Mathf.Approximately(Mathf.Abs(inputPoint.x) - Mathf.Floor((Mathf.Abs(inputPoint.x))), 0.5f))
            inputPoint.x = Mathf.Round(inputPoint.x);
        if (!Mathf.Approximately(Mathf.Abs(inputPoint.z) - Mathf.Floor((Mathf.Abs(inputPoint.z))), 0.5f))
            inputPoint.z = Mathf.Round(inputPoint.z);

        Vector3 realSpawnPoint = new Vector3(inputPoint.x * 3, 1.3f, inputPoint.z * 3);
        GameObject instanedObj = (GameObject)PrefabUtility.InstantiatePrefab(obj);
        instanedObj.transform.position = realSpawnPoint;
        instanedObj.transform.rotation = Quaternion.identity;
        instanedObj.transform.parent = GameObject.FindGameObjectWithTag("Environment").transform;
    }

    public void BuildStandartObject()
    {
        if (!GameObject.FindGameObjectWithTag("Environment"))
            CreateEmptyEnvironmentContainer();

        BuildObject(standartObject, spawnPoint);
    }

    public void BuildObjectsByLists()
    {
        if (!GameObject.FindGameObjectWithTag("Environment"))
            CreateEmptyEnvironmentContainer();

        if (spawnPointsLists.Length > 0)
            foreach (var list in spawnPointsLists)
                foreach (var inpPoint in list.spawnPoints)
                    if (list.spawnObject) 
                        BuildObject(list.spawnObject, inpPoint);
    }
}

[CustomEditor(typeof(LevelEditor))]
public class UnityEditorChanger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelEditor myScript = (LevelEditor)target;
        if (GUILayout.Button("Build Object"))
            myScript.BuildStandartObject();
        if (GUILayout.Button("Build Objects By Lists"))
            myScript.BuildObjectsByLists();
    }
}