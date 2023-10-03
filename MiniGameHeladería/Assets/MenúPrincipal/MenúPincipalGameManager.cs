
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenúPincipalGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialCanvas, sonidoCanvas;


    public void StarLevels()
    {
        SceneManager.LoadScene("Game01");
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
