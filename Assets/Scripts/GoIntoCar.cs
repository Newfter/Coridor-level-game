using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoIntoCar : MonoBehaviour
{
    [SerializeField] private Transform spawnPlayerTransform;
    [SerializeField] private GameObject carCamera, mainCamera, carPanel, goInText, car;

    private void Start()
    {
        carCamera.SetActive(false); 
        carPanel.SetActive(false);
        goInText.SetActive(false);
        car.GetComponent<CarController>().enabled = false;
    }
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.gameObject.CompareTag("Car"))
            {
                goInText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.G))
                {
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
       gameObject.GetComponent<Coin>().enabled = false;
       gameObject.GetComponent<PlayerBullets>().enabled = false;
       gameObject.GetComponent<InstGrenata>().enabled = false;
       gameObject.GetComponent<PlayerInput>().enabled = false;
       gameObject.GetComponent<StarterAssetsInputs>().enabled = false;
       gameObject.GetComponent<FirstPersonController>().enabled = false;
       gameObject.GetComponent<BasicRigidBodyPush>().enabled = false;
       gameObject.GetComponent<CharacterController>().enabled = false;
       carCamera.SetActive(true); 
       carPanel.SetActive(true);
       car.GetComponent<CarController>().enabled = true;
    }
    public void GoOutOfcar()
    {
        mainCamera.SetActive(true); 
        carCamera.SetActive(false); 
        carPanel.SetActive(false);
        gameObject.GetComponent<WeaponTouch>().enabled = true;
        gameObject.GetComponent<Coin>().enabled = true;
        gameObject.GetComponent<PlayerBullets>().enabled = true;
        gameObject.GetComponent<InstGrenata>().enabled = true;
        gameObject.GetComponent<PlayerInput>().enabled = true;
        gameObject.GetComponent<StarterAssetsInputs>().enabled = true;
        gameObject.GetComponent<FirstPersonController>().enabled = true;
        gameObject.GetComponent<BasicRigidBodyPush>().enabled = true;
        gameObject.GetComponent<CharacterController>().enabled = true;
        car.GetComponent<CarController>().enabled = false;
        gameObject.transform.position = spawnPlayerTransform.position;
    }
}
