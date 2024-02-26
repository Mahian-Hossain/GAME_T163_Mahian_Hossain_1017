using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;
    [SerializeField] float moveSpeed;
    private float[] startPositions = { 0f, 13.312f }; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var background in backgrounds)
        {
            background.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (backgrounds[0].transform.position.x <= -13.312f)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].transform.position = new Vector3(startPositions[i], 0f, 0f);
            }
        }
    }
}
