#pragma checksum "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afc72016598f9e167790064c8e3a9001d7d54ef8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_SelectManageBook), @"mvc.1.0.view", @"/Views/Home/SelectManageBook.cshtml")]
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
#line 1 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\_ViewImports.cshtml"
using LibraryRedux;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\_ViewImports.cshtml"
using LibraryRedux.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afc72016598f9e167790064c8e3a9001d7d54ef8", @"/Views/Home/SelectManageBook.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9f19031d22554875643752fe289e72abf4c8518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_SelectManageBook : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Book>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateBook", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
  
    ViewData["Title"] = "Adminstrator View";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h1>Update Book - <i>");
#nullable restore
#line 9 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
                Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</i></h1>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afc72016598f9e167790064c8e3a9001d7d54ef84522", async() => {
                WriteLiteral("\r\n    <table cellpadding=\"2\">\r\n        <tr>\r\n            <td>Book ID: </td><td><input type=\"text\"");
                BeginWriteAttribute("value", " value=\"", 312, "\"", 329, 1);
#nullable restore
#line 13 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
WriteAttributeValue("", 320, Model.Id, 320, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"id\" readonly /></td>\r\n        </tr>\r\n        <tr>\r\n            <td>Title: </td>\r\n            <td><input type=\"text\"");
                BeginWriteAttribute("value", " value=\"", 452, "\"", 472, 1);
#nullable restore
#line 17 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
WriteAttributeValue("", 460, Model.Title, 460, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"title\" pattern=\"[A-Za-z0-9]+\" /></td>\r\n        </tr>\r\n        <tr>\r\n            <td>Author: </td>\r\n            <td><input type=\"text\"");
                BeginWriteAttribute("value", " value=\"", 613, "\"", 634, 1);
#nullable restore
#line 21 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
WriteAttributeValue("", 621, Model.Author, 621, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"author\" pattern=\"[A-Za-z.]+\" /></td>\r\n        </tr>\r\n        <tr>\r\n            <td>Number Available: </td>\r\n            <td><input type=\"text\"");
                BeginWriteAttribute("value", " value=\"", 784, "\"", 808, 1);
#nullable restore
#line 25 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
WriteAttributeValue("", 792, Model.Available, 792, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"avail\" pattern=\"[0-1][0-9]\" /></td>\r\n        </tr>\r\n        <tr>\r\n            <td>Media Type: </td>\r\n            <td><input type=\"text\"");
                BeginWriteAttribute("value", " value=\"", 951, "\"", 971, 1);
#nullable restore
#line 29 "C:\Users\Jason Gardner\source\repos\LibraryRedux\LibraryRedux\Views\Home\SelectManageBook.cshtml"
WriteAttributeValue("", 959, Model.Genre, 959, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"genre\" /></td>\r\n        </tr>\r\n    </table>\r\n    <button type=\"submit\">Update</button>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Book> Html { get; private set; }
    }
}
#pragma warning restore 1591
