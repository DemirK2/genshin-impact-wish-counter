using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public DiscordController discord;
    public GameObject input;
    public GameObject display;
    public GameObject character;

    public int wishAmount;
    public string wishCharacter;

    void Start()
    {
        wishAmount = 0;
        wishCharacter = "Unknown";
        display.GetComponent<Text>().color = Color.gray;
        if (PlayerPrefs.GetInt("amount") > 0)
        {
            wishAmount = PlayerPrefs.GetInt("amount");
        }
        if (PlayerPrefs.GetString("character").Length > 0)
        {
            wishCharacter = PlayerPrefs.GetString("character");
            character.GetComponent<InputField>().text = $"{wishCharacter}";
        }
        discord.UpdateActivity();
        Display();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) Add();
        if (Input.GetKeyDown(KeyCode.Space)) Add();
        if (Input.GetKeyDown(KeyCode.Tab)) Set();
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    private void OnApplicationQuit()
    {
        discord.Disconnect();
    }

    public void Add()
    {
        int amount = int.Parse(input.GetComponent<Text>().text);
        if (amount > 0)
        {
            wishAmount = wishAmount + amount;
            PlayerPrefs.SetInt("amount", wishAmount);
        }
        discord.UpdateActivity();
        Display();
    }

    public void Set()
    {
        int amount = int.Parse(input.GetComponent<Text>().text);
        if (amount > 0)
        {
            wishAmount = amount;
            PlayerPrefs.SetInt("amount", wishAmount);
        }
        discord.UpdateActivity();
        Display();
    }

    public void Change()
    {
        if (character.transform.Find("Text").GetComponent<Text>().text.Length > 0)
        {
            wishCharacter = character.transform.Find("Text").GetComponent<Text>().text;
            PlayerPrefs.SetString("character", wishCharacter);
            discord.UpdateActivity();
        }
    }

    public void Display()
    {
        display.GetComponent<Text>().text = $"{wishAmount}";
        if (wishAmount < 75) display.GetComponent<Text>().color = Color.gray;
        if (wishAmount >= 75) display.GetComponent<Text>().color = Color.blue;
        if (wishAmount >= 89) display.GetComponent<Text>().color = Color.yellow;
    }
}
