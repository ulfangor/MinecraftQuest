using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmeraldPicker : MonoBehaviour
{
    public int emeraldsCollected = 0;
    public Text textField;

    public AudioSource emeraldPickup;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Emerald"))
        {
            emeraldsCollected += 1;
            textField.text =""+ emeraldsCollected;
            Destroy(other.gameObject);
            emeraldPickup.Play();
        }
    }

}
