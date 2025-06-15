using Microsoft.Extensions.Configuration;

namespace EcoTracker.Core.Swagger.CodeInjection
{
    public sealed class JavascriptManager
    {
        public JavascriptManager(IConfiguration configuration) => _configuration = configuration;

        private string JS;
        private readonly IConfiguration _configuration;

        public string GetJavascript()
        {
            if (JS != null)
                return JS;

            var logoURL = _configuration["Swagger:CustomLogoUrl"];
            var imgScript = logoURL != null ? @$"<img src=""{logoURL}"" style=""width: 100px"" />" : "";

            var projectName = _configuration["Swagger:Title"] ?? _configuration["ProjectName"] ?? "Api";
            var finalProjectName = "Docs - " + projectName;

            var faviconUrl = _configuration["Swagger:FavicoUrl"];
            var finalFavicoUrl = !string.IsNullOrWhiteSpace(faviconUrl) ? $"link.href = '{faviconUrl}'" : "";

            #region Javascript

            var js =

@$"
// Load Screen Creation
$(""html"").append(""<div id='loading-new-swagger' > <div class='loader'> </div> <p>Loading...</p> </div>"");

// Auxiliar Methods
const applyTheme = () => {{
    const themeSelected = window.localStorage.getItem(""theme-selected"");
    
    if (!themeSelected) {{
        $(""html"").addClass(""dark"");
        return;
    }}
    
    $(""html"").removeClass(""dark"");
    $(""html"").removeClass(""light"");

    $(""html"").addClass(themeSelected.toLowerCase());
}}

const applyCustomFavicon = () => {{
    var link = document.querySelector(""link[rel~='icon']"");
    
    if (!link) {{
        link = document.createElement('link');
        link.rel = 'icon';
        document.head.appendChild(link);
    }}

    {finalFavicoUrl}
}}

const applyCustomPageBanner = () => {{
    $('.topbar-wrapper').find('svg').remove();
    $('.topbar-wrapper').children('.link').append('{imgScript}');
}}

const applyCustomTitle = () => {{
    document.title = '{finalProjectName}'
}}

const updateApiVersionSelectLabel = () => {{
    $("".select-label"").children(""span"").text(""API Version"");
}}

const createThemeSelect = () => {{
    const themeSelected = window.localStorage.getItem(""theme-selected"");

    var select = $(""<select>"")
    .append($(""<option>"").text(""Light"").val(""Light""))
    .append($(""<option>"").text(""Dark"").val(""Dark""));

    select.attr(""id"", ""theme-select"");

    if (themeSelected) select.val(themeSelected);

    select.change(() => {{
        if (select.val() === ""Light"")
            window.localStorage.setItem(""theme-selected"", ""Light"");     
        else
            window.localStorage.setItem(""theme-selected"", ""Dark"");

        applyTheme();
    }});

    var label = $(""<label>"")
    .addClass(""select-label"")
    .append($(""<span>"").text(""Theme""))
    .append(select);

    label.attr(""for"", ""select"");

    $("".download-url-wrapper"").append(label);
}}

const hasCustomLayoutApplied = () => {{
    return $('#theme-select').attr('id') != undefined;
}}

// Main Methods

const applyCustomLayout = () => {{
    if (hasCustomLayoutApplied()) return;

    applyCustomTitle();
    
    applyCustomFavicon();
    
    applyCustomPageBanner();
    
    updateApiVersionSelectLabel();

    createThemeSelect();
}};

$(document).ready(() => {{
    window.localStorage.setItem(""theme-selected"", ""Dark"");
    
    applyTheme();
}})

var checkExist = setInterval(function () {{
    if ($('.information-container') != null && $('#select') != null) {{
        applyCustomLayout();
        
        setTimeout(() => {{
            applyCustomLayout();

            $(""#loading-new-swagger"").remove();
        }}, 2000);
        
        clearInterval(checkExist);
    }}
}}, 100);
";

            #endregion

            JS = js;
            return js;
        }
    }
}
