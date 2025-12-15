using UnityEngine;
using UnityEngine.UI;

public class SpeedCounter : MonoBehaviour
{
    public Rigidbody rb;
    public Text speedCounter;
    
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        speedCounter.text = "Speed " + (Mathf.Round(rb.linearVelocity.magnitude*10)/10).ToString();
    }
}
