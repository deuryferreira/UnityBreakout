using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public abstract class PuntajePersistente : ScriptableObject
{
    public void SaveScore(string fileName = null)
    {
        var bf = new BinaryFormatter();
        var file = File.Create(GetFilePath(fileName));
        var json = JsonUtility.ToJson(this);


        bf.Serialize(file, json);

        //Siempre hay que cerrar un archivo binario luego de usarlo, sino se corrompe
        file.Close();
    }


    public virtual void LoadScore(string fileName = null)
    {
        if (File.Exists(GetFilePath(fileName)))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(GetFilePath(fileName), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), this);
            file.Close();
        }
    }

    public string GetFilePath(string fileName = null)
    {

        var fullFileName = string.IsNullOrEmpty(fileName) ? name : fileName;
        //Use una extension personalizada jeje
        return string.Format("{0}/{1}.dfgame", Application.persistentDataPath, fullFileName);
    }
}
