#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Onerat_Placement : EditorWindow
{
    [MenuItem("Onerat/Onerat Placement #b")]
    private static void Init()
    {
        Onerat_Placement window = (Onerat_Placement)GetWindow(typeof(Onerat_Placement));
        window.title = "Onerat Placement";
        window.Show();
    }

    bool showBtn = true;
    bool showScaleOptions = true;
    bool showAdditionalOptions = true;

    bool alignToNormal = false;
    bool useAbsoluteValuesRot = false;
    bool useAbsoluteValuesScale = false;

    bool active = false;

    float xAmount = 0;
    float yAmount = 0;
    float zAmount = 0;

    float xScaleAmount = 0;
    float yScaleAmount = 0;
    float zScaleAmount = 0;

    private int X;
    private int Y;
    private int Z;

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    Vector2 scrollPos;

    void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
        active = EditorGUILayout.Toggle("Enable Placement", active);

        EditorGUILayout.Space();
        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();

        showBtn = EditorGUILayout.Toggle("Show Rotation Options", showBtn);

        if (showBtn)
        {

            EditorGUILayout.LabelField("X");
            xAmount = EditorGUILayout.Slider(xAmount, 0, 360);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Y");
            yAmount = EditorGUILayout.Slider(yAmount, 0, 360);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Z");
            zAmount = EditorGUILayout.Slider(zAmount, 0, 360);
            EditorGUILayout.Space();

            alignToNormal = EditorGUILayout.Toggle("Align to Normals", alignToNormal);
            EditorGUILayout.Space();

            useAbsoluteValuesRot = EditorGUILayout.Toggle("Use Absolute Values", useAbsoluteValuesRot);
            EditorGUILayout.Space();

            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();

        }
        showScaleOptions = EditorGUILayout.Toggle("Show Scale Options", showScaleOptions);

        if (showScaleOptions)
        {
            EditorGUILayout.LabelField("X");
            xScaleAmount = EditorGUILayout.Slider(xScaleAmount, 0f, 1f);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Y");
            yScaleAmount = EditorGUILayout.Slider(yScaleAmount, 0f, 1f);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Z");
            zScaleAmount = EditorGUILayout.Slider(zScaleAmount, 0f, 1f);
            EditorGUILayout.Space();

            useAbsoluteValuesScale = EditorGUILayout.Toggle("Use Absolute Values", useAbsoluteValuesScale);
            EditorGUILayout.Space();

            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
        }

        showAdditionalOptions = EditorGUILayout.Toggle("Show Additional Options", showAdditionalOptions);

        if (showAdditionalOptions)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Help"))
                Application.OpenURL("https://docs.google.com/document/d/1Cv4HPoDJeLHtl_zLEuAwZ2n3CkR7MmxpgDVWmRW1ahI/edit?usp=sharing");

            if (GUILayout.Button("Onerat Games"))
                Application.OpenURL("https://www.oneratgames.com/");

            GUILayout.EndHorizontal();

            GUILayout.Box("Select a prefab in the project window to get started. Middle mouse button to place an object.\n\n Version 1.0 \n\n Created by @oneratdylan ");

        }

        if (GUILayout.Button("Close"))
        {
            this.Close();
        }

        EditorGUILayout.EndScrollView();     

    }

    private void Update()
    {
        SceneView.onSceneGUIDelegate += OnScene;        
    }
    bool pressed = false;
    bool pressed2 = false;

    void OnScene(SceneView scene)
    {
        if(active)
        {
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 2)
            {
                if(!useAbsoluteValuesRot)
                {
                    X = Random.Range(0, (int)xAmount);
                    Y = Random.Range(0, (int)yAmount);
                    Z = Random.Range(0, (int)zAmount);
                }
                else
                {
                    X = (int)xAmount;
                    Y = (int)yAmount;
                    Z = (int)zAmount;
                }
                if(!useAbsoluteValuesScale)
                {
                    scaleX = Random.Range(0f, xScaleAmount);
                    scaleY = Random.Range(0f, yScaleAmount);
                    scaleZ = Random.Range(0f, zScaleAmount);
                }
                else
                {
                    scaleX = xScaleAmount;
                    scaleY = yScaleAmount;
                    scaleZ = zScaleAmount;
                }

                Quaternion randomRotation = Quaternion.Euler(X, Y, Z);

                Vector3 mousePos = e.mousePosition;
                float ppp = EditorGUIUtility.pixelsPerPoint;
                mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
                mousePos.x *= ppp;

                Ray ray = scene.camera.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out hit))
                {
                    if (alignToNormal)
                    {
                        Quaternion startRot = Quaternion.EulerAngles(X + hit.normal.x, Y + hit.normal.y, Z + hit.normal.z);
                        randomRotation = startRot;
                    }
                    var startPos = hit.point;
                    Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);
                    if(Selection.activeObject as GameObject)
                    Onerat_Placement_ActionList.PlaceObject(Selection.activeObject as GameObject, startPos, randomRotation, scale);
                }
                e.Use();
            }
        }       
    }
    RaycastHit hit;
}
#endif
