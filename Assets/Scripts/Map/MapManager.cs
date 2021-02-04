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
    public void HideAllMaps()
    {
        foreach (Map map in mapList)
        {
            map.gameObject.SetActive(false);
        }
    }
    public void ShowMap(Map map)
    {
            map.gameObject.SetActive(true);
    }
    public void HidePreviousMap()
    {
        int targetMap = currentMap - 1;
        if (targetMap >= 0)
        {
            mapList[targetMap].gameObject.SetActive(false);
        }
    }
}
