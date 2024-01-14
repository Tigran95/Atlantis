using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Siccity.GLTFUtility;
public static class LocalDataHandler
{
    public static bool DataExists(string directory, string nameOfData)
    {
        FileInfo info = new FileInfo(nameOfData);
        return File.Exists(directory + "/" + info.Name);
    }
    public static void DeleteData(string directory, string nameOfData)
    {
        FileInfo info = new FileInfo(nameOfData);
        File.Delete(directory + "/" + info.Name);
    }
    public static Texture2D GetTargetImage(string directory, string nameOfData)
    {
        FileInfo info = new FileInfo(nameOfData);
        byte[] imageData = File.ReadAllBytes(directory + "/" + info.Name);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageData);
        return tex;
    }
    
    public static void SaveTargetImage(string directory, string nameOfData,Texture2D tex)
    {
        byte[] imageData;
        FileInfo info = new FileInfo(nameOfData);
        Debug.Log(info.Extension);
        switch (info.Extension)
        {
            case ".png": imageData = tex.EncodeToPNG(); break;
            case ".jpg": imageData = tex.EncodeToJPG(); break;
            default: imageData = tex.EncodeToPNG(); break;
        }
        File.WriteAllBytes(directory + "/" + info.Name, imageData);
    }
    public static void SaveModel(string directory, string nameOfData,byte[] data)
    {
        FileInfo info = new FileInfo(nameOfData);
        File.WriteAllBytes(directory + "/" + info.Name, data);
    }
    public static GameObject GetModel(string directory, string nameOfData)
    {
        FileInfo info = new FileInfo(nameOfData);
        GameObject obj = Importer.LoadFromFile(directory + "/" + info.Name);
        return obj;
    }
    public static string GetFileName(string nameOfData)
    {
        FileInfo info = new FileInfo(nameOfData);
        return info.Name;
    }
}
