using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


/*
 * TODO : Since SEVEN can't find direct localizedString (like Unity Editor does),
 * we need to give the ID as a raw string, without any easy way to test it's validity.
 * If we want extra security, we can manually interface it with an enum (which SEVEN
 * can interprete), and put all the raw string IDs in a "Enum to stringID converter"
*/
public class LocalizedStringModifier : MonoBehaviour
{
    [SerializeField]
    LocalizeStringEvent localizeStringEvent;

    [SerializeField]
    LocalizedStringTable table;

    [SerializeField]
    Text LabelText;

    //public LocaleIdentifier localeID;
    //public TableEntryReference ter;

    public void changeEntryName(string stringKey)  // Like "default.test", check the table
    {
        localizeStringEvent.StringReference = new LocalizedString(table.TableReference, stringKey);
    }


    /*
     * The language string is set in the Text object of the displayed labeled on the dropdown menu.
     * The list is "hardcoded" in the LanguagesDropdown object.
     * I'm not satisfied with the way it's linked (a cleaner way to do it would be to directly give a 
     * string to this method), but it works :shrug:
     */
    public void changeLocale(/*string language*/) // Like "en", "fr"
    {
        string localeID;
        switch(LabelText.text)
        {
            case "Français":
                localeID = "fr";
                break;
            case "English":
                localeID = "en";
                break;
            default: 
                localeID = "en";
                break;
        }

        LocalizationSettings.Instance.SetSelectedLocale(
            LocalizationSettings.Instance.GetAvailableLocales().GetLocale(localeID)
        );
    }

}

/*
 * Below are written the enums where each entry corresponds to one key of the localization table
 * for the relevant script. By doing so, we can give to SEVEN a CraftingScriptKeys attribute that will
 * show a dropdown to select which line we want, instead of giving a raw string corresponding to the key
 */
public enum ScriptKeys
{
    // Crafting script keys
    crafting_00,
    crafting_01,
    crafting_02,
    crafting_03,
    crafting_04,
    crafting_05,
    crafting_06,
    crafting_07,
    crafting_08,
    crafting_09,
    crafting_10,
    crafting_end,

    // Discovery script keys
    discovery_00,
    discovery_01,
    discovery_02,
    discovery_03,
    discovery_04,
    discovery_05,
    discovery_06,
    discovery_07,
    discovery_08,
    discovery_09,
    discovery_10,
    discovery_11,
    discovery_12,
    discovery_13,
    discovery_14,
    discovery_15,
    discovery_16,
    discovery_17,
    discovery_18,
    discovery_19,
    discovery_end
}


public enum AudioAssetTableNames
{
    CraftingAudioScript,
    DiscoveryAudioScript,
}