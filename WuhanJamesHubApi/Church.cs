namespace WuhanJamesHubApi
{
    public class Church
    {
        public Church() { }
        public Church(string chineseName)
        {
            ChineseName = chineseName;
        }
        public Church(string chineseName, string koreanName) : this(chineseName)
        {
            KoreanName = koreanName;
        }

        public string ChineseName { get; set; }
        public string KoreanName { get; set; }
    }
}
