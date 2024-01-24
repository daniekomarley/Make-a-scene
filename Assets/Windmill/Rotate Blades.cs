using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateBlade1 : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;
    private Vector3 rotateAbout;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotateAbout = new Vector3(1, 0, 0);
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rotation += rotateAbout * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
