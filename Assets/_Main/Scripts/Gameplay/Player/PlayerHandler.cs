using UnityEngine;

/// <summary>
/// This class is responsible for character movement
/// </summary>
public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [Header("Specs Player")]
    [SerializeField]
    private float walkSpeed = 6f;
    [SerializeField]
    private float jumpSpeed = 8f;
    [SerializeField]
    private float gravity = 20f;

    [Header("Specs Camera")]
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float mouseHorizontal = 3.0f;
    [SerializeField]
    private float mouseVertical = 2.0f;
    [SerializeField]
    private float minRotation = -65f;
    [SerializeField]
    private float maxRotation = 60f;

    private float turnTime = 0.09f;
    private float turnSpeed; 

    private float hMouse;
    private float vMouse; 

    [HideInInspector]
    public Vector3 move = Vector3.zero;

    #region Unity Messages

    private void Update()
    {
        hMouse += mouseHorizontal * Input.GetAxis("Mouse X"); 
        vMouse += mouseVertical * Input.GetAxis("Mouse Y"); 

        vMouse = Mathf.Clamp(vMouse, minRotation, maxRotation);
        cam.transform.eulerAngles = new Vector3(-vMouse, hMouse, 0);

        move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")); 

        if(characterController.isGrounded && move.magnitude >= 0.1f)
        {

            //function math Two-Parameter Arctangent to get angle and multiply it to convert randians to degrees, finally adds angles in Y of the camera
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            //Smooth rotation 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, turnTime);
            //Apply new rotation 
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            move = moveDirection * walkSpeed;

            if (Input.GetKey(KeyCode.Space))
                move.y = jumpSpeed; 
        }

        move .y -= gravity * Time.deltaTime;

        characterController.Move(move * Time.deltaTime);
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods


    #endregion
}
