// Requires 
//      no dependencies

function onNotesReady(message) {
    console.log("onNotesReady()");
    console.log(message);
    appendMessageDetailsTooltip(
        $("#latest-message-notes-details")[0],
        message,
        "right");
    appendNotesBlock($("#latest-message-notes")[0], message.notes.url)
}

function appendNotesBlock(tag, link) {
    var notesTag = document.createElement('a');
    notesTag.className = "ag-btn ag-btn-round";
    notesTag.href = link;
    notesTag.text = "This Week's Sermon Notes & Branches";
    tag.appendChild(notesTag);
}

$.getJSON("https://amazing-grace-pdx-web-app.azurewebsites.net/api/messages/latest_notes", function(data) {
    onNotesReady(data[0]);
});