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

function appendMessageBlockParagraph(tag, text){
    pTag = document.createElement("p");
    pTag.innerHTML = text;
    tag.appendChild(pTag);
    return pTag;
}

function appendMessageBlockLink(tag, text, link){
    var buttonTag = document.createElement("a");
    buttonTag.innerHTML = text;
    buttonTag.className = "ag-btn ag-btn-round";
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

function formatDate(dateString){
    date = new Date(dateString + "T00:00:00");
    var formattedDate = date.toLocaleDateString('en-US', {month:"long", day:"numeric", year:"numeric"});
    return formattedDate;
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

function appendMessageBlockDescription(tag, description){
    if(!description)
        return;
    var descriptionTag = document.createElement("div");
    descriptionTag.classList.add("message-block-description");
    descriptionTag.classList.add("ag-center");
    descriptionTag.innerHTML = description;
    tag.appendChild(descriptionTag);
    return descriptionTag;
}

function appendHiddenDiv(parentTag, activatorTag){
    var divTag = document.createElement("div");
    divTag.classList.add("hidden");
    activatorTag.onmouseover = () => { 
        console.log("mouseover"); 
        divTag.classList.remove("hidden");
    };
    activatorTag.onmouseout = () => {
        console.log("mouseout");
        divTag.classList.add("hidden");
    };
    divTag.onmouseover = () => { 
        console.log("mouseover"); 
        divTag.classList.remove("hidden");
    };
    divTag.onmouseout = () => {
        console.log("mouseout");
        divTag.classList.add("hidden");
    };
    parentTag.appendChild(divTag);
    return divTag;
}

function appendMessageBlock(tag, messageDetails){
    console.log(`appendMessageBlock() called for ${tag.id}`);
    date = tag.id;
    details = messageDetails[date];
    if(details){
        headerTag = appendMessageBlockHeader(tag, details.title);
        infoDiv = appendHiddenDiv(tag, headerTag);
        dateTag = appendMessageBlockParagraph(infoDiv, formatDate(date))
        dateTag.classList.add("message-block-date");
        infoTag = appendMessageBlockDescription(infoDiv, details.description);;
        buttonGroupTag = appendButtonGroup(tag);
        appendMessageBlockLink(buttonGroupTag, "Notes", getNotesLink(details));
        appendMessageBlockLink(buttonGroupTag, "Audio", getAudioLink(details));
        appendMessageBlockLink(buttonGroupTag, "Video", getVideoLink(details));
    } else {
        tag.hidden = true;
    }
    return tag;
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
