namespace BusinessCardChallenge
{
    public class BusinessCardParser
    {
        public ContactInfo getContactInfo(string document)
        {
            return new ContactInfo(document);
        }
    }
}