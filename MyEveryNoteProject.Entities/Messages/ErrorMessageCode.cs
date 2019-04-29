
namespace MyEveryNoteProject.Entities.Messages
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExist=101,
        EmailAlreadyExist=102,
        UserIsNotActive=201,
        UsernameOrPaswordNotMatch=202,
        CheckYourEmail=301,
        UserAlreadyActive=303,
        ActivateIdDoesNotExist=304,
        UserNotFound=305,
        ProfileNotUpdated=150,
        UserCouldNotRemove = 306,
        UserCouldNotFind = 307,
        ProfileDeletionVerifyInfoWrong=308,
        UserCouldNotInserted=309,
        UserCouldNotUpdated=310
    }
}
