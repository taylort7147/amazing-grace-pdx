# Data

The data consists of a group of sermon messages. 

## Data Format

As of now, the main "database" for the website is stored as a JSON file [message_details.json](../message_details.json). This file contains one element for every message, each containing details and links to a YouTube video, audio file, and notes. The data is keyed by date, using the format *YYYY-MM-DD*. 

```json
{    
    "2019-03-31" : {
        "title": "The Story of the World",
        "videoId": "3Q7foYHXzVA",
        "playlistId": "m4931EJqwZp5YhyYNW",
        "description": "Luke 15:11-32",
        "messageStart": 212,
        "tags" : [],
        "audioLink": "https://drive.google.com/open?id=1SRmP2cyPp9m1msiTJoEwMTQoWQh3mgBz",
        "audioDownloadLink": "https://drive.google.com/uc?export=download&id=1SRmP2cyPp9m1msiTJoEwMTQoWQh3mgBz",
        "notesLink": "https://www.dropbox.com/sh/3zn82x1orun0hx7/AABIXfs-vRMBlZZdskhacMWDa/Miscellaneous%202019?dl=0&lst=&preview=Luke+15.11-32+Lost+Sons+Branches+Notes.pdf&subfolder_nav_tracking=1"
    },
    "2019-04-07" : {
        "title": "What's Worth Pursuing in Life",
        "videoId": "KDvKC0hfGQc",
        "playlistId": "m4931EJqwZp5YhyYNW",
        "description": "Philippians 3:7-16",
        "messageStart": 276,
        "tags" : [],
        "audioLink": "https://drive.google.com/open?id=1v-5qP4VcCaieNSk2lvKcgMiCsgp24nTg",
        "audioDownloadLink": "https://drive.google.com/uc?export=download&id=1v-5qP4VcCaieNSk2lvKcgMiCsgp24nTg",
        "notesLink": "https://www.dropbox.com/sh/3zn82x1orun0hx7/AABIXfs-vRMBlZZdskhacMWDa/Miscellaneous%202019?dl=0&lst=&preview=Philippians+3.7-16+Branches+Notes.pdf&subfolder_nav_tracking=1"
    },
    ...
}
```

### Message Details

* *title* - The title of the message
* *description* - A short description of the message. This usually includes the Bible passage. On the website, the contents of this field are displayed  when hovering over the title in message series pages.

### Video Details

* *videoId* - The YouTube video ID
* *playlistId* - The YouTube playlist ID
* *messageStart* - The time in seconds at which the message portion of the video begins. This is used to provide controls to seek to the start of the service or beginning of the message.
* *tags* - Unused

### Audio Details

* *audioLink* - The complete URL to the message audio for streaming. This link is used for the [message series](description-website-code.md#Message-Series-Pages) page.
* *audioDownloadLink* - The complete URL to the message audio for downloading. This is primarily used for the [main message](description-website-code.md#Messages-Front-Page) page's audio widget.

### Notes Details
* *notesLink* - The complete URL to the message notes



