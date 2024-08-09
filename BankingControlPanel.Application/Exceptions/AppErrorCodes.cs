namespace BankingControlPanel.Application.Exceptions
{
    public enum AppErrorCodes
    {
        None = 0,
        PhysicalPersonNotFound,
        PhysicalPersonAndRelatedPersonAreSame,
        RelatedPersonNotFound,
        RelationshipAlreadyExists,
        RelationshipDoesNotExist,
        FileDoesNotExist,
        CityNotFound,
        InvalidPhoneNumber,
        InvalidFirstName,
        InvalidLastName,
        InvalidGender,
        InvalidIdentificationNumber,
        InvalidAge,
        InvalidFileName,
        InvalidPhysicalPerson,
        InvalidRelationshipType
    }
}
