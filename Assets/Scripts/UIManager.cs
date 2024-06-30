using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject shop;
    public TextMeshProUGUI shopButtonText;
    public Canvas gameCanvas;

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterDamaged;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterDamaged;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterDamaged(GameObject character, int damageRecieved)
    {
        //Create text at character hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageRecieved.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
    public void OpenCloseShop()
    {
        shop.SetActive(!shop.activeSelf);
        if (shop.activeSelf)
        {
            shopButtonText.text = "Close";
        }
        else
        {
            shopButtonText.text = "Shop";

        }
    }
   
}
