using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIWrapper
{
    public static LevelName SpawnLevelName(string name){
        var level = Resources.Load<LevelName>("Prefabs/UI/LevelName");
        var l = Object.Instantiate(level);
        l.Initialize(name);
        return l;
    }
    public static InformationUI SpawnInfo(string name){
        var level = Resources.Load<InformationUI>("Prefabs/UI/Info");
        var l = Object.Instantiate(level);
        l.Initialize(name);
        return l;
    }
}
