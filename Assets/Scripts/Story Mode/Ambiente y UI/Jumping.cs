using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float amplitude1 = 0.3f;
    public float amplitude2 = 0.02f;
    public float frequency = 0.1f;
    public float defaultScale = 0.05f;
    [SerializeField]Vector3 posOrigin = new Vector3();
    Vector3 tempPos = new Vector3();
    public GameObject fatherAgent;
    // Start is called before the first frame update
    void Start()
    {
        posOrigin = fatherAgent.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        tempPos = new Vector3(fatherAgent.transform.position.x, posOrigin.y, fatherAgent.transform.position.z);
        tempPos.y = amplitude1 * Mathf.Sin((float)Time.fixedTime * (float)Mathf.PI * frequency) + .3f;
        transform.position = tempPos;
        transform.localScale = new Vector3((-Mathf.Sin((float)Time.fixedTime * (float)Mathf.PI * frequency)* amplitude2 + defaultScale), 
            (Mathf.Sin((float)Time.fixedTime * (float)Mathf.PI * frequency)* amplitude2 + defaultScale), 
            (-Mathf.Sin((float)Time.fixedTime * (float)Mathf.PI * frequency)* amplitude2 + defaultScale));
    }
}
