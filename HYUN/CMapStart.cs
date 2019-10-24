using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapStart : MonoBehaviour
{
    List<GameObject> mMapList = new List<GameObject>();

    Queue<GameObject> mMapSpawnedList = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
        Object[] tPFMap = null;
        tPFMap = Resources.LoadAll("Prefabs/Map");

        foreach (var maps in tPFMap)
        {
            GameObject tMap = maps as GameObject;
            mMapList.Add(tMap);
        }


        mMapSpawnedList.Enqueue(Instantiate<GameObject>(mMapList[0], Vector3.right * 25f, Quaternion.identity));

        for (int ti = 1; ti < 3; ti++) {
            int r = Random.Range(0, mMapList.Count);
            float tX = 200 * ti + 25;
            mMapSpawnedList.Enqueue(Instantiate<GameObject>(mMapList[r], Vector3.right * tX, Quaternion.identity));
        }
        StartCoroutine(UpdateMap());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateMap()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(7f);

            Destroy(mMapSpawnedList.Dequeue().gameObject);

            int r = Random.Range(0, mMapList.Count);

            mMapSpawnedList.Enqueue(Instantiate<GameObject>(mMapList[r], new Vector3(mMapSpawnedList.Peek().transform.position.x +400f,0f,0f), Quaternion.identity));

            Debug.Log("<color='blue'>Spawned Block</color>");

        }
    }
}
