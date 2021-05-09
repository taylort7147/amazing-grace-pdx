// Requires 
//      no dependencies

function onAudioReady(message) {
    console.log("onAudioReady()");
    console.log(message);
    appendMessageDetailsTooltip(
        $("#latest-message-audio-details")[0],
        message,
        "right");
    appendAudioBlock($("#latest-message-audio")[0], message.audio.downloadUrl)
}

function appendAudioBlock(tag, link) {

    var audioTag = document.createElement('audio');
    audioTag.controls = "controls";
    audioTag.className = "embed-responsive";
    var sourceTag = document.createElement('source');
    sourceTag.src = link;
    sourceTag.type = "audio/mp3";
    audioTag.appendChild(sourceTag);
    tag.appendChild(audioTag);
}

$.getJSON("https://amazing-grace-pdx-web-app.azurewebsites.net/api/messages/latest_audio", function(data) {
    onAudioReady(data[0]);
});