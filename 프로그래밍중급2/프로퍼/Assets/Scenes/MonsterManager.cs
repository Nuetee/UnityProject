using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //set을 만들지 않음으로 인해서 외부에서 값을 바꾸지 못하도록 한다
    public int count{
        get{
            Monster[] monsters = FindObjectsOfType<Monster>();

            return monsters.Length;
        }
    }
}
