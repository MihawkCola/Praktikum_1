using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterControl : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private SphereCollider sphereCollider;

    private Vector3 spawnPoint;
    private Rigidbody rigidbody;
    public float movementSpeed = 1.0f;
    public float fallSpeed = 2.0f;
    private Vector3 movment;

    private bool isGround = false;
    private bool isSphereHit = false;
    private RaycastHit hit;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    
        /*RaycastHit hit;
        isfall = true;
        if (Physics.SphereCast(transform.position +sphereCollider.center, sphereCollider.radius, Vector3.down, out hit, 10))
        {
            Debug.Log("Posiotion: " + sphereCollider.transform.position);
            Debug.Log("SPERECAST: " + hit.distance);
            isfall = hit.distance > 0.1f;
        }
        if (isfall) transform.position += Vector3.up * -fallSpeed * Time.deltaTime;

        if (0 == Input.GetAxis("Vertical") && 0 == Input.GetAxis("Horizontal")) return;

        float rotation = 0.0f;
        if (0 > Input.GetAxis("Horizontal")) rotation = 270.0f;
        if (0 < Input.GetAxis("Horizontal")) rotation = 90.0f;
        if (0 > Input.GetAxis("Vertical")) rotation = 180.0f;

        if (0 != Input.GetAxis("Vertical")  && 0 < Input.GetAxis("Horizontal")) rotation = rotation - 45.0f;
        if (0 != Input.GetAxis("Vertical")  && 0 > Input.GetAxis("Horizontal")) rotation = rotation + 45.0f;*/



        //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));

        // es gab bei cam.forward das problem, da die rotation der x achse der Camera sich der y Wert ändert, dass spiegelt sich auch bei der normierung ab
        movment = Vector3.zero;
        //############## Gravitation des Character  ##############
        float falldistance = fallSpeed * Time.deltaTime;
        //if (isGround && -falldistance > hit.distance) {
            //falldistance = -hit.distance + 0.01f;
            //transform.position += Vector3.up * 
        //}
       // Debug.Log("FallDistance: " + falldistance);
       // Debug.Log("Hit: " + hit.distance);
        isGround = false;
        isSphereHit = Physics.SphereCast(transform.position + sphereCollider.center, sphereCollider.radius, Vector3.down, out hit, 10);
        if (isSphereHit)
        {
            Debug.Log("Hit: " + hit.distance);
            isGround = hit.distance <= 1.1f+ sphereCollider.radius;
            if (isGround) {
                falldistance = 0.0f;
                //transform.position = transform.position + sphereCollider.center + Vector3.down * (sphereCollider.radius + 1.1f);
            }
        }
        animator.SetBool("Grounded", isGround);
       // Debug.Log("IsGround: " + isGround);
        movment += Vector3.down * falldistance;
     

        //############## Bewegung des Carakter aus der sicht der Camera ##############
        //Quelle: https://stackoverflow.com/questions/41464037/move-toward-transform-forward-ignoring-rotation/41464722
        //Get Forward face
        Vector3 forward = cam.forward;
        //Reset/Ignore y axis
        forward.y = 0;
        forward.Normalize();

        movment += forward * (Input.GetAxis("Vertical") * movementSpeed) + cam.right * (Input.GetAxis("Horizontal") * movementSpeed);
        animator.SetFloat("Speed", 0.0f);
        //############## Rotieren vom Movment ##############
        if (movment.x != 0.0f || movment.z != 0.0f)
        {
            Vector3 rotation = movment;
            rotation.y = 0;
            transform.rotation = Quaternion.LookRotation(rotation.normalized);
            animator.SetFloat("Speed", 1.0f);
        }

        //############## aktualisiere bewegung  ##############
        transform.position += movment;
    }
    public void resetPlayer()
    {
        transform.position = spawnPoint;
    } 
}
