namespace WuhanJamesHubApi
{
    public class JJBRowIndexOptions
    {
        //public int StartColumn { get; set; }
        public List<JJBRowIndexItem> List { get; set; }
    }

    public class JJBRowIndexItem
    {
        /// <summary>
        /// 行名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 行索引
        /// </summary>
        public int Index { get; set; }
    }
}
