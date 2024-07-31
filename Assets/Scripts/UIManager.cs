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

    public void CharacterDamaged(GameObject character, int damageReceived)
    {
        CreateText(damageTextPrefab, character.transform.position, damageReceived.ToString());
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        CreateText(healthTextPrefab, character.transform.position, healthRestored.ToString());
    }

    private void CreateText(GameObject textPrefab, Vector3 position, string text)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(position);
        TMP_Text tmpText = Instantiate(textPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = text;
    }

    public void OpenCloseShop()
    {
        bool isActive = shop.activeSelf;
        shop.SetActive(!isActive);
        shopButtonText.text = isActive ? "Shop" : "Close";
    }

}
