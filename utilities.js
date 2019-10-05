function formatDate(dateString) {
    date = new Date(dateString);
    var formattedDate = date.toLocaleDateString('en-US', { month: "long", day: "numeric", year: "numeric" });
    return formattedDate;
}

function appendHiddenDiv(parentTag, activatorTag) {
    var divTag = document.createElement("div");
    divTag.classList.add("hidden");
    activatorTag.onmouseover = () => {
        console.log("mouseover");
        divTag.classList.remove("hidden");
    };
    parentTag.onmouseleave = () => {
        console.log("mouseout");
        divTag.classList.add("hidden");
    };
    parentTag.appendChild(divTag);
    return divTag;
}

function appendTooltip(parentTag, position = "top") {
    var tooltipTag = document.createElement("span");
    tooltipTag.classList.add("ag-tooltip-text");
    tooltipTag.classList.add(`ag-tooltip-text-${position}`)
    parentTag.classList.add("ag-tooltip");
    parentTag.appendChild(tooltipTag);
    return tooltipTag;
}