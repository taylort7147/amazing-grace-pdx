// Requires 
//      utilities.js

function appendMessageBlockHeader(tag, text) {
    headerTag = document.createElement("h3");
    headerTag.innerHTML = text;
    tag.appendChild(headerTag);
    return headerTag;
}

function appendMessageBlockParagraph(tag, text) {
    pTag = document.createElement("p");
    pTag.innerHTML = text;
    pTag.classList.add("ag-text");
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

function getVideoLink(details) {
    if (details && details.youTubeVideoId && details.youTubeVideoId.length > 0)
        return `https://www.youtube.com/watch?v=${details.youTubeVideoId}&t=${details.messageStartTimeSeconds}`;
}

function getAudioLink(details) {
    if (details && details.streamUrl && details.streamUrl.length > 0)
        return details.streamUrl;
}

function getNotesLink(details) {
    if (details && details.url && details.url.length > 0)
        return details.url;
}

function appendMessageBlockDescription(tag, description) {
    if (!description)
        return;
    var descriptionTag = document.createElement("pre");
    descriptionTag.classList.add("message-block-description");
    descriptionTag.classList.add("ag-center");
    descriptionTag.classList.add("ag-text");
    descriptionTag.innerHTML = description;
    tag.appendChild(descriptionTag);
    return descriptionTag;
}
function appendMessageBlock(parentTag, data) {
    console.log(`appendMessageBlock() called for ${parentTag.id}`);
    if (data == null) { return; }

    // Tag
    var tag = document.createElement("div");
    tag.classList.add("message-block");
    parentTag.appendChild(tag);

    // Header
    headerTag = appendMessageBlockHeader(tag, data.title);

    // Information
    infoDiv = appendTooltip(headerTag, "top");
    dateTag = appendMessageBlockParagraph(infoDiv, formatDate(data.date))
    dateTag.classList.add("message-block-date");
    infoTag = appendMessageBlockDescription(infoDiv, data.description);;

    // Button group
    buttonGroupTag = appendButtonGroup(tag);
    appendMessageBlockLink(buttonGroupTag, "Notes", getNotesLink(data.notes));
    appendMessageBlockLink(buttonGroupTag, "Audio", getAudioLink(data.audio));
    appendMessageBlockLink(buttonGroupTag, "Video", getVideoLink(data.video));
    return tag;
}

function getMessageSeries(seriesName, cb) {
    console.log(`Series name: ${seriesName}`);
    var seriesUri = encodeURIComponent(seriesName);
    var uri = `https://amazing-grace-pdx.azurewebsites.net/api/messages?series=${seriesUri}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

function populateMessageSeriesBlock(parentTag, series) {
    console.log(parentTag);
    if (series == null) {
        console.log("No messages found for series");
        return;
    }
    console.log(`Series: (${series.length} entries)`);
    console.log(series);
    console.log(typeof (series));
    series.forEach((message) => appendMessageBlock(parentTag, message));
}

messageSeriesBlocks = $("div.message-series-block");
console.log(`Number of message series blocks: ${messageSeriesBlocks.length}`);
messageSeriesBlocks.each((i, tag) => getMessageSeries(tag.id, data =>
    populateMessageSeriesBlock(tag, data)));
