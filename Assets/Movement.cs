using UnityEngine;


public class Movement : MonoBehaviour
{
    public CharacterController hitBox;
    public Camera cameraObject;
    public Vector2 moveInput;
    public Vector2 camInput;
    public Vector2 camSense;
    public float moveSpeed = 5f;
    public float rotationX;

    public float velocityY;
    public float g= -1f;
    public float jumpForse = 1f;

    public LayerMask groundLayer;
    public bool isGrounded;


    void Gravity_FixedUpdate()
    {
        if (isGrounded)
        {
            if (velocityY<-1f) velocityY = -1f;// Reset velocityY to -1f when grounded
        }
        
        velocityY += g * Time.fixedDeltaTime;
        Vector3 move = new Vector3(0, velocityY, 0);
        hitBox.Move(move * Time.fixedDeltaTime);
    }



    void IsGrounded_FixedUpdate()
    {
        float sphereRadius = 0.1f;
        Vector3 spherePosition = hitBox.transform.position;
        isGrounded = Physics.CheckSphere(spherePosition, sphereRadius, groundLayer);
    }
    void OnDrawGizmos()
    {
        if (hitBox != null)
        {
            Gizmos.color = Color.red;
            float sphereRadius = 0.1f;
            Vector3 spherePosition = hitBox.transform.position;
            Gizmos.DrawWireSphere(spherePosition, sphereRadius);
        }
    }




    void ReadInput_Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        camInput.x = Input.GetAxis("Mouse X");
        camInput.y = Input.GetAxis("Mouse Y");
        if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocityY = jumpForse;
            }
    }
    //0.02ss
    void Move_FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = hitBox.transform.TransformDirection(move);
        hitBox.Move(move * Time.fixedDeltaTime * moveSpeed);
    }
    void Rotate_FixedUpdate()
    {
        Vector3 rotate = new Vector3(0, camInput.x, 0);
        hitBox.transform.Rotate(rotate * Time.fixedDeltaTime * camSense.x);
    }
    
    void RotateCam_FixedUpdate()
    {
        rotationX -= camInput.y * camSense.y* Time.fixedDeltaTime;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Vector3 rotate = new Vector3(rotationX, 0, 0);
        cameraObject.transform.localEulerAngles = rotate;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        print("Mouse Locked by Movement");
    }

    
    void Update()
    {
    
        ReadInput_Update();
       
    }

    void FixedUpdate()
    {
     
      IsGrounded_FixedUpdate();
      Gravity_FixedUpdate();
      Move_FixedUpdate();
      Rotate_FixedUpdate();
      RotateCam_FixedUpdate();
    
    }
}
 