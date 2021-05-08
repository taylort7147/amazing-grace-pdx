import pyodbc
import pandas as pd
import getpass


class Series():
    def __init__(self):
        self.id = None
        self.name = None
        self.description = None
        self.playlist = None


class Message():
    def __init__(self):
        self.id = None
        self.title = None
        self.description = None
        self.date = None
        self.audio_id = None
        self.notes_id = None
        self.video_id = None
        self.series_id = None

    def __str__(self):
        return "Message{ " +\
            f"id: {message.id} ({type(message.id)}), " +\
            f"title: {message.title} ({type(message.title)}), " +\
            f"description: {message.description} ({type(message.description)}), " +\
            f"date: {message.date} ({type(message.date)}), " +\
            f"audio_id: {message.audio_id} ({type(message.audio_id)}), " +\
            f"notes_id: {message.notes_id} ({type(message.notes_id)}), " +\
            f"video_id: {message.video_id} ({type(message.video_id)}), " +\
            f"series_id {message.series_id} ({type(message.series_id)}) " +\
            "}"


class Audio():
    def __init__(self):
        self.id = None
        self.stream_url = None
        self.download_url = None
        self.message_id = None


class Notes():
    def __init__(self):
        self.id = None
        self.url = None
        self.message_id = None


class Video():
    def __init__(self):
        self.id = None
        self.youtube_video_id = None
        self.message_start_time_seconds = None
        self.message_id = None


def get_input_data(conn):
    sql_input_query = """
        SELECT TOP (1000) [Series].[Name] as SeriesName,
            [Message].[Title],
            [Message].[Description],
            [Message].[Date],
            [Audio].[StreamUrl] as AudioStreamUrl,
            [Audio].[DownloadUrl] as AudioDownloadUrl,
            [Notes].[Url] as NotesUrl,
            [Video].[YouTubeVideoId],
            [Video].[MessageStartTimeSeconds]
        FROM [dbo].[Message]
            LEFT JOIN [dbo].[Series] ON [Message].[SeriesId]=[Series].[Id]
            LEFT JOIN [dbo].Audio ON [Message].[AudioId]=[Audio].[Id]
            LEFT JOIN [dbo].[Notes] ON [Message].[NotesId]=[Notes].[Id]
            LEFT JOIN [dbo].[Video] ON [Message].[VideoId]=[Video].[Id]
    """
    return pd.read_sql_query(sql_input_query, conn)


def get_series_id(conn, series):
    series_result = pd.read_sql_query(
        f"SELECT TOP 1 [Id], [Name] FROM [Series] WHERE [Name]=?", conn, params=(series.name,))
    if(not series_result.empty):
        return int(series_result["Id"].iloc[0])


def try_insert_series(conn, series):
    series_id = get_series_id(conn, series)
    if series_id is None:
        cursor = conn.cursor()
        cursor.execute(
            "INSERT INTO [Series] ([Name]) OUTPUT INSERTED.[Id] VALUES (?)", (series.name,))
        return int(cursor.fetchval())
    return series_id


def get_message_id(conn, message):
    message_result = pd.read_sql_query(
        f"SELECT TOP 1 [Id], [Title] FROM [Message] WHERE [Title]=? AND [Date]=?",
        conn,
        params=(message.title, message.date))
    if(not message_result.empty):
        return int(message_result["Id"].iloc[0])


def try_insert_message(conn, message):
    message_id = get_message_id(conn, message)
    if message_id is None:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO [Message] ([Title], [Description], [Date], [SeriesId]) OUTPUT INSERTED.[Id] VALUES (?, ?, ?, ?)",
                       (message.title,
                        message.description,
                        message.date,
                        message.series_id))
        return int(cursor.fetchval())
    return message_id


def update_message(conn, message):
    """
    UPDATE table_name
    SET column1 = value1, column2 = value2, ...
    WHERE condition;
    """
    cursor = conn.cursor()
    print(message)
    cursor.execute("UPDATE [Message] SET [AudioId]=?, [NotesId]=?, [VideoId]=? WHERE [Id]=?",
                   (message.audio_id, message.notes_id, message.video_id, message.id))


def get_audio_id(conn, audio):
    audio_result = pd.read_sql_query(
        f"SELECT TOP 1 [Id], [StreamUrl] [DownloadUrl] FROM [Audio] WHERE [MessageId]=?", conn, params=(audio.message_id,))
    if(not audio_result.empty):
        return int(audio_result["Id"].iloc[0])


def try_insert_audio(conn, audio):
    if audio.stream_url is None or audio.download_url is None:
        print(f"Skipping audio for message: {audio.message_id}")
        return
    else:
        print(f"Inserting audio for message: {audio.message_id}")
    audio_id = get_audio_id(conn, audio)
    if audio_id is None:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO [Audio] ([StreamUrl], [DownloadUrl], [MessageId]) OUTPUT INSERTED.[Id] VALUES (?, ?, ?)",
                       (audio.stream_url, audio.download_url, audio.message_id))
        return int(cursor.fetchval())
    return audio_id


def get_notes_id(conn, notes):
    notes_result = pd.read_sql_query(
        f"SELECT TOP 1 [Id], [Url] FROM [Notes] WHERE [MessageId]=?", conn, params=(notes.message_id,))
    if(not notes_result.empty):
        return int(notes_result["Id"].iloc[0])


def try_insert_notes(conn, notes):
    if notes.url is None:
        print(f"Skipping notes for message: {notes.message_id}")
        return
    else:
        print(f"Inserting notes for message: {notes.message_id}")
    notes_id = get_notes_id(conn, notes)
    if notes_id is None:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO [Notes] ([Url], [MessageId]) OUTPUT INSERTED.[Id] VALUES (?, ?)",
                       (notes.url, notes.message_id))
        return int(cursor.fetchval())
    return notes_id


def get_video_id(conn, video):
    video_result = pd.read_sql_query(
        f"SELECT TOP 1 [Id], [YouTubeVideoId], [MessageStartTimeSeconds] FROM [Video] WHERE [MessageId]=?", conn, params=(video.message_id,))
    if(not video_result.empty):
        return int(video_result["Id"].iloc[0])


def try_insert_video(conn, video):
    if video.youtube_video_id is None or video.message_start_time_seconds is None:
        print(f"Skipping video for message: {video.message_id}")
        return
    else:
        print(f"Inserting video for message: {video.message_id}")
    video_id = get_video_id(conn, video)
    if video_id is None:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO [Video] ([YouTubeVideoId], [MessageStartTimeSeconds], [MessageId]) OUTPUT INSERTED.[Id] VALUES (?, ?, ?)",
                       (video.youtube_video_id, video.message_start_time_seconds, video.message_id))
        return int(cursor.fetchval())
    return video_id


print("Input database")
print("=" * 80)
input_username = input("Username: ")
input_password = getpass.getpass()
print()

print("Output database")
print("=" * 80)
output_username = input("Username: ")
output_password = getpass.getpass()

input_connection_string = f"""
Driver={{SQL Server}};
Server=tcp:amazing-grace-pdx.database.windows.net,1433;
Database=message-db;
Uid={input_username};
Pwd={input_password};
Encrypt=yes;
TrustServerCertificate=no;
Connection Timeout=30;
"""

# output_connection_string = f"""
# Driver={{SQL Server}};
# Server=(local);
# Database=message-db;
# Trusted_Connection=yes";
# """

output_connection_string = f"""
Driver={{SQL Server}};
Server=tcp:amazing-grace-pdx-server.database.windows.net,1433;
Database=message-db;
Uid={output_username};
Pwd={output_password};
Encrypt=yes;
TrustServerCertificate=no;
Connection Timeout=30;
"""

conn_in = pyodbc.connect(input_connection_string)
conn_out = pyodbc.connect(output_connection_string)

data_all = get_input_data(conn_in)


series_list = data_all["SeriesName"].unique()
for series_name in series_list:
    series = Series()
    series.name = series_name

    data_series = data_all[data_all["SeriesName"] == series.name]
    # print(series)

    series_message = data_series[
        ["SeriesName", "Title"]]
    # print(series_message)

    message_columns = [
        "Title", "Description", "Date",
        "AudioStreamUrl", "AudioDownloadUrl",
        "NotesUrl",
        "YouTubeVideoId", "MessageStartTimeSeconds"
    ]
    messages_full = data_series[message_columns]

    # Insert data into tables
    series.id = try_insert_series(conn_out, series)
    print(f"series={series.name}, id={series.id}")
    for message_row in messages_full.values:
        # Data structures
        message = Message()
        audio = Audio()
        notes = Notes()
        video = Video()

        # Insert message
        message.series_id = series.id
        message.title = message_row[message_columns.index("Title")]
        message.description = message_row[message_columns.index("Description")]
        message.date = message_row[message_columns.index("Date")]
        message.id = try_insert_message(conn_out, message)

        # Update message ID in dependent objects
        audio.message_id = message.id
        notes.message_id = message.id
        video.message_id = message.id

        # Insert audio
        audio.stream_url = message_row[message_columns.index("AudioStreamUrl")]
        audio.download_url = message_row[
            message_columns.index("AudioDownloadUrl")]
        audio.id = try_insert_audio(conn_out, audio)
        message.audio_id = audio.id

        # Insert notes
        notes.url = message_row[message_columns.index("NotesUrl")]
        notes.id = try_insert_notes(conn_out, notes)
        message.notes_id = notes.id

        # Insert video
        video.youtube_video_id = message_row[
            message_columns.index("YouTubeVideoId")]
        video.message_start_time_seconds = message_row[
            message_columns.index("MessageStartTimeSeconds")]
        video.id = try_insert_video(conn_out, video)
        message.video_id = video.id

        # Update message with newly created IDs
        update_message(conn_out, message)

    print()


print("New series:")
print("=" * 80)
new_series = pd.read_sql_query("SELECT * FROM [Series]", conn_out)
print(new_series)
print()

print("New message:")
print("=" * 80)
new_message = pd.read_sql_query("SELECT * FROM [Message]", conn_out)
print(new_message)
print()

should_commit_prompt = input("Commit? [y/N] ")
if len(should_commit_prompt) > 0 and should_commit_prompt.lower() in ("y", "yes"):
    conn_out.commit()
