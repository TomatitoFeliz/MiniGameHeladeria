
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenúPincipalGameManager : MonoBehaviour
{
    [SerializeField]
    Button xButton, menuButton;
    [SerializeField]
    GameObject tutorialCanvas, sonidoCanvas, recuadroMenu, fondoLvlSelection, lvl1, lvl2, lvl3, infinite, xButtonLvl;
    [SerializeField]
    Image lvlFondo;

    private void Start()
    {
        recuadroMenu.SetActive(false);
    }
    private void OutAnimation()
    {
        xButton.interactable = true;
        menuButton.interactable = true;
    }

    public void StarLevels()
    {
        fondoLvlSelection.SetActive(true);
        LeanTween.value(fondoLvlSelection, 0, 1, 2).setEaseOutQuart()
            .setOnUpdate((value) =>
        {
            lvlFondo.fillAmount = value;
        })
            .setOnComplete(() => 
        { 
            lvl1.SetActive(true); 
            lvl2.SetActive(true); 
            lvl3.SetActive(true); 
            infinite.SetActive(true);
            xButtonLvl.SetActive(true);
        });
        //.LoadScene("Game01");
    }
    public void ExitLevels()
    {
        lvl1.SetActive(false);
        lvl2.SetActive(false);
        lvl3.SetActive(false);
        infinite.SetActive(false);
        xButtonLvl.SetActive(false);
        LeanTween.value(fondoLvlSelection, 1, 0, 2).setEaseOutQuart()
           .setOnUpdate((value) =>
           {
               lvlFondo.fillAmount = value;
           }).setOnComplete(() => { fondoLvlSelection.SetActive(false); });
    } 

    public void Ajustes()
    {
        recuadroMenu.SetActive(true);
        xButton.interactable = false;
        menuButton.interactable = false;
        if (recuadroMenu != null)
        {
            LeanTween.moveLocalX(recuadroMenu, -709f, 1.5f).setEaseOutQuart().setOnComplete(OutAnimation);
        }
    }
    public void SalirMenu()
    {
        xButton.interactable = false;
        menuButton.interactable = false;
        LeanTween.moveLocalX(recuadroMenu, -1347f, 2f).setEaseInCubic().setOnComplete(Start).setOnComplete(OutAnimation);
    }
    public void Sonido()
    {
        if (sonidoCanvas.activeInHierarchy != true)
        {
            sonidoCanvas.SetActive(true);
        }
        else if (sonidoCanvas.activeInHierarchy != false)
        {
            sonidoCanvas.SetActive(false);
        }
    }
    public void Tutorial()
    {
        if (tutorialCanvas.activeInHierarchy != true)
        {
            tutorialCanvas.SetActive(true);
        }
        else if (tutorialCanvas.activeInHierarchy != false)
        {
            tutorialCanvas.SetActive(false);
        }
    }
    public void Salir()
    {
        Application.Quit();
    }

    //LVLs:
    public void LVL1()
    {
        SceneManager.LoadScene("Game01");
    }
    public void LVL2()
    {
        SceneManager.LoadScene("Game02");
    }
    public void LVL3()
    {
        SceneManager.LoadScene("Game03");
    }
    public void Infinite()
    {
        SceneManager.LoadScene("Infinite");
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("ganado01") == 1)
        {
            lvl2.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("ganado02") == 1)
        {
            lvl3.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("ganado03") == 1)
        {
            infinite.GetComponent<Button>().interactable = true;
        }
    }
}
