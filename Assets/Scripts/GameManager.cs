using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //for Debuging

public class GameManager : MonoBehaviour
{
    private aboutCamera cameraScript;

    public bool isLive;
    public float weaponDamage;
    public List<Weapon> availableWeapons = new List<Weapon>();
    public List<Weapon> selectedWeapons = new List<Weapon>();

    public static GameManager Instance;

    [Header("#Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public bool isLvUp = false;

    [Header("#Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};

    [Header("#Game Object")]
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

    public void GetEXP()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
            isLvUp = true;
            WhenLevelUp();
        }
    }

    public List<Weapon> GetRandomWeapons()
    {
        List<Weapon> randomWeapons = new List<Weapon>();

        while (randomWeapons.Count < 3)
        {
            Weapon randomWeapon = availableWeapons[Random.Range(0, availableWeapons.Count)];

            if (!randomWeapons.Contains(randomWeapon))
            {
                randomWeapons.Add(randomWeapon);
            }
        }
        
        selectedWeapons = randomWeapons;
        Debug.Log("Random Weapons: " + string.Join(", ", randomWeapons.Select(w => w.name)));
        return randomWeapons;
    }

    public void WhenLevelUp()
    {
        List<Weapon> randomWeapons = GetRandomWeapons();

        int playerChoice = GetPlayerChoice();

        EnhanceWeapon(randomWeapons[playerChoice]);
    }

    private int GetPlayerChoice()
    {
        // 플레이어의 입력을 받아 선택한 무기의 인덱스를 반환
        // 이 부분은 키보드 입력, 마우스 입력 또는 터치 등을 사용하여 구현할 수 있습니다.
        // 예를 들면, 1, 2, 3 키 중 하나를 누르도록 하는 방법이 있습니다.
        int playerChoice = 0; // 일단은 0으로 초기화
        // 여기에 입력을 받는 로직을 추가하세요.
        return playerChoice;
    }

    private void EnhanceWeapon(Weapon weapon)
    {
        // 선택한 무기를 강화하는 로직을 구현
        // 이 부분에서는 무기의 특성(공격력 등)을 증가시키는 등의 작업을 수행합니다.
    }
}

[System.Serializable]
public class Weapon
{
    public string name;
    public float damage;
    public int maxAmmo;
    public GunAttributes attributes;
}