using Common;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System.Data.Common;

namespace WuhanJamesHubApi
{
    public class JJBService
    {
        private int GetPropertyRowIndex(JJBRowIndexOptions rowIndexOptions, string propertyName)
        {
            var item = rowIndexOptions.List.FirstOrDefault(p => p.Name == propertyName);
            if (item == null)
            {
                var msg = $"配置文件中没有‘{propertyName}’的配置";
                throw new Exception(msg);
            }
            return item.Index;
        }

        private string GetItemString(JJBRowIndexOptions rowIndexOptions, List<string> itemList, string propertyName)
        {
            var rowIndex = GetPropertyRowIndex(rowIndexOptions, propertyName) - 1;
            return itemList[rowIndex];
        }

        public List<Member>? LoadMemberList(List<List<string>> request, out string errMsg)
        {
            JJBRowIndexOptions rowIndexOptions = new JJBRowIndexOptions()
            {
                List = new List<JJBRowIndexItem>()
                {
                    new JJBRowIndexItem(){ Index = 1, Name = "排序" },
                    new JJBRowIndexItem(){ Index = 2, Name = "韩语姓名" },
                    new JJBRowIndexItem(){ Index = 3, Name = "工号" },
                    new JJBRowIndexItem(){ Index = 4, Name = "汉语姓名" },
                    new JJBRowIndexItem(){ Index = 5, Name = "国籍" },
                    new JJBRowIndexItem(){ Index = 6, Name = "法定出生生日" },
                    new JJBRowIndexItem(){ Index = 7, Name = "性别" },
                    new JJBRowIndexItem(){ Index = 8, Name = "现住址" },
                    new JJBRowIndexItem(){ Index = 9, Name = "身份证地址" },
                    new JJBRowIndexItem(){ Index = 10, Name = "电话" },
                    new JJBRowIndexItem(){ Index = 11, Name = "身高" },
                    new JJBRowIndexItem(){ Index = 12, Name = "血型" },
                    new JJBRowIndexItem(){ Index = 13, Name = "婚否" },
                    new JJBRowIndexItem(){ Index = 14, Name = "CD类型" },
                    new JJBRowIndexItem(){ Index = 15, Name = "出身教团" },
                    new JJBRowIndexItem(){ Index = 16, Name = "其他宗教" },
                    new JJBRowIndexItem(){ Index = 17, Name = "教会名" },
                    new JJBRowIndexItem(){ Index = 18, Name = "职分" },
                    new JJBRowIndexItem(){ Index = 19, Name = "信仰时间" },
                    new JJBRowIndexItem(){ Index = 20, Name = "母胎信仰" },
                    new JJBRowIndexItem(){ Index = 21, Name = "邮箱" },
                    new JJBRowIndexItem(){ Index = 23, Name = "最终学历" },

                    // new RowIndexItem(){ Index = 23, Name = "学历信息1分类" },
                    // new RowIndexItem(){ Index = 24, Name = "学历信息1学校名" },
                    // new RowIndexItem(){ Index = 25, Name = "学历信息1专业" },
                    // new RowIndexItem(){ Index = 26, Name = "学历信息1状态" },
                    // new RowIndexItem(){ Index = 27, Name = "学历信息1期间" },

                    new JJBRowIndexItem(){ Index = 38, Name = "兄弟姐妹关系" },
                    new JJBRowIndexItem(){ Index = 40, Name = "家族事项1姓名" },
                    new JJBRowIndexItem(){ Index = 41, Name = "家族事项1性别" },
                    new JJBRowIndexItem(){ Index = 42, Name = "家族事项1生日" },
                    new JJBRowIndexItem(){ Index = 43, Name = "家族事项1关系" },
                    new JJBRowIndexItem(){ Index = 44, Name = "家族事项1出席教会" },
                    new JJBRowIndexItem(){ Index = 45, Name = "家族事项1SCJ工号" },
                    new JJBRowIndexItem(){ Index = 46, Name = "家族事项2姓名" },
                    new JJBRowIndexItem(){ Index = 47, Name = "家族事项2性别" },
                    new JJBRowIndexItem(){ Index = 48, Name = "家族事项2生日" },
                    new JJBRowIndexItem(){ Index = 49, Name = "家族事项2关系" },
                    new JJBRowIndexItem(){ Index = 50, Name = "家族事项2出席教会" },
                    new JJBRowIndexItem(){ Index = 51, Name = "家族事项2SCJ工号" },
                }
            };

            errMsg = "";
            try
            {
                if (request.Count == 0)
                {
                    errMsg = "请传入数据";
                    return null;
                }

                #region 验证行，获取新增动态行数量

                var dynamicRowCount = 0;
                var educationNumber = 1;

                if (request[0].Count == 51) // 1个学历
                {
                    dynamicRowCount = 0; // 动态增加0行
                    educationNumber = 1;
                }
                else if (request[0].Count == 56) // 2个学历
                {
                    dynamicRowCount = 5; // 动态增加5行
                    educationNumber = 2;
                }
                else if (request[0].Count == 61) // 3个学历
                {
                    dynamicRowCount = 10; // 动态增加10行
                    educationNumber = 3;
                }
                else if (request[0].Count == 66) // 4个学历
                {
                    dynamicRowCount = 15; // 动态增加15行
                    educationNumber = 4;
                }
                else
                {
                    errMsg = $"表格的行数不正确，行数只能为51/56/61/66，您的行数是{request[0].Count}";
                    return null;
                }

                for (int i = 0; i < educationNumber; i++)
                {
                    rowIndexOptions.List.Add(new JJBRowIndexItem() { Index = 24 + (i * 5), Name = $"学历信息{i + 1}分类" });
                    rowIndexOptions.List.Add(new JJBRowIndexItem() { Index = 25 + (i * 5), Name = $"学历信息{i + 1}学校名" });
                    rowIndexOptions.List.Add(new JJBRowIndexItem() { Index = 26 + (i * 5), Name = $"学历信息{i + 1}专业" });
                    rowIndexOptions.List.Add(new JJBRowIndexItem() { Index = 27 + (i * 5), Name = $"学历信息{i + 1}状态" });
                    rowIndexOptions.List.Add(new JJBRowIndexItem() { Index = 28 + (i * 5), Name = $"学历信息{i + 1}期间" });
                }

                rowIndexOptions.List.First(p => p.Name == "兄弟姐妹关系").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项1姓名").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项1性别").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项1生日").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项1关系").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项1出席教会").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项1SCJ工号").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项2姓名").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项2性别").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项2生日").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项2关系").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项2出席教会").Index += dynamicRowCount;
                rowIndexOptions.List.First(p => p.Name == "家族事项2SCJ工号").Index += dynamicRowCount;

                #endregion

                var list = new List<Member>(); // 定义学生列表
                for (int index = 0; index < request.Count; index++)
                {
                    var itemList = request[index];

                    var member = new Member()
                    {
                        ColumnIndex = index + 1,
                        Index = GetItemString(rowIndexOptions, itemList, "排序"),
                        KoreanName = GetItemString(rowIndexOptions, itemList, "韩语姓名"),
                        ScjNumber = GetItemString(rowIndexOptions, itemList, "工号"),
                        ChineseName = GetItemString(rowIndexOptions, itemList, "汉语姓名"),
                        Country = GetItemString(rowIndexOptions, itemList, "国籍"),
                        IDCardBirthday = GetItemString(rowIndexOptions, itemList, "法定出生生日"),
                        Gender = GetItemString(rowIndexOptions, itemList, "性别"),
                        // ActualBirthday = GetItemString(rowIndexOptions, itemList, "实际生日"),
                        // IDCardAddressChinese = GetItemString(rowIndexOptions, itemList, "身份证地址中文"),
                        // IDCardAddressKorean = GetItemString(rowIndexOptions, itemList, "身份证地址韩文"),
                        CurrentAddressChinese = GetItemString(rowIndexOptions, itemList, "现住址"),
                        IDCardAddressChinese = GetItemString(rowIndexOptions, itemList, "身份证地址"),
                        // CurrentAddressKorean = GetItemString(rowIndexOptions, itemList, "现住址翻译"),
                        Phone = GetItemString(rowIndexOptions, itemList, "电话"),
                        Height = GetItemString(rowIndexOptions, itemList, "身高"),
                        BloodType = GetItemString(rowIndexOptions, itemList, "血型"),
                        IsMarried = GetItemString(rowIndexOptions, itemList, "婚否"),
                        CDType = GetItemString(rowIndexOptions, itemList, "CD类型"),
                        //CDTypeComment = GetColumnComment(itemList, "CD类型"),
                        //CDTypeCommentString = GetColumnCommentString(itemList, "CD类型"),
                        BirthReligion = GetItemString(rowIndexOptions, itemList, "出身教团"),
                        OtherReligion = GetItemString(rowIndexOptions, itemList, "其他宗教"),
                        ChurchName = GetItemString(rowIndexOptions, itemList, "教会名"),
                        Position = GetItemString(rowIndexOptions, itemList, "职分"),
                        YearsOfFaith = GetItemString(rowIndexOptions, itemList, "信仰时间"),
                        MaternalFaith = GetItemString(rowIndexOptions, itemList, "母胎信仰"),
                        Email = GetItemString(rowIndexOptions, itemList, "邮箱"),
                        FinalEducation = GetItemString(rowIndexOptions, itemList, "最终学历"),
                        SiblingRelationship = GetItemString(rowIndexOptions, itemList, "兄弟姐妹关系"),
                        EducationList = new List<EducationInfo>(),
                        FamilyMemberList = new List<FamilyMemberInfo>()
                    };

                    #region 不需要那么严格

                    member.CDType = member.CDType.TrimEnd();
                    //member.CDTypeCommentString = (member.CDTypeCommentString ?? "").TrimEnd();
                    member.FinalEducation = member.FinalEducation.TrimEnd();
                    // member.SiblingRelationship = member.SiblingRelationship.Trim();

                    #endregion

                    for (int i = 1; i <= educationNumber; i++)
                    {
                        var educationInfo = new EducationInfo()
                        {
                            Index = i,
                            Category = GetItemString(rowIndexOptions, itemList, $"学历信息{i}分类"),
                            SchoolName = GetItemString(rowIndexOptions, itemList, $"学历信息{i}学校名"),
                            Major = GetItemString(rowIndexOptions, itemList, $"学历信息{i}专业"),
                            Status = GetItemString(rowIndexOptions, itemList, $"学历信息{i}状态"),
                            Period = GetItemString(rowIndexOptions, itemList, $"学历信息{i}期间")
                        };
                        if (!string.IsNullOrEmpty(educationInfo.Category) || !string.IsNullOrEmpty(educationInfo.SchoolName) || !string.IsNullOrEmpty(educationInfo.Major) || !string.IsNullOrEmpty(educationInfo.Status) || !string.IsNullOrEmpty(educationInfo.Period))
                        {
                            member.EducationList.Add(educationInfo);
                        }
                    }
                    for (int i = 1; i <= 2; i++)
                    {
                        var familyInfo = new FamilyMemberInfo()
                        {
                            Index = i,
                            Name = GetItemString(rowIndexOptions, itemList, $"家族事项{i}姓名"),
                            Gender = GetItemString(rowIndexOptions, itemList, $"家族事项{i}性别"),
                            Birthday = GetItemString(rowIndexOptions, itemList, $"家族事项{i}生日"),
                            Relationship = GetItemString(rowIndexOptions, itemList, $"家族事项{i}关系"),
                            // Religion = GetItemString(rowIndexOptions, itemList, $"家族事项{i}教团"),
                            // OtherReligion = GetItemString(rowIndexOptions, itemList, $"家族事项{i}教团-其他"),
                            // BirthChurch = GetItemString(rowIndexOptions, itemList, $"家族事项{i}出身教会"),
                            BirthChurch = GetItemString(rowIndexOptions, itemList, $"家族事项{i}出席教会"),
                            SCJNumber = GetItemString(rowIndexOptions, itemList, $"家族事项{i}SCJ工号")
                        };

                        #region 不需要那么严格

                        familyInfo.Name = familyInfo.Name.Trim();
                        familyInfo.Relationship = familyInfo.Relationship.TrimEnd();

                        #endregion

                        if (!string.IsNullOrEmpty(familyInfo.Name) || !string.IsNullOrEmpty(familyInfo.Gender) || !string.IsNullOrEmpty(familyInfo.Birthday) || !string.IsNullOrEmpty(familyInfo.Relationship) || !string.IsNullOrEmpty(familyInfo.BirthChurch) || !string.IsNullOrEmpty(familyInfo.SCJNumber))
                        {
                            member.FamilyMemberList.Add(familyInfo);
                        }
                    }

                    // 序号 韩文名 中文名
                    if (!string.IsNullOrEmpty(member.ChineseName))
                    {
                        list.Add(member);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                errMsg = ex.ToLogString();
                Console.WriteLine(errMsg);
                return null;
            }
        }
    }
}
