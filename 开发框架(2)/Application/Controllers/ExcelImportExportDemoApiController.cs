using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Aspose.Cells;
using System.Data;

namespace Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcelImportExportDemoApiController : ControllerBase
    {
        private static IHostingEnvironment _hostingEnvironment;

        public ExcelImportExportDemoApiController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 导入EXCEL
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CommonDto.ResultInfoOutput ImportExcel()
        {
            CommonDto.ResultInfoOutput resultInfoOutput = new CommonDto.ResultInfoOutput();
            resultInfoOutput.resultInfo = new Dto.ResultInfo();
            string strExtension = Path.GetExtension(Request.Form.Files[0].FileName);
            if (!(strExtension.ToUpper() == ".XLS" || strExtension.ToUpper() == ".XLSX")) {
                resultInfoOutput.resultInfo.IsSuccess = 0;
                resultInfoOutput.resultInfo.ErrorInfo = "文件后缀必须是.xls或.xlsx";
            }
            Workbook workbook = new Workbook(Request.Form.Files[0].OpenReadStream());
            Cells cells = workbook.Worksheets[0].Cells;
            int maxDataRow = workbook.Worksheets[0].Cells.MaxDataRow;
            int maxDataColumn = workbook.Worksheets[0].Cells.MaxDataColumn + 1;
            DataTable dataTable = workbook.Worksheets[0].Cells.ExportDataTable(2, 0, maxDataRow, maxDataColumn);

            resultInfoOutput.resultInfo.IsSuccess = 1;
            return resultInfoOutput;
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        [HttpPost]
        public Dto.ExportExcelOutput ExportExcel() {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("acc");
            for (int i = 0; i <= 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = "姓名" + i;
                dr["acc"] = "acc" + i;
                dt.Rows.Add(dr);
            }
            dt.TableName = "table";

            Workbook wb = new Workbook(Path.Combine(_hostingEnvironment.WebRootPath, "template\\test.xls"));
            WorkbookDesigner designer = new WorkbookDesigner(wb);
            designer.SetDataSource(dt);

            designer.Process();
            designer.Workbook.CalculateFormula(true);//如需计算“合计”之类的东西请添加本行代码
            Random rnd = new Random();
            int intRnd = rnd.Next(1000, 9999);
            string strPath = "DownLoad/" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + intRnd + ".xlsx";
            string downLoadFilePath = Path.Combine(_hostingEnvironment.WebRootPath, strPath);
            wb.Save(downLoadFilePath, new XlsSaveOptions(SaveFormat.Xlsx));

            Dto.ExportExcelOutput exportExcelOutput = new Dto.ExportExcelOutput();
            exportExcelOutput.resultInfo = new Dto.ResultInfo();
            exportExcelOutput.resultInfo.IsSuccess = 1;
            exportExcelOutput.downLoadPath =Cathy.ConfigHelper.GetSection("WebUrl").Value +"/"+ strPath;
            return exportExcelOutput;
        }


        [HttpPost]
        public JsonResult UploadFile()
        {
            IFormFileCollection fileCollection = HttpContext.Request.Form.Files;
            var currentDate = DateTime.Now;
            var webRootPath = _hostingEnvironment.WebRootPath;//相当于HttpContext.Current.Server.MapPath("") 

            if (fileCollection.Count > 0)
            {
                List<string> listFilePath = new List<string>();
                foreach (FormFile formFile in fileCollection)
                {
                    try
                    {
                        var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";

                        //创建每日存储文件夹
                        if (!Directory.Exists(webRootPath + filePath))
                        {
                            Directory.CreateDirectory(webRootPath + filePath);
                        }


                        //文件后缀
                        var fileExtension = Path.GetExtension(formFile.FileName);//获取文件格式，拓展名

                        //判断文件大小
                        var fileSize = formFile.Length;

                        //if (fileSize > 1024 * 1024 * 10) //10M TODO:(1mb=1024X1024b)
                        //{
                        //    return new JsonResult(new { isSuccess = false, resultMsg = "上传的文件不能大于10M" });
                        //}

                        //保存的文件名称(以名称和保存时间命名)
                        var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + currentDate.ToString("HHmmss") + fileExtension;

                        //文件保存
                        using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                        {
                            formFile.CopyTo(fs);
                            fs.Flush();
                        }

                        //完整的文件路径
                        var completeFilePath = Path.Combine(filePath, saveName);
                        listFilePath.Add(completeFilePath);
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult(new { isSuccess = false, resultMsg = "文件保存失败，异常信息为：" + ex.Message });
                    }

                }
                return new JsonResult(new { isSuccess = true, returnMsg = "上传成功", completeFilePath = listFilePath });
            }
            else
            {
                return new JsonResult(new { isSuccess = false, resultMsg = "没有可上传的文件" });
            }
        }
    }
}
