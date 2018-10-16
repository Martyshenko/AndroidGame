using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }

       

        DontDestroyOnLoad(gameObject);

       

        Load();

        Debug.Log(HelperScript.Serialize<SaveState>(state));
    }

    //Save the whole state of this saveState script to the player prefab

    public void Save()
    {
        PlayerPrefs.SetString("save", HelperScript.Serialize<SaveState>(state));
    }

    //Load the previous saved state from player prefs
     
    public void Load()
    {
        //Do we already have a save
        if (PlayerPrefs.HasKey("save"))
        {
            state = HelperScript.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating a new one");
        }
    }

    //Check if the skin is owned
    public bool IsSkinOwned(int index)
    {
        //Check if the bit is set, if so the skin is owned
        return (state.skinOwned & (1 << index))!=0;

    }

    //Check if the weapon is owned
    public bool IsWeaponOwned(int index)
    {
        //Check if the bit is set, if so the weapon is owned
        return (state.weaponOwned & (1 << index))!=0;

    }

    //Attempt buying a skin,  return true or false

    public bool BuySkin(int index, int cost)
    {
        if(state.gold >= cost)
        {
            //Enough money, remove from the current gold stack
            state.gold -= cost;
            UnlockSkin(index);

            //Save progress
            Save();

            return true;
        }
        else
        {
            //Not enough money
            return false;
        }
    }

    public bool BuyWeapon(int index, int cost)
    {
        if (state.gold >= cost)
        {
            //Enough money, remove from the current gold stack
            state.gold -= cost;
            UnlockWeapon(index);

            //Save progress
            Save();

            return true;
        }
        else
        {
            //Not enough money
            return false;
        }
    }

    //Unlock a skin in the "skinOwned" int
    public  void UnlockSkin(int index)
    {
        //Toggle on the bit at index
        state.skinOwned |= 1 << index;

    }

    //Unlock a weapon in the "weaponOwned" int
    public void UnlockWeapon(int index)
    {
        //Toggle on the bit at index
        state.weaponOwned |= 1 << index;

    }

    //Complete level
    public void CompleteLevel(int index)
    {
        //if this is the  current active level
        if(state.completedLevel == index)
        {
            state.completedLevel++;
            Save();
        }

    }




    //Reset the whole save file
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }

}
