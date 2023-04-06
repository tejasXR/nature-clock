using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class MusicStreamer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string streamURL;

    private bool _streaming;
    
    /*private void Start()
    {
        StreamFromUrl(streamURL);
    }*/

    private void Update()
    {
        if (_streaming) return;
        
        if (Input.anyKey)
            StreamFromUrl(streamURL);
            
    }

    private async void StreamFromUrl(string url)
    {
        _streaming = true;
        
        AudioType audioType = AudioType.OGGVORBIS;
#if UNITY_WEBGL || UNITY_WEBGL_API
        audioType = AudioType.MPEG;
#endif
        
        var www = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
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
        audioSource.Play();
        
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
