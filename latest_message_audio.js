// Requires 
//      barrier.js
//      load_message_details.js

function getLatestAudio(obj) {
    var sortedKeys = Object.keys(obj).sort();
    index = sortedKeys.length - 1;
    while(index >= 0){
        key = sortedKeys[index];
        if(obj[key].audioDownloadLink && obj[key].audioDownloadLink.length > 0) { return obj[key]; }
        --index;
    }
}

function onAudioReady(data){
    console.log("onAudioReady()");
    var messageDetails = getLatestAudio(data);
    appendAudioBlock($("#latest-message-audio")[0], messageDetails.audioDownloadLink)
}

function appendAudioBlock(tag, link){

    var audioTag = document.createElement('audio');
    audioTag.controls = "controls";
    audioTag.className = "embed-responsive";
    var sourceTag = document.createElement('source');
    sourceTag.src = link;
    sourceTag.type = "audio/mp3";
    audioTag.appendChild(sourceTag);
    tag.appendChild(audioTag);
}

registerMessageDetailsCallback(onAudioReady);