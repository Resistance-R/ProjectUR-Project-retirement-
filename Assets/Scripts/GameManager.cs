using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private aboutCamera cameraScript;

    public bool isLive;
    public float weaponDamage;

    public static GameManager Instance;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public PoolManager pool;
    public PlayerController player;

    private void Awake()
    {
        isLive = true;
        cameraScript = Camera.main.GetComponent<aboutCamera>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
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
