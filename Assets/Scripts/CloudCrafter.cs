using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40;

    //num of clouds to make
    public GameObject cloudPrefab;

    //prefab for clouds
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);

    //max and min for cloud
    public float cloudScaleMin = -1;
    public float cloudScaleMax = 3;

    //cloud speed
    public float cloudSpeedMult = 0.05f;

    private GameObject[] cloudInstances;

    void Awake()
    {
        //array to hold clouds
        cloudInstances = new GameObject[numClouds];

        //find CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");

        //iterate and set clouds in array
        GameObject cloud;
        for (int i= 0; i<numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);

            //set position of cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            //scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            //small clouds far away and transforms cloud
            cPos.z = 100 - 90 * scaleU;
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            //makes cloud child of anchor and add to array
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //iterate for each cloud
        foreach(GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            //larger clouds move faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            //if cloud is too far left move to the right
            if(cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }

            //apply new pos for cloud
            cloud.transform.position = cPos;
        }
    }
}
