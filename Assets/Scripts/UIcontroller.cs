// functions for all of the functional ui elements, like select track, escape to main menu, pause, customise car, ect.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Rendering;
public class UIcontroller : MonoBehaviour
{
    public void TESTMENU()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Track1()
    {
        SceneManager.LoadScene("Track1");
    }
    public void Track2()
    {
        SceneManager.LoadScene("Track2");
    }
    public void Track3()
    {
        SceneManager.LoadScene("Track3");
    }
    public void PlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");
    }
    public void CustomiseCar()
    {
        SceneManager.LoadScene("CustomiseCar");
    }
    // settings will be a popup
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EscapeToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}