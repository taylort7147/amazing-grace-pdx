if (typeof YouTubeIframeAPILoaded === "undefined") {
    console.log("Setting up YouTubeIframeAPI handlers");
    YouTubeIframeAPIReadyCallbacks = [];
    YouTubeIframeAPILoaded = false;

    function registerYouTubeIframeAPIReadyCallback(cb) {
        console.log("registerYouTubeIframeAPIReadyCallback()");
        console.log(YouTubeIframeAPILoaded);
        if (YouTubeIframeAPILoaded) {
            console.log("YouTubeIframeAPI is already loaded. Calling callback directly.");
            cb(); // Call the API is ready
        } else {
            console.log("Adding YouTubeIframeAPIReady callback.");
            YouTubeIframeAPIReadyCallbacks.push(cb);
        }
    }

    function onYouTubeIframeAPIReady() {
        console.log("onYouTubeIframeAPIReady()");
        if (YouTubeIframeAPILoaded) {
            return;
        }
        console.log("Calling YouTubeIframeAPIReady callbacks");
        YouTubeIframeAPILoaded = true;
        YouTubeIframeAPIReadyCallbacks.forEach(callback => callback());
    }

    function loadYouTubeIframeAPI() {
        if (YouTubeIframeAPILoaded) { return; }
        console.log("injectYouTubeIframeAPILoadScript()");
        var tag = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        tag.type = "application/javascript";
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    }
}