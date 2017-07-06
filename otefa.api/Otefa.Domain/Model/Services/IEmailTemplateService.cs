namespace Otefa.Domain.Model.Services
{
    public interface IEmailTemplateService
    {
        string RenderBody(string emailTemplatePath, object anonymousModel);
    }
}