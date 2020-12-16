using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class SceneManagement : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        PlayerBehaviour.life = 3;
        PlayerBehaviour.lightlvl = 1;
        PlayerBehaviour.maxLightRadius = 4;
        PlayerBehaviour.maxLightRadiusLast = 4;
        PlayerBehaviour.oillvl = 0;
        PlayerBehaviour.maxLife = 3;
        PlayerBehaviour.life = 3;
        PlayerBehaviour.resetLight();
    }
}
