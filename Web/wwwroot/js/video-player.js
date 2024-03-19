
const PLAY_ICON_PATH = "/img/player-play.svg";
const STOP_ICON_PATH = "/img/player-stop.svg";

const VOLUME_HIGH_ICON_PATH = "/img/player-sound-high.svg";
const VOLUME_MEDIUM_ICON_PATH = "/img/player-sound-medium.svg";
const VOLUME_LOW_ICON_PATH = "/img/player-sound-low.svg";

const PLAYER_VOLUME = "player_volume";

const DEFAULT_VOLUME = 0.5;
const SKIP_SECONDS = 10;
const HIDE_CONTROLS_TIME = 3 * 1000;

window.initializePlayer = (dotnet) => {
    const container = document.getElementById("container");
    const player = document.getElementById("player");
    const video = document.getElementById("video");

    const playButton = document.getElementById("button-play");
    const playButtonIcon = document.getElementById("control-icon");

    const skipForwardButton = document.getElementById("button-forward");
    const skipBackButton = document.getElementById("button-back");

    const muteButton = document.getElementById("button-mute");
    const muteButtonIcon = document.getElementById("control-volume-icon");
    const volumeInput = document.getElementById("input-volume");

    const currentTimeSpan = document.getElementById("span-current-time");
    const durationSpan = document.getElementById("span-duration");
    const progressBar = document.getElementById("progress-bar");
    const videoTimeline = document.getElementById("video-timeline");
    const progressTimeSpan = document.getElementById("span-progress-time");

    const fullscreenButton = document.getElementById("button-fullscreen");

    let timer;
    let ready = false;
    let muted = false;
    let volume = localStorage.getItem(PLAYER_VOLUME) ?? DEFAULT_VOLUME;

    let retrievedProgress = 0;

    const formatTime = (time) => {
        let seconds = Math.floor(time % 60);
        let minutes = Math.floor(time / 60) % 60;
        let hours = Math.floor(time / 3600);

        seconds = seconds < 10 ? `0${seconds}` : seconds;
        minutes = minutes < 10 ? `0${minutes}` : minutes;
        hours = hours < 10 ? `0${hours}` : hours;

        if (seconds < 0 || minutes < 0 || hours < 0)
            return "";
        
        if (hours == 0)
            return `${minutes}:${seconds}`

        return `${hours}:${minutes}:${seconds}`;
    }

    const toggleVideo = () => {

        if (video.paused) {
            play();
        }
        else {
            stop();
        }
    }

    const play = (notifyChanged = true) => {

        console.log(`play incoked with parameters video.paused = ${video.paused}`)

        if (video.paused == false)
            return;

        video.play();

        if (notifyChanged)
            window.notifyPlaybackStarted();
    }

    const stop = (notifyChanged = true) => {
        if (video.paused == true)
            return;

        video.pause();

        if (notifyChanged)
            window.notifyPlaybackStoped();
    }

    const getProgress = () => {
        var result = (video.currentTime / video.duration);
        return isNaN(result) ? 0 : result;
    }

    const setProgress = (value) => {
        video.currentTime = video.duration * value;
    }

    const setSavedProgress = (value) => {
        if (value == null || isNaN(value))
            return;

        window.invokeSaveProgress(value);
    }

    const skipForward = () => {
        video.currentTime += SKIP_SECONDS;
        window.notifyProgressChanged(getProgress());
    }

    const skipBack = () => {
        video.currentTime -= SKIP_SECONDS;
        window.notifyProgressChanged(getProgress());
    }

    const changeToPlayButtonIcon = () => {
        playButtonIcon.src = PLAY_ICON_PATH;
    }

    const changeToStopButtonIcon = () => {
        playButtonIcon.src = STOP_ICON_PATH;
    }

    const changeVolume = (value) => {
        volume = value;
        video.volume = volume;
        localStorage.setItem(PLAYER_VOLUME, volume);

        if (volume == 0){
            muteButtonIcon.src = VOLUME_LOW_ICON_PATH;
            return;
        }

        if (volume <= 0.5) {
            muteButtonIcon.src = VOLUME_MEDIUM_ICON_PATH;
            return;
        }

        muteButtonIcon.src = VOLUME_HIGH_ICON_PATH;
    }

    const toggleFullscreen = () => {
        if (document.fullscreenElement) {
            document.exitFullscreen();
            return
        }
        container.requestFullscreen();
    }

    const toggleMute = () => {
        if (muted) {
            muted = false;
            volumeInput.value = volume;
            changeVolume(volume)
            return;
        }
        muted = true;
        volumeInput.value = 0;
        video.volume = 0;
        muteButtonIcon.src = VOLUME_LOW_ICON_PATH;
    }

    const onContainerMouseMove = () => {
        container.classList.remove("hide-controls");
        clearTimeout(timer);
        hideControls();
    }

    const onVideoTimeUpdate = (e) => {
        let { currentTime, duration } = e.target;
        let percent = (currentTime / duration);

        setSavedProgress(percent);

        progressBar.style.width = `${percent * 100}%`;
        currentTimeSpan.innerText = formatTime(currentTime);
    }

    const onVideoDataLoaded = () => {
        durationSpan.innerText = formatTime(video.duration);
        ready = true;

        if (retrievedProgress > 0) {
            setProgress(retrievedProgress)
            progressBar.style.width = `${retrievedProgress * 100}%`;
            retrievedProgress = 0;
        }

        history.replaceState(null, '', window.location.href.split('?')[0]);   
    }

    const onTimelineMouseMove = (e) => {
        let timelineWidth = videoTimeline.clientWidth;
        let offsetX = e.offsetX;
        let percent = Math.floor((offsetX / timelineWidth) * video.duration);
        offsetX = offsetX < 20 ? 20 : (offsetX > timelineWidth - 20) ? timelineWidth - 20 : offsetX;
        progressTimeSpan.style.left = `${offsetX}px`;
        progressTimeSpan.innerText = formatTime(percent);
    }

    const onTimelineMouseClick = (e) => {
        let timelineWidth = videoTimeline.clientWidth;
        var progress = (e.offsetX / timelineWidth);
        video.currentTime = progress * video.duration;

        window.notifyProgressChanged(progress);
        setSavedProgress(progress);
    }

    const onTimelineDrag = (e) => {
        let timelineWidth = videoTimeline.clientWidth;
        progressBar.style.width = `${e.offsetX}px`;
        var progress = (e.offsetX / timelineWidth);
        video.currentTime = progress * video.duration;
        currentTimeSpan.innerText = formatTime(video.currentTime);

        window.notifyProgressChanged(progress);
        setSavedProgress(progress);
    }

    const hideControls = () => {
        if (video.paused)
            return;

        timer = setTimeout(() => {
            container.classList.add("hide-controls");
        }, HIDE_CONTROLS_TIME);
    }

    hideControls();
    changeVolume(volume);
    volumeInput.value = volume;

    playButton.addEventListener("click", toggleVideo);
    video.addEventListener("play", changeToStopButtonIcon);
    video.addEventListener("pause", changeToPlayButtonIcon);
    skipForwardButton.addEventListener("click", skipForward);
    skipBackButton.addEventListener("click", skipBack);
    volumeInput.addEventListener("input", (e) => changeVolume(e.target.value));
    muteButton.addEventListener("click", toggleMute);

    video.addEventListener("timeupdate", onVideoTimeUpdate);
    video.addEventListener("loadeddata", onVideoDataLoaded);

    videoTimeline.addEventListener("mousemove", onTimelineMouseMove);
    videoTimeline.addEventListener("click", onTimelineMouseClick);
    videoTimeline.addEventListener("mousedown", () => videoTimeline.addEventListener("mousemove", onTimelineDrag));
    document.addEventListener("mouseup", () => videoTimeline.removeEventListener("mousemove", onTimelineDrag));

    window.addEventListener('beforeunload', () => { dotnet.invokeMethodAsync("OnBeforeUnload"); });

    fullscreenButton.addEventListener("click", toggleFullscreen);

    container.addEventListener("mousemove", onContainerMouseMove);

    window.changePlayerSource = (url) => {
        video.setAttribute("src", url);
        video.currentTime = 0;
        progressBar.style.width = `0%`;
        ready = false;
        changeToPlayButtonIcon();
    }

    window.setRetrievedProgress = (value) => {
        retrievedProgress = value;
    }

    window.getProgress = () => {
        return getProgress();
    }

    window.setProgress = (value) => {
        setProgress(value);
        setSavedProgress(value);
    }

    window.play = (notifyChanged = true) => {
        console.log("Play invoked in js")
        play(notifyChanged);
    }

    window.stop = (notifyChanged = true) => {
        stop(notifyChanged);
    }

    window.isPlaying = () => {
        return video.paused == false;
    }

    window.isReady = () => {
        return ready;
    }

    window.invokeSaveProgress = (value) => {
        dotnet.invokeMethodAsync("SaveProgress", value);
    } 

    window.notifyPlaybackStarted = () => {
        dotnet.invokeMethodAsync("NotifyPlaybackStarted");
    }

    window.notifyPlaybackStoped = () => {
        dotnet.invokeMethodAsync("NotifyPlaybackStoped");
    }

    window.notifyProgressChanged = (value) => {
        dotnet.invokeMethodAsync("NotifyProgressChanged", value);
    }
}