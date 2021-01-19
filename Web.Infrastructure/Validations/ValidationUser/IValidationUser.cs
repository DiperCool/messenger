namespace Web.Infrastructure.Validations.ValidationUser
{
    public interface IValidationUser
    {
        bool userIsExist(string login);
    }
}