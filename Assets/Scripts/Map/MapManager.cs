using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

[RequireComponent(typeof(LevelVisualizer))]
public class MapManager : MonoBehaviour
{
    [SerializeField] Map[] mapList;
    int currentMap = 0;

    public Map GetNextMap()
    {
        currentMap++;
        return GetMap();
    }
    public Map GetMap()
    {
        if (currentMap >= mapList.Length)
        {
            return null;
        }
        else
        {
            return mapList[currentMap];
        }
    }
}
