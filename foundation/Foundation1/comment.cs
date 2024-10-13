using System;

namespace Foundation1
{
    public class Comment
    {
        // Member variables with _underscoreCamelCase
        private string _name;
        private string _text;

        // Constructor
        public Comment(string name, string text)
        {
            _name = name;
            _text = text;
        }

        // Properties to access member variables
        public string Name
        {
            get { return _name; }
        }

        public string Text
        {
            get { return _text; }
        }
    }
}
