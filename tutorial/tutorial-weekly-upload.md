
# Tutorial: Weekly upload

## Overview

1. [Upload video, audio, and notes](#Upload-video,-audio,-and-notes)
2. [Add details to *message_details.json*](#Add-details)
3. [Publish the changes](#Publish-the-changes)

## Upload video, audio, and notes

* Video
    * Trim the video to capture the entire service
        * Start at introduction before first hymn
        * End just after the announcments
        * Don't include music videos before or after the service
        * Name the file according to the format ***YYYY-MM-DD*_service.mp4**
    * Upload video to YouTube using Amazing Grace's account
        * Set the *Title* according to the format **MM.DD.YYYY Title**
        * Set the *Description* to include the Bible passage(s) and optionally a short description of the message
        * Set the *Playlist* to the sermon series name
        * Set the *Recording Date* to the date of the service
        * Make sure the *Visibility* is *Public*
        * Add any *Tags* that pertain to the service.

* Audio
    * Trim audio to capture just the sermon message
        * Name the file according to the format ***YYYY-MM-DD*_sermon.mp3**
    * Upload the file to Amazing Grace's Google Drive folder under **Sermon Audios/*Series Name***

* Notes
    * Notes should be uploaded as a PDF
        * Pastor usually takes care of this

## Add the details to the database

This tutorial assumes you have set up the sermon series in [message_details.json](../message_details.json) ahead of time, so you should have an existing block for each message in the sermon series. If you haven't then refer back to [this tutoial](tutorial-adding-a-new-series.md). We'll focus on just the block for this message.

```json
 "2019-06-23" : {
        "title": "The Seeds We Sow",
        "videoId": "",
        "playlistId": "",
        "description": "2 Samuel 3:2-39",
        "messageStart": 0,
        "tags" : [],
        "audioLink": "",
        "audioDownloadLink": "",
        "notesLink": ""
    },
```
As-is, this message block is ready for the website. The title and description will be functional if a matching `<div>` exists on a series web page. The video, audio, and notes buttons will be disabled since all the links are empty.

![Empty message](images/ex_card_empty_message.png)

### Gather the details

Get the video ID from the YouTube video URL:
<span>https</span>://www.youtube.com/watch?v=<mark>mWH1jVwcjuM</mark> (videoId)

Get the playlist ID from the playlist's URL: <span>https</span>://www.youtube.com/watch?v=mWH1jVwcjuM&list=<mark>PLfBOebmxfChGt7oRuCD2MvX_ZB3WltQE7</mark> (playlistId)

Find out what time in seconds the message starts (usually after the first hymn). In this case, the message starts at 3:45, so the start time for the JSON data will be <mark>225</mark> (messageStart).

From the Google Drive web interface, get the link for the audio file by right-clicking on the file and selecting **Get shareable link**

![image](images/ex_google_drive_get_shareable_link.png)


This will give you the link to play the file:
<mark><span>https</span>://drive.google.com/open?id=1ThZTdv-HXmDYuqLZxszIQMECSgffpgWA</mark> (audioLink)

To get the *download* link, take the ID from the above link and insert it into this URL: 

<mark><span>https<span>://drive.google.com/uc?export=download&id=1ThZTdv-HXmDYuqLZxszIQMECSgffpgWA</mark> (audioDownloadLink)

This link will initiate a download instead of playing the file. This link is used by the [front page](description-website-code.md#Messages-Front-Page) for the embedded audio player.

### Update the JSON file

Fill in the details for all the resources, and it should look like this: 

```json    
"2019-06-23" : {
        "title": "The Seeds We Sow",
        "videoId": "mWH1jVwcjuM",
        "playlistId": "PLfBOebmxfChGt7oRuCD2MvX_ZB3WltQE7",
        "description": "2 Samuel 3:2-39",
        "messageStart": 225,
        "tags" : [],
        "audioLink": "https://drive.google.com/open?id=1ThZTdv-HXmDYuqLZxszIQMECSgffpgWA",
        "audioDownloadLink": "https://drive.google.com/uc?export=download&id=1ThZTdv-HXmDYuqLZxszIQMECSgffpgWA",
        "notesLink": "https://www.dropbox.com/sh/3zn82x1orun0hx7/AAAKloonCx5bcVMKz6-Fetopa/David%20Long%20Live%20the%20King?dl=0&preview=Notes+2+Samuel+3.2-39+Seeds+We+Sow.pdf&subfolder_nav_tracking=1"
       },
```

## Publish the changes

### Validate the JSON file

If the JSON file is improperly formatted, no data will be available to the website, and all message links will be broken (that's bad!). To prevent this from happening, you can check the JSON file with an online JSON validator. I use https://jsonlint.com/.

![JSON Lint](images/ex_json_lint.png)

### Publish

Once you've updated [message_details.json](../message_details.json), commit the file to this repo and push it. You can either edit the file through the GitHub in-browser editor or push the commit using an external GIT client.

Once published to the master branch, the changes will be live on the website.

![Populated message](images/ex_card_populated_message.png)

Result: http://amazinggracepdx.com/david-long-live-the-king 
