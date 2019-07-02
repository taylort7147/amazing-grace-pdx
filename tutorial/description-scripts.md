# Scripts

* *load_message_details.js*<br>*load_youtube_iframe.js*
    * Provide functions for loading either *message_details.json* or the YouTube IFrame API
    * Provide functions for attaching callbacks for when the above resources are available.

* *latest_message_video.js*<br>
  *latest_message_audio.js*<br>
  *latest_message_notes.js*
    * Pull the latest data block (determined by the date key) that has a non-empty field for the message video, audio, or notes field, depending on the script.
    * When the data is loaded, the script injects it into the various elements on the message front page.
    * **All three require *load_message_details.js***
    * ***load_message_video.js* additionally requires *load_youtube_iframe.js***

* *message_series.js*
    * Like the above three scripts, but used for the message series pages to inject details into "message blocks", which are HTML \<div> elements - one for each message in the series.
    * **Requires *load_message_details.js***

* *message_front_page.js* 
    * Deprecated

* *barrier.js*
    * Provides a utility function to *barrier*, or wait until, multiple resources are available before proceeding.
