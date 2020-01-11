using MyVet.Prism.Interfaces;
using MyVet.Prism.Resources;
using Xamarin.Forms;

namespace MyVet.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Email => Resource.Email;

        public static string EmailError => Resource.EmailError;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Error => Resource.Error;

        public static string Forgot => Resource.Forgot;

        public static string Login => Resource.Login;

        public static string LoginError => Resource.LoginError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Rememberme => Resource.Rememberme;
        public static string Delete => Resource.Delete;

        public static string EditPet => Resource.EditPet;

        public static string ChangeImage => Resource.ChangeImage;

        public static string Name => Resource.Name;

        public static string NameError => Resource.NameError;

        public static string NamePlaceHolder => Resource.NamePlaceHolder;

        public static string Race => Resource.Race;

        public static string RaceError => Resource.RaceError;

        public static string RacePlaceHolder => Resource.RacePlaceHolder;

        public static string PetType => Resource.PetType;

        public static string PetTypeError => Resource.PetTypeError;

        public static string PetTypePlaceHolder => Resource.PetTypePlaceHolder;

        public static string Born => Resource.Born;

        public static string Remarks => Resource.Remarks;

        public static string Saving => Resource.Saving;

        public static string Save => Resource.Save;

        public static string CreateEditPetConfirm => Resource.CreateEditPetConfirm;

        public static string Created => Resource.Created;

        public static string Edited => Resource.Edited;

        public static string Confirm => Resource.Confirm;

        public static string QuestionToDeletePet => Resource.QuestionToDeletePet;

        public static string Yes => Resource.Yes;

        public static string No => Resource.No;

        public static string Ok => Resource.Ok;
    }
}
