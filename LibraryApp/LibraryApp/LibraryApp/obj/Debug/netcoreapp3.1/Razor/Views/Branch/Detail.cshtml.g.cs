#pragma checksum "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17ec7c7ae4a74b02b4e2724e8afc318a38aa2142"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Branch_Detail), @"mvc.1.0.view", @"/Views/Branch/Detail.cshtml")]
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
#line 1 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\_ViewImports.cshtml"
using LibraryApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\_ViewImports.cshtml"
using LibraryApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
using LibraryApp.Models.Branch;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17ec7c7ae4a74b02b4e2724e8afc318a38aa2142", @"/Views/Branch/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4d938fd633361b29333d6c38f61b28eb71ba8ab", @"/Views/_ViewImports.cshtml")]
    public class Views_Branch_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LibraryApp.Models.Branch.BranchDetailModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
  
    ViewBag.Title = @Model.Name + " Branch";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://code.jquery.com/jquery-3.1.1.slim.min.js"" integrity=""sha384-A7FZj7v+d/sdmMqp/nOQwliLvUsJfDHW+k9Omg/a/EheAdgtzNs3hpfag6Ed950n"" crossorigin=""anonymous""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/tether/1.4.0/js/tether.min.js"" integrity=""sha384-DztdAPBWPRXSA/3eYEEUWrWCy7G5KFbe8fFjk5JAIxUYHKkDx6Qin1DkWx51bBrb"" crossorigin=""anonymous""></script>
    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js"" integrity=""sha384-vBWWzlZJ8ea9aCX4pEW3rVHjgjt7zpkNpZk+02D9phzyeVkE+jo0ieGizqPLForn"" crossorigin=""anonymous""></script>
");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css\" integrity=\"sha384-rwoIResjU2yc3z8GV/NPeZWAv56rSmLldC3R/AZzGRnGxQQKnKkoFVhFQhNUwEyJ\" crossorigin=\"anonymous\">\r\n");
            }
            );
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"header clearfix detailHeading\">\r\n        <h2 class=\"text-muted\">Branch Information</h2>\r\n    </div>\r\n    <div class=\"jumbotron\">\r\n        <div class=\"row\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 1227, "\"", 1248, 1);
#nullable restore
#line 23 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
WriteAttributeValue("", 1233, Model.ImageUrl, 1233, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n        </div>\r\n        <div class=\"row branchInfo\">\r\n            <div class=\"col-md-8\">\r\n                <div>\r\n                    <h2>");
#nullable restore
#line 28 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                   Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                    <div class=\"branchContact\">\r\n                        <div id=\"branchAddress\">Address: ");
#nullable restore
#line 30 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                                    Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <div id=\"branchTel\">Telephone: ");
#nullable restore
#line 31 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                                  Write(Model.TelephoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    </div>\r\n                    <div id=\"branchDescription\">\r\n                        <p class=\"caption\">");
#nullable restore
#line 34 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                      Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    </div>\r\n\r\n                    <div id=\"branchHours\">\r\n                        <ul>\r\n");
#nullable restore
#line 39 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                             foreach (var day in @Model.OpenHours)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li>");
#nullable restore
#line 41 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                               Write(day);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 42 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </ul>
                    </div>
                </div>
            </div>
            <div class=""col-md-4 detailInfo"">
                <table>
                    <tr>
                        <td class=""itemLabel"">Date Opened: </td>
                        <td class=""itemValue"">");
#nullable restore
#line 51 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                         Write(Model.OpenDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"itemLabel\">Number Of Patrons: </td>\r\n                        <td class=\"itemValue\">");
#nullable restore
#line 55 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                         Write(Model.NumberOfPatrons);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"itemLabel\">Number of Assets: </td>\r\n                        <td class=\"itemValue\">");
#nullable restore
#line 59 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                         Write(Model.NumberOfAssets);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"itemLabel\">Value of Assets: </td>\r\n                        <td class=\"itemValue\">$");
#nullable restore
#line 63 "C:\Users\Joe\Documents\VS2019 Projects\LibraryApp\LibraryApp\LibraryApp\Views\Branch\Detail.cshtml"
                                          Write(Model.TotalAssetValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LibraryApp.Models.Branch.BranchDetailModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
