using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject damageArmorTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject shop;
    public GameObject PauseMenuUI;
    public TextMeshProUGUI shopButtonText;
    public Canvas gameCanvas;
    public GameObject textsParents;
    public GameObject Buttons;

    public static bool isGamePaused = false;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!shop.activeSelf)
            {
                if (isGamePaused)
                {
                    PauseMenuUI.SetActive(false);

                    Resume();
                }
                else
                {
                    PauseMenuUI.SetActive(true);


                    Pause();
                }
            }
            else if (shop.activeSelf)
            {
                shop.SetActive(false);

                PauseMenuUI.SetActive(true);
            }
        }
    }

    public void CharacterDamaged(GameObject character, int damageReceivedHealth, int damageRecievedArmor)
    {
        if (damageRecievedArmor == 0)
        {
            CreateText(damageTextPrefab, character.transform.position, damageReceivedHealth.ToString());
        }
        else if (damageReceivedHealth == 0)
        {
            CreateText(damageArmorTextPrefab, character.transform.position, damageRecievedArmor.ToString());
        }
        else
        {
            CreateText(damageTextPrefab, character.transform.position, (damageReceivedHealth + damageRecievedArmor).ToString());
        }
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        CreateText(healthTextPrefab, character.transform.position, healthRestored.ToString());
    }

    private void CreateText(GameObject textPrefab, Vector3 position, string text)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(position);
        TMP_Text tmpText = Instantiate(textPrefab, spawnPosition, Quaternion.identity, textsParents.transform).GetComponent<TMP_Text>();
        tmpText.text = text;
    }

    public void OpenCloseShop()
    {
        if(GameManager.Instance.unlockShop)
        {
            bool isActive = shop.activeSelf;
            shop.SetActive(!isActive);
            shopButtonText.text = isActive ? "Shop" : "Close";
            if (isActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Buttons.SetActive(false);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().enabled = false;
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        Buttons.SetActive(true);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().enabled = true;
        Time.timeScale = 1f;
        isGamePaused = false;
        if (PauseMenuUI.activeSelf)
        {
            PauseMenuUI.SetActive(false);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

}
