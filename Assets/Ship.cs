using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public Vector2 p, v;
    float maxThrust = 1000f;
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
        var targetDirection = -v.normalized * 0.4f + targetV * 0.60f;
        var angleRad = angle * Mathf.Deg2Rad;
        var targetA = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.MoveTowardsAngle(angle, targetA, rotationSpeed * Time.deltaTime);
        var heading = targetDirection.normalized; // new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
        var vA = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        var thrust = maxThrust; // (1f - Mathf.Clamp01(Mathf.DeltaAngle(angle, targetA) / 90f)) * maxThrust;
        var dv = heading * Time.deltaTime * thrust;
        // if slowing down, come to a stop rather than go past
        if (targetV.sqrMagnitude < 0.1f && Mathf.DeltaAngle(targetA, vA) > 90 && v.sqrMagnitude < dv.sqrMagnitude)
            dv = dv / dv.sqrMagnitude * v.sqrMagnitude;
        v += dv;
        p += v * Time.deltaTime;

        if (dv.sqrMagnitude > 0.1f)
        {
            var euler = transform.localEulerAngles;
            euler.z = angle - 90f;
            transform.localEulerAngles = euler;
        }
    }

    // Update is called once per frame
    void Update () {
        Main.Instance.bg.rectTransform.anchoredPosition = -p;
	}
}
