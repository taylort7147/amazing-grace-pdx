# Data

The main data structure is a *message*. Each *message* contains information about the message as well as links to the video, audio, and notes for that message. Messages are grouped into series in a separate database table.

## Database

The current database is a MSSQL database hosted on Azure. 

Database tables:
* Message
* Bible Reference
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
* *NotesId* - The ID of the notes, for locating in the Notes table

### Bible Reference Table
Bible references are represented as a range of integer indices for book, chapter, and verse (e.g., "Genesis 3:5-7" would be stored as [1, 3, 5] - [1, 3, 7]). This is for simplicity of querying in the database, allowing quick look-ups and range-based queries. This project contains a custom parser which can convert between this index representation and friendly names.
* *Id* - The ID of this entry in the table
* *StartBook* - The range start book
* *StartChapter* - The range start chapter
* *StartVerse* - The range start verse
* *EndBook* - The range end book
* *EndChapter* - The range end chapter
* *EndVerse* - The range end verse
* *MessageId* - The ID of the message, for locating in the Message table

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


