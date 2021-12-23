using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPos;
    const float tau = 2 * Mathf.PI;
    float cycles;
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movementVec;
    [SerializeField] float movementFactor;
    
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        cycles = Time.time / period;
        float sineWave = Mathf.Sin(cycles * tau);

        movementFactor = (sineWave + 1) / 2f;
        
        Vector3 offset = movementVec * movementFactor;
        transform.position = startPos + offset;
    }
}
