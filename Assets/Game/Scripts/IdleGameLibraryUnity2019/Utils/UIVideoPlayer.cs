using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class UIVideoPlayer : MonoBehaviour {
    private RenderTexture movie;
    private Image image;
    private RawImage rawImage;
    private VideoPlayer player;
    public UIMode UGUI;
    public float progressValue = 4;
    public Slider slider;
    public enum UIMode {
        None = 0,
        Image = 1,
        RawImage = 2
    }
    // Use this for initialization
    void Start() {
        movie = new RenderTexture(Screen.width, Screen.height, 24);
        player = GetComponent<VideoPlayer>();
        player.sendFrameReadyEvents = true;
        if (UGUI == UIMode.Image) {
            image = GetComponent<Image>();
            player.renderMode = VideoRenderMode.RenderTexture;
            player.targetTexture = movie;
        }
        else if (UGUI == UIMode.RawImage) {
            rawImage = GetComponent<RawImage>();
            player.renderMode = VideoRenderMode.APIOnly;
        }
        player.loopPointReached += OnVideoLoopOrPlayFinished;
        player.frameReady += OnVideoPlaying;
        if (slider != null) {
            slider.maxValue = GetTime();
        }
    }

    void OnDestroy() {
        player.loopPointReached -= OnVideoLoopOrPlayFinished;
        player.frameReady -= OnVideoPlaying;
    }
    // Update is called once per frame
    void Update() {
        if (UGUI == UIMode.Image) {
            //在Image上播放视频
            if (player.targetTexture == null) return;
            int width = player.targetTexture.width;
            int height = player.targetTexture.height;
            Texture2D t = new Texture2D(width, height, TextureFormat.ARGB32, false);
            RenderTexture.active = player.targetTexture;
            t.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            t.Apply();
            image.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f)) as Sprite;
        }
        if (UGUI == UIMode.RawImage) {
            //在RawImage上播放视频
            if (player.texture == null) return;
            rawImage.texture = player.texture;
        }
    }
    /// <summary>
    /// 设置播放视频
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="play"></param>
    public void SetVideoClip(VideoClip clip, bool play = true) {
        player.source = VideoSource.VideoClip;
        player.clip = clip;
        if (play) {
            player.Play();
        }
    }
    /// <summary>
    /// 设置播放地址
    /// </summary>
    /// <param name="url"></param>
    /// <param name="play"></param>
    public void SetVideoClip(string url, bool play = true) {
        player.source = VideoSource.Url;
        player.url = url;
        if (play) {
            player.Play();
        }
    }
    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="replay"></param>
    public void Play(bool replay = false) {
        if (replay) {
            player.frame = 0;
        }
        player.Play();
    }
    /// <summary>
    /// 播放或者暂停
    /// </summary>
    public void PlayOrPause() {
        if (player.isPlaying) {
            Pause();
        }
        else {
            Play();
        }
    }
    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause() {
        player.Pause();
    }
    /// <summary>
    /// 停止
    /// </summary>
    public void Stop() {
        player.Stop();
    }
    /// <summary>
    /// 获取视频时长
    /// </summary>
    /// <returns></returns>
    public int GetTime() {
        return (int)(player.frameCount / player.frameRate + 0.5f);
    }
    /// <summary>
    /// 设置播放时间点
    /// </summary>
    /// <param name="time"></param>
    public void SetTime(int time) {
        player.frame = (long)(time * player.frameRate);
        player.Play();
    }
    /// <summary>
    /// 播放进度条拖拽时间控制
    /// </summary>
    /// <param name="value"></param>
    public void OnValueChanged(float value) {
        SetTime((int)value);
    }
    /// <summary>
    /// 播放完成或者循环完成事件
    /// </summary>
    /// <param name="vp"></param>
    public void OnVideoLoopOrPlayFinished(VideoPlayer vp) {
        if (slider != null) {
            slider.value = GetTime();
            Pause();
        }
    }
    /// <summary>
    /// 播放中事件（给进度条赋值）
    /// </summary>
    /// <param name="vp"></param>
    /// <param name="frameindex"></param>
    public void OnVideoPlaying(VideoPlayer vp, long frameindex) {
        if (slider != null) {
            slider.value = (int)player.time;
        }
    }
    /// <summary>
    /// 进度加
    /// </summary>
    public void ProgressPlus() {
        Pause();
        float value = (float)player.time;
        value += progressValue;
        SetTime((int)value);
    }
    /// <summary>
    /// 进度减
    /// </summary>
    public void ProgressSub() {
        Pause();
        float value = (float)player.time;
        value -= progressValue;
        SetTime((int)value);
    }
}