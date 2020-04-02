// Requires 
//      barrier.js
//      load_youtube_iframe_api.js
//      utilities.js

class MessageVideoControls {
    constructor() {
        this.player = null;
        this.createElements();
    }

    createElements() {
        this.buttonGroup = document.createElement("div");
        this.buttonGroup.classList.add("ag-btn-group");
        this.buttonGroup.classList.add("ag-center");
        this.buttonGroup.attributes["role"] = "group";
        this.buttonGroup.attributes["aria-label"] = "Player controls";

        this.jumpToBeginningButton = document.createElement("button");
        this.jumpToBeginningButton.classList.add("ag-btn");
        this.jumpToBeginningButton.classList.add("ag-btn-round");
        this.jumpToBeginningButton.innerHTML = "Jump to Beginning";
        this.jumpToBeginningButton.attributes["type"] = "button";
        this.jumpToBeginningButton.onclick = () => this.player.seekTo(0);
        this.buttonGroup.appendChild(this.jumpToBeginningButton);

        this.jumpToMessageButton = document.createElement("button");
        this.jumpToMessageButton.classList.add("ag-btn");
        this.jumpToMessageButton.classList.add("ag-btn");
        this.jumpToMessageButton.classList.add("ag-btn-round");
        this.jumpToMessageButton.innerHTML = "Jump to Message";
        this.jumpToMessageButton.attributes["type"] = "button";
        this.jumpToMessageButton.onclick = null;
        this.buttonGroup.appendChild(this.jumpToMessageButton);
    }

    setPlayer(player) {
        console.log("setPlayer()");
        this.player = player;
    }

    loadVideo(videoDetails) {
        console.log("loadVideo()");
        console.log(videoDetails);
        if (this.player) {
            this.player.loadVideoById({
                "videoId": videoDetails.youTubeVideoId,
                "startSeconds": videoDetails.messageStartTimeSeconds
            });
            this.jumpToMessageButton.onclick = () => this.player.seekTo(videoDetails.messageStartTimeSeconds);
        }
    }
}

function jumpTo(player, timeInSeconds) {
    console.log(`jumpTo(${timeInSeconds})`);
    player.seekTo(timeInSeconds);
}

// ************** Create a YouTube player *********************
function createPlayer(selector, videoDetails, onReady, onStateChange) {
    console.log("createPlayer()");
    console.log(selector);
    console.log(videoDetails);
    var player = new YT.Player(selector, {
        height: '100%',
        width: '100%',
        playerVars: {
            color: "white",
            modestbranding: 1,
            rel: 0
        },
        events: {
            'onReady': (event) => onReady(event, player),
            'onStateChange': onStateChange
        }
    });

    if (videoDetails) {
        player.loadVideoById({
            "videoId": videoDetails.youTubeVideoId,
            "startSeconds": videoDetails.messageStartTimeSeconds
        });
    }
    return player;
}