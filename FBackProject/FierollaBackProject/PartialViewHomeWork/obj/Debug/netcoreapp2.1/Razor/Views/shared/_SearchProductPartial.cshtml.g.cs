#pragma checksum "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\_SearchProductPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3eaca7c943cf2782144aba2a71f83abb862bff3c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_shared__SearchProductPartial), @"mvc.1.0.view", @"/Views/shared/_SearchProductPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/shared/_SearchProductPartial.cshtml", typeof(AspNetCore.Views_shared__SearchProductPartial))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3eaca7c943cf2782144aba2a71f83abb862bff3c", @"/Views/shared/_SearchProductPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7351bf22c8b3447ad5bf605fabd1a93497c17c1f", @"/Views/_ViewImports.cshtml")]
    public class Views_shared__SearchProductPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(31, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\_SearchProductPartial.cshtml"
 foreach (Product item in Model)
{

#line default
#line hidden
            BeginContext(70, 20, true);
            WriteLiteral("    <li>\r\n        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 90, "\"", 126, 2);
            WriteAttributeValue("", 97, "/ProductDetail/Index/", 97, 21, true);
#line 7 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\_SearchProductPartial.cshtml"
WriteAttributeValue("", 118, item.Id, 118, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(127, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(129, 10, false);
#line 7 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\_SearchProductPartial.cshtml"
                                           Write(item.Title);

#line default
#line hidden
            EndContext();
            BeginContext(139, 17, true);
            WriteLiteral("</a>\r\n    </li>\r\n");
            EndContext();
#line 9 "C:\Users\User\Desktop\PartialViewHomeWork\PartialViewHomeWork\Views\shared\_SearchProductPartial.cshtml"
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