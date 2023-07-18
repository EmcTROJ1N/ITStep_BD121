using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FileExplorer;

public static class CarouselItemHelper
{
    public static IDisposable CarouselItem(this IHtmlHelper html, string imgPath)
    {
        string beginHtml = $@"
            <div class=""single-hero-item set-bg"" data-setbg=""{imgPath}"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""col-lg-12"">
                            <div class=""hero-text"">
        ";
        string endHtml = @"
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        ";
        html.ViewContext.Writer.Write(beginHtml);
        return new SectionWrapper(endHtml, html.ViewContext.Writer);
    }
}

public class SectionWrapper : IDisposable
{
    private readonly string ClosingStr;
    private readonly TextWriter Writer;
    public SectionWrapper(string closingStr, TextWriter writer)
    {
        this.ClosingStr = closingStr;
        this.Writer = writer;
    }
    public void Dispose() =>
        this.Writer.Write(this.ClosingStr);
}