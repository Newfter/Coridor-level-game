using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GoIntoCar : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera, carPanel, goInText, zKT;
    public bool inCar;
    public CarController currentCar;

    private void Start()
    {
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
       currentCar.camera.SetActive(true); 
       carPanel.SetActive(true);
       inCar = true;
       FindAnyObjectByType<WeaponTouch>().bulletPanel.SetActive(false);
       FindAnyObjectByType<InstGrenata>().grenadePanel.SetActive(false);
       zKT.SetActive(false);
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
        if(FindAnyObjectByType<WeaponTouch>().haveGun) { FindAnyObjectByType<WeaponTouch>().bulletPanel.SetActive(true); }
        zKT.SetActive(true);
        if (FindAnyObjectByType<InstGrenata>().haveGrenade) { FindAnyObjectByType<InstGrenata>().grenadePanel.SetActive(true); }
        
        gameObject.transform.position = currentCar.spawn.position;
        currentCar.playerInCar = false;
        currentCar = null;
        inCar = false;
    }
}
