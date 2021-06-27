using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MonsterManager monsterManager;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(monsterManager.count);
    }
}
