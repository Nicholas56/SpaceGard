using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public SaveData CreateSave()
    {
        SaveData data = new SaveData();
        GameObject player = FindObjectOfType<PlayerShip>().gameObject;
        data.playerPos = player.transform.position;
        data.playerRot = player.transform.rotation;

        EnemyShip[] enemies = FindObjectsOfType<EnemyShip>();
        foreach(EnemyShip enemy in enemies)
        {
            data.enemyPos.Add(enemy.transform.position);
            data.enemyRot.Add(enemy.transform.rotation);
        }
        OrbitMotion[] orbits = FindObjectsOfType<OrbitMotion>();
        foreach(OrbitMotion orbit in orbits)
        {
            data.planetOrbits.Add(orbit.orbitProgress);
        }

        return data;
    }
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(CreateSave());

        File.WriteAllText(Application.persistentDataPath + "/Spacegard.save", jsonString);
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Spacegard.save"))
        {
            string jsonString = File.ReadAllText(Application.persistentDataPath + "/Spacegard.save");
            SaveData data = JsonUtility.FromJson<SaveData>(jsonString);

            GameObject player = FindObjectOfType<PlayerShip>().gameObject;
            player.transform.position = data.playerPos;
            player.transform.rotation = data.playerRot;

            EnemyShip[] enemies = FindObjectsOfType<EnemyShip>();
            foreach (EnemyShip enemy in enemies)
            {
                enemy.Death();
            }
            for (int i=0;i<data.enemyPos.Count;i++)
            {
                GameObject enemy = pool.GetObject("Enemy");
                enemy.transform.position = data.enemyPos[i];
                enemy.transform.rotation = data.enemyRot[i];
            }
            OrbitMotion[] orbits = FindObjectsOfType<OrbitMotion>();
            for (int i = 0; i < data.planetOrbits.Count; i++)
            {
                orbits[i].orbitProgress = data.planetOrbits[i];
            }
        }
    }
}

public class SaveData
{
    public Vector3 playerPos;
    public Quaternion playerRot;
    public List<Vector3> enemyPos;
    public List<Quaternion> enemyRot;

    public List<float> planetOrbits;
    public int score;

    public SaveData()
    {
        enemyPos = new List<Vector3>();
        enemyRot = new List<Quaternion>();
        planetOrbits = new List<float>();
    }
}