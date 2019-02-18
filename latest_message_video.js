// ************** Utilities ***************
function getLatest(obj) {
    var sortedKeys = Object.keys(obj).sort();
    var maxKey = sortedKeys[sortedKeys.length - 1];
    return obj[maxKey];
}

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

// 5. The API calls this function when the player's state changes.
function onPlayerStateChange(event) {
}

// ************** Iframe logic ***************
function injectYouTubeIframeAPILoadScript(){
    console.log("injectYouTubeIframeAPILoadScript()");
    var tag = document.createElement('script');
    tag.src = "https://www.youtube.com/iframe_api";
    tag.type="application/javascript";
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
}

function onYouTubeIframeAPIReady() {    
    console.log("onYouTubeIframeAPIReady() called");
    // TODO: Consider passing this as an argument 
    dataUrl = "https://raw.githubusercontent.com/taylort7147/amazing-grace-pdx/master/message_details.json";
    $.getJSON(dataUrl, (data) => { 
        videoDetails = getLatest(data);
        var player = createPlayer(videoDetails);
        $("#latest-message-video").class = "embed-responsive-item";
        //player.seekTo(videoDetails.messageStart);
        initializeButtonCallbacks(player, videoDetails);
    });
}

function createPlayer(videoDetails) {
    var player = new YT.Player('player', {
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

// Start the process by injecting the YouTube iframe API load script
injectYouTubeIframeAPILoadScript();
