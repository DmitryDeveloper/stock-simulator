using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //for seriliazation

public class MainManager : MonoBehaviour
{
    //Note the keyword static after the keyword public.
    //This keyword means that the values stored in this class member will be shared by all the instances of that class.
    //(f.e. if we have not singletone then all objects will share this value between each)
    public static MainManager Instance { get; private set; } //accessable from another classes but restricted to be set

    public Color TeamColor; //will pass color between scenes

    private void Awake()
    {
        // we do it as singletone to prevent creating new instance between scene transitions
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //object will not destroyed between scenes
        DontDestroyOnLoad(gameObject);

        //fetch saved color from file during initialization of game
        LoadColor();
    }

    [System.Serializable] //it's needed for JsonUtility
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);
    
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}
