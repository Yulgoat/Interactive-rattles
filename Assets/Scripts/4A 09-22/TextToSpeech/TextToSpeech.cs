using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
    private AudioSource _audio;

    private string[] _languageChoices = { "fr-fr", "en-gb" };
    private string[] _audio_format = { "wav", "mp3" };
    private string[] _audio_voice_fr = { "Bette", "Iva", "Zola", "Axel" };
    private string[] _audio_voice_en = { "Alice", "Nancy", "Lily", "Harry" };

    private const string _rapid_api_key_ = "b5eb632911msh48437856033e039p1ddbdajsn6e4aae138989";
    private const string _rss_api_key_ = "a9d4ca44592c44938b018459d931e7a5";

    [Header("Api keys")]
    public string rapid_apî_key = _rapid_api_key_;
    public string rss_api_key = _rss_api_key_;

    [Header("Text to speech")]
    public string text;

    [Header("Language choice")]
    [Dropdown("_languageChoices")]
    public string language;

    [Header("Audio format choice")]
    [Dropdown("_audio_format")]
    public string format;
    
    [Header("Audio voices")]
    [Dropdown("_audio_voice_fr")]
    public string voice_fr;
    [Dropdown("_audio_voice_en")]
    public string voice_en;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the the audio source of the object to which it is attached
        _audio = gameObject.GetComponent<AudioSource>();

        // Call the correct method with respect to the laguage choice (french or english)
        if(language == "fr-fr")
        {
            StartCoroutine(GetTTS(text, language, format, voice_fr));
        }
        else 
        {
            StartCoroutine(GetTTS(text, language, format, voice_en));
        }        
    }

    // Replace the "spaces" with "% 20" for the link can be interpreted
    private string urlFormatString(string text)
    {
        string result = text.Replace(" ", "%20");
        return result;
    }

    // Give the expected audio or error in case of bad request
    IEnumerator GetTTS(string text, string _language_, string _format_, string _voice_)
    {
        // Text formatting for the url
        string _formated_text_ = urlFormatString(text);

        // Define the url with the parameters
        string url = $"https://voicerss-text-to-speech.p.rapidapi.com/?key={_rss_api_key_}&src={_formated_text_}&hl={_language_}&r=0&c={_format_}&v={_voice_}&f=8khz_8bit_mono";
        Debug.Log(url);

        // Define the correct request with respect to the audio format
        UnityWebRequest www;
        if(_format_ == "wav")
        {
            www = UnityWebRequestMultimedia.GetAudioClip(url,AudioType.WAV);
        } else {
            www = UnityWebRequestMultimedia.GetAudioClip(url,AudioType.MPEG);
        }
        
        // Set the headers of the request
        www.SetRequestHeader("X-RapidAPI-Key", _rapid_api_key_);
        www.SetRequestHeader("X-RapidAPI-Host", "voicerss-text-to-speech.p.rapidapi.com");

        using (www)
        {
            // Send the request
            yield return www.SendWebRequest();

            // Catch error in case of bad request
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.responseCode);
                Debug.Log(www.error);
            }
            else
            {
                // Get and set the audio received from the request
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                _audio.clip = myClip;

                // Play the audio
                GetComponent<AudioSource>().Play();
            }
        }
    }
}