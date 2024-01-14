using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class LocalPathsAPI
{
    private static readonly string _basePath;

    static LocalPathsAPI()
    {
        _basePath = Application.persistentDataPath;
    }
    public static string TargetImageDirectory
    {
        get
        {
            string dir = _basePath + "/TargetImagesDirectory";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
    public static string ModelDirectory
    {
        get
        {
            string dir = _basePath + "/TargetModelsDirectory";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
