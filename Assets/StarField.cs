using UnityEngine;
using System.Collections.Generic;

public class StarField : MonoBehaviour {
    public GameObject starPrefab;

    int numStars = 100;
    List<RectTransform> stars;

	// Use this for initialization
	void Start () {
	    for (int i = 0; i < numStars; ++i)
        {
            var go = GameObject.Instantiate(starPrefab);
            var star = go.GetComponent<Star>();
            star.Setup(transform, GetComponentInParent<Canvas>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
