// Requires 
//      utilities.js
//      message_utilities.js


function populateSearchContainer(parentTag, id){
    var anchorTag = document.createElement("a");
    anchorTag.id = "top";
    parentTag.appendChild(anchorTag);

    var searchBoxTag = document.createElement("div");
    searchBoxTag.className = "input-group mb-3 ag-search";
    parentTag.appendChild(searchBoxTag);

    var formTag = document.createElement("div");
    formTag.className = "form-inline";
    searchBoxTag.appendChild(formTag);

    var inputTag = document.createElement("input");
    inputTag.id = id;
    inputTag.type = "search";
    inputTag.className = "form-control";
    inputTag.placeholder = "Search for messages";
    formTag.appendChild(inputTag);

    var buttonTag = document.createElement("button");
    buttonTag.type = "search";
    buttonTag.className = "btn btn-primary";
    buttonTag.innerHTML = "Search"
    formTag.appendChild(buttonTag);        
    
    var resultsTag = document.createElement("div");
    resultsTag.className = "ag-search-results";
    parentTag.appendChild(resultsTag);
    
    var toTopTag = document.createElement("a");
    toTopTag.href = "#top";
    toTopTag.innerHTML = "Back to top";
    toTopTag.className = "ag-center ag-fill";
    parentTag.appendChild(toTopTag);
    
    var searchFunction = () => search(resultsTag, inputTag.value);
    buttonTag.onclick = searchFunction;
    formTag.onkeyup = evt => {
        console.log(evt.key);
        if(evt.key == "Enter"){
            searchFunction();
        }
    };
    return searchBoxTag;    
}

function loadMessages(){
    
}

function search(tag, searchString) {
    removeChildElements(tag);
    loadSearchResults(tag, searchString);
}

// On document ready, load a search block
$(document).ready(() => {
    var searchTag = document.getElementById("ag-msg-search");
    if(searchTag != null){
        populateSearchContainer(searchTag, "ag-msg-search-form");
    }
});

