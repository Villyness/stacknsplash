#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Onerat_Placement_ActionList
{
    public static void PlaceObject(GameObject gameObject, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        GameObject objectToSpawn = UnityEditor.PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
        if(objectToSpawn)
        {
            UnityEditor.Undo.RegisterCreatedObjectUndo(objectToSpawn, "Created objectToSpawn");
            objectToSpawn.transform.position = pos;
            objectToSpawn.transform.rotation = rot;
            objectToSpawn.transform.localScale += scale;
        }
    }
}
#endif

