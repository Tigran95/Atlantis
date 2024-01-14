using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MobileAPI 
{
    private static readonly string _targetImageURL;
    private static readonly string _targetModelURL;

    static MobileAPI()
    {
        _targetImageURL = "https://user74522.clients-cdnnow.ru/static/uploads/mrk6440mark.png";
        _targetModelURL = "https://user74522.clients-cdnnow.ru/static/uploads/mrk6564obj.glb";
    }

   

    public static string GetTargetImageURL => _targetImageURL;
    public static string GetTargetModelURL => _targetModelURL;
}
