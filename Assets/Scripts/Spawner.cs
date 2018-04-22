using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public UnityEngine.Object entityToSpawn;
    [SerializeField]
    private float interval = 15f;
    public float Interval
    {
        get
        {
            return interval;
        }
        set
        {
            interval = value;
            if (isTheSyndra)
            {
                GameObject.Find("SpawnText").GetComponent<TextMesh>().text = "Syndra spawns every \n" + interval.ToString("N1") + " seconds";
            }
        }
    }
    public bool returnObject = true;
    public bool isTheSyndra = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (returnObject)
            {
                Instantiate(entityToSpawn, Randomify(gameObject.transform.position, -2, -3), new Quaternion(0, 180, 0, 0));
            }
            else
            {
                Instantiate(entityToSpawn, Randomify(gameObject.transform.position, -2, -3), new Quaternion());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    private Vector3 Randomify(Vector3 source, float minRange, float maxRange, bool keepY = true)
    {
        return new Vector3(source.x + Random.Range(minRange, maxRange), keepY ? source.y : source.y + Random.Range(minRange, maxRange), source.z + Random.Range(minRange, maxRange));
    }
}
