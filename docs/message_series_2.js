// Requires 
//      utilities.js

function appendSeparator(tag) {
    var sepTag = document.createElement("div");
    sepTag.className = "ag-separator";
    tag.appendChild(sepTag);

    return sepTag;
}

function appendMessageBlockHeader(tag, data) {
    // Container
    var headerTag = document.createElement("div");
    headerTag.className = "ag-message-block-header";
    tag.appendChild(headerTag);

    // Title
    var titleTag = document.createElement("div");
    titleTag.className = "ag-message-block-title";
    titleTag.innerHTML = data.title;
    headerTag.appendChild(titleTag);

    // Date
    var dateTag = document.createElement("div");
    dateTag.className = "ag-message-block-date";
    dateTag.innerHTML = formatDate(data.date);
    headerTag.appendChild(dateTag);

    return headerTag;
}

function appendMessageBlockBody(tag, data) {
    // Container
    var bodyTag = document.createElement("div");
    bodyTag.className = "ag-message-block-body";
    tag.appendChild(bodyTag);
    
    // Description
    if(data.description && data.description.length > 0) {
        var descriptionTag = document.createElement("span");
        descriptionTag.className = "ag-message-block-description";
        descriptionTag.innerHTML = data.description;
        bodyTag.appendChild(descriptionTag);
    }
    
    // Separator
    if(data.description && data.description.length > 0 && data.bibleReferencesStringList.length > 0) {
        appendSeparator(bodyTag);
    }

    // Bible references
    if(data.bibleReferencesStringList.length > 0) {
        var bibTag = document.createElement("div");
        bibTag.className = "ag-message-block-bible-references";
        bodyTag.appendChild(bibTag);
        
        // Bible references title
        var bibTitleTag = document.createElement("span");
        bibTitleTag.innerHTML = "Bible References";
        bibTag.appendChild(bibTitleTag);
        
        // Bible references list
        var bibListTag = document.createElement("ul");
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

function appendBubbleGroup(tag)
{
    var bubbleGroupTag = document.createElement("div");
    bubbleGroupTag.className = "ag-bubble-group";
    tag.appendChild(bubbleGroupTag);
    return bubbleGroupTag;
}

function appendBubble(tag, linkUrl, btnClass) {
    var hasLink = linkUrl && linkUrl.length > 0;
    var bubbleContainerTag = document.createElement("div");
    bubbleContainerTag.className = "ag-bubble";
    tag.appendChild(bubbleContainerTag);

    if(hasLink) {
        // Button
        var bubbleTag = document.createElement("a");
        bubbleTag.className = "btn ag-bubble-btn";
        bubbleTag.classList.add(btnClass);
        bubbleTag.role = "button";
        bubbleTag.href = linkUrl;
        bubbleContainerTag.appendChild(bubbleTag);

        // Background
        var bubbleBackgroundTag = document.createElement("div");
        bubbleBackgroundTag.className = "ag-bubble-background";
        bubbleContainerTag.appendChild(bubbleBackgroundTag);
    }
    else {
        bubbleContainerTag.style.display = "none";
    }
    return bubbleContainerTag;
}

function appendMessageBlockFooter(tag, data) {
    var footerTag = document.createElement("div");
    footerTag.className = "ag-message-block-footer";
    tag.appendChild(footerTag);

    var bubbleGroupTag = appendBubbleGroup(footerTag);
    bubbleGroupTag.classList.add("ag-container-align-center");

    appendBubble(bubbleGroupTag, getNotesLink(data.notes), "ag-notes-background");
    appendBubble(bubbleGroupTag, getAudioLink(data.audio), "ag-audio-background");
    appendBubble(bubbleGroupTag, getVideoLink(data.video), "ag-video-background");
    
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

    // Image
    if(data.videoId) {
        var imageContainerTag = document.createElement("div");
        imageContainerTag.className = "ag-message-block-background-container ag-border-clip";
        tag.appendChild(imageContainerTag);

        var imageTag = document.createElement("div");
        imageTag.className = "ag-message-block-background";
        imageTag.style.backgroundImage = `url(${getVideoThumbnailLink(data.video)})`
        imageContainerTag.appendChild(imageTag)


        var imageOverlayTag = document.createElement("div");
        imageOverlayTag.className = "ag-message-block-background-overlay";
        imageContainerTag.appendChild(imageOverlayTag);
    }

    // Header
    appendMessageBlockHeader(tag, data);

    // Body
    if(data.description || data.bibleReferencesStringList.length > 0) {
        appendMessageBlockBody(tag, data);
    }

    // Footer
    appendMessageBlockFooter(tag, data);
    
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
    var messageSeriesBlocks = $("div.message-series-block");
    console.log(`Number of message series blocks: ${messageSeriesBlocks.length}`);
    messageSeriesBlocks.each((i, tag) => getMessageSeries(tag.id, data =>
        populateMessageSeriesBlock(tag, data)));
});