using System.Text.Json.Serialization;

namespace JsonGuard.Benchmarks.Simple;

// No checks
#pragma warning disable CS8618
public class PayloadNoChecks
{
    public Guid TransactionId { get; set; }
    public string Timestamp { get; set; }
    public int Version { get; set; }
    public UserDetailsNoChecks User { get; set; }
    public AppMetadataNoChecks Metadata { get; set; }
    public List<OrderRecordNoChecks> Orders { get; set; }
}

public class UserDetailsNoChecks
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Age { get; set; }
    public bool IsPremium { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}

public class AppMetadataNoChecks
{
    public string ClientIp { get; set; }
    public string UserAgent { get; set; }
    public string AppVersion { get; set; }
    public string DeviceId { get; set; }
    public double Latency { get; set; }
}

public class OrderRecordNoChecks
{
    public string OrderId { get; set; }
    public double TotalValue { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string CreatedAt { get; set; }
    public List<string> Tags { get; set; }
}
#pragma warning restore CS8618

// JsonGuard
[JsonGuard]
public partial class PayloadJsonGuard
{
    public Guid TransactionId { get; set; }
    public string Timestamp { get; set; }
    public int Version { get; set; }
    public UserDetailsJsonGuard User { get; set; }
    public AppMetadataJsonGuard Metadata { get; set; }
    public List<OrderRecordJsonGuard> Orders { get; set; }
}

[JsonGuard]
public partial class UserDetailsJsonGuard
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Age { get; set; }
    public bool IsPremium { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}

[JsonGuard]
public partial class AppMetadataJsonGuard
{
    public string ClientIp { get; set; }
    public string UserAgent { get; set; }
    public string AppVersion { get; set; }
    public string DeviceId { get; set; }
    public double Latency { get; set; }
}

[JsonGuard]
public partial class OrderRecordJsonGuard
{
    public string OrderId { get; set; }
    public double TotalValue { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string CreatedAt { get; set; }
    public List<string> Tags { get; set; }
}

// JsonRequired
#pragma warning disable CS8618
public class PayloadJsonRequired
{
    [JsonRequired] public Guid TransactionId { get; set; }
    [JsonRequired] public string Timestamp { get; set; }
    [JsonRequired] public int Version { get; set; }
    [JsonRequired] public UserDetailsJsonRequired User { get; set; }
    [JsonRequired] public AppMetadataJsonRequired Metadata { get; set; }
    [JsonRequired] public List<OrderRecordJsonRequired> Orders { get; set; }
}

public class UserDetailsJsonRequired
{
    [JsonRequired] public string FirstName { get; set; }
    [JsonRequired] public string LastName { get; set; }
    [JsonRequired] public string Email { get; set; }
    [JsonRequired] public string PhoneNumber { get; set; }
    [JsonRequired] public int Age { get; set; }
    [JsonRequired] public bool IsPremium { get; set; }
    [JsonRequired] public string AddressLine1 { get; set; }
    [JsonRequired] public string AddressLine2 { get; set; }
    [JsonRequired] public string City { get; set; }
    [JsonRequired] public string Country { get; set; }
    [JsonRequired] public string ZipCode { get; set; }
}

public class AppMetadataJsonRequired
{
    [JsonRequired] public string ClientIp { get; set; }
    [JsonRequired] public string UserAgent { get; set; }
    [JsonRequired] public string AppVersion { get; set; }
    [JsonRequired] public string DeviceId { get; set; }
    [JsonRequired] public double Latency { get; set; }
}

public class OrderRecordJsonRequired
{
    [JsonRequired] public string OrderId { get; set; }
    [JsonRequired] public double TotalValue { get; set; }
    [JsonRequired] public string Currency { get; set; }
    [JsonRequired] public string Status { get; set; }
    [JsonRequired] public string CreatedAt { get; set; }
    [JsonRequired] public List<string> Tags { get; set; }
}
#pragma warning restore CS8618

// Required
public class PayloadRequired
{
    public required Guid TransactionId { get; set; }
    public required string Timestamp { get; set; }
    public required int Version { get; set; }
    public required UserDetailsRequired User { get; set; }
    public required AppMetadataRequired Metadata { get; set; }
    public required List<OrderRecordRequired> Orders { get; set; }
}

public class UserDetailsRequired
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required int Age { get; set; }
    public required bool IsPremium { get; set; }
    public required string AddressLine1 { get; set; }
    public required string AddressLine2 { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
}

public class AppMetadataRequired
{
    public required string ClientIp { get; set; }
    public required string UserAgent { get; set; }
    public required string AppVersion { get; set; }
    public required string DeviceId { get; set; }
    public required double Latency { get; set; }
}

public class OrderRecordRequired
{
    public required string OrderId { get; set; }
    public required double TotalValue { get; set; }
    public required string Currency { get; set; }
    public required string Status { get; set; }
    public required string CreatedAt { get; set; }
    public required List<string> Tags { get; set; }
}
