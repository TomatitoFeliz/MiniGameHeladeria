
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenúPincipalGameManager : MonoBehaviour
{
    [SerializeField]
    Button xButton, menuButton;
    [SerializeField]
    GameObject tutorialCanvas, sonidoCanvas, recuadroMenu;

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
        SceneManager.LoadScene("Game01");
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
}
