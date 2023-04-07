using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class MusicStreamer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string[] streamMP3Urls;
    [SerializeField] private string[] streamAACUrls;

    private bool _streaming;
    
    private void Start()
    {
        PlayAudio();
        // StartCoroutine(GetAudioClip());
    }

    /*private void Update()
    {
        if (_streaming) return;

        if (Input.anyKey)
        {
            #if UNITY_EDITOR_WIN
            StreamFromUrl(streamMP3Urls[0], AudioType.MPEG);
            return;
            #endif

            #if UNITY_WEBGL
            StreamFromUrl(streamACCUrls[0], AudioType.ACC);
            return;
            #endif
        }
    }*/

    public void PlayAudio()
    {
        audioSource.Play();
    }
    
    IEnumerator GetAudioClip()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(streamAACUrls[0], AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.LogError($"Sent web request");
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                Debug.LogError($"Clip is null: {myClip == null}");
            }
        }
    }

    private async void StreamFromUrl(string url, AudioType audioType)
    {
        _streaming = true;
        
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
        await www.SendWebRequest();
        var clip = DownloadHandlerAudioClip.GetContent(www);
        Debug.LogError($"Clip is null: {clip == null}");


        /*var www = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
        DownloadHandlerAudioClip dHA = new DownloadHandlerAudioClip(string.Empty, audioType);
        dHA.streamAudio = true;
        www.downloadHandler = dHA;

        await www.SendWebRequest();

        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(20));
        
        await UniTask.WaitUntil(() => www.downloadProgress >= 1, cancellationToken: cts.Token);

        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError(www.error);
                return;
        }

        var audioClip = DownloadHandlerAudioClip.GetContent(www);
        audioSource.clip = audioClip;
        audioSource.Play();*/
        
        /*using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.MPEG);
        DownloadHandlerAudioClip dHA = new DownloadHandlerAudioClip(string.Empty, AudioType.MPEG);
        dHA.streamAudio = true;
        www.downloadHandler = dHA;
        www.SendWebRequest();
        while (www.downloadProgress < 1) {
            Debug.Log(www.downloadProgress);
            yield return new WaitForSeconds(.1f);
        }
        if (www.responseCode != 200 || www.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log("error");
        } else {
            audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            Debug.Log("Start audio play");
            audioSource.Play();
        }*/
    }
}
