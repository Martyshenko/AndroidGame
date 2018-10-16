
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene 
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    //<summary>  
    //Will load a new scene upon being called   
    //</summary>   
    //<param name="levelName">The name of the level we want to go to</param> 

    public RectTransform menuContainer;

    public Transform levelPanel;
    public Transform skinPanel;
    public Transform weaponPanel;

    public Text skinBuySetText;
    public Text weaponBuySetText;
    public Text goldText;

    private int[] skinCost = new int[] { 0, 5, 5, 5, 10, 10, 10, 15, 15, 10 };
    private int[] weaponCost = new int[] { 0, 20, 40, 40, 60, 60, 80, 80, 100, 100 };
    private int selectedSkinIndex;
    private int selectedWeaponIndex;
    private int activeSkinIndex;
    private int activeWeaponIndex;


    public Toggle toggle;

    private Vector3 desiredMenuPosition;

    private void Start()
    {
        //$$ TEMPORARY
        SaveManager.Instance.state.gold = 9999;

        SetCamera(Manager.Instance.menuFocus);

        //Tell our gold text how much he should be displaying
        UpdateGoldText();

        if (PlayerPrefs.GetInt("2Players") == 1)
        {
            toggle.isOn = true;
        }
        else
            toggle.isOn = false;

        //Add button OnClick events to shop buttons
        InitShop();

        //Add button onClick events to levels
        InitLevel();

        //Set players preferences (skin&weapon)
        OnSkinSelect(SaveManager.Instance.state.activeSkin);
        SetSkin(SaveManager.Instance.state.activeSkin);

        OnWeaponSelect(SaveManager.Instance.state.activeWeapon);
        SetWeapon(SaveManager.Instance.state.activeWeapon);

        //Make  the buttons bigger for the selected items
        skinPanel.GetChild(SaveManager.Instance.state.activeWeapon).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        weaponPanel.GetChild(SaveManager.Instance.state.activeWeapon).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        
    }

    private void Update()
    {
        //Menu navigator(smooth)
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition,0.1f);
    }

    private void InitLevel()
    {
        //Just make sure we've assigned references
        if (levelPanel == null)
        {
            Debug.Log("Did not asign  the level panel in the inspector ");
        }
        //For every children transform under the panel find the button and add OnClick event
        int i = 0;
        foreach (Transform t in levelPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));

            Image img = t.GetComponent<Image>();

            //Is it unlocked?
            if(i <= SaveManager.Instance.state.completedLevel)
            {
                //It is unlocked!
                if(i == SaveManager.Instance.state.completedLevel)
                {
                    //Its not completed!
                    img.color = Color.white;
                }
                else
                {
                    //Level is already completed
                    img.color = Color.green;
                }
            }
            else
            {
                //Level isn't unlock, disable the button
                b.interactable = false;

                //Set to a dark color
                img.color = Color.grey;

            }

            i++;
        }
    }

    private void OnLevelSelect(int currentIndex)
    {
        Manager.Instance.currentLevel = currentIndex;
        SceneManager.LoadScene("FirstLevel");
        Debug.Log("Selecting level: " + currentIndex);
    }

    public void IsTwoPlayers()
    {
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt("2Players", 1);

        }else
            PlayerPrefs.SetInt("2Players", 0);
    }

    private void InitShop()
    {

        //Just make sure we've assigned references
        if (skinPanel == null || weaponPanel == null)
        {
            Debug.Log("Did not asign  the skin/weapon panel in the inspector ");
        }
            //For every children transform under the panel find the button and add OnClick event
            int i = 0;
            foreach(Transform t in weaponPanel){
                int currentIndex = i;
                Button b = t.GetComponent<Button>();
                b.onClick.AddListener(() => OnWeaponSelect(currentIndex));

            //Set color of the image, based on if owned or not
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsSkinOwned(i) ? Color.white : new Color(0.7f, 0.7f, 0.7f);


                i++;
            }
            //Reset index
            i = 0;

            foreach (Transform t in skinPanel)
            {
                int currentIndex = i;
                Button b = t.GetComponent<Button>();
                b.onClick.AddListener(() => OnSkinSelect(currentIndex));

            //Set color of the image, based on if owned or not
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsWeaponOwned(i) ? Color.white : new Color(0.7f, 0.7f, 0.7f);

            i++;
            }
        
    }


    public void LoadLevel(string levelName)   
    {        SceneManager.LoadScene(levelName);    }

    private void SetCamera(int menuIndex)
    {
        NaigateTo(menuIndex);
        menuContainer.anchoredPosition3D = desiredMenuPosition;
    }

    private void NaigateTo(int menuIndex)
    {
        
        switch(menuIndex)
        {
            // 0 && default case = Main Menu 
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;
            // 1 = Level Menu
            case 1:
                desiredMenuPosition = Vector3.right * 1280;
                break;
            // 2 = Shop Menu
            case 2:
                desiredMenuPosition = Vector3.left * 1280;
                break;

        }

    }

    private void SetSkin(int index) {

        activeSkinIndex = index;
        SaveManager.Instance.state.activeSkin = index;
        //Change the skin on the player  model

        //Change bue/set button text
        skinBuySetText.text = "Current";

        //Remember preferences
        SaveManager.Instance.Save();
    }

    private void SetWeapon(int index) {

        activeWeaponIndex = index;
        SaveManager.Instance.state.activeWeapon = index;
        //Change the weapon on the player  model

        //Change bue/set button text
        weaponBuySetText.text = "Current";

        //Remember preferences
        SaveManager.Instance.Save();
    }

    private void UpdateGoldText()
    {
        goldText.text = SaveManager.Instance.state.gold.ToString();
    }



    //Buttons

    public void OnBackClick()
    {
        NaigateTo(0);
        Debug.Log("Back button has been clicked");
    }

    public void OnPlayClick()
    {
        
        Debug.Log("Play button has been clicked");
    }

    public void OnSelectLevelClick()
    {
        NaigateTo(1);
        Debug.Log("Select level button has been clicked");
    }

    public void OnShopClick()
    {
        NaigateTo(2);
        Debug.Log("Shop button has been clicked" );
    }

    private void OnWeaponSelect(int currentIndex)
    {

        Debug.Log("Selecting weapon button : " + currentIndex);

        //if the button clicked is already selected, exit
        if (selectedWeaponIndex == currentIndex)
            return;

        //Make the icon slightly bigger
        weaponPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //Put the previous one on normal state
        weaponPanel.GetChild(selectedWeaponIndex).GetComponent<RectTransform>().localScale = Vector3.one;
        

        //Set the selected Weapon

        selectedWeaponIndex = currentIndex;

        //Change the content of the bue/set button, depending on the state of the weapon
        if (SaveManager.Instance.IsWeaponOwned(currentIndex))
        {
            //Weapon is owned
            //Is it already our current weapon
            if (activeWeaponIndex == currentIndex)
            {
                weaponBuySetText.text = "Current";

            }
            else
            {
                weaponBuySetText.text = "Select";
            }
        }
        else
        {
            //Weapon isn't owned
            weaponBuySetText.text = "Buy: " + weaponCost[currentIndex].ToString();
        }

    }

    private void OnSkinSelect(int currentIndex)
    {

        Debug.Log("Selecting skin button : " + currentIndex);


        //if the button clicked is already selected, exit
        if (selectedSkinIndex == currentIndex)
            return;

        //Make the icon slightly bigger
        skinPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //Put the previous one on normal state
        skinPanel.GetChild(selectedSkinIndex).GetComponent<RectTransform>().localScale = Vector3.one;
       


        //Set the selected Skin

        selectedSkinIndex = currentIndex;

        //Change the content of the bue/set button, depending on the state of the skin
        if (SaveManager.Instance.IsSkinOwned(currentIndex))
        {
            //Skin is owned
            //Is it already our current skin
            if (activeSkinIndex == currentIndex)
            {
                skinBuySetText.text = "Current";

            }
            else
            {
                skinBuySetText.text = "Select";
            }
        }
        else
        {
            //Skin isn't owned
            skinBuySetText.text = "Buy: " + skinCost[currentIndex].ToString();
        }

    }
    
    public void OnWeaponBuySet()
    {
        Debug.Log("Buy/Set weapon");

        //Is the selected weapon owned

        if (SaveManager.Instance.IsWeaponOwned(selectedWeaponIndex))
        {
            //Set the weapon!
            SetWeapon(selectedWeaponIndex);
        }
        else
        {
            //Attempt to Buy the weapon
            if (SaveManager.Instance.BuyWeapon(selectedWeaponIndex, weaponCost[selectedWeaponIndex]))
            {
                //Success!
                SetWeapon(selectedWeaponIndex);

                //Change the color of the button
                weaponPanel.GetChild(selectedWeaponIndex).GetComponent<Image>().color = Color.white;

                //Update gold text
                UpdateGoldText();
            }
            else
            {
                //Do not have enough gold!
                //Play sound feedback
                Debug.Log("Not enough gold");
            }

        }
    }

    public void OnSkinBuySet()
    {
        Debug.Log("Buy/Set skin");

        //Is the  selected skin owned

        if (SaveManager.Instance.IsSkinOwned(selectedSkinIndex))
        {
            //Set the skin!
            SetSkin(selectedSkinIndex);
        }
        else
        {
            //Attempt to Buy the skin
            if(SaveManager.Instance.BuySkin(selectedSkinIndex, skinCost[selectedSkinIndex]))
            {
                //Success!
                SetSkin(selectedSkinIndex);

                //Change the color of the button
                skinPanel.GetChild(selectedSkinIndex).GetComponent<Image>().color = Color.white;

                //Update gold text
                UpdateGoldText();
            }
            else
            {
                //Do not have enough gold!
                //Play sound feedback
                Debug.Log("Not enough gold");
            }

        }
    }
}
