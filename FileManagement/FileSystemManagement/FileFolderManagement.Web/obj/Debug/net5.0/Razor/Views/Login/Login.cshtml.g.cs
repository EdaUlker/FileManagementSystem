#pragma checksum "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "37c8af03b690cf7aa8e1b390b0384f725e194cb4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Login), @"mvc.1.0.view", @"/Views/Login/Login.cshtml")]
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
#nullable restore
#line 1 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\_ViewImports.cshtml"
using FileFolderManagement.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\_ViewImports.cshtml"
using FileFolderManagement.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37c8af03b690cf7aa8e1b390b0384f725e194cb4", @"/Views/Login/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"74a4aedaf31260c84b96fd155363b1a24d53f065", @"/Views/_ViewImports.cshtml")]
    public class Views_Login_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FileSystemManagement.Core.Models.TblUser>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
  
    var returnUrl = ViewData["ReturnUrl"] as string;
    var error = TempData["Error"] as string;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<style>
    .divider:after,
    .divider:before {
        content: """";
        flex: 1;
        height: 1px;
        background: #eee;
    }
</style>

<link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3"" crossorigin=""anonymous"">
<script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"" integrity=""sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p"" crossorigin=""anonymous""></script>



<section class=""vh-100"">
    <div class=""container py-5 h-100"">
        <div class=""row d-flex align-items-center justify-content-center h-100"">
            <div class=""col-md-8 col-lg-7 col-xl-6"">
                <!--<img src=""https://www.freepik.com/free-icon/login_14224655.htm"" />-->
                <!--<img src=""/FileSystemManagement/FileFolderManagement.Web/Images/workspace_48px.png"" />-->
                <!--<img sr");
            WriteLiteral("c=\"/Images/295128.png\" />-->\r\n            </div>\r\n            <div class=\"col-md-7 col-lg-5 col-xl-5 offset-xl-1\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "37c8af03b690cf7aa8e1b390b0384f725e194cb45201", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 33 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
                     if (!string.IsNullOrEmpty(error))
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <h2 class=\"alert alert-danger\">");
#nullable restore
#line 35 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
                                          Write(error);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n");
#nullable restore
#line 36 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <!-- Email input -->\r\n                    <div class=\"form-outline mb-4\">\r\n                        ");
#nullable restore
#line 39 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
                   Write(Html.TextBoxFor(model => model.Username, new { @class = "form-control form-control-lg", id = "form1Example13", type = "" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        <label class=\"form-label\" for=\"form1Example13\">Email address</label>\r\n                    </div>\r\n\r\n                    <!-- Password input -->\r\n                    <div class=\"form-outline mb-4\">\r\n                        ");
#nullable restore
#line 45 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
                   Write(Html.TextBoxFor(model => model.Password, new { @class = "form-control form-control-lg", id = "form1Example23", type = "password" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        <label class=""form-label"" for=""form1Example23"">Password</label>
                    </div>

                    <div class=""d-flex justify-content-around align-items-center mb-4"">
                        <!-- Checkbox -->
                        <a href=""#!"">Forgot password?</a>
                    </div>

    
                    <!-- Submit button -->
                    <button type=""submit"" class=""btn btn-primary btn-lg btn-block"">Sign in</button>

                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 32 "C:\Users\DEYTEK-DELL\Desktop\FileManagement\FileSystemManagement\FileFolderManagement.Web\Views\Login\Login.cshtml"
AddHtmlAttributeValue("", 1326, Url.Action("Login","Login"), 1326, 28, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FileSystemManagement.Core.Models.TblUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
