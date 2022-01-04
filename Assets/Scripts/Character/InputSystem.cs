using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]

public class InputSystem : MonoBehaviour
{
    Movement moveScripts;
    
    [System.Serializable]
    
    public class InputSettings
    {
        public string forwardInput = "Vertical";
        public string strafeInput = "Horizontal";
        public string sprintInput = "Sprint";
        public string aim = "Fire2";
        public string fire = "Fire1";
    }
    [SerializeField]
    public InputSettings input;

    [Header("Camera & Character Syncing")]
    public float lookDistance = 5;
    public float lookSpeed = 5;

    [Header("Aiming Settings")]
    RaycastHit hit;
    public LayerMask aimLayers;
    Ray ray;

    [Header("Spine Settings")]
    public Transform spine;
    public Vector3 spineOffset;

    [Header("Head Rotation Settings")]
    public float lookAtPoint = 2.8f;
    
    Transform camCenter;
    Transform mainCam;

    public Bow bowScripts;
    bool isAiming;

    public bool testAim;

    bool hitDetected;
    public int damageAmount = 10;

    Animator playerAnim;
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        moveScripts = GetComponent<Movement>();
        camCenter = Camera.main.transform.parent;
        mainCam = Camera.main.transform;
        playerAnim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        bowScripts.bowSettings.arrowDisplay.text = bowScripts.bowSettings.arrowCount.ToString();
        if (Input.GetAxis(input.forwardInput) != 0 || Input.GetAxis(input.strafeInput) != 0)
            RotateToCamView();
        
        isAiming = Input.GetButton(input.aim);
        
        if (testAim)
            isAiming = true;

        if (bowScripts.bowSettings.arrowCount < 1)
            isAiming = false;

        moveScripts.AnimateCharacter(Input.GetAxis(input.forwardInput), Input.GetAxis(input.strafeInput));
        moveScripts.SprintCharacter(Input.GetButton(input.sprintInput));
        moveScripts.CharacterAim(isAiming);

        if (isAiming) 
        {
            Aim();
            bowScripts.EquipBow();

            
            if (bowScripts.bowSettings.arrowCount > 0)
                moveScripts.CharacterPullString(Input.GetButton(input.fire));

            
            
            if (Input.GetButtonUp(input.fire))
            {
                
                moveScripts.CharacterFireArrow();
                if (hitDetected)
                {
                    bowScripts.Fire(hit.point);
                    EnemyScript e = hit.transform.GetComponent<EnemyScript>();
                    if (e != null)
                    {
                        e.TakeDamage(damageAmount);
                        return;
                    }
                }
                
                
                else
                {
                    bowScripts.Fire(ray.GetPoint(300f));
                }
            }
                
        }
        else
        {
            bowScripts.UnEquipBow();
            bowScripts.RemoveCrosshair();
            DisableArrow();
            Release();
        }
           
    }

    void LateUpdate()
    {
        if (isAiming)
            RotateCharacterSpine();
    }

    void RotateToCamView()
    {
        Vector3 camCenterPos = camCenter.position;

        Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDistance);
        Vector3 direction = lookPoint - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        Quaternion finalRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        transform.rotation = finalRotation;
    }
    
    //Does the aiming and sends a raycast to a target
    void Aim()
    {
        hitDetected = true;
        Vector3 camPosition = mainCam.position;
        Vector3 dir = mainCam.forward;

        ray = new Ray(camPosition, dir);
        if(Physics.Raycast(ray, out hit, 500f, aimLayers))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            bowScripts.ShowCrossHair(hit.point);
        }
        else
        {
            hitDetected = false;
            bowScripts.RemoveCrosshair();
        }
    }

    void RotateCharacterSpine()
    {
        RotateToCamView();
        spine.LookAt(ray.GetPoint(50));
        spine.Rotate(spineOffset);
    }

    public void Pull()
    {
        bowScripts.PullString();

    }

    public void EnableArrow()
    {
        bowScripts.PickArrow();
    }

    public void DisableArrow()
    {
        bowScripts.DisableArrow();
    }

    public void Release()
    {
        bowScripts.ReleaseString();
    }
    public void PlayPullSound()
    {
        bowScripts.PullAudio();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isAiming)
        {
            playerAnim.SetLookAtWeight(1f);
            playerAnim.SetLookAtPosition(ray.GetPoint(lookAtPoint));
        }
        else
        {
            playerAnim.SetLookAtWeight(0);
        }
    }
}
