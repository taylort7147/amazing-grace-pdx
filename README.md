# amazing-grace-pdx

A set of scripts and resources for amazinggracepdx.com

# Data

The data consists of a group of sermon messages. 

## Usage
The Data is used in several places on the website for links and embedded media players.

For more details, see [Web Pages](#Web-Pages)

## Data Format

As of now, the main "database" for the website is stored as a JSON file **message_details.json**. This file contains one element for every message, each containing details for a YouTube video, audio file, and notes. The data is keyed by date, using the format *YYYY-MM-DD*. 

### Message Details

* *title* - The title of the message
* *description* - A short description of the message. This usually includes the Bible passage. On the website, the contents of this field are displayed  when hovering over the title in message series pages.

### Video Details

* *videoId* - The YouTube video ID
* *playlistId* - The YouTube playlist ID
* *messageStart* - The time in seconds at which the message portion of the video begins. This is used to provide controls to seek to the start of the service or beginning of the message.
* *tags* - Unused

### Audio Details

* *audioLink* - The complete URL to the message audio for streaming. This link is used for the message series page.
* *audioDownloadLink* - The complete URL to the message audio for downloading. This is primarily used for the main message page's audio widget.

### Notes Details
* *notesLink* - The complete URL to the message notes

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

# CSS

Our site has a custom CSS style sheet - *global_style.css*. This provides styles for various custom elements to match the default website's element styles.

To accomplish this, a root style is provided that can be overriden on a per-page basis to allow for custom color schemes.

```css
:root {
    --bg-color-primary: #F9AF0D;
    --fg-color-primary: #FFFFFF;
    --border-color-primary: #00000000;
    --shadow-color: #F7B00680;
    --hover-bg-color: #FEE0AC;
    --active-bg-color: var(--bg-color-primary);
    --disabled-bg-color: lightgrey;
    --disabled-fg-color: grey;
    --disabled-border-color: darkgrey;
}
```
By providing a `:root{}` block in the page's custom CSS section, any one or more of these fields can be overriden.

# Code Injection

The website that manages amazinggracepdx.com (nucleus.church) provides the ability to inject code into every page. We use this to inject Google Analytics, JQuery, our custom CSS, and a few custom scripts.

This is our code injection block as of writing this:

```HTML
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-66147796-1"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag(){dataLayer.push(arguments);}
    gtag('js', new Date());

    gtag('config', 'UA-66147796-1');
</script>

<!-- JQuery -->
<script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>

<!-- Taylor's CSS -->
<link rel="stylesheet" href="https://amazinggracepdx.netlify.com/global_style.css">

<!-- Taylor's scripts -->
<script type="text/javascript" src="https://amazinggracepdx.netlify.com/barrier.js"></script>
<script type="text/javascript" src="https://amazinggracepdx.netlify.com/load_message_details.js"></script>
<script type="text/javascript" src="https://amazinggracepdx.netlify.com/load_youtube_iframe_api.js"></script>
```
**Note that the latter two load scripts only provide functions for loading resources - they don't actually initiate the load.**

# Web Pages 

## Messages Front Page

--- TODO: Expand on this section with code exambles ---

This page contains links and embedded media players for the latest message video, audio, and notes.

http://amazinggracepdx.com/messages-1

## Message Series Pages

Each message series has its own page. Each message in the series is is represented as a title with links to the video, audio, and notes.

Example: http://amazinggracepdx.com/jesus-mountainside-message

# A note on Netlifly

The way GitHub exposes its files, it  is non-trivial to load a javascript file as a script. To mitigate this, we use Netlifly which exposes GitHub repositories as web sites. Netlifly can serve an entire website, but  we only use it as a way to expose our scripts.