using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GenerateSolarSystem : MonoBehaviour
{
    public SystemData data;
    public Material[] planetMats;
    int[] matIndex;
    public Material trailMat;

    public OrbitMotion[] planets;
    public Elipse orbitSpacing = new Elipse(15f,10f);
    public List<GameObject> asteroids;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0;
        data = new SystemData();
        //Stores the last planet number in player prefs, or creates new random number
        if (PlayerPrefs.GetInt("PlanetNum") > 0) { data.planetNum = PlayerPrefs.GetInt("PlanetNum"); }
        else { data.planetNum = Random.Range(1, 21);PlayerPrefs.SetInt("PlanetNum", data.planetNum); }

        planets = FindObjectsOfType<OrbitMotion>();
        for (int i = 1; i < planets.Length; i++)
        {
            AddTrail(planets[i].transform.GetChild(0).gameObject);
            //Sets planets after planetNum to inactive
            if (i > data.planetNum) { planets[i].orbitActive = false; }
        }
    }

    SystemData CreateSystem()
    {
        SystemData save = new SystemData();

        //loop round all the objects to grab their x,y,z positions, you could extend this easily for rotation
        for (int i = 0; i < planets.Length; i++)
        {
            save.objPos.Add(planets[i].transform.position);
            save.objRot.Add(planets[i].transform.rotation);
            save.objScale.Add(planets[i].transform.localScale);
            save.materialIndex.Add(matIndex[i]);
            save.objMass.Add(planets[i].GetComponent<Rigidbody>().mass);
            save.objVelocity.Add(planets[i].GetComponent<Rigidbody>().velocity);
        }
        //return back the save data to wherever this code is called
        return save;
    }

    public void SaveSystem()
    {
        //Create the SaveGame object using the above
        SystemData save = CreateSystem();
        //create a new string to hold our JSON
        string jsonString = JsonUtility.ToJson(save);
        //this uses the built in file handling to create a text file persistentDataPath means 
        //we always have the same path and are guaranteed a space to save 
        File.WriteAllText(Application.persistentDataPath + "/SpaceGard.save", jsonString);

        Debug.Log("Saving as JSON: " + jsonString + Application.persistentDataPath + "/SpaceGard.save");
    }

    public void LoadSystem()
    {
        //check if the save file exists could use this above to prompt
        if (File.Exists(Application.persistentDataPath + "/SpaceGard.save"))
        {
            //find the file and load it into memory
            string jsonString = File.ReadAllText(Application.persistentDataPath + "/SpaceGard.save");
            //use the JSONUtility to convert back from JSON to string
            SystemData save = JsonUtility.FromJson<SystemData>(jsonString);

            Debug.Log("Loading as JSON: " + jsonString);
            MakeNewPlanets(save.planetNum, true);

            //loop round the planets and put them back into place
            for (int i = 0; i < save.planetNum; i++)
            {
                Vector3 position = save.objPos[i];
                Quaternion rot = save.objRot[i];
                Vector3 scale = save.objScale[i];
                planets[i].transform.position = position;
                planets[i].transform.rotation = rot;
                planets[i].transform.localScale = scale;

                planets[i].GetComponent<Renderer>().material = planetMats[save.materialIndex[i]];
                planets[i].GetComponent<Rigidbody>().mass = save.objMass[i];
                planets[i].GetComponent<Rigidbody>().velocity = save.objVelocity[i];
            }

            Debug.Log(gameObject.name + " Game Loaded");
        }
    }

    public void NewGame()
    {
        Elipse newSpace = orbitSpacing;
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i].RandomizeOrbit(newSpace);
            newSpace = new Elipse(newSpace.xAxis*2,newSpace.yAxis*2);
        }
        Time.timeScale = 1;
    }

    void MakeNewPlanets(int numOfPlanets, bool loading)
    {
        //planets.Clear();
        var sphereToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rb = sphereToCopy.AddComponent<Rigidbody>();
        rb.useGravity = false;

        float planetDistance = 50f;
        for (int i = 0; i < numOfPlanets; i++)
        {
            var sp = Instantiate(sphereToCopy);
            if (!loading)
            {
                planetDistance += Random.Range(10,40);
                //This is data that could be pulled from save file
                sp.transform.position = this.transform.position + new Vector3(planetDistance, 0, 0);
                sp.transform.localScale *= Random.Range(0.5f, 2f);
                int randMat = Random.Range(0, planetMats.Length);
                matIndex = new int[numOfPlanets];
                matIndex[i] = randMat;
                sp.GetComponent<Renderer>().material = planetMats[randMat];
                sp.GetComponent<Rigidbody>().mass = Random.Range(1f, 3f);
                sp.GetComponent<Rigidbody>().velocity = Vector3.forward * 10;
            }
            AddTrail(sp);
            AddAttractor(sp);
            //planets.Add(sp);
        }
        Destroy(sphereToCopy);
    }

    void AddTrail(GameObject obj)
    {
        TrailRenderer tr = obj.AddComponent<TrailRenderer>();
        tr.time = 1f; tr.startWidth = 0.1f; tr.endWidth = 0;
        tr.material = trailMat; tr.startColor = new Color(1, 1, 0, 0.1f); tr.endColor = new Color(0, 0, 0, 0);
    }

    void AddAttractor(GameObject obj)
    {
        Attractor attr = obj.AddComponent<Attractor>();
        attr.rb = obj.GetComponent<Rigidbody>();
    }
}
