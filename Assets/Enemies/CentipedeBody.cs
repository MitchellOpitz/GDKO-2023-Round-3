using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeBody : MonoBehaviour
{
    public GameObject bodySegmentPrefab; // The prefab for the body segments
    public int numSegments = 10; // The number of body segments

    private GameObject[] bodySegments; // The array of body segment game objects

    void Start()
    {
        // Create the body segments and position them relative to the head
        bodySegments = new GameObject[numSegments];
        Vector3 segmentPosition = transform.position - new Vector3(2, 0, 0);
        for (int i = 0; i < numSegments; i++)
        {
            GameObject segment = Instantiate(bodySegmentPrefab, segmentPosition, Quaternion.identity);
            bodySegments[i] = segment;
            segmentPosition += new Vector3(-2, 0, 0); // Move the segment down one unit
        }
    }
}

