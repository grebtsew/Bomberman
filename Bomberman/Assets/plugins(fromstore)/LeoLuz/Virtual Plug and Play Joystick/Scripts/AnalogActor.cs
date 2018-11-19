using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnalogActor : MonoBehaviour {
    RectTransform rectTramsform;
    public float Deslocaton = 20f;
    public Vector3 InitialPosition;
    public float speed=10f;
    public Vector3 initialScale;
    public Vector3 ScaleSmear = new Vector3(1.5f, 0.7f, 0f);
    Vector3 oldPosition;
    public float _MaxVelocitySmear = 0.2f;
    public float MinvelocitySmear;
  //public float MaxXScale;
    // Use this for initialization
    void Start () {
        rectTramsform = GetComponent<RectTransform>();
        InitialPosition = rectTramsform.anchoredPosition3D;
        initialScale = rectTramsform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x > 0.05f)
            x = 1;
        if (x < -0.05f)
            x = -1;
        if (y > 0.05f)
            y = 1f;
        if (y < -0.05f)
            y = -1f;
        rectTramsform.anchoredPosition3D = Vector3.Lerp(rectTramsform.anchoredPosition3D,(InitialPosition + (new Vector3(x * Deslocaton, y * Deslocaton, 0f))), speed * Time.deltaTime);
        Vector3 diff = transform.position - oldPosition;
        //print(diff.magnitude);
        if (diff.magnitude > 0.001f)
        {
            float factor = Mathf.Clamp(diff.magnitude / _MaxVelocitySmear, 0f, 1f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Lerp(initialScale, ScaleSmear, factor),speed*Time.deltaTime);
            //   print("diff magni" + diff.magnitude);


        transform.rotation = Quaternion.FromToRotation(Vector3.right, new Vector3(diff.x, diff.y, 0f).normalized);
        } else
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = initialScale;
        }
        oldPosition = transform.position;
    }
}
