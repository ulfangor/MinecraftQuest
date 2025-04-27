using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameSystem : MonoBehaviour
{
    // Player
    public GameObject Player;

    // Messages et entrée secrète
    public GameObject portalMessage1;
    public GameObject secretEntrance;
    public GameObject lighterMessage1;
    public GameObject lighterMessage2;
    public GameObject portalMessage2;
    public Text endMessage;
    public GameObject endMenu;
    public Text timer;
    public Text emeralds;


    // Booléens pour savoir où le joueur en est dans le jeu
    private int headsCount;
    private bool allHeads = false;
    private bool hasLighter = false;

    // Têtes sur la map
    public GameObject inMapHead1;
    public GameObject inMapHead2;
    public GameObject inMapHead3;

    // Têtes UI
    public GameObject UIHeads;
    public GameObject inUIHead1;
    public GameObject inUIHead2;
    public GameObject inUIHead3;
    public Text headCount;

    // Têtes Stand
    public GameObject standHead1;
    public GameObject standHead2;
    public GameObject standHead3;

    // Lighter
    public GameObject UILighter;

    // Portails
    public GameObject OffPortal;
    public GameObject LitPortal;

    // Audios
    public AudioSource creeperSound;
    public AudioSource chestSound;
    public AudioSource flintSound;

    // Hints
    public GameObject portalTP;
    public GameObject chestTP;
    public GameObject hints;
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (headsCount == 3)
            hints.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // au niveau du portail

        if (other.CompareTag("InPortalRoom"))
        {
            // Salle du briquet découverte et tp dispo
            secretEntrance.SetActive(false);
            portalTP.SetActive(true);

            // On check où en est le joueur
            if (!hasLighter)
            {
                portalMessage1.SetActive(true);
            }
            else
            {
                portalMessage2.SetActive(true);
                flintSound.Play();
                OffPortal.SetActive(false);
                LitPortal.SetActive(true);
            }
        }

        // au niveau de la salle du briquet

        if (other.CompareTag("InLighterRoom"))
        {
            // on active les têtes, le tp et les indices
            UIHeads.SetActive(true);
            chestTP.SetActive(true);
            hints.SetActive(true);

            if (!allHeads)
            {
                lighterMessage1.SetActive(true);
                if (headsCount == 0)
                {
                    inMapHead1.SetActive(true);
                    inMapHead2.SetActive(true);
                    inMapHead3.SetActive(true);
                }
                else if (headsCount == 1)
                {
                    inMapHead2.SetActive(true);
                    inMapHead3.SetActive(true);
                }
                else if (headsCount == 2)
                {
                    inMapHead3.SetActive(true);
                }
            }
            else
            {
                lighterMessage2.SetActive(true);
                chestSound.Play();
                hasLighter = true;
                UILighter.SetActive(true);
            }
        }

        if (other.CompareTag("Head"))
        {
            headsCount++;
            headCount.text = "" + headsCount;
            other.gameObject.SetActive(false);
            creeperSound.Play();

            // apparition des têtes dans l'inventaire et sur les stands et disparition de la flèche d'aide
            if (headsCount == 1) 
            { 
                inUIHead1.SetActive(true);
                standHead1.SetActive(true);
                arrow1.SetActive(false);
            }
            if (headsCount == 2)
            {
                inUIHead2.SetActive(true);
                standHead2.SetActive(true);
                arrow2.SetActive(false);
            }
            if (headsCount == 3)
            {
                inUIHead3.SetActive(true);
                standHead3.SetActive(true);
                arrow3.SetActive(false);

                allHeads = true;
            }
        }

        if (other.CompareTag("Portal"))
        {
            if (hasLighter)
            {
                endMenu.SetActive(true);
                endMessage.text = "Congrats ! You got out of the maze in " + timer.text + " seconds and collected " + emeralds.text + " emeralds !\r\nYou can restart to try to get a better score !\r\nThanks for playing !";
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InPortalRoom"))
        {
            if (!hasLighter)
            {
                portalMessage1.SetActive(false);
            }
            else
            {
                portalMessage2.SetActive(false);
            }

        }
        if (other.CompareTag("InLighterRoom"))
        {
            if (!allHeads)
            {
                lighterMessage1.SetActive(false);
            }
            else
            {
                lighterMessage2.SetActive(false);
            }
        }
    }

    public void TpToPortal()
    {
        Player.transform.position = new Vector3(8.3f, 0 , 59.7f);
    }

    public void TpToChest()
    {
        Player.transform.position = new Vector3(36, 0, 52);
    }

    public void ShowHeadDirection()
    {
        if (headsCount == 0)
            arrow1.SetActive(true);
        if (headsCount == 1)
            arrow2.SetActive(true);
        if (headsCount == 2)
            arrow3.SetActive(true);
    }
}