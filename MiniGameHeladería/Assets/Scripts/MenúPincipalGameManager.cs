
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenúPincipalGameManager : MonoBehaviour
{
    [SerializeField]
    Button xButton, menuButton, tutorialB, sonidoB, salirB;
    [SerializeField]
    GameObject tutorialCanvas, sonidoCanvas, recuadroMenu, fondoLvlSelection, lvl1, lvl2, lvl3, infinite, xButtonLvl, lvl1TutoI, lvl2TutoI, lvl3TutoI, tutorialI, sonidoI, salirI, cruzI, lvl1Tuto, lvl2Tuto, lvl3Tuto, flecha, tutorial, sonido, salir, cruz, recuadroMenuTutoriales, tutorial01, tutorial02, tutorial03, camara;
    [SerializeField]
    Image lvlFondo;
    [SerializeField]
    TextMeshProUGUI record;
    [SerializeField]
    Slider soundSlider;

    private Vector2 tutorialLocation;
    private Vector2 flechaLocation;

    private void Start()
    {
        soundSlider.maxValue = camara.GetComponent<AudioSource>().volume;
        soundSlider.value = PlayerPrefs.GetFloat("sonido");
        recuadroMenu.SetActive(false);
    }
    private void OutAnimation()
    {
        tutorialI.SetActive(false);
        sonidoI.SetActive(false);
        salirI.SetActive(false);
        cruzI.SetActive(false);
        tutorial.SetActive(true);
        sonido.SetActive(true);
        salir.SetActive(true);
        cruz.SetActive(true);
        tutorialLocation = tutorial.transform.position;
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
        menuButton.interactable = false;
        tutorialI.SetActive(true);
        sonidoI.SetActive(true);
        salirI.SetActive(true);
        cruzI.SetActive(true);
        tutorial.SetActive(false);
        sonido.SetActive(false);
        salir.SetActive(false);
        cruz.SetActive(false);
        if (recuadroMenu != null)
        {
            LeanTween.moveLocalX(recuadroMenu, -709f, 1.5f).setEaseOutQuart().setOnComplete(OutAnimation);
            recuadroMenuTutoriales.transform.localPosition = (new Vector2(-709.0004f, -146.3296f));
            recuadroMenuTutoriales.SetActive(false);
        }
    }
    public void SalirMenu()
    {
        tutorialI.SetActive(true);
        sonidoI.SetActive(true);
        salirI.SetActive(true);
        cruzI.SetActive(true);
        tutorial.SetActive(false);
        sonido.SetActive(false);
        salir.SetActive(false);
        cruz.SetActive(false);
        menuButton.interactable = true;
        LeanTween.moveLocalX(recuadroMenu, -1347f, 2f).setEaseInCubic().setOnComplete(Start).setOnComplete(OutAnimation);
        recuadroMenuTutoriales.SetActive(true);
        recuadroMenuTutoriales.transform.localPosition = (new Vector2(-1347f, -146.3296f));

    }
    public void Tutorial()
    {
        flechaLocation = flecha.transform.position;
        recuadroMenuTutoriales.SetActive(true);
        tutorial.transform.localPosition = new Vector2(-706f, tutorial.transform.localPosition.y);
        sonido.SetActive(false);
        salir.SetActive(false);
        cruz.SetActive(false);
        tutorialI.SetActive(true);
        sonidoI.SetActive(true);
        salirI.SetActive(true);
        cruzI.SetActive(true);
        LeanTween.scale(tutorialI, Vector2.zero, 0.3f);
        LeanTween.scale(sonidoI, Vector2.zero, 0.3f).setEaseInOutQuart();
        LeanTween.scale(salirI, Vector2.zero, 0.3f).setEaseInOutQuart();
        LeanTween.scale(cruzI, Vector2.zero, 0.3f).setEaseInOutQuart().setOnComplete(() =>
        {
            tutorialI.SetActive(false);
            sonidoI.SetActive(false);
            salirI.SetActive(false);
            cruzI.SetActive(false);
            lvl1TutoI.SetActive(true);
            lvl2TutoI.SetActive(true);
            lvl3TutoI.SetActive(true);
            LeanTween.scale(lvl1TutoI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
            {
                LeanTween.scale(lvl2TutoI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
                {
                    LeanTween.scale(lvl3TutoI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
                    {
                        LeanTween.moveLocalX(flecha, 146.91f, 0.7f).setEaseInOutQuart().setOnComplete(() =>
                        {
                            lvl1Tuto.SetActive(true);
                            lvl2Tuto.SetActive(true);
                            lvl3Tuto.SetActive(true);
                            lvl1TutoI.SetActive(false);
                            lvl2TutoI.SetActive(false);
                            lvl3TutoI.SetActive(false);
                        });
                    });
                });
            });
        });
    }
    public void SalirTutorial()
    {
        lvl1TutoI.SetActive(true);
        lvl2TutoI.SetActive(true);
        lvl3TutoI.SetActive(true);
        lvl1Tuto.SetActive(false);
        lvl2Tuto.SetActive(false);
        lvl3Tuto.SetActive(false);
        LeanTween.scale(lvl1TutoI, Vector2.zero, 0.3f).setEaseInOutQuart();
        LeanTween.scale(lvl2TutoI, Vector2.zero, 0.3f).setEaseInOutQuart();
        LeanTween.scale(lvl3TutoI, Vector2.zero, 0.3f).setEaseInOutQuart();
        LeanTween.move(flecha, flechaLocation, 0.7f).setEaseInOutQuart().setOnComplete(() =>
        {
            tutorialI.SetActive(true);
            sonidoI.SetActive(true);
            salirI.SetActive(true);
            cruzI.SetActive(true);
            lvl1TutoI.SetActive(false);
            lvl2TutoI.SetActive(false);
            lvl3TutoI.SetActive(false);
            LeanTween.scale(tutorialI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
            {
                LeanTween.scale(sonidoI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
                {
                    LeanTween.scale(salirI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
                    {
                        LeanTween.scale(cruzI, Vector2.one, 0.5f).setEaseInOutQuart().setOnComplete(() =>
                        {
                            recuadroMenuTutoriales.SetActive(false);
                            tutorial.transform.position = tutorialLocation;
                            sonido.SetActive(true);
                            salir.SetActive(true);
                            cruz.SetActive(true);
                            tutorialI.SetActive(false);
                            sonidoI.SetActive(false);
                            salirI.SetActive(false);
                            cruzI.SetActive(false);
                        });
                    });
                });
            });
        });
    }
    
    //Buttons:
    public void Tutorial01()
    {
        tutorial01.SetActive(true);
    }
    public void SalirTutorial01()
    {
        tutorial01.SetActive(false);
    }

    public void Tutorial02()
    {
        tutorial02.SetActive(true);
    }
    public void SalirTutorial02()
    {
        tutorial02.SetActive(false);
    }

    public void Tutorial03()
    {
        tutorial03.SetActive(true);
    }
    public void SalirTutorial03()
    {
        tutorial03.SetActive(false);
    }

    public void SonidoJuego()
    {
        if (sonidoCanvas.activeInHierarchy != true)
        {
            sonidoCanvas.SetActive(true);
        }
        else
        {
            sonidoCanvas.SetActive(false);
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
        //LVL Unlock:
        record.text = ("Infinite mode actual record: " + PlayerPrefs.GetFloat("record").ToString("00.00"));
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

        //TutorialUnlock:
        if (PlayerPrefs.GetInt("tutorial01") == 1)
        {
            lvl1Tuto.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("tutorial02") == 1)
        {
            lvl2Tuto.GetComponent<Button>().interactable = true;
        }
        if (PlayerPrefs.GetInt("tutorial03") == 1)
        {
            lvl3Tuto.GetComponent<Button>().interactable = true;
        }

        //Sonido:
        camara.GetComponent<AudioSource>().volume = soundSlider.value;
        PlayerPrefs.SetFloat("sonido", soundSlider.value);
    }
}
