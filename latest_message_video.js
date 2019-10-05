// Requires 
//      barrier.js
//      load_youtube_iframe_api.js
//      utilities.js


// ************** Video details functions ****************
function updateVideoDetails(videoDetails) {
    var parentTag = document.getElementById("latest-message-video-details");
    var messageDetails = videoDetails.message;
    var tooltipTag = appendTooltip(parentTag, "right");

    var titleTag = document.createElement("p");
    titleTag.classList.add("ag-text");
    titleTag.innerHTML = messageDetails.title;
    tooltipTag.appendChild(titleTag);

    var dateTag = document.createElement("p");
    dateTag.classList.add("ag-text");
    dateTag.innerHTML = formatDate(messageDetails.date);
    tooltipTag.appendChild(dateTag);

    var descriptionTag = document.createElement("p")
    descriptionTag.classList.add("ag-text");
    descriptionTag.innerHTML = messageDetails.description
    tooltipTag.appendChild(descriptionTag);
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
    updateVideoDetails(videoDetails);
    initializeButtonCallbacks(player, videoDetails);
}

var videoBarrier = new Barrier(["api", "data"], onResultsReady);
$.getJSON("https://amazing-grace-pdx.azurewebsites.net/api/videos/latest", function (data) {
    videoBarrier.addResult("data", data);
});

registerYouTubeIframeAPIReadyCallback(() => videoBarrier.addResult("api", true));
