#pragma checksum "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b80bd05a41af703b080522fc866462838523c661"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_shared_Components_Products_Default), @"mvc.1.0.view", @"/Views/shared/Components/Products/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/shared/Components/Products/Default.cshtml", typeof(AspNetCore.Views_shared_Components_Products_Default))]
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
#line 1 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\_ViewImports.cshtml"
using PartialViewHomeWork.Models;

#line default
#line hidden
#line 2 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\_ViewImports.cshtml"
using PartialViewHomeWork.ViewModel;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b80bd05a41af703b080522fc866462838523c661", @"/Views/shared/Components/Products/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7351bf22c8b3447ad5bf605fabd1a93497c17c1f", @"/Views/_ViewImports.cshtml")]
    public class Views_shared_Components_Products_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Product>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
 foreach (Product item in Model)
{
  

#line default
#line hidden
            BeginContext(70, 106, true);
            WriteLiteral("    <div class=\"col-sm-6 col-md-4 col-lg-3 mt-3\">\r\n        <div class=\"product-item text-center\" data-id=\"");
            EndContext();
            BeginContext(177, 28, false);
#line 6 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
                                                  Write(item.Category.Name.ToLower());

#line default
#line hidden
            EndContext();
            BeginContext(205, 84, true);
            WriteLiteral("\">\r\n            <div class=\"img\">\r\n                <a href=\"\">\r\n                    ");
            EndContext();
            BeginContext(289, 58, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "f12915c114604b75ba06ec796f90eaca", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 299, "~/img/", 299, 6, true);
#line 9 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
AddHtmlAttributeValue("", 305, item.ImageName, 305, 15, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(347, 102, true);
            WriteLiteral("\r\n                </a>\r\n            </div>\r\n            <div class=\"title mt-3\">\r\n                <h6>");
            EndContext();
            BeginContext(450, 10, false);
#line 13 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
               Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(460, 125, true);
            WriteLiteral("</h6>\r\n            </div>\r\n            <div class=\"price\">\r\n               \r\n                <a class=\"addtobasket\" data-id=\"");
            EndContext();
            BeginContext(586, 7, false);
#line 17 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
                                           Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(593, 99, true);
            WriteLiteral("\"><span class=\"text-black-50\">Add to cart</span></a>\r\n                <span class=\"text-black-50\">$");
            EndContext();
            BeginContext(693, 10, false);
#line 18 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
                                        Write(item.Price);

#line default
#line hidden
            EndContext();
            BeginContext(703, 57, true);
            WriteLiteral("</span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
#line 22 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\Components\Products\Default.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
