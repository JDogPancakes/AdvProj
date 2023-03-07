using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Scriptable Object", menuName = "ScriptableObjects/Dungeon")]
public class Dungeon : ScriptableObject
{
    public string dungeonName;
    public int numRooms;
    public GameObject[] tiles;
    public GameObject bossTile;

}
