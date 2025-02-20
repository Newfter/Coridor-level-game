using StarterAssets;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class GoIntoCar : MonoBehaviour
{
    [SerializeField] private GameObject[] canvases;
    [SerializeField] private GameObject mainCamera, carPanel, goInText;
    [SerializeField] private AudioResource[] carAudioResources;
    [SerializeField] private AudioSource carStart;
    public bool inCar;
    public CarController currentCar;
    private WalkingSound wS;

    private void Start()
    {
        wS = FindAnyObjectByType<WalkingSound>();
        inCar = false;
        carPanel.SetActive(false);
        goInText.SetActive(false);
    }
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F) && inCar) GoOutOfcar();
        if (inCar) return;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.gameObject.CompareTag("Car"))
            {
                goInText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F)  && !inCar)
                {
                    currentCar = hitTransform.gameObject.GetComponent<CarController>();
                    currentCar.playerInCar = true;
                    GoIntocar();
                    goInText.SetActive(false);
                }
            }
            else {goInText.SetActive(false);}
        }
        else{goInText.SetActive(false);}
    }

    private void GoIntocar()
    {
       mainCamera.SetActive(false);
       gameObject.GetComponent<WeaponTouch>().enabled = false;
       gameObject.GetComponent<PlayerBullets>().enabled = false;
       gameObject.GetComponent<InstGrenata>().enabled = false;
       gameObject.GetComponent<PlayerInput>().enabled = false;
       gameObject.GetComponent<StarterAssetsInputs>().enabled = false;
       gameObject.GetComponent<FirstPersonController>().enabled = false;
       gameObject.GetComponent<BasicRigidBodyPush>().enabled = false;
       gameObject.GetComponent<CharacterController>().enabled = false;
       foreach (var i in canvases) { i.SetActive(false); }
       currentCar.camera.SetActive(true); 
       wS.SetSound(carAudioResources, carStart);
       carPanel.SetActive(true);
       inCar = true;
    }
    public void GoOutOfcar()
    {
        mainCamera.SetActive(true); 
        currentCar.camera.SetActive(false); 
        carPanel.SetActive(false);
        gameObject.GetComponent<WeaponTouch>().enabled = true;
        gameObject.GetComponent<PlayerBullets>().enabled = true;
        gameObject.GetComponent<InstGrenata>().enabled = true;
        gameObject.GetComponent<PlayerInput>().enabled = true;
        gameObject.GetComponent<StarterAssetsInputs>().enabled = true;
        gameObject.GetComponent<FirstPersonController>().enabled = true;
        gameObject.GetComponent<BasicRigidBodyPush>().enabled = true;
        gameObject.GetComponent<CharacterController>().enabled = true;
        foreach (var i in canvases) { i.SetActive(true); }
        if(FindAnyObjectByType<WeaponTouch>().haveGun) { FindAnyObjectByType<WeaponTouch>().bulletPanel.SetActive(true); }
        else{FindAnyObjectByType<WeaponTouch>().bulletPanel.SetActive(false);}
        if (FindAnyObjectByType<InstGrenata>().haveGrenade) { FindAnyObjectByType<InstGrenata>().grenadePanel.SetActive(true); }
        else { FindAnyObjectByType<InstGrenata>().grenadePanel.SetActive(true); }
        gameObject.transform.position = currentCar.spawn.position;
        currentCar.playerInCar = false;
        currentCar = null;
        inCar = false;
    }
}
