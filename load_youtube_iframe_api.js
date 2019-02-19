if(typeof YouTubeIframeAPIReadyCallbacks === "undefined"){
    YouTubeIframeAPIReadyCallbacks = [];
    function registerYouTubeIframeAPIReadyCallback(cb){
        YouTubeIframeAPIReadyCallbacks.push(cb);
    }

    function injectYouTubeIframeAPILoadScript(){
        console.log("injectYouTubeIframeAPILoadScript()");
        var tag = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        tag.type="application/javascript";
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    }

    function onYouTubeIframeAPIReady() {  
        console.log("onYouTubeIframeAPIReady()");
        YouTubeIframeAPIReadyCallbacks.forEach(callback => callback());
    }
    
    injectYouTubeIframeAPILoadScript();
}