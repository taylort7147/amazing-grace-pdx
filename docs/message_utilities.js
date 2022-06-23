// Requires 
//      utilities.js


/**
 * Append a loading block to an element
 * @param {*} parentTag The element to append the loading block to
 * @returns The new "loading" element
 */
 function appendLoadingBlock(parentTag) {
    var loadingTag = document.createElement("h3");
    loadingTag.className = "ag-loading";
    loadingTag.innerHTML = "Loading";
    parentTag.appendChild(loadingTag);
    return loadingTag;
}

/**
 * Remove a loading block from the document
 * @param {*} tag The loading block to remove
 * @returns null
 */
function removeLoadingBlock(tag) {
    if(tag == null) {
        return;
    }
    tag.remove();
}

/**
 * Appends a separator to an element
 * @param {*} parentTag The element to append the separator to
 * @returns The new separator element
 */
function appendSeparator(parentTag) {
    var sepTag = document.createElement("div");
    sepTag.className = "ag-separator";
    parentTag.appendChild(sepTag);

    return sepTag;
}

/**
 * Appends a message block header to an element
 * @param {*} parentTag The element to append the header to 
 * @param {*} message Message returned by /api/messages/{id}
 * @returns The new header element
 */
function appendMessageBlockHeader(parentTag, message) {
    // Container
    var headerTag = document.createElement("div");
    headerTag.className = "ag-message-block-header";
    parentTag.appendChild(headerTag);

    // Title
    var titleTag = document.createElement("div");
    titleTag.className = "ag-message-block-title";
    titleTag.innerHTML = message.title;
    headerTag.appendChild(titleTag);

    // Date
    var dateTag = document.createElement("div");
    dateTag.className = "ag-message-block-date";
    dateTag.innerHTML = formatDate(message.date);
    headerTag.appendChild(dateTag);

    return headerTag;
}

/**
 * Appends a message block body to an element
 * @param {*} parentTag The element to append the body to
 * @param {*} message Message returned by /api/messages/{id}
 * @returns The new block element
 */
function appendMessageBlockBody(parentTag, message) {
    // Container
    var bodyTag = document.createElement("div");
    bodyTag.className = "ag-message-block-body";
    parentTag.appendChild(bodyTag);
    
    // Description
    if(message.description && message.description.length > 0) {
        var descriptionTag = document.createElement("span");
        descriptionTag.className = "ag-message-block-description";
        descriptionTag.innerHTML = message.description;
        bodyTag.appendChild(descriptionTag);
    }
    
    // Separator
    if(message.description && message.description.length > 0 && message.bibleReferencesStringList.length > 0) {
        appendSeparator(bodyTag);
    }

    // Bible references
    if(message.bibleReferencesStringList.length > 0) {
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
                listItemTag.className = "ag-message-block-bible-reference";
                listTag.appendChild(listItemTag);
            };
            return fInner;
        };
        
        message.bibleReferencesStringList.forEach(f(bibListTag));
    }
    return bodyTag;
}

/**
 * Appends a message block footer to an element 
 * @param {*} parentTag The element to append the footer to
 * @param {*} message Message returned by /api/messages/{id}
 * @returns The new footer element
 */
function appendMessageBlockFooter(parentTag, message) {
    var footerTag = document.createElement("div");
    footerTag.className = "ag-message-block-footer";
    parentTag.appendChild(footerTag);

    var bubbleGroupTag = appendBubbleGroup(footerTag);
    bubbleGroupTag.classList.add("ag-container-align-center");

    appendBubble(bubbleGroupTag, getNotesLink(message.notes), "ag-notes-background");
    appendBubble(bubbleGroupTag, getAudioLink(message.audio), "ag-audio-background");
    appendBubble(bubbleGroupTag, getVideoLink(message.video), "ag-video-background");
    
    return footerTag;
}

/**
 * Appends a bubble group to an element
 * @param {*} parentTag The element to append the bubble group to
 * @returns The new bubble group element
 */
function appendBubbleGroup(parentTag)
{
    var bubbleGroupTag = document.createElement("div");
    bubbleGroupTag.className = "ag-bubble-group";
    parentTag.appendChild(bubbleGroupTag);
    return bubbleGroupTag;
}

/**
 * Appends a bubble to an element
 * @param {*} parentTag The element to append the bubble to
 * @param {*} linkUrl The href link for the bubble
 * @param {*} btnClass A class name to apply to the bubble button
 * @returns The new bubble element
 */
function appendBubble(parentTag, linkUrl, btnClass) {
    var hasLink = linkUrl && linkUrl.length > 0;
    var bubbleContainerTag = document.createElement("div");
    bubbleContainerTag.className = "ag-bubble";
    parentTag.appendChild(bubbleContainerTag);

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

/**
 * Gets a URL to the thumbnail for a video
 * @param {*} details A video object (from a message object) 
 * @returns A video thumbnail URL
 */
function getVideoThumbnailLink(details) {
    if (details && details.youTubeVideoId && details.youTubeVideoId.length > 0)
    return `https://img.youtube.com/vi/${details.youTubeVideoId}/0.jpg`;

}

/**
 * Gets a URL to the message video
 * @param {*} details A video object (from a message object) 
 * @returns A video URL
 */
function getVideoLink(details) {
    if (details && details.youTubeVideoId && details.youTubeVideoId.length > 0)
        return `https://www.youtube.com/watch?v=${details.youTubeVideoId}&t=${details.messageStartTimeSeconds}`;
}

/**
 * Gets a URL to an audio stream
 * @param {*} details An audio object (from a message object)
 * @returns An audio URL 
 */
function getAudioLink(details) {
    if (details && details.streamUrl && details.streamUrl.length > 0)
        return details.streamUrl;
}

/**
 * Gets a URL to the message notes
 * @param {*} details A notes object (from a message object)
 * @returns A notes URL
 */
function getNotesLink(details) {
    if (details && details.url && details.url.length > 0)
        return details.url;
}

/**
 * Appends a hidden message block placeholder to be populated by populateMessageBlock()
 * @param {*} parentTag The parent tag to append the message block to
 * @param {*} message Message returned by /api/messages/{id}
 * @returns The newly created message block element
 */
function appendMessageBlock(parentTag, message) {
    console.log(`appendMessageBlock() called for ${parentTag.id}`);
    if (message == null) { return; }

    // Tag
    var tag = document.createElement("div");
    tag.classList.add("ag-message-block");
    tag.classList.add("hidden");
    parentTag.appendChild(tag);

    return tag;
}

/**
 * Populates the provided message block with the message details.
 * @param {*} tag The message block element
 * @param {*} message 
 * @returns The original message block element
 */
function populateMessageBlock(tag, message) {
    tag.classList.remove("hidden");
    // Image
    var imageContainerTag = document.createElement("div");
    imageContainerTag.className = "ag-message-block-background-container ag-border-clip";
    tag.appendChild(imageContainerTag);
    
    var imageTag = document.createElement("div");
    imageTag.className = "ag-message-block-background";
    if(message.videoId) {
        imageTag.style.backgroundImage = `url(${getVideoThumbnailLink(message.video)})`
    }
    imageContainerTag.appendChild(imageTag)

    var imageOverlayTag = document.createElement("div");
    imageOverlayTag.className = "ag-message-block-background-overlay";
    imageContainerTag.appendChild(imageOverlayTag);

    // Header
    appendMessageBlockHeader(tag, message);

    // Body
    if(message.description || message.bibleReferencesStringList.length > 0) {
        appendMessageBlockBody(tag, message);
    }

    // Footer
    appendMessageBlockFooter(tag, message);

    return tag;
}

function highlightText(messageTag, text) {
    console.log(`Highlight text: ${text}`);
    var targetTags = [];
    targetTags = targetTags.concat(Array.from(messageTag.getElementsByClassName("ag-message-block-title")));
    targetTags = targetTags.concat(Array.from(messageTag.getElementsByClassName("ag-message-block-description")));
    targetTags = targetTags.concat(Array.from(messageTag.getElementsByClassName("ag-message-block-bible-reference")));
    targetTags.forEach(t => {
        var re = new RegExp(text, "i");
        t.innerHTML = t.innerHTML.replace(re, "<span style=\"font-weight:bold\">$&</span>");
        console.log(t.innerHTML);
    });

}

function populateSearchResults(parentTag, messages, highlightTexts) {
    messages.forEach(m => {
        loadMessage(parentTag, m.id, messageTag => {
            // Reverse sort the list to make sure supersets are highlighted first
            sortedHighlightedTexts = highlightTexts.sort().reverse(); 
            highlightTexts.forEach(text => highlightText(messageTag, text));
        });
    });
}

// 
/**
 * Populates a message series block by appending message blocks and loading 
 * their details
 * @param {*} seriesTag The series element to populate
 * @param {*} series  Series returned by /api/series/{name}
 * @returns The original series element
 */
function populateMessageSeriesBlock(seriesTag, series) {
    console.log(seriesTag);
    if (series == null) {
        console.log("No series found");
        return;
    }
    if (series.messages == null || series.messages.length == 0) {
        console.log("No messages found for series");
        return;
    }

    var titleTag = document.createElement("h1");
    titleTag.className = "ag-series-title";
    titleTag.innerHTML = series.name;
    seriesTag.appendChild(titleTag);

    var descriptionTag = document.createElement("span");
    descriptionTag.classList = "ag-series-description";
    descriptionTag.innerHTML = series.description;
    seriesTag.appendChild(descriptionTag);

    console.log(`Series: (${series.messages.length} entries)`);
    console.log(series.messages);
    series.messages.forEach(message => {
        loadMessage(seriesTag, message.id);
    });

    return seriesTag
}

/**
 * Gets a series' details without loading message content and invokes a callback
 * @param {*} seriesName The name of the series in the database
 * @param {*} cb JQuery callback to invoke with the returned series
 */
function getMessageSeries(seriesName, cb) {
    console.log(`Series name: ${seriesName}`);
    var seriesUri = encodeURI(seriesName);
    // TODO: Actually load the series, not just the messages.
    var uri = `https://amazing-grace-pdx-web-app.azurewebsites.net/api/series/byname?name=${seriesUri}&loadMessages=true`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

/**
 * Gets a single message's details and invokes a callback
 * @param {*} messageId The ID of the message in the database
 * @param {*} cb JQuery callback to invoke with the returned message
 */
function getMessage(messageId, cb) {
    console.log(`Getting message: ${messageId}`);
    var seriesUri = encodeURIComponent(messageId);
    var uri = `https://amazing-grace-pdx-web-app.azurewebsites.net/api/messages/${seriesUri}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

/**
 * Loads a collection of messages based on search text and invokes a callback
 * @param {*} searchText The text to search for
 * @param {*} cb JQuery callback to invoke with the returned messages
 */
function getSearchResults(searchText, cb) {
    console.log(`Search: ${searchText}`);
    var searchUri = encodeURIComponent(searchText);
    var uri = `https://amazing-grace-pdx-web-app.azurewebsites.net/api/messages/search?searchText=${searchUri}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

/**
 * Loads a series by name into an element
 * @param {*} tag The element to load the series into
 * @param {*} seriesName The name of the series in the database
 */
function loadSeries(tag, seriesName) {
    getMessageSeries(seriesName, series => populateMessageSeriesBlock(tag, series));
}

/**
 * Loads a message by ID into an element
 * @param {*} tag The element to load the message into
 * @param {*} messageId The ID of the message in the database
 * @param {*} cb Callback to invoke with the loaded message
 */
function loadMessage(tag, messageId, cb) {
    var messageTag = appendMessageBlock(tag, messageId);
    getMessage(messageId, data => {
        populateMessageBlock(messageTag, data);
        if(cb)
        {
            cb(messageTag);
        }
    });
}

function loadSearchResults(tag, searchText) {
    var loadingTag = appendLoadingBlock(tag);
    getSearchResults(searchText, result => {
        removeLoadingBlock(loadingTag);

        if(result.messages.length > 0) {
            var clearSearchTag = document.createElement("button");
            clearSearchTag.className = "btn btn-secondary ag-clear ag-center";
            clearSearchTag.innerHTML = "Clear Results";
            clearSearchTag.onclick = e => removeChildElements(tag);
            tag.appendChild(clearSearchTag);
        }

        processSearchResults(tag, searchText, result);

    });
}

function processSearchResults(tag, searchText, result) {
    console.log("Search result:");
    console.log(result);
    var searchTextList = searchText.split();
    console.log("Search text list: ");
    console.log(searchTextList); 
    if(result.success) {
        if(result.messages.length == 0) {
            var noResultsTag = document.createElement("span");
            noResultsTag.innerHTML = "No results found";
            tag.appendChild(noResultsTag);
        }
        else{
            var matchingText = searchTextList.concat(result.matchingBibleReferences)
            populateSearchResults(tag, result.messages, matchingText);
        }
    }
    else {
        var ul = document.createElement("ul");
        ul.className = "ag-error";
        tag.appendChild(ul);

        result.errors.forEach(e => {
            var li = document.createElement("li");
            li.className = "ag-error-item";
            li.innerHTML = e;
            ul.appendChild(li);
        });
    }
}