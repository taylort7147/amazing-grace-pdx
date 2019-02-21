if(typeof YouTubeIframeAPIReadyCallbacks === "undefined"){
    
    YouTubeIframeAPIReadyCallbacks = [];
    YouTubeIframeAPILoaded = false;
    
    function registerYouTubeIframeAPIReadyCallback(cb){
        YouTubeIframeAPIReadyCallbacks.push(cb);
    }

    function onYouTubeIframeAPIReady() {  
        console.log("onYouTubeIframeAPIReady()");
        YouTubeIframeAPIReadyCallbacks.forEach(callback => callback());
    }

    function loadYouTubeIframeAPI(){
        if(YouTubeIframeAPILoaded) { return; }
        YouTubeIframeAPILoaded = true;
        console.log("injectYouTubeIframeAPILoadScript()");
        var tag = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        tag.type="application/javascript";
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    }
}