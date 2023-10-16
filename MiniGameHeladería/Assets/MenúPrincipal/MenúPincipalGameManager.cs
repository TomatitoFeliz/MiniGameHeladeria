
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenúPincipalGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialCanvas, sonidoCanvas, recuadroMenu;

    private void Start()
    {
        recuadroMenu.SetActive(false);
    }
    public void StarLevels()
    {
        SceneManager.LoadScene("Game01");
    }
    public void Ajustes()
    {
        recuadroMenu.SetActive(true);
        if (recuadroMenu != null)
        {
            LeanTween.moveLocalX(recuadroMenu, -520f, 1.5f).setEaseInBounce();
        }
    }
    public void SalirMenu()
    {
        LeanTween.moveLocalX(recuadroMenu, -882, 1.5f).setEaseInBounce().setOnComplete(Start);
        
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
