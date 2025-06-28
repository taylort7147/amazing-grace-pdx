// Requires 
//      barrier.js
//      load_youtube_iframe_api.js
//      utilities.js


// ************** Video details functions ****************
function updateVideoDetails(message) {
    var parentTag = document.getElementById("latest-message-video-details");
    appendMessageDetailsTooltip(parentTag, message, "right");
}

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

function onPlayerStateChange(event) {}

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
    message = results["message"];
    console.log(message);
    var player = createPlayer(message.video);
    updateVideoDetails(message);
    initializeButtonCallbacks(player, message.video);
}

var videoBarrier = new Barrier(["api", "message"], onResultsReady);
$.getJSON("https://message-manager.uptheirons.net/api/messages/latest_video", function(data) {
    videoBarrier.addResult("message", data[0]);
});

registerYouTubeIframeAPIReadyCallback(() => videoBarrier.addResult("api", true));