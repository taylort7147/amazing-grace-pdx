// Requires 
//      utilities.js



function getMessageSeries(seriesName, cb) {
    console.log(`Series name: ${seriesName}`);
    var seriesUri = encodeURIComponent(seriesName);
    var uri = `https://amazing-grace-pdx.azurewebsites.net/api/messages?series=${seriesUri}`
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
        var carouselItem = document.createElement("div");
        carouselItem.classList.add("carousel-item");
        carouselItem.appendChild(item);
        this.inner.appendChild(carouselItem);

        if (index == 0) {
            carouselItem.classList.add("active");
        }

        // var image = document.createElement("img");
        // image.classList.add("d-block");
        // image.classList.add("w-100");
        // image.src = "https://media.pitchfork.com/photos/5bf2f92403d2bf08410e7e57/1:1/w_320/iron%20maiden_piece%20of%20mind.jpg"
        // carouselItem.appendChild(image);

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
}

function createMessageBlock(messageData) {
    console.log(messageData);

    var block = document.createElement("p");
    block.innerHTML = messageData.title;
    block.style.width = 400;
    block.style.height = 300;
    return block;
}

container = $(".ag-carousel");
container.each((i, tag) => {
    var series = tag.getAttribute("series");
    var carouselId = tag.id + "_" + i;
    var carousel = new Carousel(carouselId);
    tag.appendChild(carousel.root);

    getMessageSeries(series, seriesData => {
        seriesData.forEach(messageData => {
            var messageBlock = createMessageBlock(messageData);
            carousel.addItem(messageBlock, messageData.title);

        });
    });
});