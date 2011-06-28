using System.Text;

namespace Witinyki.Models
{
    public class PageModel
    {
        private readonly byte[] _content;

        public PageModel(string pageName, string version, byte[] content)
        {
            _content = content;
            PageName = pageName;
            Version = version;
        }

        public string Content
        {
            get { return Encoding.ASCII.GetString(_content); }
        }

        public string PageName { get; set; }
        public string Version { get; set; }
    }
}