using System;
using System.Linq;
using MessageManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MessageManager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MessageContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<MessageContext>>()))
            {
                if (!context.Message.Any())
                {
                    ClearDatabase(context);
                    SeedDatabase(context);
                }
            }
        }

        // Debug method
        private static void ClearDatabase(MessageContext context)
        {
            context.RemoveRange(context.Message);
            context.RemoveRange(context.Audio);
            context.RemoveRange(context.Notes);
            context.RemoveRange(context.Video);
            context.RemoveRange(context.Series);
            context.RemoveRange(context.Playlist);
            context.SaveChanges();
        }

        // Debug method
        private static void SeedDatabase(MessageContext context)
        {

            // Series
            var series0 = new Series();
            series0.Name = "Series 0";
            context.Series.Add(series0);

            var series1 = new Series();
            series1.Name = "Series 1";
            context.Series.Add(series1);

            context.SaveChanges();

            // Playlists
            var playlist0 = new Playlist();
            playlist0.YouTubePlaylistId = "abcdefghijklmnopqrstuvwxyz01234567";
            playlist0.SeriesId = series0.Id;
            context.Playlist.Add(playlist0);

            context.SaveChanges();

            // Messages
            for (var i = 1; i <= 10; ++i)
            {
                var message = new Message();
                message.Title = "Message " + i;
                message.Description = "Description for message " + i;
                message.Date = new DateTime(i, i, i);
                message.SeriesId = series0.Id;
                context.Message.Add(message);
                context.SaveChanges();

                if (i % 2 == 1)
                {
                    var audio = new Audio();
                    audio.DownloadUrl = "http://dl.audio.com/" + i;
                    audio.StreamUrl = "http://stream.audio.com/" + i;
                    audio.MessageId = message.Id;
                    context.Audio.Add(audio);

                    var notes = new Notes();
                    notes.Url = "http://notes.com/" + i;
                    notes.MessageId = message.Id;
                    context.Notes.Add(notes);

                    var video = new Video();
                    video.YouTubeVideoId = String.Format("Video{0:000000}", i);
                    video.MessageStartTimeSeconds = i;
                    video.MessageId = message.Id;
                    context.Video.Add(video);

                    context.SaveChanges();
                    message.AudioId = audio.Id;
                    message.NotesId = notes.Id;
                    message.VideoId = video.Id;
                }

            }

            context.SaveChanges();
        }
    }
}