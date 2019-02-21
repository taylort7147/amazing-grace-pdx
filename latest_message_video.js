// Requires 
//      barrier.js
//      load_message_details.js
//      load_youtube_iframe_api.js

// ************** Button control functions ***************
function initializeButtonCallbacks(player, videoDetails){
    console.log("initializeButtonCallbacks()");
    $("#latest-message-video-controls-jump-to-beginning").on("click", () => jumpTo(player, 0));
    $("#latest-message-video-controls-jump-to-message").on("click", () => jumpTo(player, videoDetails.messageStart));
}

function jumpTo(player, timeInSeconds){
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
        videoId: videoDetails.videoId,
        playerVars: {
            start: videoDetails.messageStart,
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

function getLatestVideo(obj) {
    var sortedKeys = Object.keys(obj).sort();
    index = sortedKeys.length - 1;
    while(index >= 0){
        key = sortedKeys[index];
        if(obj[key].videoId && obj[key].videoId.length > 0) { return obj[key]; }
        --index;
    }
}

function onResultsReady(results){
    console.log("onResultsReady()");
    data = results["data"];
    var videoDetails = getLatestVideo(data);
    var player = createPlayer(videoDetails);
    $("#latest-message-video").className = "embed-responsive-item";
    initializeButtonCallbacks(player, videoDetails);
}

var videoBarrier = new Barrier(["api", "data"], onResultsReady);
registerMessageDetailsCallback(data => videoBarrier.addResult("data", data));
registerYouTubeIframeAPIReadyCallback(() => videoBarrier.addResult("api", true));
