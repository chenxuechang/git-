#pragma checksum "D:\git\开发框架(2)\Application\Areas\SystemManage\Views\Manager\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bcae8091c8f8e92c9e5a19f42f1983bc6bc45d4c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_SystemManage_Views_Manager_Edit), @"mvc.1.0.view", @"/Areas/SystemManage/Views/Manager/Edit.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bcae8091c8f8e92c9e5a19f42f1983bc6bc45d4c", @"/Areas/SystemManage/Views/Manager/Edit.cshtml")]
    public class Areas_SystemManage_Views_Manager_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<el-dialog title=\"账单编辑\" v-model=\"editFormDialog\" top=\"5vh\">");
            WriteLiteral(@"
    <div style=""height:60vh;overflow:auto;"">
        <div style=""width:98%"">
            <el-form size=""small"" label-width=""80px"" :model=""userInfo"" ref=""userInfo"" :rules=""rules"">
                <el-form-item label=""成员"" prop=""userAccount"">
                    <el-input v-model=""userInfo.userAccount""></el-input>
                </el-form-item>
             
            <el-form-item label=""日期"" prop=""riqi"">

               
                <div class=""block"">
                    <span class=""demonstration""></span>
                    <el-date-picker v-model=""userInfo.riqi""
                                    value-format=""YYYY-MM-DD"" ");
            WriteLiteral("\r\n                                    type=\"date\" ");
            WriteLiteral("\r\n                                    placeholder=\"选择日期时间\">\r\n                                    \r\n                    </el-date-picker>\r\n              \r\n                </div>\r\n");
            WriteLiteral("            </el-form-item>\r\n\r\n");
            WriteLiteral("\r\n                <el-form-item label=\"类型\" prop=\"userSex\" >\r\n                    <el-radio-group v-model=\"userInfo.userSex\">\r\n");
            WriteLiteral(@"                        <select v-model=""userInfo.userSex"" id=""sex"" style=""width: 120px; height: 30px; border-color: darkgray;color:darkgray "">
                            <option label=""收入"">1</option>
                            <option label=""支出"">2</option>
                        </select>
                    </el-radio-group>
                </el-form-item>


                

                <el-form-item label=""用途"" prop=""userRoles"">
                    <el-select multiple collapse-tags filterable placeholder=""请选择"" v-model=""userInfo.userRoles"">
                        <el-option v-for=""item in userRole""
                                   :key=""item.roleId""
                                   :label=""item.roleName""
                                   :value=""item.roleId"">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label=""备注"">
                    <el-input v-model=""userInfo.userTitles""></el-input>
  ");
            WriteLiteral("              </el-form-item>\r\n");
            WriteLiteral("                <el-form-item label=\"金额\" prop=\"userMobilePhone\">\r\n                    <el-input v-model=\"userInfo.userMobilePhone\" onkeyup=\"value = value.replace(/(\\d)(?=(?:\\d{3})+$));\"></el-input>\r\n                </el-form-item>\r\n");
            WriteLiteral(@"            </el-form>
        </div>
    </div>
    <template #footer>
        <span class=""dialog-footer"">
            <el-button type=""primary"" size=""small"" v-on:click=""save()"">确 定</el-button>
            <el-button size=""small"" v-on:click=""editFormDialog=false"">取 消</el-button>
        </span>
    </template>
</el-dialog>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
