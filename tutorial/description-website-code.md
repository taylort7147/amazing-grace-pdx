# Website Code

**Caveat: This page assumes you have knowledge of web programming.**

Currently we use [Nucleus](http://nucleus.church) as our website builder. It provides tools for building web pages (or "cards") using a simple drag-and-drop editor. Among the buildng blocks availble are HTML blocks which allow you to embed HTML code into a section of the page. This is used heavily for loading and displaying the resources from this GitHub project. It also provides finer control of the website's behavior via custom CSS and code-injection.

This page outlines the different web elements that make up the website.  It contains information about the styles, HTML blocks, and scripts that are used and how to utilize them using the website editor.

Contents:
* [CSS](#CSS)
* [Code Injection](#Code-Injection)
* [Web Pages](#Web-Pages)
    * [Messages Front Page](#Messages-Front-Page)
    * [Message Series Pages](#Message-Series-Pages)

---

## CSS

Our site has a [custom CSS style sheet](../global_style.css). This provides styles for various custom elements to match the default website's element styles.

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
By providing a `:root{}` block in the page's custom CSS section, any one or more of these fields can be overriden. To do this, in the page's custom CSS, only include the only the fields you want to override. Fields that aren't present will inherit the defaults from the global CSS.

```css
: root {
    --bg-color-primary: #ABA8F0;
}
```
This example changes the background color of UI elements on this page, specifically buttons. This mechanism can be used for setting the color scheme of a series, for example.

---

## Code Injection

Nucleus provides the ability to inject code into every page. We use this to inject Google Analytics, JQuery, our custom CSS, and a few custom scripts.

![Code injection](images/ex_code_injection.png)

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
<link rel="stylesheet" href="https://taylort7147.github.io/amazing-grace-pdx/global_style.css">

<!-- Taylor's scripts -->
<script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/barrier.js"></script>
<script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/load_youtube_iframe_api.js"></script>
<script  type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/utilities.js"></script>
```

---

## Web Pages 

**Unfortunately, Nucleus doesn't display a good preview of these HTML blocks, so they'll just look like empty HTML blocks until you open the preview.**

---

### Messages Front Page

This page contains links and embedded media players for the latest message video, audio, and notes. Each element consists of three elements:
* `<h3>` element that contains an `id` equal to `"latest-message-[type]-details"`. This header adds a hover-over text field displaying details about the message such as title, description, and date.
* `<div>` element(s) that contains an `id` specifying its type
* `<script>` element to load and bind the data to it. The script uses the `id` field of the `<div>` elements to locate them on the page and inject code into them.

#### Video Block

The video block embeds a YouTube player with the latest message. The script that loads this player provides enhancements over the standard YouTube embedded video, including:

* No YouTube logo overlay
* Ability to bind buttons to the player
    * See *Jump to Beginning* and *Jump to Message* buttons in the screenshot below
* Control over the *suggested videos* that appear when the video completes
    * Only showing videos from Amazing Grace's channel

HTML block code:
```html
<h3>Video <span id="latest-message-video-details">[+]</span></h3>

<div align="center" class="" >
    <!-- YouTube iframe container -->
    <div class="ag-embed-responsive">
        <div id="latest-message-video" class="ag-embed-responsive-item"></div>
    </div>

    <!-- Controls for the YouTube player -->
    <div id="latest-message-video-controls" class="ag-btn-group ag-center" role="group" aria-label="Player controls">
        <button id="latest-message-video-controls-jump-to-beginning" type="button" class="ag-btn ag-btn-round">Jump To Beginning</button>
        <button id="latest-message-video-controls-jump-to-message" type="button" class="ag-btn ag-btn-round">Jump To Message</button>
    </div>
</div>

<script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/latest_message_video.js"></script>

<script>
    loadYouTubeIframeAPI();
</script>
```

Notice the last `<script>` tag in this block. It calls the *load* function that loads the YouTube API for loading the video player. This block requires [load_youtube_iframe_api.js](../load_youtube_iframe_api.js) to be loaded before hand. This is done automatically by the site's [code injection](#Code-Injection).

![Embedded video player](images/ex_card_embedded_video_player.png)

#### Audio Block

This HTML block embeds an audio widget of the latest message.

HTML block code:
```html
<h3>Audio <span id="latest-message-audio-details">[+]</span></h3>
<div id="latest-message-audio" class="ag-center"></div>

<script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/latest_message_audio.js"></script>
```

![Embedded video player](images/ex_card_embedded_audio_player.png)


#### Notes Block

This HTML block creates a button that links to the latest message notes.

HTML block code:

```html
<h3>Notes <span id="latest-message-notes-details">[+]</span></h3>
<div id="latest-message-notes" class="ag-center"></div>

<script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/latest_message_notes.js"></script>
```

![Notes button](images/ex_card_notes_button.png)


The end result can be seen here: http://amazinggracepdx.com/messages-1

---

### Message Series Pages

Each message series has its own page. Each message in the series is is represented as a title with links to the video, audio, and notes.

![Message series](images/ex_card_message_series.png)

The message series page is made up of one HTML block. The block contains a `<div>` element whose `class` must be `"message-series-block"`, and `id` must be the name of the series in the database (see [Series Table](description-data.md#Series-Table)). The block also contains a `<script>` tag that is used to load the series from the database.

```html
<div id="Christian Worldview"  class="message-series-block"></div>

<script type="text/javascript" src="https://taylort7147.github.io/amazing-grace-pdx/message_series.js">
```

For any missing link in the details for any particular message, the button for that link will be greyed out and disabled.

![Disabled links](images/ex_card_empty_message.png)

The end result can be seen here:
http://amazinggracepdx.com/seeing-reality-as-it-is

---

### How are the scripts hosted?

The scripts are hosted via the *GitHub Pages* for this project, located at https://taylort7147.github.io/amazing-grace-pdx. *GitHub Pages* must be enabled in the project settings for this to work.
