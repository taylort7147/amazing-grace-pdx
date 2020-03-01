# Data

The main data structure is a *message*. Each *message* contains information about the message as well as links to the video, audio, and notes for that message. Messages are grouped into series in a separate database table.

## Database

The current database is a MSSQL database hosted on Azure. 

Database tables:
* Message
* Video
* Audio
* Notes
* Series

### Message Table
* *Id* - The ID of this entry in the table
* *Title* - The title of the message (3-120 characters, must start with a number of capital letter)
* *Description* - A description of the message (1-4095 characters)
* *Date* - The date of the message
* *VideoId* - The ID of the video, for locating in the Video table
* *AudioId* - The ID of the audio, for locating in the Audio table
* *title* - The title of the message
* *description* - A short description of the message. This usually includes the Bible passage. On the website, the contents of this field are displayed  when hovering over the title in message series pages.

### Video Table
* *Id* - The ID of this entry in the table
* *YouTubeVideoId* - The YouTube video ID (not to be confused with the database ID of the video)
* *YouTubePlaylistId* - The YouTube playlist ID
* *MessageStartTimeSeconds* - The offset in seconds into the video at which point the message starts (since the video generally includes the entire service)
* *MessageId* - The ID of the message, for locating in the Message table

### Audio Table
* *Id* - The ID of this entry in the table
* *StreamUrl* - The URL used to stream the audio. This link is used for the [message series](description-website-code.md#Message-Series-Pages) page.
* *DownloadUrl* - The URL used to download the audio file. This is primarily used for the [main message](description-website-code.md#Messages-Front-Page) page's audio widget.
* *MessageId* - The ID of the message, for locating in the Message table

### Notes Table
* *Id* - The ID of this entry in the table
* *Url* The URL of the notes file.
* *MessageId* - The ID of the message, for locating in the Message table

### Series Table
* *Id* - The ID of this entry in the table
* *Name* - The name of the series


