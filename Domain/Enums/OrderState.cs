namespace Domain.Enums
{
    public enum OrderState
    {
        New = 1,
        Created = 2,
        SentToUNS = 3,
        SentToSAP = 4,
        SendToSapFailed = 200
    }
}
