#pragma checksum "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5deb7c0b3761a0a5b904e28abd118f5416291269"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__CartPartial), @"mvc.1.0.view", @"/Views/Shared/_CartPartial.cshtml")]
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
#line 1 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\_ViewImports.cshtml"
using MuiltyShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\_ViewImports.cshtml"
using MuiltyShop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\_ViewImports.cshtml"
using MuiltyShop.Models.Product.Category;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
using MuiltyShop.Areas.Product.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5deb7c0b3761a0a5b904e28abd118f5416291269", @"/Views/Shared/_CartPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1aa5f571d911d52215de61bd809e948680ff7c5", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__CartPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route", "cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 7 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
  
    var cart = _cartService.GetCartItems();

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5deb7c0b3761a0a5b904e28abd118f54162912694576", async() => {
                WriteLiteral("\r\n        <i class=\"fas fa-shopping-cart text-primary\"></i>\r\n");
#nullable restore
#line 11 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
         if (cart.Count() > 0)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <span class=\"badge text-secondary border border-secondary rounded-circle\" style=\"padding-bottom: 2px;\">");
#nullable restore
#line 13 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
                                                                                                              Write(cart.Count);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n");
#nullable restore
#line 14 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <span class=\"badge text-secondary border border-secondary rounded-circle\" style=\"padding-bottom: 2px;\">0</span>\r\n");
#nullable restore
#line 18 "D:\wordspace\languages\sharp\project\MuiltyShop\MuiltyShop\Views\Shared\_CartPartial.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Route = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public CartService _cartService { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
