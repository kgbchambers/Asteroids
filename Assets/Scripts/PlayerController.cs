using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public static PlayerController playerControllerInstance;

    public GameObject projectilePref;
    public List<GameObject> projectilesList;

    private PlayerInput playerInputActions;
    private float playerRotation = 0;
    private Rigidbody rb;
    private float playerThrustActivated;

    public float thrustForce;
    public float rotationSpeed;


    private void Awake()
    {
        playerInputActions = new PlayerInput();
        playerInputActions.Enable();
        if (playerControllerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            playerControllerInstance = this;
        }
    }

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {

        //read directional player input and apply rotation based on input.
        playerRotation = playerInputActions.Player.Rotate.ReadValue<float>();
        transform.Rotate(transform.forward * playerRotation * rotationSpeed);

        //read forward input and apply force to the objects rigidbody
        //Note - Mass and drag on rigidbody component affect movement.
        playerThrustActivated = playerInputActions.Player.Thruster.ReadValue<float>();
        rb.AddForce(transform.up * playerThrustActivated * thrustForce);

    }


    //read player input for Spacebar(shoot), evoke player event using Input System
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Spawn projectile and shoot it
            //Manage projectile instance
            //Delete when projectile collides with object
            GameObject projectileInstance = Instantiate(projectilePref, new Vector3(transform.position.x, transform.position.y, transform.position.z), this.transform.rotation);
            projectilesList.Add(projectileInstance);
        }
    }


    public void DestroyProjectile(GameObject projectile)
    {
        projectilesList.Remove(projectile);
        Destroy(projectile);
    }

}
