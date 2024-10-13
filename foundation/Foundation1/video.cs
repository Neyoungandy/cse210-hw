using System;
using System.Collections.Generic;

namespace Foundation1
{
    public class Video
    {
        // Member variables with _underscoreCamelCase
        private string _title;
        private string _author;
        private int _length; // in seconds
        private List<Comment> _comments;

        // Constructor
        public Video(string title, string author, int length)
        {
            _title = title;
            _author = author;
            _length = length;
            _comments = new List<Comment>();
        }

        // Properties to access member variables
        public string Title
        {
            get { return _title; }
        }

        public string Author
        {
            get { return _author; }
        }

        public int Length
        {
            get { return _length; }
        }

        // Method to add a comment
        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        // Method to return the number of comments
        public int GetCommentCount()
        {
            return _comments.Count;
        }

        // Property to access comments
        public List<Comment> Comments
        {
            get { return _comments; }
        }
    }
}
