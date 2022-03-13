using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    #region singleton
    public static Pooler instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private Pool[] pools;
    private Dictionary<string, Queue<GameObject>> poolDico;

    private void Start()
    {
        poolDico = new Dictionary<string, Queue<GameObject>>();
    }

    public void InitPools()
    {
        foreach(Pool p in pools)
        {
            Queue<GameObject> newPool = new Queue<GameObject>();

            int i = 0;
            while (i < p.capacity)
            {
                var obj = Instantiate(p.prefab);
                obj.SetActive(false);
                newPool.Enqueue(obj);
                i++;
            }

            poolDico.Add(p.id, newPool);
        }
    }

    public GameObject Spawn(string id, Vector3 position, Quaternion rotation)
    {
        /*if (!poolDico.ContainsKey(tag))
            return null;*/

        var obj = poolDico[id].Dequeue();

        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);

        poolDico[id].Enqueue(obj);

        return obj;
    }

    #region Class
    [System.Serializable]
    public class Pool
    {
        public string id;
        public GameObject prefab;
        public int capacity;
    }
    #endregion
}
