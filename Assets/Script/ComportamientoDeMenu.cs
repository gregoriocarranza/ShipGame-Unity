using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoDeMenu : MonoBehaviour
{
    private static ComportamientoDeMenu instance;

    [SerializeField] private GameObject[] Menues; // Pantallas del menú
    private int currentMenuIndex = 0; // Índice del menú actual

    public void StartGame(int IndiceNivel)
    {
        // Iniciar el nivel sin necesidad de usar Update
        SceneManager.LoadScene(IndiceNivel);
    }

    public void ExitGame()
    {
        // Salir del juego
        Application.Quit();
    }

    public void NextMenu()
    {
        if (currentMenuIndex < Menues.Length - 1)
        {
            // Desactivar el menú actual
            Menues[currentMenuIndex].SetActive(false);

            // Avanzar al siguiente menú
            currentMenuIndex++;

            // Activar el nuevo menú
            Menues[currentMenuIndex].SetActive(true);
        }
    }

    public void PreviousMenu()
    {
        if (currentMenuIndex > 0)
        {
            // Desactivar el menú actual
            Menues[currentMenuIndex].SetActive(false);

            // Retroceder al menú anterior
            currentMenuIndex--;

            // Activar el nuevo menú
            Menues[currentMenuIndex].SetActive(true);
        }
    }

    // Opción para ir a un menú específico, si lo necesitas
    public void GoToMenu(int menuIndex)
    {
        if (menuIndex >= 0 && menuIndex < Menues.Length)
        {
            // Desactivar el menú actual
            Menues[currentMenuIndex].SetActive(false);

            // Actualizar al nuevo menú
            currentMenuIndex = menuIndex;

            // Activar el nuevo menú
            Menues[currentMenuIndex].SetActive(true);
        }
    }
}
