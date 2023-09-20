using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //bool redP = true;
    //bool redM;
    int maquina;
    int player = 0;
    bool results = false;
    float timer = 4f;
    float x = 0f;
    int random;

    [SerializeField]
    GameObject win, loose;
    [SerializeField]
    Image redPI, redMI;
    [SerializeField] 
    TextMeshProUGUI xText;
    // Start is called before the first frame update
    void Start()
    {
        //random = Random.Range(0, 2);
        maquina = Random.Range(0, 3);

        win.SetActive(false);
        loose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*//Sistema de aleatoriedad del objetivo:
        if (random == 1)
        {
            redM = true;
        }
        else if (random == 0)
        {
            redM = false;
        }*/


        
        //Sistema de temporizador de cada ronda:
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            loose.SetActive(true);
            results = true;


            Debug.Log("stop");
        }
        else if (x == 7)
        {
            win.SetActive(true);
            results = true;
            Time.timeScale = 0f;
            Debug.Log("stop");
        }


        //Sistema de Gameplay:
        if (Input.GetKeyDown(KeyCode.Q) & results == false)
        {
            /*if (redP == true)
            {
                redP = false;
            }
            else if (redP == false)
            {
                redP = true;
            }*/
            Debug.Log(player);
            if (player == 0)
            {
                player = 1;
            }
            if (player == 1)
            {
                player = 2;
            }
            if (player == 2)
            {
                player = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) & results == false)
        {
            if (/*redP != redM*/ maquina != player)
            {
                loose.SetActive(true);
                results = true;
                Time.timeScale = 0f;
                Debug.Log("stop");
            }
            else if (/*redM == redP*/ maquina == player)
            {
                //random = Random.Range(0, 2);
                maquina = Random.Range(0, 3);
                x++;
                timer = (((8f - x) / (9f - x))) * 4f;
            }
        }

        //Debug.Log(timer.ToString("00.00"));
        //Debug.Log(redM);

        /*//Sistema de colores:
        if (redM == false)
        {
            redMI.color = Color.red;
        }
        else if (redM == true)
        {
            redMI.color = Color.green;  
        }

        if (redP == false)
        {
            redPI.color = Color.red;
        }
        else if (redP == true)
        {
            redPI.color = Color.green;
        }*/

        if (maquina == 0)
        {
            redMI.color = Color.red;
        }
        else if (maquina == 1)
        {
            redMI.color = Color.green;
        }
        else if (maquina == 2)
        {
            redMI.color = Color.blue;
        }

        if (player == 0)
        {
            redPI.color = Color.red;
        }
        else if (player == 1)
        {
            redPI.color = Color.green;
        }
        else if (player == 2)
        {
            redPI.color = Color.blue;
        }


        xText.text = x.ToString();
    }
}
