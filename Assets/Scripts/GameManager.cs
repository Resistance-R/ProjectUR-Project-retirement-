using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private aboutCamera cameraScript;

    public bool isLive;

    public static GameManager Instance;

    private void Awake()
    {
        isLive = true;
        cameraScript = Camera.main.GetComponent<aboutCamera>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleSanityChange(float sanity)
    {
        if (sanity <= 0)
        {
            cameraScript.StartShake();
        }
        else
        {
            cameraScript.StopShake();
        }
    }
}
