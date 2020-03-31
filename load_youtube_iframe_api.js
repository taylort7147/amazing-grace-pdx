if (typeof YouTubeIframeAPILoaded === "undefined") {
    console.log("Setting up YouTubeIframeAPI handlers");
    YouTubeIframeAPIReadyCallbacks = [];
    YouTubeIframeAPILoaded = false;

    function registerYouTubeIframeAPIReadyCallback(cb) {
        console.log("registerYouTubeIframeAPIReadyCallback()");
        if (YouTubeIframeAPILoaded) {
            cb(); // Call the API is ready
        } else {
            YouTubeIframeAPIReadyCallbacks.push(cb);
        }
    }

    function onYouTubeIframeAPIReady() {
        console.log("onYouTubeIframeAPIReady()");
        YouTubeIframeAPILoaded = true;
        YouTubeIframeAPIReadyCallbacks.forEach(callback => callback());
    }

    function loadYouTubeIframeAPI() {
        if (YouTubeIframeAPILoaded) { return; }
        YouTubeIframeAPILoaded = true;
        console.log("injectYouTubeIframeAPILoadScript()");
        var tag = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        tag.type = "application/javascript";
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    }
}