namespace EasyITSystemCenter.SystemStructure {

    public class SystemTabs {
        private readonly int?    _dbRecId;
        private readonly string _header;
        private readonly object _content;
        private readonly string _tag;

        public SystemTabs(int? dbRecId, string header, object content, string tag = null) {
            _dbRecId = dbRecId;
            _header = header;
            _content = content;
            _tag = tag;
        }

        public int? DbRecId {
            get { return _dbRecId; }
        }

        public string Header {
            get { return _header; }
        }

        public object Content {
            get { return _content; }
        }

        public object Tag {
            get { return _tag; }
        }
    }

    public class SubjectName {
        public string AlternativSubjectName { get; set; }
    }
}