// Requires 
//      utilities.js

class Carousel {
    constructor(id) {
        this.id = id;
        this.data = {};
        this.createElements();
    }

    createElements() {
        // Root
        this.root = document.createElement("div");
        this.root.id = this.id;
        this.root.classList.add("carousel");
        this.root.classList.add("slide");
        this.root.setAttribute("data-wrap", "false");
        this.root.setAttribute("data-interval", "false");

        // Item container
        this.inner = document.createElement("div");
        this.inner.classList.add("carousel-inner");
        this.root.appendChild(this.inner);

        // Indicators
        this.indicators = document.createElement("ol");
        this.indicators.classList.add("carousel-indicators");
        this.root.appendChild(this.indicators);

        // Previous button
        this.controlPrev = document.createElement("a");
        this.controlPrev.classList.add("carousel-control-prev");
        this.controlPrev.href = "#" + this.id;
        this.controlPrev.setAttribute("role", "button");
        this.controlPrev.setAttribute("data-slide", "prev");

        {
            var icon = document.createElement("span");
            icon.classList.add("carousel-control-prev-icon");
            icon.setAttribute("aria-hidden", "true");
            this.controlPrev.appendChild(icon);
        }
        this.root.appendChild(this.controlPrev);

        // Next button
        this.controlNext = document.createElement("a");
        this.controlNext.classList.add("carousel-control-next");
        this.controlNext.href = "#" + this.id;
        this.controlNext.setAttribute("role", "button");
        this.controlNext.setAttribute("data-slide", "next");

        {
            var icon = document.createElement("span");
            icon.classList.add("carousel-control-next-icon");
            icon.setAttribute("aria-hidden", "true");
            this.controlNext.appendChild(icon);
        }
        this.root.appendChild(this.controlNext);
    }

    getData(index) {
        return this.data[index];
    }

    addItem(item, caption, data) {
        console.log("addItem()");
        console.log(item);
        console.log(caption);
        console.log(data);
        var index = this.size();

        this.data[index] = data;

        // Create carousel item wrapper
        var carouselItem = document.createElement("div");
        carouselItem.classList.add("carousel-item");

        // Add caption
        var captionBlock = document.createElement("div");
        captionBlock.classList.add("carousel-caption");
        captionBlock.classList.add("d-none");
        captionBlock.classList.add("d-md-block");
        captionBlock.appendChild(caption);
        carouselItem.appendChild(captionBlock);

        // Add the item to the carousel item
        carouselItem.appendChild(item);

        // Add the carousel item to the carousel
        this.inner.appendChild(carouselItem);

        // Select this index if it is the first item
        if (index == 0) {
            carouselItem.classList.add("active");
        }

        // Add indicator
        var indicator = document.createElement("li");
        indicator.setAttribute("data-target", "#" + this.id);
        indicator.setAttribute("data-slide-to", index);
        if (index == 0) {
            indicator.classList.add("active");
        }
        this.indicators.appendChild(indicator);
    }

    size() {
        return this.inner.children.length;
    }

    select(n) {
        $(`#${this.id}`).carousel(n);
    }
}

function createMessageCaption(messageData) {
    var caption = document.createElement("div");

    var title = document.createElement("h4");
    title.innerHTML = messageData.title;

    var series = document.createElement("h5");
    series.innerHTML = messageData.series.name;

    var date = document.createElement("h5");
    date.innerHTML = formatDate(messageData.date);

    caption.appendChild(date);
    caption.appendChild(series);
    caption.appendChild(title);
    return caption;
}

function createMessageBlock(messageData, videoBlock, index) {

    var block = document.createElement("div");
    block.classList.add("ag-tile");

    var background = document.createElement("div");
    background.classList.add("ag-background");
    background.classList.add("ag-translucent");
    block.appendChild(background);

    block.appendChild(createSeparator("1rem", true));
    block.appendChild(createSeparator("1px", false));
    block.appendChild(createSeparator("1rem", true));

    // Notes
    var notesContainer = document.createElement("div")
    notesContainer.classList.add("ag-tile-container");
    block.appendChild(notesContainer);
    if (messageData.notes != null) {
        var notesBlock = createNotesBlock(messageData.notes.url);
        notesContainer.appendChild(notesBlock);
    } else {
        notesContainer.appendChild(createTextTag("h4", "Notes", ["ag-tile-title"]));
        var unavailableBlock = createTextTag("h5", "Unavailable");
        notesContainer.append(unavailableBlock);
    }

    block.appendChild(createSeparator("1rem", true));
    block.appendChild(createSeparator("1px", false));

    var mediaContainer = document.createElement("div");
    mediaContainer.classList.add("ag-tile-container");
    block.appendChild(mediaContainer);

    var mediaTabs = document.createElement("ul");
    mediaTabs.id = "media-tabs-message-" + messageData.id;
    mediaTabs.classList.add("nav");
    mediaTabs.classList.add("ag-btn-group");
    // mediaTabs.classList.add("nav-tabs");
    mediaTabs.classList.add("nav-pills");
    mediaTabs.setAttribute("role", "tablist");
    mediaContainer.appendChild(mediaTabs);
    mediaContainer.appendChild(createSeparator("0.5rem", true));


    var mediaTabContent = document.createElement("div");
    mediaTabContent.id = "media-tab-content-message-" + messageData.id;
    mediaTabContent.classList.add("tab-content");
    mediaContainer.appendChild(mediaTabContent);


    {
        // Video
        // Tab
        var videoTabItem = document.createElement("li");
        videoTabItem.classList.add("nav-item");
        mediaTabs.appendChild(videoTabItem);
        var videoTabLink = createTextTag("a", "Video");
        videoTabLink.id = "video-message-" + messageData.id + "-tab";
        videoTabLink.classList.add("nav-link");
        videoTabLink.classList.add("ag-btn");
        videoTabLink.classList.add("ag-btn-round");
        videoTabLink.href = "#video-message-" + messageData.id;
        videoTabLink.setAttribute("role", "tab");
        videoTabLink.setAttribute("data-toggle", "tab");
        videoTabLink.setAttribute("aria-controls", "video");
        videoTabLink.setAttribute("aria-selected", "false");
        videoTabItem.appendChild(videoTabLink);

        // Tab pane
        var videoTabPane = document.createElement("div");
        videoTabPane.id = "video-message-" + messageData.id;
        videoTabPane.classList.add("tab-pane");
        videoTabPane.classList.add("fade");
        videoTabPane.classList.add("active");
        videoTabPane.setAttribute("role", "tabpanel");
        videoTabPane.setAttribute("aria-labeledby", "video-message-" + messageData.id + "-tab");

        // videoContainer.appendChild(createTextTag("h4", "Video", ["ag-tile-title"]));
        if (messageData.video != null) {
            console.log("Creating video");
            $(document).ready(() =>
                $(document).on("slide.bs.carousel", (event) => {
                    console.log(event);
                    if (event.to == index) {
                        console.log(`Moving videoBlock to carousel item ${index}`)
                        console.log(videoBlock);
                        videoTabPane.appendChild(videoBlock);
                        $(videoTabLink).tab("show");
                    }
                }));
            videoTabPane.appendChild(videoBlock);
        } else {
            var unavailableBlock = createTextTag("h5", "Unavailable");
            videoTabPane.append(unavailableBlock);
        }
        mediaTabContent.appendChild(videoTabPane);

        // Audio
        // Tab
        var audioTabItem = document.createElement("li");
        audioTabItem.classList.add("nav-item");
        mediaTabs.appendChild(audioTabItem);
        var audioTabLink = createTextTag("a", "Audio");
        audioTabLink.id = "audio-message-" + messageData.id + "-tab";
        audioTabLink.classList.add("nav-link");
        audioTabLink.classList.add("ag-btn");
        audioTabLink.classList.add("ag-btn-round");
        audioTabLink.href = "#audio-message-" + messageData.id;
        audioTabLink.setAttribute("role", "tab");
        audioTabLink.setAttribute("data-toggle", "tab");
        audioTabLink.setAttribute("aria-controls", "audio");
        audioTabLink.setAttribute("aria-selected", "false");
        audioTabItem.appendChild(audioTabLink);

        // Tab pane
        var audioTabPane = document.createElement("div");
        audioTabPane.id = "audio-message-" + messageData.id;
        audioTabPane.classList.add("tab-pane");
        audioTabPane.classList.add("fade");
        audioTabPane.setAttribute("role", "tabpanel");
        audioTabPane.setAttribute("aria-labeledby", "audio-message-" + messageData.id + "-tab");
        if (messageData.audio != null) {
            var audioBlock = createAudioBlock(messageData.audio.downloadUrl);
            audioTabPane.appendChild(audioBlock);
        } else {
            var unavailableBlock = createTextTag("h5", "Unavailable");
            audioTabPane.append(unavailableBlock);
        }
        mediaTabContent.appendChild(audioTabPane);
    }


    return block;
}

function createTextTag(tagType, text, classList) {
    var tag = document.createElement(tagType);
    if (classList) {
        classList.forEach(class_ => tag.classList.add(class_));
    }
    tag.innerHTML = text;
    return tag;
}

function createNotesBlock(link) {
    var notesTag = document.createElement('a');
    notesTag.classList.add("ag-btn");
    notesTag.classList.add("ag-btn-round");
    notesTag.href = link;
    notesTag.text = "Sermon Notes & Branches";
    return notesTag;
}

function createPlayerFragment(playerElement, controls) {
    console.log("createPlayerFragment()");
    playerElement.id = "player";

    // var fragment = document.createDocumentFragment();
    var fragment = document.createElement("div");

    var block = document.createElement("div");
    block.style.textAlign = "center";

    var embededVideo = document.createElement("div");
    embededVideo.classList.add("ag-embed-responsive");

    var embededVideoItem = document.createElement("div");
    embededVideoItem.classList.add("ag-embed-responsive-item");

    var buttonBlock = controls.buttonGroup;

    embededVideoItem.appendChild(playerElement);
    embededVideo.appendChild(embededVideoItem);
    block.appendChild(embededVideo);
    block.appendChild(buttonBlock);

    fragment.appendChild(block);
    return fragment;
}

function createAudioBlock(link) {
    var audioTag = document.createElement('audio');
    audioTag.controls = "controls";
    audioTag.classList.add("embed-responsive");
    var sourceTag = document.createElement('source');
    sourceTag.src = link;
    sourceTag.type = "audio/mp3";
    audioTag.appendChild(sourceTag);
    return audioTag;
}

function createSeparator(height, isTransparent) {
    var sep = document.createElement("div");
    sep.classList.add("ag-separator");
    sep.style.height = height;
    if (isTransparent) {
        sep.classList.add("ag-transparent");
    }
    return sep;
}

function getMessages(n, cb) {
    console.log(`Number of messages to retrieve: ${n}`);
    var uri = `https://amazing-grace-pdx.azurewebsites.net/api/messages/latest?n=${n}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

function onResultsReady(results, carousel) {
    console.log("onResultsReady()");
    console.log(results);
    console.log(carousel);

    var playerId = "player";
    var playerElement = document.createElement("div", { "id": playerId });
    var messageVideoControls = new MessageVideoControls();
    var playerFragment = createPlayerFragment(playerElement, messageVideoControls);

    var allMessages = results["allMessages"].reverse();
    var index = 0;
    var lastMessageData = null;
    allMessages.forEach(messageData => {
        var messageBlock = createMessageBlock(messageData, playerFragment, index);
        var caption = createMessageCaption(messageData);
        carousel.addItem(messageBlock, caption, messageData);
        lastMessageData = messageData;
        index += 1;
    });
    messageVideoControls.setVideo(lastMessageData.video);
    $(document).ready(() => $(carousel.root).carousel(carousel.size() - 1));

    // Must create the player after the fragment has been added to each item
    var player = createPlayer(playerId,
        /*videoDetails*/
        null,
        /*onReady*/
        (event, player) => {
            console.log("onReady");
            console.log(event);
            messageVideoControls.setPlayer(player);
            messageVideoControls.cueVideo();
        },
        /*onStateChange*/
        (event) => {
            console.log("onStateChange");
        });

    $(document).ready(() => {
        $(document).on("slide.bs.carousel", (event) => {
            var data = carousel.getData(event.to);
            player.pauseVideo();
            messageVideoControls.setVideo(data.video);
        });
    });
}

container = $(".ag-carousel");
container.each((i, tag) => {
    var n = tag.getAttribute("n");
    var carouselId = tag.id + "_" + i;
    var carousel = new Carousel(carouselId);
    tag.appendChild(carousel.root);

    var videoBarrier = new Barrier(["youtubeApi", "allMessages"], (results) => onResultsReady(results, carousel));
    registerYouTubeIframeAPIReadyCallback(data => videoBarrier.addResult("youtubeApi", data));
    getMessages(n, allMessages => videoBarrier.addResult("allMessages", allMessages));
});

$(document).ready(() => {
    $(document).on("show.bs.tab", (event) => {
        console.log("show.bs.tab");
        console.log(event);
    });
    $(document).on("shown.bs.tab", (event) => {
        console.log("shown.bs.tab");
        console.log(event);
    });
});