using System;
using System.Collections.Generic;

namespace Foundation1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Video objects
            Video video1 = new Video("How to Cook Pasta", "Chef John", 300);
            Video video2 = new Video("Guitar Tutorial", "MusicMan", 600);
            Video video3 = new Video("Python Programming Basics", "Techie", 900);

            // Add Comments to video1
            video1.AddComment(new Comment("Alice", "Great video, thanks!"));
            video1.AddComment(new Comment("Bob", "I tried this recipe, and it worked perfectly."));
            video1.AddComment(new Comment("Charlie", "Could you show how to make the sauce?"));

            // Add Comments to video2
            video2.AddComment(new Comment("Dave", "I love this tutorial!"));
            video2.AddComment(new Comment("Eve", "I learned so much from this video."));
            video2.AddComment(new Comment("Frank", "Can you do a follow-up video?"));

            // Add Comments to video3
            video3.AddComment(new Comment("Grace", "This helped me understand Python basics."));
            video3.AddComment(new Comment("Hank", "Clear and concise explanation."));
            video3.AddComment(new Comment("Ivy", "Looking forward to more advanced tutorials!"));

            // Store Videos in a list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Iterate through the list of Videos and display information
            foreach (Video video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.Length} seconds");
                Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
                Console.WriteLine("Comments:");
                foreach (Comment comment in video.Comments)
                {
                    Console.WriteLine($"- {comment.Name}: {comment.Text}");
                }
                Console.WriteLine(); // Blank line for readability
            }

            // Keep the console window open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
