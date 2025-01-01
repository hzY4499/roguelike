using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for button interaction

public class PanelClose : MonoBehaviour
{
    public GameObject panel; // The panel that will be hidden
    public Button closeButton; // The button that will close the panel

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the button is assigned and attach an event listener to it
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel);
        }
    }

    // Method to hide the panel when called
    void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Disable the panel to make it disappear
            Time.timeScale = 1f;
        }
    }
}
