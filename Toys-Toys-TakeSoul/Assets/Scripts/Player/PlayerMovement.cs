using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables outside of function so we can use them anywhere in this script
    public float speed = 6f;
    Vector3 movement;
    Animator anim; // refrence to the animation component
    Rigidbody playerRigidbody; // reference to the rigidbody
    int floorMask;  // floor quad. thats the thing we want to raycast into, use a layer mask stored as an integer to tell ray cast to cast to the floor
    float camRayLength = 100f;  // length of the ray we cast from the camera
    //Awake and fixed update are monobehavior function, automaticly called by unity
    void Awake()  // called weather the script is enabled or not
    {
        // set up the references
        floorMask = LayerMask.GetMask ("Floor");  // 
        anim = GetComponent<Animator>(); // denotes the type we are looking for
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()  // function unity auto calls, fires every physics update, normal update = rendering, fixed update = physics
    {
        float h = Input.GetAxisRaw("Horizontal"); // w and s keya s well as your up and down arrows
        //input from horizontal and vertical axis, the raw input not the standard input, minus one zero or one -> snap to full speed
        //an axie is actually input
        float v = Input.GetAxisRaw("Vertical");
        // fixed update so they are called ever rigid body movement
        Move(h, v);
        Turning();
        Animating(h, v);
    }
    //split up the operations of this player movement script
    //put them in seperate functions to make them moduler
    void Move (float h, float v)
    {
        movement.Set(h, 0f, v); //x and z are flat along the ground. lateral movement in the game

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()  // no parameters for this one
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating (float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
