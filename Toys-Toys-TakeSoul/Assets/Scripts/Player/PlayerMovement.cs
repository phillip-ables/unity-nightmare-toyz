using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim; // refrence to the animation component
    Rigidbody playerRigidbody; // reference to the rigidbody
    int floorMask;  // floor quad. thats the thing we want to raycast into, use a layer mask stored as an integer to tell ray cast to cast to the floor
    float camRayLength = 100f;  // length of the ray we cast from the camera

    void Awake()  // called weather the script is enabled or not
    {
        // set up the references
        floorMask = LayerMask.GetMask ("Floor");  // 
        anim = GetComponent<Animator>(); // denotes the type we are looking for
        playerRigidbody = GetComponent<Rigidbody>();
    }
}
