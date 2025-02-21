using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpsHandler : MonoBehaviour
{
    [SerializeField]
    private RawImage[] picture = new RawImage[5];
    [SerializeField]
    private TMP_InputField[] inputField = new TMP_InputField[5];
    [SerializeField]
    private TMP_Text[] text = new TMP_Text[5];
    [SerializeField]
    private string url = "https://rickandmortyapi.com/api/character";
    [SerializeField]
    string DBurl = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SendRequest()
    {
        int[] ints = new int[5];
        if (Int32.TryParse(inputField[0].text, out ints[0]))
        {

        }
        else
        {
            text[0].text = "not found";
        }
        if (Int32.TryParse(inputField[1].text, out ints[1]))
        {

        }
        else
        {
            text[1].text = "not found";
        }
        if (Int32.TryParse(inputField[2].text, out ints[2]))
        {

        }
        else
        {
            text[2].text = "not found";
        }
        if (Int32.TryParse(inputField[3].text, out ints[3]))
        {

        }
        else
        {
            text[3].text = "not found";
        }
        if (Int32.TryParse(inputField[4].text, out ints[4]))
        {

        }
        else
        {
            text[4].text = "not found";
        }
        StartCoroutine(GetCharacter(ints[0], 0));
        StartCoroutine(GetCharacter(ints[1], 1));
        StartCoroutine(GetCharacter(ints[2], 2));
        StartCoroutine(GetCharacter(ints[3], 3));
        StartCoroutine(GetCharacter(ints[4], 4));
    }

    IEnumerator GetCharacter(int id, byte pic)
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "/" + id);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.responseCode == 200)
            {

                Character character = JsonUtility.FromJson<Character>(www.downloadHandler.text);

                StartCoroutine(GetImage(character.image, pic));
                text[pic].text = character.name;

            }
            else
            {
                string mensaje = "status:" + www.responseCode;
                mensaje += "\nErro: " + www.error;
                Debug.Log(mensaje);
            }
        }
    }
    IEnumerator GetImage(string imageUrl, byte pic)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            picture[pic].texture = texture;
        }
    }
}

public class Character
{
    public int id;
    public string name;
    public string species;
    public string image;
}
[System.Serializable]
public class CharList
{
    public Character[] results;
}
