
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;

namespace XhO_OKit
{
    public class DataTableExport
    {
        /// <summary>
        /// 导出目录
        /// </summary>
        private const string ExportJsonFolder = "Assets/ExcelExport/Json/";
        private const string ExportConfigFolder = "Assets/ExcelExport/Config/";
        private const string ExportConfigTableFolder = "Assets/ExcelExport/ConfigTable/";

        [MenuItem("XhO_OKit/Excel/ExcelExport")]
        public static void ExcelExport()
        {
            ExportAll();
        }

        public static void ExcelExportProtobuf()
        {

        }

        public static void ExportAll()
        {
            ExportJson();
            ExportToModel();
            AssetDatabase.Refresh();
        }
        public static void ExportJson()
        {
            foreach (string item in Directory.GetFiles("Assets/Excels"))
            {
                if (Path.GetExtension(item) != ".xlsx") continue;
                XSSFWorkbook xssfWorkbook = new XSSFWorkbook(item);
                FileUtil.SaveAsset($"{ExportJsonFolder}{Path.GetFileNameWithoutExtension(item)}.txt", SheetToJson(xssfWorkbook.GetSheetAt(0)));
                FileUtil.SaveAsset($"{ExportJsonFolder}{Path.GetFileNameWithoutExtension(item)}.txt", SheetToJson(xssfWorkbook.GetSheetAt(0)));
            }
        }
        public static void ExportToModel()
        {
            foreach (string item in Directory.GetFiles("Assets/Excels"))
            {
                if (Path.GetExtension(item) != ".xlsx") continue;
                FileUtil.SaveAsset($"{ExportConfigTableFolder}{Path.GetFileNameWithoutExtension(item)}Table.cs", SheetToAConfigTable("namespace XhO_OKit\n", Path.GetFileNameWithoutExtension(item)));
                XSSFWorkbook xssfWorkbook = new XSSFWorkbook(item);
                FileUtil.SaveAsset($"{ExportConfigFolder}{Path.GetFileNameWithoutExtension(item)}.cs", SheetToConfig(xssfWorkbook.GetSheetAt(0), "namespace XhO_OKit\n", Path.GetFileNameWithoutExtension(item)));
            }
        }

        public static string SheetToJson(ISheet sheet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n\t");
            //第0行格子个数
            int count = sheet.GetRow(0).LastCellNum;
            List<Cell> cellList = new List<Cell>();
            for (int i = 0; i < count; i++)
            {
                string fieldName = GetCell(sheet, 0, i);
                string fieldType = GetCell(sheet, 1, i);
                string fieldDesc = GetCell(sheet, 2, i);
                cellList.Add(new Cell(fieldName, fieldType, fieldDesc));
            }
            //从第三行开始到最后
            for (int i = 3; i <= sheet.LastRowNum; i++)
            {
                if (string.IsNullOrEmpty(GetCell(sheet, i, 0))) continue;
                IRow iRow = sheet.GetRow(i);
                for (int j = 0; j < count; j++)
                {
                    Cell cell = cellList[j];
                    if (cell.desc.StartsWith("#")) continue;
                    string value = GetCell(iRow, j);
                    if (string.IsNullOrEmpty(value)) continue;
                    if (j > 0)
                    {
                        stringBuilder.Append(", ");
                    }
                    if (cell.name == "id")
                    {
                        stringBuilder.Append($"\"{value}\" : {{");
                        stringBuilder.Append($"\"Id\" : {Convert(cell.type, value)}");
                    }
                    else
                    {
                        stringBuilder.Append($"\"{cell.name}\" : {Convert(cell.type, value)}");
                    }
                }
                if (i < sheet.LastRowNum)
                {
                    stringBuilder.Append("},\n\t");
                }
                else
                {
                    stringBuilder.Append("}\n");
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }



        public static string SheetToAConfigTable(string classHead, string configName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(classHead);
            stringBuilder.Append("{\n");
            stringBuilder.Append("\t[Config]\n");
            stringBuilder.Append($"\tpublic class {configName}Table : ConfigTable<{configName}>\n");
            stringBuilder.Append("\t{\n");
            stringBuilder.Append("\t}\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        public static string SheetToConfig(ISheet sheet, string classHead, string configName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(classHead);
            stringBuilder.Append("{\n");
            stringBuilder.Append($"\tpublic class {configName} : IConfig\n");
            stringBuilder.Append("\t{\n");
            stringBuilder.Append("\t\tpublic int Id\n");
            stringBuilder.Append("\t\t{\n");
            stringBuilder.Append("\t\t\tget; set;\n");
            stringBuilder.Append("\t\t}");
            //第0行格子个数
            int count = sheet.GetRow(0).LastCellNum;
            for (int i = 0; i < count; i++)
            {
                string fieldName = GetCell(sheet, 0, i);
                string fieldType = GetCell(sheet, 1, i);
                string fieldDesc = GetCell(sheet, 2, i);
                if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldType) || fieldDesc.StartsWith("#")) continue;
                if (fieldName == "id") continue;
                if (i > 0)
                {
                    stringBuilder.Append("\n");
                }
                stringBuilder.Append($"\t\tpublic {fieldType} {fieldName};\n");
            }
            stringBuilder.Append("\t}\n");
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        public static string Convert(string type, string value)
        {
            switch (type)
            {
                case "int[]":
                case "int32[]":
                case "long[]":
                case "float[]":
                    return $"[{value}]";
                case "string[]":
                    return $"[{value}]";
                case "int":
                case "int32":
                case "int64":
                case "long":
                case "float":
                case "double":
                    return value;
                case "string":
                    return $"\"{value}\"";
                default:
                    throw new Exception($"不支持此类型 : {type}");
            }
        }
        public static string GetCell(ISheet sheet, int row, int cell)
        {
            IRow iRow = sheet?.GetRow(row);
            if (iRow != null)
            {
                return GetCell(iRow, cell);
            }
            return string.Empty;
        }
        public static string GetCell(IRow row, int cell)
        {
            ICell iCell = row?.GetCell(cell);
            if (iCell != null)
            {
                return GetCell(iCell);
            }
            return string.Empty;
        }
        public static string GetCell(ICell cell)
        {
            if (cell != null)
            {
                if (cell.CellType == CellType.Numeric || (cell.CellType == CellType.Formula && cell.CachedFormulaResultType == CellType.Numeric))
                {
                    return cell.NumericCellValue.ToString();
                }
                else if (cell.CellType == CellType.String || (cell.CellType == CellType.Formula && cell.CachedFormulaResultType == CellType.String))
                {
                    return cell.StringCellValue.ToString();
                }
                else if (cell.CellType == CellType.Boolean || (cell.CellType == CellType.Formula && cell.CachedFormulaResultType == CellType.Boolean))
                {
                    return cell.BooleanCellValue.ToString();
                }
                else
                {
                    return cell.ToString();
                }
            }
            return string.Empty;
        }
        
        public class Cell
        {
            public string name;
            public string type;
            public string desc;
            public Cell()
            {
            }
            public Cell(string name, string type, string desc)
            {
                this.name = name;
                this.type = type;
                this.desc = desc;
            }
        }
    }
}