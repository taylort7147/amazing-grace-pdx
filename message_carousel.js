// Requires 
//      utilities.js




class Carousel {
    constructor(id) {
        this.id = id;
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

    addItem(item, caption) {
        var index = this.size();
        console.log(index);
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

function createVideoBlock(messageData) {
    /*
    <h3>Video <span id="latest-message-video-details">[+]</span></h3>

    <div align="center" class="" >
        <!-- YouTube iframe container -->
        <div class="ag-embed-responsive">
            <div id="latest-message-video" class="ag-embed-responsive-item"></div>
        </div>

        <!-- Controls for the YouTube player -->
        <div id="latest-message-video-controls" class="ag-btn-group ag-center" role="group" aria-label="Player controls">
            <button id="latest-message-video-controls-jump-to-beginning" type="button" class="ag-btn ag-btn-round">Jump To Beginning</button>
            <button id="latest-message-video-controls-jump-to-message" type="button" class="ag-btn ag-btn-round">Jump To Message</button>
        </div>
    </div>

    <script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/latest_message_video.js"></script>
    */
    var videoData = messageData.video;

    var block = document.createElement("div");
    block.style.textAlign = "center";

    var embededVideo = document.createElement("div");
    embededVideo.classList.add("ag-embed-responsive");

    var embededVideoItem = document.createElement("div");
    embededVideoItem.classList.add("ag-embed-responsive-item");
    embededVideoItem.id = "embeded_video_" + videoData.youTubeVideoId;
    var player = createPlayer(embededVideoItem.id, videoData);

    var buttonBlock = createButtons(player, videoData);

    embededVideo.appendChild(embededVideoItem);
    block.appendChild(embededVideo);
    block.appendChild(buttonBlock);

    return block;
}

function createMessageCaption(messageData) {
    var caption = document.createElement("div");

    var title = document.createElement("h5");
    title.innerHTML = messageData.title;

    var series = document.createElement("h6");
    series.innerHTML = messageData.series.name;

    var date = document.createElementNS("h6");
    data.innerHTML = messageData.date;

    caption.appendChild(title);
    caption.appendChild(series);
    caption.appendChild(date);
    return caption;
}

function createMessageBlock(messageData) {
    console.log(messageData);

    var block = document.createElement("div");
    block.classList.add("ag-tile");

    var background = document.createElement("div");
    background.classList.add("ag-background");
    background.classList.add("ag-translucent");
    block.appendChild(background);
    if (messageData.video != null) {
        console.log("Creating video");
        console.log(messageData.video);
        var videoBlock = createVideoBlock(messageData);
        block.appendChild(videoBlock);
    }
    return block;
}

function getMessages(n, cb) {
    console.log(`Number of messages to retrieve: ${n}`);
    var uri = `https://amazing-grace-pdx.azurewebsites.net/api/messages/latest?n=${n}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

function onResultsReady(results, carousel) {
    console.log("onResultsReady()");
    var allMessages = results["allMessages"];
    allMessages.forEach(messageData => {
        var messageBlock = createMessageBlock(messageData);
        var caption = createMessageCaption(messageData);
        carousel.addItem(messageBlock, caption);
    });
    $(carousel.root).carousel(carousel.size() - 1);
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