// Requires 
//      utilities.js
//      message_utilities.js


// On document ready, find all message series blocks and populate them
$(document).ready(function() {
    var messageSeriesBlocks = $("div.message-series-block");
    console.log(`Number of message series blocks: ${messageSeriesBlocks.length}`);
    messageSeriesBlocks.each((i, tag) => loadSeries(tag, tag.id)); // tag.id == seriesName
});
