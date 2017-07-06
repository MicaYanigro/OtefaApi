using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
using Otefa.Domain.Model.Services;

namespace Otefa.Infrastructure.EmailSending
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string RenderBody(string emailTemplateFileName, object anonymousModel)
        {
            var emailTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "EmailTemplates", emailTemplateFileName);

            return Engine.Razor.RunCompile(File.ReadAllText(emailTemplatePath), Guid.NewGuid().ToString(), null, anonymousModel);
        }
    }
}
