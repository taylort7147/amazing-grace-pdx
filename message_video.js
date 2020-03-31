// Requires 
//      barrier.js
//      load_youtube_iframe_api.js
//      utilities.js


// ************** Video details functions ****************
function updateVideoDetails(videoDetails) {
    var parentTag = document.getElementById("latest-message-video-details");
    var messageDetails = videoDetails.message;
    appendMessageDetailsTooltip(parentTag, messageDetails, "right");
}

// ************** Button control functions ***************
function initializeButtonCallbacks(player, videoDetails) {
    console.log("initializeButtonCallbacks()");
    $("#latest-message-video-controls-jump-to-beginning").on("click", () => jumpTo(player, 0));
    $("#latest-message-video-controls-jump-to-message").on("click", () => jumpTo(player, videoDetails.messageStartTimeSeconds));
}

function createButtons(player, videoDetails) {
    var buttonGroup = document.createElement("div");
    buttonGroup.classList.add("ag-btn-group");
    buttonGroup.classList.add("ag-center");
    buttonGroup.attributes["role"] = "group";
    buttonGroup.attributes["aria-label"] = "Player controls";

    var jumpToBeginningButton = document.createElement("button");
    jumpToBeginningButton.classList.add("ag-btn");
    jumpToBeginningButton.classList.add("ag-btn-round");
    jumpToBeginningButton.innerHTML = "Jump to Beginning";
    jumpToBeginningButton.attributes["type"] = "button";
    jumpToBeginningButton.onclick = () => jumpTo(player, 0);
    buttonGroup.appendChild(jumpToBeginningButton);

    var jumpToMessageButton = document.createElement("button");
    jumpToMessageButton.classList.add("ag-btn");
    jumpToMessageButton.classList.add("ag-btn");
    jumpToMessageButton.classList.add("ag-btn-round");
    jumpToMessageButton.innerHTML = "Jump to Message";
    jumpToMessageButton.attributes["type"] = "button";
    jumpToMessageButton.onclick = () => jumpTo(player, videoDetails.messageStartTimeSeconds);
    buttonGroup.appendChild(jumpToMessageButton);

    return buttonGroup;
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
function createPlayer(selector, videoDetails) {
    var player = new YT.Player(selector, {
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