using UnityEngine;

public class groundColliderScript : MonoBehaviour
{
    public bool PlayerTouchesGround = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
            if (other.gameObject.layer == 3){
                PlayerTouchesGround = true;
            }
    }

    private void OnTriggerExit(Collider other) {
            if (other.gameObject.layer == 3){
                PlayerTouchesGround = false;
            }
    }

}
