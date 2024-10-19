using NPOI.SS.UserModel;

namespace Common
{
    public static class NpoiExtension
    {
        public static IWorkbook GetNpoiWorkbook(this string path, out string err, params string[] passwords)
        {
            IWorkbook workbook = null;
            err = "";
            try
            {
                workbook = WorkbookFactory.Create(path);
            }
            catch (Exception e1)
            {
                err = e1.Message;
                foreach (var password in passwords)
                {
                    try
                    {
                        workbook = WorkbookFactory.Create(path, password);
                    }
                    catch (Exception e2)
                    {
                        err = e2.Message;
                    }
                }
            }
            if (err.Contains("process"))
            {
                err = "文件可能被占用，请先关闭文件再尝试操作";
            }
            return workbook;
        }
    }
}
