# Scripts

* *load_youtube_iframe.js*
    * Provide functions for loading the YouTube IFrame API
    * Provide functions for attaching callbacks for when the above resource is available.

* *latest_message_video.js*<br>
  *latest_message_audio.js*<br>
  *latest_message_notes.js*
    * Pull the latest available resource.
    * When the data is loaded, the script injects it into the various elements on the message front page.
    * ***load_message_video.js* requires *load_youtube_iframe.js***

* *message_series.js*
    * Like the above three scripts, but used for the message series pages to inject details into a "message series block", which is an HTML `<div>` element with an `id` indicating the message series.

* *search.js*
    * Locates a `<div>` with `id="ag-msg-search"` and turns it into a search bar with an empty results `<div>` which is populated by issuing a search.

* *utilities.js*
    * Various JS utilities. **Required by most scripts**

* *message_utilities*
    * Various message-related JS utilities. **Required by all message-related scripts**

* *barrier.js*
    * Provides a utility function to *barrier*, or wait until, multiple resources are available before proceeding.
