using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public delegate void Boost(Character target);
    public Boost playerBoost; // public event Boost playerBoost; => delegate가 event가 아닌 방향으로 사용되는것을 제한한다
    // subscriber가 멋대로 event를 발동시키거나 event를 덮어씌우는 행위를 방지한다. (event를 갖고 있는 publisher가 발동시켜야함. 여기서는 Character)
    // 주의깊게 짠다면 event 키워드 없이도 event를 구현할 수 있지만 안전한 코드 작성을 위해 event 권장
    public string playerName = "Jaemin";
    public float hp = 100f;
    public float defense = 50f;
    public float damage = 30f;

    void Start()
    {
        playerBoost(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerBoost(this);
        }
    }
}
