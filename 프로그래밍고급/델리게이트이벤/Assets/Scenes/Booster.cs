using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public void HealthBoost(Character target)
    {
        Debug.Log(target.playerName + "의 체력을 강화했다");
        target.hp += 10;
    }

    public void ShieldBoost(Character target)
    {
        Debug.Log(target.playerName + "방어력을 강화했다");
        target.defense +=10;
    }

    public void DamageBoost(Character target)
    {
        Debug.Log(target.playerName + "의 공격력을 강화했다.");
        target.damage += 10;
    }

    void Awake()
    {
        Character player = FindObjectOfType<Character>();
        player.playerBoost += HealthBoost;
        player.playerBoost += ShieldBoost;
        player.playerBoost += DamageBoost;
    }
}
