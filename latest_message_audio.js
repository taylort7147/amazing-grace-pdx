// Requires 
//      no dependencies

function onAudioReady(data) {
    console.log("onAudioReady()");
    console.log(data);
    appendMessageDetailsTooltip(
        $("#latest-message-audio-details")[0],
        data.message,
        "right");
    appendAudioBlock($("#latest-message-audio")[0], data.downloadUrl)
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

$.getJSON("https://amazing-grace-pdx.azurewebsites.net/api/audio/latest", function (data) {
    onAudioReady(data);
});