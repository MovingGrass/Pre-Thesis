
using UnityEngine;
using UnityEngine.UI; 
using TMPro;          

public class UIPopupController : MonoBehaviour
{
    public static UIPopupController instance; 

    [Header("UI References")]
    public GameObject popupPanel;  
    public Image popupImage;       
     

    
    private FPController playerMouseLook;
    private PlayerMovement playerMovement;

    

    private void Awake()
    {
        
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
        playerMouseLook = FindObjectOfType<FPController>();
        playerMovement = FindObjectOfType<PlayerMovement>();

       
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    

    
    public void ShowPopup(Sprite imageToShow)
    {
        if (popupPanel == null || popupImage == null) return;
        GameManager.isUIOpen = true;
        popupImage.sprite = imageToShow; 
        popupPanel.SetActive(true);      

        
        FreezePlayer(true);
    }

    
    public void ClosePopup()
    {
        GameManager.isUIOpen = false;   
        popupPanel.SetActive(false);

        
        FreezePlayer(false);
    }

    private void FreezePlayer(bool freeze)
    {
        if (freeze)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            if (playerMouseLook != null) playerMouseLook.enabled = false;
            if (playerMovement != null) playerMovement.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            if (playerMouseLook != null) playerMouseLook.enabled = true;
            if (playerMovement != null) playerMovement.enabled = true;
        }
    }
}