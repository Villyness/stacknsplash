using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentManager : MonoBehaviour
{
    public Material Midday;
    public Material Sunset;
    public Material Midnight;
    public Material Daybreak;

    //was going to make the sky change with the level but i took it out because it looks jarring and weeeird
    private void Update()
    {
        switch (GameManager.Instance.Level)
        {
            case 1:
                RenderSettings.skybox = Midday;
                break;
            case 2:
                RenderSettings.skybox = Sunset;
                break;
            case 3:
                RenderSettings.skybox = Midnight;
                break;
            case 4:
                RenderSettings.skybox = Daybreak;
                break;

        }
    }
}
