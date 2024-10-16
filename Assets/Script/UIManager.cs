using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [Header("Pantalla de Pausa")]
    [SerializeField] private GameObject MenuPausa;
    [SerializeField] private bool pausa = false;
    public static Action<bool> Pause;

    private void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked;


    }
    private void Start()
    {
        Pause?.Invoke(false);
        MenuPausa.SetActive(pausa);
    }
    private void FixedUpdate()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();

        }

    }
    public void TogglePause()
    {
        pausa = !pausa;
        Debug.Log(pausa);
        MenuPausa.SetActive(pausa);
        if (pausa) Cursor.lockState = CursorLockMode.None; else Cursor.lockState = CursorLockMode.Locked;

        Pause?.Invoke(pausa);
    }

    // Botones menu-------------------------------------------

    public void CambiarNivel(int Indice)
    {
        SceneManager.LoadScene(Indice);
    }
}
