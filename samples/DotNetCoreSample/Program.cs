﻿using System;
using System.Linq;
using WeihanLi.Extensions;
using WeihanLi.Npoi;

// ReSharper disable All
namespace DotNetCoreSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //FluentSettingsForExcel();

            //using (var conn = new SqlConnection("server=.;uid=liweihan;pwd=Admin888;database=Reservation"))
            //{
            //    var list = conn.Select<TestEntity>(@"SELECT * FROM [Reservation].[dbo].[tabSystemSettings]");
            //    list.ToExcelFile(ApplicationHelper.MapPath("test.xlsx"));
            //    list.ToExcelFileAsync(ApplicationHelper.MapPath("testAsync.xlsx")).Wait();
            //}

            //Console.WriteLine("Success!");

            var mapping = ExcelHelper.ToEntityList<ProductPriceMapping>(@"C:\Users\liweihan.TUHU\Desktop\temp\tempFiles\mapping.xlsx");
            var mappingTemp = ExcelHelper.ToEntityList<ProductPriceMapping>(@"C:\Users\liweihan.TUHU\Desktop\temp\tempFiles\mapping_temp.xlsx");

            Console.WriteLine($"-----normal({mapping.Count}【{mapping.Select(_ => _.Pid).Distinct().Count()}】)----");
            foreach (var shop in mapping.GroupBy(_ => _.ShopCode).OrderBy(_ => _.Key))
            {
                Console.WriteLine($"{shop.Key}---{shop.Count()}---distinct pid count:{shop.Select(_ => _.Pid).Distinct().Count()}");
            }

            Console.WriteLine($"-----temp({mappingTemp.Count}【{mappingTemp.Select(_ => _.Pid).Distinct().Count()}】)----");
            foreach (var shop in mappingTemp.GroupBy(_ => _.ShopCode).OrderBy(_ => _.Key))
            {
                Console.WriteLine($"{shop.Key}---{shop.Count()}---distinct pid count:{shop.Select(_ => _.Pid).Distinct().Count()}");
            }

            Console.ReadLine();
        }

        private static void FluentSettingsForExcel()
        {
            var setting = ExcelHelper.SettingFor<TestEntity>();
            // ExcelSetting
            setting.HasAuthor("WeihanLi")
                .HasTitle("WeihanLi.Npoi test")
                .HasDescription("")
                .HasSubject("");

            setting.HasSheetConfiguration(0, "系统设置列表");

            setting.HasFilter(0, 1)
                .HasFreezePane(0, 1, 2, 1);
            setting.Property(_ => _.SettingId)
                .HasColumnIndex(0);

            setting.Property(_ => _.SettingName)
                .HasColumnTitle("设置名称")
                .HasColumnIndex(1);

            setting.Property(_ => _.DisplayName)
                .HasColumnTitle("设置显示名称")
                .HasColumnIndex(2);

            setting.Property(_ => _.SettingValue)
                .HasColumnTitle("设置值")
                .HasColumnIndex(3);

            setting.Property(_ => _.CreatedTime)
                .HasColumnTitle("创建时间")
                .HasColumnIndex(5)
                .HasColumnFormatter("yyyy-MM-dd HH:mm:ss");

            setting.Property(_ => _.CreatedBy)
                .HasColumnIndex(4)
                .HasColumnTitle("创建人");

            setting.Property(_ => _.UpdatedBy).Ignored();
            setting.Property(_ => _.UpdatedTime).Ignored();
            setting.Property(_ => _.PKID).Ignored();
        }
    }

    internal abstract class BaseEntity
    {
        public int PKID { get; set; }
    }

    internal class TestEntity : BaseEntity
    {
        public Guid SettingId { get; set; }

        public string SettingName { get; set; }

        public string DisplayName { get; set; }
        public string SettingValue { get; set; }

        public string CreatedBy { get; set; } = "liweihan";

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}
