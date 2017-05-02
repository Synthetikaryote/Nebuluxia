﻿using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public Vector2 p, v;
    float thrust = 1000f;
    float angle = 0f;
    float rotationSpeed = 720f; // deg/sec

	// Use this for initialization
	void Start () {
        p = Vector2.zero;
	}

    void FixedUpdate()
    {
        var screenMiddle = new Vector2(Screen.width, Screen.height) * 0.5f;
        var mouseV = Input.GetMouseButton(0) ? (Vector2)Input.mousePosition - screenMiddle : Vector2.zero;
        mouseV.Normalize();
        var keyboardV = Vector2.zero;
        keyboardV.x = (Input.GetKey(KeyCode.O) ? -1f : 0f) + (Input.GetKey(KeyCode.U) ? 1f : 0f);
        keyboardV.y = (Input.GetKey(KeyCode.E) ? -1f : 0f) + (Input.GetKey(KeyCode.Period) ? 1f : 0f);
        keyboardV.Normalize();
        var targetV = mouseV + keyboardV;
        targetV.Normalize();
        if (targetV.sqrMagnitude > 0f)
        {
            var normalizedV = v.normalized;
            var targetDirection = (-normalizedV * 0.9f + targetV).normalized;
            Debug.Log("targetDirection: " + targetDirection);
            var targetA = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            Debug.Log("targetA: " + targetA);
            angle = Mathf.MoveTowardsAngle(angle, targetA, rotationSpeed * Time.deltaTime);
            var euler = transform.localEulerAngles;
            euler.z = angle - 90f;
            transform.localEulerAngles = euler;
            var heading = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            v += heading * Time.deltaTime * thrust;
        }
        p += v * Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
        Main.Instance.bg.rectTransform.anchoredPosition = -p;
	}
}
