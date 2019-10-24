using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapScroll : MonoBehaviour
{
    float mScrollSpeed = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(Vector3.left * Time.fixedDeltaTime * mScrollSpeed);
    }

}
