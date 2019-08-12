using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Editor.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MessageContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<MessageContext>>()))
            {
                ClearDatabase(context);
                SeedDatabase(context);
            }
        }

        // Debug method
        private static void ClearDatabase(MessageContext context)
        {
            context.RemoveRange(context.Message);
            context.RemoveRange(context.Audio);
            context.RemoveRange(context.Notes);
            context.RemoveRange(context.Video);
            context.SaveChanges();
        }

        // Debug method
        private static void SeedDatabase(MessageContext context)
        {
            for(var i = 1; i <= 10; ++i)
            {
                var message = new Message();
                message.Id = i;
                message.Title = "Message " + i;
                message.Description = "Description for message " + i;
                message.Date = new DateTime(i, i, i);
                if(i % 2 == 1)
                {
                    var audio = new Audio();
                    audio.Id = i;
                    audio.DownloadUrl = "http://dl.audio.com/" + i;
                    audio.StreamUrl = "http://stream.audio.com/" + i;
                    audio.MessageId = i;
                    context.Audio.Add(audio);

                    var notes = new Notes();
                    notes.Id = i;
                    notes.Url = "http://notes.com/" + i;
                    notes.MessageId = i;
                    context.Notes.Add(notes);

                    var video = new Video();
                    video.Id = i;
                    video.YouTubeVideoId = String.Format("Video{0:000000}", i);
                    video.YouTubePlaylistId = String.Format("Playlist{0:0000000000}", i);
                    video.MessageStartTimeSeconds = i;
                    video.MessageId = i;
                    context.Video.Add(video);

                    message.AudioId = i;
                    message.NotesId = i;
                    message.VideoId = i;
                }

                context.Message.Add(message);
            }

            context.SaveChanges();
        }
    }
}