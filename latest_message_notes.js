// Requires 
//      barrier.js
//      load_message_details.js

function getLatestNotes(obj) {
    var sortedKeys = Object.keys(obj).sort();
    index = sortedKeys.length - 1;
    while(index >= 0){
        key = sortedKeys[index];
        if(obj[key].notesLink && obj[key].notesLink.length > 0) { return obj[key]; }
        --index;
    }
}

function onNotesReady(data){
    console.log("onNotesReady()");
    var messageDetails = getLatestNotes(data);
    appendNotesBlock($("#latest-message-notes")[0], messageDetails.notesLink)
}

function appendNotesBlock(tag, link){
    var notesTag = document.createElement('a');
    notesTag.className = "ag-btn ag-btn-round";
    notesTag.href = link;
    notesTag.text = "This Week's Sermon Notes & Branches";
    tag.appendChild(notesTag);
}

registerMessageDetailsCallback(onNotesReady);