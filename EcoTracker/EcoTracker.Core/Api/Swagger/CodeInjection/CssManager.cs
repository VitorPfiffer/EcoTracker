using Microsoft.Extensions.Configuration;

namespace EcoTracker.Core.Swagger.CodeInjection
{
    public sealed class CssManager
    {
        public CssManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string CSS;
        private readonly IConfiguration _configuration;

        public string GetCSS()
        {
            if (CSS != null)
                return CSS;

            var themeCode = _configuration["Swagger:Theme"];

            #region CSS

            var css =
@$"
.dark {{
    --muted-foreground: 240 5% 64.9%;
    --muted-border-foreground: 240 5% 30.9%;
    --main-color: #191919;
    --secondary-color: #FEFEFE;
    --third-color: {themeCode ?? "green"};
}}

.light {{
    --main-color: #FEFEFE;
    --secondary-color: #09090B;
    --muted-foreground: 9 0% 30%;
    --muted-border-foreground: 9 0% 10%;
    --third-color: {themeCode ?? "green"};
}}

html {{
    background-color: var(--main-color) !important;
    overflow-x: hidden;
}}

@keyframes loading {{
    0% {{ opacity: 1; }}
    80% {{ opacity: 1; }}    
    100% {{ opacity: 0; }}
}}

#loading-new-swagger {{
    z-index: 99;
    position: absolute;
    width: 100vw;
    height: 100vh;
    background-color: var(--main-color);
    overflow-x: hidden;    
    overflow-y: hidden;
    display: flex;
    flex-direction: column;
    justify-content: center;
    flex-wrap: wrap;
    align-items: center;
    opacity: 0;
    animation: loading 1s ease-out;
    gap: 20px;
}}

#loading-new-swagger p {{
    font-family: sans-serif !important;
    font-size: 20px !important;
    color: var(--secondary-color) !important;
    font-weight: lighter !important;
    opacity: 1 !important;
}}

@keyframes spin {{
  0% {{ transform: rotate(0deg); }}
  100% {{ transform: rotate(360deg); }}
}}


.loader {{
  border: 2px solid var(--secondary-color);
  border-top: 2px solid var(--third-color);
  border-radius: 50%;
  filter: drop-shadow(0 0 1em var(--third-color));
  width: 80px;
  height: 80px;
  animation: spin 2s linear infinite;
}}

#loading-new-swagger img {{
    width: 400px;
}}

section, .scheme-container {{
    background-color: var(--main-color) !important;
}}

.modal-ux {{
    color: hsl(var(--muted-foreground)) !important;
    background-color: var(--main-color) !important;
}}

.modal-ux h3, label {{
    color: hsl(var(--muted-foreground)) !important;
}}

.modal-ux input {{
    color: hsl(var(--muted-foreground)) !important;
    border: 1px solid hsl(var(--muted-foreground)) !important;
}}

select {{
    color: hsl(var(--muted-foreground)) !important;
    background-color: var(--main-color) !important;
    border: 1px solid hsl(var(--muted-foreground)) !important;
}}

.light .topbar {{
    border-bottom: 1px solid rgba(33, 33, 33, 0.2) !important;
}}

.example.microlight {{
    color: var(--secondary-color) !important;
}}

.topbar {{
    background-color: var(--main-color) !important;
    border-bottom: 1px solid hsl(var(--muted-border-foreground)) !important;
}}

label {{
    width: 30% !important;
}}

.title {{
    color: var(--secondary-color) !important;
}}

p {{
    color: hsl(var(--muted-foreground)) !important;
}}

:not(code) > span {{
    color: hsl(var(--muted-foreground)) !important;
}}

.wrapper :not(code) > span {{
    color: var(--secondary-color) !important;
}}

.swagger-ui .info .title small.version-stamp {{
    background-color: var(--third-color) !important;
}}

.operation-filter-input {{
    color: var(--secondary-color) !important;
    border: 1px solid hsl(var(--muted-foreground)) !important;
    background-color: var(--main-color) !important;
}}

.opblock-section-header {{
    background-color: var(--main-color) !important;
}}

.opblock-body .opblock-section-header .tryout {{
    background-color: var(--main-color) !important;
}}

.response-controls {{
    background: transparent !important;
}}

.opblock-body .opblock-section-header .tryout button {{
    color: white !important;
    border: 1px solid hsl(var(--muted-foreground)) !important;
}}

.btn.execute {{
    border: 1px solid hsl(var(--muted-foreground)) !important;
    font-weight: 100 !important;
    background: transparent !important;
    transition: color ease-in-out 300ms, transform ease-in-out 300ms, background ease-in-out 300ms, font-weight ease-in-out 500ms, box-shadow ease-in-out 300ms;
}}

.btn.execute:hover {{
    background: var(--third-color) !important;
    color: var(--main-color) !important;
    transform: scale(101%) !important;
    box-shadow: 0px 0px 10px rgba(var(--third-color), 0.5) !important;
}}

.swagger-ui .opblock-body pre.microlight {{
    color: #FEFEFE !important;
}}

h4 {{
    color: var(--secondary-color) !important;    
}}

td {{
    color: var(--secondary-color) !important;
}}

input {{
    background-color: var(--main-color) !important;
    border: none !important;
    color: var(--secondary-color) !important;
}}

.opblock-summary button:focus {{
    outline: none!important;
}}

button {{
    color: var(--secondary-color) !important;
}}

.opblock-tag-section pre {{
    background: #191919 !important;
}}

.opblock-tag-section textarea {{
    color: var(--secondary-color) !important;
    background: var(--main-color) !important;
}}

code {{
    color: #FEFEFE !important;
}}

span.opblock-summary-method {{
    color: var(--main-color) !important;
}}

span.opblock-summary-path {{
    font-weight: 100 !important;
}}

.opblock-summary svg {{
    fill: var(--secondary-color) !important;
}}

.description a {{
    color: var(--third-color) !important;
    text-decoration: none !important;
}}
";

            #endregion

            CSS = css;

            return css;
        }
    }
}
