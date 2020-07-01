using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int currentSceneIndex = 0;
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        currentSceneIndex = sceneNumber;
    }
}
