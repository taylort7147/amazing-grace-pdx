// Requires 
//      barrier.js
//      load_youtube_iframe_api.js

// ************** Button control functions ***************
function initializeButtonCallbacks(player, videoDetails) {
    console.log("initializeButtonCallbacks()");
    $("#latest-message-video-controls-jump-to-beginning").on("click", () => jumpTo(player, 0));
    $("#latest-message-video-controls-jump-to-message").on("click", () => jumpTo(player, videoDetails.messageStartTimeSeconds));
}

function jumpTo(player, timeInSeconds) {
    console.log(`jumpTo(${timeInSeconds})`);
    player.seekTo(timeInSeconds);
}

// ************** YouTube player event handlers ***************
function onPlayerReady(event, player, videoDetails) {
    console.log(`onPlayerReady(${event}, ${videoDetails})`);
}

function onPlayerStateChange(event) {
}

// ************** Create a YouTube player *********************
function createPlayer(videoDetails) {
    var player = new YT.Player('latest-message-video', {
        height: '390',
        width: '640',
        videoId: videoDetails.youTubeVideoId,
        playerVars: {
            start: videoDetails.messageStartTimeSeconds,
            color: "white",
            modestbranding: 1,
            rel: 0
        },
        events: {
            'onReady': (event) => onPlayerReady(event, player, videoDetails),
            'onStateChange': onPlayerStateChange
        }
    });
    return player;
}

function onResultsReady(results) {
    console.log("onResultsReady()");
    videoDetails = results["data"];
    console.log(videoDetails);
    var player = createPlayer(videoDetails);
    initializeButtonCallbacks(player, videoDetails);
}

var videoBarrier = new Barrier(["api", "data"], onResultsReady);
$.getJSON("https://amazing-grace-pdx.azurewebsites.net/api/videos/latest", function (data) {
    videoBarrier.addResult("data", data);
});

registerYouTubeIframeAPIReadyCallback(() => videoBarrier.addResult("api", true));
