// Requires 
//      no dependencies

function onNotesReady(data) {
    console.log("onNotesReady()");
    console.log(data);
    appendNotesBlock($("#latest-message-notes")[0], data.url)
}

function appendNotesBlock(tag, link) {
    var notesTag = document.createElement('a');
    notesTag.className = "ag-btn ag-btn-round";
    notesTag.href = link;
    notesTag.text = "This Week's Sermon Notes & Branches";
    tag.appendChild(notesTag);
}

$.getJSON("https://amazing-grace-pdx.azurewebsites.net/api/notes/latest", function (data) {
    onNotesReady(data);
});