using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] propPrefabs;
    private BoxCollider area;
    public int count = 100; // 100개의 prop들을 찍어낼 것
    // prop을 재생성 하지 않고 껐다가 다시 키는 방식, 위치만 섞어준다.
    private List<GameObject> props = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();

        for(int i = 0;i<count;i++)
        {
            Spawn();
        }

        area.enabled = false; //collider들을 생성하고 난 후, 이 box collider를 남기면 ball과 충돌 할 수 있기 때문에 없애준다.
    }

    private void Spawn()
    {
        int selectipn = Random.Range(0, propPrefabs.Length);

        GameObject selectedPrefab = propPrefabs[selectipn];

        Vector3 spawnpos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, spawnpos,Quaternion.identity);

        props.Add(instance);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x/2f, size.x/2f);

        float posY = basePosition.y + Random.Range(-size.y/2f, size.y/2f);

        float posZ = basePosition.z + Random.Range(-size.z/2f, size.z/2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }
    
    public void Reset()
    {
        for(int i =0; i < props.Count; i++)
        {
            props[i].transform.position = GetRandomPosition();
            props[i].SetActive(true); // 혹시 꺼져있을지 모르는 오브젝트 켜줌
        }
    }
}
