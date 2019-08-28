function onMessagesReady(data) {
    console.log(`onMessagesReady()`);
    console.log(data);
    messageBlocks = $("div.message-block");
    console.log(`Number of message bocks: ${messageBlocks.length}`);
    messageBlocks.each((i, tag) => appendMessageBlock(tag, data));
}

function appendMessageBlockHeader(tag, text) {
    headerTag = document.createElement("h3");
    headerTag.innerHTML = text;
    tag.appendChild(headerTag);
    return headerTag;
}

function appendMessageBlockParagraph(tag, text) {
    pTag = document.createElement("p");
    pTag.innerHTML = text;
    tag.appendChild(pTag);
    return pTag;
}

function appendMessageBlockLink(tag, text, link) {
    var buttonTag = document.createElement("a");
    buttonTag.innerHTML = text;
    buttonTag.className = "ag-btn ag-btn-round";
    buttonTag.role = "button";
    if (link && link.length > 0) {
        buttonTag.href = link;
    } else {
        buttonTag.className += " disabled";
    }
    tag.appendChild(buttonTag);
    return buttonTag;
}

function appendButtonGroup(tag) {
    var buttonGroupTag = document.createElement("div");
    buttonGroupTag.className = "btn-group ag-btn-group";
    tag.appendChild(buttonGroupTag);
    return buttonGroupTag;
}

function formatDate(dateString) {
    date = new Date(dateString + "T00:00:00");
    var formattedDate = date.toLocaleDateString('en-US', { month: "long", day: "numeric", year: "numeric" });
    return formattedDate;
}

function getVideoLink(details) {
    if (details.youTubeVideoId && details.youTubeVideoId.length > 0)
        return `https://www.youtube.com/watch?v=${details.youTubeVideoId}&t=${details.messageStartTimeSeconds}`;
}

function getAudioLink(details) {
    if (details.streamUrl && details.streamUrl.length > 0)
        return details.streamUrl;
}

function getNotesLink(details) {
    if (details.url && details.url.length > 0)
        return details.url;
}

function appendMessageBlockDescription(tag, description) {
    if (!description)
        return;
    var descriptionTag = document.createElement("div");
    descriptionTag.classList.add("message-block-description");
    descriptionTag.classList.add("ag-center");
    descriptionTag.innerHTML = description;
    tag.appendChild(descriptionTag);
    return descriptionTag;
}

function appendHiddenDiv(parentTag, activatorTag) {
    var divTag = document.createElement("div");
    divTag.classList.add("hidden");
    activatorTag.onmouseover = () => {
        console.log("mouseover");
        divTag.classList.remove("hidden");
    };
    parentTag.onmouseleave = () => {
        console.log("mouseout");
        divTag.classList.add("hidden");
    };
    parentTag.appendChild(divTag);
    return divTag;
}

function appendMessageBlock(tag, data) {
    console.log(`appendMessageBlock() called for ${tag.id}`);
    date = tag.id;
    details = data.find(function (e) {
        var d = new Date(e.date);
        var dateString = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate()
        console.log(date);
        console.log(dateString);
        return dateString == date;
    });
    if (details) {
        // Header
        headerTag = appendMessageBlockHeader(tag, details.title);

        // Information
        infoDiv = appendHiddenDiv(tag, headerTag);
        dateTag = appendMessageBlockParagraph(infoDiv, formatDate(date))
        dateTag.classList.add("message-block-date");
        infoTag = appendMessageBlockDescription(infoDiv, details.description);;

        // Button group
        buttonGroupTag = appendButtonGroup(tag);
        appendMessageBlockLink(buttonGroupTag, "Notes", getNotesLink(details.notes));
        appendMessageBlockLink(buttonGroupTag, "Audio", getAudioLink(details.audio));
        appendMessageBlockLink(buttonGroupTag, "Video", getVideoLink(details.video));
    } else {
        tag.hidden = true;
    }
    return tag;
}

$.getJSON("https://amazing-grace-pdx.azurewebsites.net/api/messages", function (data) {
    onMessagesReady(data);
});