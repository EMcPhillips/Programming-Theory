using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleEnterExit : MonoBehaviour
{
    public TextMeshProUGUI gUI;
    public TextMeshProUGUI crosshair;
    public string playerTagName;
    public Behaviour[] componentsToDisableIfNotDriver;
    public float timeToInsure;

    public bool isInVehicle;
    private GameObject player;
    private bool firetag;

    public Camera playerCamera;
    public Camera vehicleCamera;

    // Start is called before the first frame update
    void Start()
    {
        PlayerView();
        isInVehicle = false;
        gUI.gameObject.SetActive(false);
        for (int i = 0; i < componentsToDisableIfNotDriver.Length; i++)
        {
            componentsToDisableIfNotDriver[i].enabled = false;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < componentsToDisableIfNotDriver.Length; i++)
        {
            componentsToDisableIfNotDriver[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Entering();
        Exiting();
    }

    void Exiting()
    {
        if (isInVehicle == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerView();
                player.transform.position = GetComponentInChildren<SpawnPoint>().gameObject.transform.position;
                player.SetActive(true);

                for (int i = 0; i < componentsToDisableIfNotDriver.Length; i++)
                {
                    componentsToDisableIfNotDriver[i].enabled = false;
                }
            }
        }
    }

    void Entering()
    {
        if (firetag == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CarView();
                player.SetActive(false);
                gUI.gameObject.SetActive(false);
                StartCoroutine(Waiting());
                for (int i = 0; i < componentsToDisableIfNotDriver.Length; i++)
                {
                    componentsToDisableIfNotDriver[i].enabled = true;
                }
                player.transform.position = GetComponentInChildren<SpawnPoint>().gameObject.transform.position;
            }

        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(timeToInsure);
        isInVehicle = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTagName)
        {
            gUI.gameObject.SetActive(true);
            firetag = true;
            player = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTagName)
        {
            gUI.gameObject.SetActive(false);
            firetag = false; // out of range
            player = null;
            isInVehicle = false;
        }
    }
    public void CarView()
    {
        playerCamera.enabled = false;
        vehicleCamera.enabled = true;
        crosshair.gameObject.SetActive(false);
    }

    public void PlayerView()
    {
        playerCamera.enabled = true;
        vehicleCamera.enabled = false;
        crosshair.gameObject.SetActive(true);
    }
}
