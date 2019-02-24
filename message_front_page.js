function loadMessageDetails() {    
    console.log("loadMessageDetails()");
    // TODO: Consider making this an argument
    dataUrl = "https://raw.githubusercontent.com/taylort7147/amazing-grace-pdx/master/message_details.json";
    $.getJSON(dataUrl, onMessageDetailsLoaded);
}

function onMessageDetailsLoaded(data){
    console.log(`onMessageDetailsLoaded(${data})`);
    messageBlocks = $("div.message-block");
    console.log(`Number of message bocks: ${messageBlocks.length}`);
    messageBlocks.each((i, tag) => appendMessageBlock(tag, data));
}

function appendMessageBlockHeader(tag, text){
    headerTag = document.createElement("h3");
    headerTag.innerHTML = text;
    tag.appendChild(headerTag);
    return headerTag;
}

function appendMessageBlockLink(tag, text, link){
    var buttonTag = document.createElement("a");
    buttonTag.innerHTML = text;
    buttonTag.className = "btn ag-btn";
    buttonTag.role = "button";
    if(link && link.length > 0){
        buttonTag.href = link;
    }else{
        buttonTag.className += " disabled";
    }
    tag.appendChild(buttonTag);
    return buttonTag;
}

function appendButtonGroup(tag){
    var buttonGroupTag = document.createElement("div");
    buttonGroupTag.className = "btn-group ag-btn-group";
    tag.appendChild(buttonGroupTag);
    return buttonGroupTag;
}

function getVideoLink(details){
    if(details.videoId && details.videoId.length > 0)
        return `https://www.youtube.com/watch?v=${details.videoId}`;
}

function getAudioLink(details){
    if(details.audioLink && details.audioLink.length > 0)
        return details.audioLink;
}

function getNotesLink(details){
    if(details.notesLink && details.notesLink.length > 0)
        return details.notesLink;
}

function appendMessageBlock(tag, messageDetails){
    console.log(`appendMessageBlock() called for ${tag.id}`);
    key = tag.id;
    details = messageDetails[key];
    if(details){
        appendMessageBlockHeader(tag, details.title);
        buttonGroupTag = appendButtonGroup(tag);
        appendMessageBlockLink(buttonGroupTag, "Notes", getNotesLink(details));
        appendMessageBlockLink(buttonGroupTag, "Audio", getAudioLink(details));
        appendMessageBlockLink(buttonGroupTag, "Video", getVideoLink(details));
    } else {
        tag.hidden = true;
    }
}

function injectMessageDetailsLoadScript(){
    console.log("injectMessageDetailsLoadScript()");
    var target = $("script").last();
    var tag = document.createElement('script');
    tag.id = "message-details-load-script";
    tag.type = "application/javascript";
    tag.innerHTML = "loadMessageDetails();";
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
}

// Inject script tag to load the message details
injectMessageDetailsLoadScript()
