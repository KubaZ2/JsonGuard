namespace JsonGuard.Benchmarks.Simple;

public static class JsonData
{
    public static readonly byte[] Json = """
        {"TransactionId":"d94285b0-8c26-4d26-9c4c-3edeb565ff51","Timestamp":"2023-10-27T10:00:00Z","Version":2,"User":{"FirstName":"John","LastName":"Doe","Email":"john.doe@example.com","PhoneNumber":"+1-555-0198","Age":34,"IsPremium":true,"AddressLine1":"123 Main St","AddressLine2":"Apt 4B","City":"New York","Country":"USA","ZipCode":"10001"},"Metadata":{"ClientIp":"192.168.1.100","UserAgent":"Mozilla/5.0","AppVersion":"1.4.2","DeviceId":"device-xy-999","Latency":42.5},"Orders":[{"OrderId":"ORD-001","TotalValue":150.75,"Currency":"USD","Status":"Shipped","CreatedAt":"2023-10-25T08:30:00Z","Tags":["electronics","sale"]},{"OrderId":"ORD-002","TotalValue":22.50,"Currency":"USD","Status":"Pending","CreatedAt":"2023-10-26T14:15:00Z","Tags":["books"]},{"OrderId":"ORD-003","TotalValue":999.99,"Currency":"USD","Status":"Delivered","CreatedAt":"2023-10-20T10:00:00Z","Tags":["computers","high-value","fragile"]}]}
        """u8.ToArray();
}
