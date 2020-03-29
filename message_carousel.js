// Requires 
//      utilities.js



function getMessages(n, cb) {
    console.log(`Number of messages to retrieve: ${n}`);
    var uri = `https://amazing-grace-pdx.azurewebsites.net/api/messages/latest?n=${n}`
    console.log(`URI: ${uri}`);
    $.getJSON(uri, cb);
}

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

    addItem(item, captionText) {
        var index = this.size();
        console.log(index);
        // Create carousel item wrapper
        var carouselItem = document.createElement("div");
        carouselItem.classList.add("carousel-item");

        // Add the item to the carousel item
        carouselItem.appendChild(item);

        // Add the carousel item to the carousel
        this.inner.appendChild(carouselItem);

        // Select this index if it is the first item
        if (index == 0) {
            carouselItem.classList.add("active");
        }

        // Add caption
        var caption = document.createElement("div");
        caption.classList.add("carousel-caption");
        caption.classList.add("d-none");
        caption.classList.add("d-md-block");
        carouselItem.appendChild(caption);

        var captionHeader = document.createElement("h5");
        captionHeader.innerHTML = captionText;
        caption.appendChild(captionHeader);

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

function createMessageBlock(messageData) {
    console.log(messageData);

    var block = document.createElement("div");
    block.classList.add("ag-tile");

    var background = document.createElement("div");
    background.classList.add("ag-background");
    background.classList.add("ag-translucent");
    block.appendChild(background);

    var videoBlock = document.createElement("div");
    var width = 95;
    var height = width * 9 / 16;
    videoBlock.style.width = `${width}%`;
    videoBlock.style.height = "300px"; //`${height}%`;
    videoBlock.style.display = "block";
    videoBlock.style.background = "black";
    videoBlock.style.textAlign = "center";
    videoBlock.style.color = "white";
    videoBlock.style.margin = "auto";
    videoBlock.innerHTML = "Video";
    block.appendChild(videoBlock);

    block.appendChild(document.createElement("br"));

    var audioBlock = document.createElement("div");
    var width = 95;
    var height = width * 9 / 16;
    audioBlock.style.width = `${width}%`;
    audioBlock.style.height = "50px"; //`${height}%`;
    audioBlock.style.display = "block";
    audioBlock.style.background = "black";
    audioBlock.style.textAlign = "center";
    audioBlock.style.color = "white";
    audioBlock.style.margin = "auto";
    audioBlock.innerHTML = "Audio";
    block.appendChild(audioBlock);

    return block;
}

container = $(".ag-carousel");
container.each((i, tag) => {
    var n = tag.getAttribute("n");
    var carouselId = tag.id + "_" + i;
    var carousel = new Carousel(carouselId);
    tag.appendChild(carousel.root);

    getMessages(n, seriesData => {
        seriesData.forEach(messageData => {
            var messageBlock = createMessageBlock(messageData);
            carousel.addItem(messageBlock, messageData.title);
        });
        $(carousel.root).carousel(carousel.size() - 1);
    });
});