// Requires 
//      utilities.js

function appendSeparator(tag) {
    sepTag = document.createElement("div");
    sepTag.className = "ag-separator";
    tag.appendChild(sepTag);

    return sepTag;
}

function appendMessageBlockHeader(tag, data) {
    // Container
    headerTag = document.createElement("div");
    headerTag.className = "ag-message-block-header";
    tag.appendChild(headerTag);

    // Title
    titleTag = document.createElement("div");
    titleTag.className = "ag-message-block-title";
    titleTag.innerHTML = data.title;
    headerTag.appendChild(titleTag);

    // Date
    dateTag = document.createElement("div");
    dateTag.className = "ag-message-block-date";
    dateTag.innerHTML = formatDate(data.date);
    headerTag.appendChild(dateTag);

    return headerTag;
}

function appendMessageBlockBody(tag, data) {
    // Container
    bodyTag = document.createElement("div");
    bodyTag.className = "ag-message-block-body";
    tag.appendChild(bodyTag);
    
    // Description
    descriptionTag = document.createElement("span");
    descriptionTag.className = "ag-message-block-description";
    descriptionTag.innerHTML = data.description;
    bodyTag.appendChild(descriptionTag);
    
    if(data.bibleReferencesStringList.length > 0) {
        appendSeparator(bodyTag);
        
        // Bible references
        bibTag = document.createElement("div");
        bibTag.className = "ag-message-block-bible-references";
        bodyTag.appendChild(bibTag);
        
        // Bible references title
        bibTitleTag = document.createElement("span");
        bibTitleTag.innerHTML = "Bible References";
        bibTag.appendChild(bibTitleTag);
        
        // Bible references list
        bibListTag = document.createElement("ul");
        bibTag.appendChild(bibListTag);
        
        // Bible references list elements
        function f(listTag) {
            function fInner(bibleReference) {
                var listItemTag = document.createElement("li");
                listItemTag.innerHTML = bibleReference;
                listTag.appendChild(listItemTag);
            };
            return fInner;
        };
        
        data.bibleReferencesStringList.forEach(f(bibListTag));
    }
    return bodyTag;
}

function appendMessageBlockFooter(tag, data) {
    footerTag = document.createElement("div");
    footerTag.className = "ag-message-block-footer";
    tag.appendChild(footerTag);
    
    buttonGroupTag = appendButtonGroup(footerTag);
    appendButton(buttonGroupTag, "Notes", getNotesLink(data.notes));
    appendButton(buttonGroupTag, "Audio", getAudioLink(data.audio));
    appendButton(buttonGroupTag, "Video", getVideoLink(data.video));
    
    return footerTag;
}

function appendButton(tag, text, link) {
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
    buttonGroupTag.className = "btn-group ag-message-block-btn-group";
    tag.appendChild(buttonGroupTag);
    return buttonGroupTag;
}

function getVideoThumbnailLink(details) {
    if (details && details.youTubeVideoId && details.youTubeVideoId.length > 0)
    return `https://img.youtube.com/vi/${details.youTubeVideoId}/0.jpg`;

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

function appendMessageBlock(parentTag, data) {
    console.log(`appendMessageBlock() called for ${parentTag.id}`);
    if (data == null) { return; }

    // Tag
    var tag = document.createElement("div");
    tag.classList.add("ag-message-block");
    parentTag.appendChild(tag);

    // Click handler
    var clickTag = document.createElement("div");
    clickTag.onclick = x => document.getElementById("ag-message-panel").style.display = "block";
    clickTag.className = "ag-message-block-overlay";
    tag.appendChild(clickTag);

    // Image
    imageTag = document.createElement("div");
    imageTag.className = "ag-message-block-background";
    imageTag.style.backgroundImage = getVideoThumbnailLink(data.video);
    imageTag.style.backgroundImage = `url(${getVideoThumbnailLink(data.video)})`
    imageTag.style.backgroundSize = "150%";
    imageTag.style.backgroundPosition = "50%";
    tag.appendChild(imageTag)

    // Header
    headerTag = appendMessageBlockHeader(tag, data);

    // Body
    bodyTag = appendMessageBlockBody(tag, data);

    // Footer
    footerTag = appendMessageBlockFooter(tag, data);

    // Button group
    
    
    return tag;
}

function getMessageSeries(seriesName, cb) {
    console.log(`Series name: ${seriesName}`);
    var seriesUri = encodeURIComponent(seriesName);
    var uri = `https://amazing-grace-pdx-web-app.azurewebsites.net/api/messages?series=${seriesUri}`
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
    console.log(typeof(series));
    series.forEach((message) => appendMessageBlock(parentTag, message));
}

$(document).ready(function() {
    messageSeriesBlocks = $("div.message-series-block");
    console.log(`Number of message series blocks: ${messageSeriesBlocks.length}`);
    messageSeriesBlocks.each((i, tag) => getMessageSeries(tag.id, data =>
        populateMessageSeriesBlock(tag, data)));
});