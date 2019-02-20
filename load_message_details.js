if(typeof MessageDetailsCallbacks === "undefined"){
    var MessageDetailsCallbacks = [];
    function registerMessageDetailsCallback(cb){
        MessageDetailsCallbacks.push(cb);
    }
    
    function onMessageDetailsLoaded(data){
        console.log("onMessageDetailsLoaded");
        MessageDetailsCallbacks.forEach(cb => cb(data));
    }
    
    function loadMessageDetails() {    
        console.log("loadMessageDetails()");
        // TODO: Consider making this an argument
        dataUrl = "https://raw.githubusercontent.com/taylort7147/amazing-grace-pdx/master/message_details.json";
        $.getJSON(dataUrl, onMessageDetailsLoaded);
    }
    
    function injectMessageDetailsLoadScript(){
        console.log("injectMessageDetailsLoadScript()");
        var tag = document.createElement('script');
        tag.type = "application/javascript";
        tag.innerHTML = "loadMessageDetails();"
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    }
}