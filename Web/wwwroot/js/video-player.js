
const PLAY_ICON_PATH = "/img/player-play.svg";
const STOP_ICON_PATH = "/img/player-stop.svg";

const VOLUME_HIGH_ICON_PATH = "/img/player-sound-high.svg";
const VOLUME_MEDIUM_ICON_PATH = "/img/player-sound-medium.svg";
const VOLUME_LOW_ICON_PATH = "/img/player-sound-low.svg";

const PLAYER_VOLUME = "player_volume";

const SKIP_SECONDS = 10;
const HIDE_CONTROLS_TIME = 3 * 1000;

window.initializePlayer = () => {
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
    let muted = false;
    let volume = localStorage.getItem(PLAYER_VOLUME);

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
        video.paused ? video.play() : video.pause();
    }

    const skipForward = () => {
        video.currentTime += SKIP_SECONDS;
    }

    const skipBack = () => {
        video.currentTime -= SKIP_SECONDS;
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
        let percent = (currentTime / duration) * 100;
        progressBar.style.width = `${percent}%`;
        currentTimeSpan.innerText = formatTime(currentTime);
    }

    const onVideoDataLoaded = () => {
        durationSpan.innerText = formatTime(video.duration);
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
        video.currentTime = (e.offsetX / timelineWidth) * video.duration;
    }

    const onTimelineDrag = (e) => {
        let timelineWidth = videoTimeline.clientWidth;
        progressBar.style.width = `${e.offsetX}px`;
        video.currentTime = (e.offsetX / timelineWidth) * video.duration;
        currentTimeSpan.innerText = formatTime(video.currentTime);
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

    fullscreenButton.addEventListener("click", toggleFullscreen);

    container.addEventListener("mousemove", onContainerMouseMove);

    window.changePlayerSource = (value) => {
        video.setAttribute("src", value);
        video.currentTime = 0;
        progressBar.style.width = `0%`;
        changeToPlayButtonIcon();
    }
}