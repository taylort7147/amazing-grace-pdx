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

    if (messageData.video != null) {
        console.log("Creating video");
        $(document).ready(() =>
            $(document).on("slide.bs.carousel", (event) => {
                console.log(event);
                if (event.to == index) {
                    console.log(`Moving videoBlock to carousel item ${index}`)
                    console.log(block);
                    console.log(videoBlock);
                    block.appendChild(videoBlock);
                }
            }));
    } else {
        var videoUnavailableBlock = document.createElement("div");
        var header = document.createElement("h5");
        header.innerHTML = "Video Unavailable";
        videoUnavailableBlock.appendChild(header);
        block.appendChild(videoUnavailableBlock);
    }



    block.appendChild(videoBlock); // Add video to newest block by default
    return block;
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

function getMessages(n, cb) {
    console.log(`Number of messages to retrieve: ${n}`);
    var uri = `https://amazing-grace-pdx.azurewebsites.net/api/messages/latest?n=${n}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

function onResultsReady(results, carousel) {
    console.log("onResultsReady()");

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