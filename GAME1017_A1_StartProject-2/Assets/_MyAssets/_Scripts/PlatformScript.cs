using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
	[SerializeField] Transform startPos;
	[SerializeField] Transform endPos;
	[SerializeField] float oscillationSpeed = 1.0f;

    void Update()
	{
        // Calculate the cosine wave between -1 and 1 over time
        float oscillationFactor = Mathf.Cos(Time.time * oscillationSpeed * 2.0f * Mathf.PI);

        // Map the cosine wave from the range [-1, 1] to [0, 1]
        float weight = (oscillationFactor * 0.5f) + 0.5f;

        // Interpolate between startPos and endPos based on the calculated weight
        Vector3 newPosition = Vector3.Lerp(endPos.position, startPos.position, weight);

        // Set the platform's position to the interpolated value
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}