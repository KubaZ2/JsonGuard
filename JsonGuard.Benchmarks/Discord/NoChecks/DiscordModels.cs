using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonGuard.Benchmarks.Discord.NoChecks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class JsonRole
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("colors")]
    public JsonRoleColors Colors { get; set; }

    [JsonPropertyName("hoist")]
    public bool Hoist { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("unicode_emoji")]
    public string? UnicodeEmoji { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("permissions")]
    public ulong Permissions { get; set; }

    [JsonPropertyName("managed")]
    public bool Managed { get; set; }

    [JsonPropertyName("mentionable")]
    public bool Mentionable { get; set; }

    [JsonPropertyName("tags")]
    public JsonRoleTags? Tags { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }
}

public class JsonRoleColors
{
    [JsonPropertyName("primary_color")]
    public int PrimaryColor { get; set; }

    [JsonPropertyName("secondary_color")]
    public int? SecondaryColor { get; set; }

    [JsonPropertyName("tertiary_color")]
    public int? TertiaryColor { get; set; }
}

public class NullConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => true;

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteNullValue();
    }
}

public class JsonRoleTags
{
    [JsonPropertyName("bot_id")]
    public ulong? BotId { get; set; }

    [JsonPropertyName("integration_id")]
    public ulong? IntegrationId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(NullConverter))]
    [JsonPropertyName("premium_subscriber")]
    public bool IsPremiumSubscriber { get; set; }

    [JsonPropertyName("subscription_listing_id")]
    public ulong? SubscriptionListingId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(NullConverter))]
    [JsonPropertyName("available_for_purchase")]
    public bool IsAvailableForPurchase { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(NullConverter))]
    [JsonPropertyName("guild_connections")]
    public bool GuildConnections { get; set; }
}

public class JsonEmoji
{
    [JsonPropertyName("id")]
    public ulong? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("roles")]
    public ulong[]? AllowedRoles { get; set; }

    [JsonPropertyName("user")]
    public JsonUser? Creator { get; set; }

    [JsonPropertyName("require_colons")]
    public bool? RequireColons { get; set; }

    [JsonPropertyName("managed")]
    public bool? Managed { get; set; }

    [JsonPropertyName("animated")]
    public bool? Animated { get; set; }

    [JsonPropertyName("available")]
    public bool? Available { get; set; }
}

public class JsonUser
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("discriminator")]
    public ushort Discriminator { get; set; }

    [JsonPropertyName("global_name")]
    public string? GlobalName { get; set; }

    [JsonPropertyName("avatar")]
    public string? AvatarHash { get; set; }

    [JsonPropertyName("bot")]
    public bool? IsBot { get; set; }

    [JsonPropertyName("system")]
    public bool? IsSystemUser { get; set; }

    [JsonPropertyName("mfa_enabled")]
    public bool? MfaEnabled { get; set; }

    [JsonPropertyName("banner")]
    public string? BannerHash { get; set; }

    [JsonPropertyName("accent_color")]
    public int? AccentColor { get; set; }

    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    [JsonPropertyName("verified")]
    public bool? Verified { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("flags")]
    public ulong? Flags { get; set; }

    [JsonPropertyName("premium_type")]
    public int? PremiumType { get; set; }

    [JsonPropertyName("public_flags")]
    public ulong? PublicFlags { get; set; }

    [JsonPropertyName("avatar_decoration_data")]
    public JsonAvatarDecorationData? AvatarDecorationData { get; set; }

    [JsonPropertyName("collectibles")]
    public JsonCollectibles? Collectibles { get; set; }

    [JsonPropertyName("primary_guild")]
    public JsonUserPrimaryGuild? PrimaryGuild { get; set; }

    [JsonPropertyName("member")]
    public JsonGuildUser? GuildUser { get; set; }
}

public class JsonAvatarDecorationData
{
    [JsonPropertyName("asset")]
    public string Hash { get; set; }

    [JsonPropertyName("sku_id")]
    public ulong SkuId { get; set; }
}

public class JsonCollectibles
{
    [JsonPropertyName("nameplate")]
    public JsonNameplate? Nameplate { get; set; }
}

public class JsonNameplate
{
    [JsonPropertyName("sku_id")]
    public ulong SkuId { get; set; }

    [JsonPropertyName("asset")]
    public string Asset { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonPropertyName("palette")]
    public string Palette { get; set; }
}

public class JsonUserPrimaryGuild
{
    [JsonPropertyName("identity_guild_id")]
    public ulong? IdentityGuildId { get; set; }

    [JsonPropertyName("identity_enabled")]
    public bool? IdentityEnabled { get; set; }

    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("badge")]
    public string? BadgeHash { get; set; }
}

public class JsonGuildUser
{
    [JsonPropertyName("user")]
    public JsonUser User { get; set; }

    [JsonPropertyName("nick")]
    public string? Nickname { get; set; }

    [JsonPropertyName("avatar")]
    public string? GuildAvatarHash { get; set; }

    [JsonPropertyName("banner")]
    public string? GuildBannerHash { get; set; }

    [JsonPropertyName("roles")]
    public ulong[] RoleIds { get; set; }

    [JsonPropertyName("joined_at")]
    public DateTimeOffset? JoinedAt { get; set; }

    [JsonPropertyName("premium_since")]
    public DateTimeOffset? GuildBoostStart { get; set; }

    [JsonPropertyName("deaf")]
    public bool Deafened { get; set; }

    [JsonPropertyName("mute")]
    public bool Muted { get; set; }

    [JsonPropertyName("flags")]
    public int GuildFlags { get; set; }

    [JsonPropertyName("pending")]
    public bool? IsPending { get; set; }

    [JsonPropertyName("permissions")]
    public ulong? Permissions { get; set; }

    [JsonPropertyName("communication_disabled_until")]
    public DateTimeOffset? TimeOutUntil { get; set; }

    [JsonPropertyName("avatar_decoration_data")]
    public JsonAvatarDecorationData? GuildAvatarDecorationData { get; set; }
}

public class JsonVoiceState
{
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }

    [JsonPropertyName("user_id")]
    public ulong UserId { get; set; }

    [JsonPropertyName("member")]
    public JsonGuildUser? User { get; set; }

    [JsonPropertyName("session_id")]
    public string SessionId { get; set; }

    [JsonPropertyName("deaf")]
    public bool IsDeafened { get; set; }

    [JsonPropertyName("mute")]
    public bool IsMuted { get; set; }

    [JsonPropertyName("self_deaf")]
    public bool IsSelfDeafened { get; set; }

    [JsonPropertyName("self_mute")]
    public bool IsSelfMuted { get; set; }

    [JsonPropertyName("self_stream")]
    public bool? SelfStreamExists { get; set; }

    [JsonPropertyName("self_video")]
    public bool SelfVideoExists { get; set; }

    [JsonPropertyName("suppress")]
    public bool Suppressed { get; set; }

    [JsonPropertyName("request_to_speak_timestamp")]
    public DateTimeOffset? RequestToSpeakTimestamp { get; set; }
}

public class JsonPermissionOverwrite
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("allow")]
    public ulong Allowed { get; set; }

    [JsonPropertyName("deny")]
    public ulong Denied { get; set; }
}

public class JsonGuildThreadMetadata
{
    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("auto_archive_duration")]
    public int AutoArchiveDuration { get; set; }

    [JsonPropertyName("archive_timestamp")]
    public DateTimeOffset ArchiveTimestamp { get; set; }

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("invitable")]
    public bool? Invitable { get; set; }

    [JsonPropertyName("create_timestamp")]
    public DateTimeOffset? CreatedAt { get; set; }
}

public class JsonThreadCurrentUser
{
    [JsonPropertyName("join_timestamp")]
    public DateTimeOffset JoinTimestamp { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }
}

public class JsonForumTag
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("moderated")]
    public bool Moderated { get; set; }

    [JsonPropertyName("emoji_id")]
    public ulong? EmojiId { get; set; }

    [JsonPropertyName("emoji_name")]
    public string? EmojiName { get; set; }
}

public class JsonForumGuildChannelDefaultReaction
{
    [JsonPropertyName("emoji_id")]
    public ulong? EmojiId { get; set; }

    [JsonPropertyName("emoji_name")]
    public string? EmojiName { get; set; }
}

public class JsonGuildChannelMention
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class JsonAttachment
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("filename")]
    public string FileName { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("content_type")]
    public string? ContentType { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("proxy_url")]
    public string ProxyUrl { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }

    [JsonPropertyName("placeholder")]
    public string? Placeholder { get; set; }

    [JsonPropertyName("placeholder_version")]
    public int? PlaceholderVersion { get; set; }

    [JsonPropertyName("ephemeral")]
    public bool? Ephemeral { get; set; }

    [JsonPropertyName("duration_secs")]
    public double? DurationSeconds { get; set; }

    [JsonPropertyName("waveform")]
    public byte[]? Waveform { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    [JsonPropertyName("clip_participants")]
    public JsonUser[]? ClipParticipants { get; set; }

    [JsonPropertyName("clip_created_at")]
    public DateTimeOffset? ClipCreatedAt { get; set; }

    [JsonPropertyName("application")]
    public JsonApplication? Application { get; set; }
}

public class JsonTeamUser
{
    [JsonPropertyName("membership_state")]
    public int MembershipState { get; set; }

    [JsonPropertyName("team_id")]
    public ulong TeamId { get; set; }

    [JsonPropertyName("user")]
    public JsonUser User { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }
}

public class JsonTeam
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("members")]
    public JsonTeamUser[] Users { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("owner_user_id")]
    public ulong OwnerId { get; set; }
}

public class JsonApplicationInstallParams
{
    [JsonPropertyName("scopes")]
    public string[] Scopes { get; set; }

    [JsonPropertyName("permissions")]
    public ulong Permissions { get; set; }
}

public class JsonApplicationIntegrationTypeConfiguration
{
    [JsonPropertyName("oauth2_install_params")]
    public JsonApplicationInstallParams? OAuth2InstallParams { get; set; }
}

public class JsonApplication
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("rpc_origins")]
    public string[] RpcOrigins { get; set; }

    [JsonPropertyName("bot_public")]
    public bool? BotPublic { get; set; }

    [JsonPropertyName("bot_require_code_grant")]
    public bool? BotRequireCodeGrant { get; set; }

    [JsonPropertyName("bot")]
    public JsonUser? Bot { get; set; }

    [JsonPropertyName("terms_of_service_url")]
    public string? TermsOfServiceUrl { get; set; }

    [JsonPropertyName("privacy_policy_url")]
    public string? PrivacyPolicyUrl { get; set; }

    [JsonPropertyName("owner")]
    public JsonUser? Owner { get; set; }

    [JsonPropertyName("verify_key")]
    public string VerifyKey { get; set; }

    [JsonPropertyName("team")]
    public JsonTeam? Team { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("guild")]
    public JsonGuild? Guild { get; set; }

    [JsonPropertyName("primary_sku_id")]
    public ulong? PrimarySkuId { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("cover_image")]
    public string? CoverImageHash { get; set; }

    [JsonPropertyName("flags")]
    public uint? Flags { get; set; }

    [JsonPropertyName("approximate_guild_count")]
    public int? ApproximateGuildCount { get; set; }

    [JsonPropertyName("approximate_user_install_count")]
    public int? ApproximateUserInstallCount { get; set; }

    [JsonPropertyName("redirect_uris")]
    public string[]? RedirectUris { get; set; }

    [JsonPropertyName("interactions_endpoint_url")]
    public string? InteractionsEndpointUrl { get; set; }

    [JsonPropertyName("role_connections_verification_url")]
    public string? RoleConnectionsVerificationUrl { get; set; }

    [JsonPropertyName("event_webhooks_url")]
    public string? EventWebhooksUrl { get; set; }

    [JsonPropertyName("event_webhooks_status")]
    public int EventWebhooksStatus { get; set; }

    [JsonPropertyName("event_webhooks_types")]
    public string[]? EventWebhooksTypes { get; set; }

    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

    [JsonPropertyName("install_params")]
    public JsonApplicationInstallParams? InstallParams { get; set; }

    [JsonPropertyName("integration_types_config")]
    public IReadOnlyDictionary<int, JsonApplicationIntegrationTypeConfiguration> IntegrationTypesConfiguration { get; set; }

    [JsonPropertyName("custom_install_url")]
    public string? CustomInstallUrl { get; set; }
}

public class JsonEmbedFooter
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; set; }

    [JsonPropertyName("proxy_icon_url")]
    public string? ProxyIconUrl { get; set; }
}

public class JsonEmbedImage
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }
}

public class JsonEmbedThumbnail
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }
}

public class JsonEmbedVideo
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; set; }

    [JsonPropertyName("height")]
    public int? Height { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }
}

public class JsonEmbedProvider
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class JsonEmbedAuthor
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; set; }

    [JsonPropertyName("proxy_icon_url")]
    public string? ProxyIconUrl { get; set; }
}

public class JsonEmbedField
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("inline")]
    public bool? Inline { get; set; }
}

public class JsonEmbed
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTimeOffset? Timestamp { get; set; }

    [JsonPropertyName("color")]
    public int? Color { get; set; }

    [JsonPropertyName("footer")]
    public JsonEmbedFooter? Footer { get; set; }

    [JsonPropertyName("image")]
    public JsonEmbedImage? Image { get; set; }

    [JsonPropertyName("thumbnail")]
    public JsonEmbedThumbnail? Thumbnail { get; set; }

    [JsonPropertyName("video")]
    public JsonEmbedVideo? Video { get; set; }

    [JsonPropertyName("provider")]
    public JsonEmbedProvider? Provider { get; set; }

    [JsonPropertyName("author")]
    public JsonEmbedAuthor? Author { get; set; }

    [JsonPropertyName("fields")]
    public JsonEmbedField[] Fields { get; set; }
}

public class JsonMessageReactionCountDetails
{
    [JsonPropertyName("burst")]
    public int Burst { get; set; }

    [JsonPropertyName("normal")]
    public int Normal { get; set; }
}

public class JsonMessageReaction
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("count_details")]
    public JsonMessageReactionCountDetails CountDetails { get; set; }

    [JsonPropertyName("me")]
    public bool Me { get; set; }

    [JsonPropertyName("me_burst")]
    public bool MeBurst { get; set; }

    [JsonPropertyName("emoji")]
    public JsonEmoji Emoji { get; set; }

    [JsonPropertyName("burst_colors")]
    public int[] BurstColors { get; set; }
}

public class JsonMessageActivity
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("party_id")]
    public string? PartyId { get; set; }
}

public class JsonMessageReference
{
    [JsonPropertyName("type")]
    public byte? Type { get; set; }

    [JsonPropertyName("message_id")]
    public ulong? MessageId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("fail_if_not_exists")]
    public bool? FailIfNotExists { get; set; }
}

public class JsonMessageSnapshot
{
    [JsonPropertyName("message")]
    public JsonMessageSnapshotMessage Message { get; set; }
}

public class JsonMessageSnapshotMessage
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("embeds")]
    public JsonEmbed[] Embeds { get; set; }

    [JsonPropertyName("attachments")]
    public JsonAttachment[] Attachments { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("edited_timestamp")]
    public DateTimeOffset? EditedAt { get; set; }

    [JsonPropertyName("flags")]
    public uint? Flags { get; set; }

    [JsonPropertyName("mentions")]
    public JsonUser[] MentionedUsers { get; set; }

    [JsonPropertyName("mention_roles")]
    public ulong[] MentionedRoleIds { get; set; }

    [JsonPropertyName("sticker_items")]
    public JsonMessageSticker[]? Stickers { get; set; }
}

public class JsonMessageSticker
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("format_type")]
    public int Format { get; set; }
}

public class JsonMessageInteractionMetadata
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("user")]
    public JsonUser User { get; set; }

    [JsonPropertyName("authorizing_integration_owners")]
    public IReadOnlyDictionary<int, ulong> AuthorizingIntegrationOwners { get; set; }

    [JsonPropertyName("original_response_message_id")]
    public ulong? OriginalResponseMessageId { get; set; }

    [JsonPropertyName("interacted_message_id")]
    public ulong? InteractedMessageId { get; set; }

    [JsonPropertyName("triggering_interaction_metadata")]
    public JsonMessageInteractionMetadata? TriggeringInteractionMetadata { get; set; }
}

public class JsonChannel
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }

    [JsonPropertyName("permission_overwrites")]
    public JsonPermissionOverwrite[]? PermissionOverwrites { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("topic")]
    public string? Topic { get; set; }

    [JsonPropertyName("nsfw")]
    public bool? Nsfw { get; set; }

    [JsonPropertyName("last_message_id")]
    public ulong? LastMessageId { get; set; }

    [JsonPropertyName("bitrate")]
    public int? Bitrate { get; set; }

    [JsonPropertyName("user_limit")]
    public int? UserLimit { get; set; }

    [JsonPropertyName("rate_limit_per_user")]
    public int? Slowmode { get; set; }

    [JsonPropertyName("recipients")]
    public JsonUser[]? Users { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("owner_id")]
    public ulong? OwnerId { get; set; }

    [JsonPropertyName("application_id")]
    public ulong? ApplicationId { get; set; }

    [JsonPropertyName("managed")]
    public bool? Managed { get; set; }

    [JsonPropertyName("parent_id")]
    public ulong? ParentId { get; set; }

    [JsonPropertyName("last_pin_timestamp")]
    public DateTimeOffset? LastPin { get; set; }

    [JsonPropertyName("rtc_region")]
    public string? RtcRegion { get; set; }

    [JsonPropertyName("video_quality_mode")]
    public int? VideoQualityMode { get; set; }

    [JsonPropertyName("message_count")]
    public int? MessageCount { get; set; }

    [JsonPropertyName("member_count")]
    public int? UserCount { get; set; }

    [JsonPropertyName("thread_metadata")]
    public JsonGuildThreadMetadata? Metadata { get; set; }

    [JsonPropertyName("member")]
    public JsonThreadCurrentUser? CurrentUser { get; set; }

    [JsonPropertyName("default_auto_archive_duration")]
    public int? DefaultAutoArchiveDuration { get; set; }

    [JsonPropertyName("permissions")]
    public ulong? Permissions { get; set; }

    [JsonPropertyName("flags")]
    public int? Flags { get; set; }

    [JsonPropertyName("total_message_sent")]
    public int? TotalMessageSent { get; set; }

    [JsonPropertyName("available_tags")]
    public JsonForumTag[]? AvailableTags { get; set; }

    [JsonPropertyName("applied_tags")]
    public ulong[]? AppliedTags { get; set; }

    [JsonPropertyName("default_reaction_emoji")]
    public JsonForumGuildChannelDefaultReaction? DefaultReactionEmoji { get; set; }

    [JsonPropertyName("default_thread_rate_limit_per_user")]
    public int? DefaultThreadSlowmode { get; set; }

    [JsonPropertyName("default_sort_order")]
    public int? DefaultSortOrder { get; set; }

    [JsonPropertyName("default_forum_layout")]
    public int? DefaultForumLayout { get; set; }

    [JsonPropertyName("newly_created")]
    public bool? NewlyCreated { get; set; }
}

public class JsonUserActivityTimestamps
{
    [JsonPropertyName("start")]
    public long? StartTime { get; set; }

    [JsonPropertyName("end")]
    public long? EndTime { get; set; }
}

public class JsonParty
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("size")]
    public long[]? Size { get; set; }
}

public class JsonUserActivityAssets
{
    [JsonPropertyName("large_image")]
    public string? LargeImageId { get; set; }

    [JsonPropertyName("large_text")]
    public string? LargeText { get; set; }

    [JsonPropertyName("small_image")]
    public string? SmallImageId { get; set; }

    [JsonPropertyName("small_text")]
    public string? SmallText { get; set; }
}

public class JsonUserActivitySecrets
{
    [JsonPropertyName("join")]
    public string? Join { get; set; }

    [JsonPropertyName("spectate")]
    public string? Spectate { get; set; }

    [JsonPropertyName("match")]
    public string? Match { get; set; }
}

public class JsonUserActivity
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }

    [JsonPropertyName("timestamps")]
    public JsonUserActivityTimestamps? Timestamps { get; set; }

    [JsonPropertyName("application_id")]
    public ulong? ApplicationId { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("emoji")]
    public JsonEmoji? Emoji { get; set; }

    [JsonPropertyName("party")]
    public JsonParty? Party { get; set; }

    [JsonPropertyName("assets")]
    public JsonUserActivityAssets? Assets { get; set; }

    [JsonPropertyName("secrets")]
    public JsonUserActivitySecrets? Secrets { get; set; }

    [JsonPropertyName("instance")]
    public bool? Instance { get; set; }

    [JsonPropertyName("flags")]
    public int? Flags { get; set; }

    [JsonPropertyName("buttons")]
    public string[]? Buttons { get; set; }
}

public class JsonPresence
{
    [JsonPropertyName("user")]
    public JsonPresenceUser User { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("activities")]
    public JsonUserActivity[]? Activities { get; set; }

    [JsonPropertyName("client_status")]
    public IReadOnlyDictionary<string, string> Platform { get; set; }
}

public class JsonPresenceUser
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("discriminator")]
    public ushort? Discriminator { get; set; }

    [JsonPropertyName("global_name")]
    public string? GlobalName { get; set; }

    [JsonPropertyName("avatar")]
    public string? AvatarHash { get; set; }

    [JsonPropertyName("bot")]
    public bool? IsBot { get; set; }

    [JsonPropertyName("system")]
    public bool? IsSystemUser { get; set; }

    [JsonPropertyName("mfa_enabled")]
    public bool? MfaEnabled { get; set; }

    [JsonPropertyName("banner")]
    public string? BannerHash { get; set; }

    [JsonPropertyName("accent_color")]
    public int? AccentColor { get; set; }

    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    [JsonPropertyName("verified")]
    public bool? Verified { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("flags")]
    public ulong? Flags { get; set; }

    [JsonPropertyName("premium_type")]
    public int? PremiumType { get; set; }

    [JsonPropertyName("public_flags")]
    public ulong? PublicFlags { get; set; }

    [JsonPropertyName("avatar_decoration_data")]
    public JsonAvatarDecorationData? AvatarDecorationData { get; set; }

    [JsonPropertyName("collectibles")]
    public JsonCollectibles? Collectibles { get; set; }

    [JsonPropertyName("primary_guild")]
    public JsonUserPrimaryGuild? PrimaryGuild { get; set; }

    [JsonPropertyName("member")]
    public JsonGuildUser? GuildUser { get; set; }
}

public class JsonWelcomeScreenChannel
{
    [JsonPropertyName("channel_id")]
    public ulong ChannelId { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("emoji_id")]
    public ulong? EmojiId { get; set; }

    [JsonPropertyName("emoji_name")]
    public string? EmojiName { get; set; }
}

public class JsonGuildWelcomeScreen
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("welcome_channels")]
    public JsonWelcomeScreenChannel[] WelcomeChannels { get; set; }
}

public class JsonStageInstance
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong ChannelId { get; set; }

    [JsonPropertyName("topic")]
    public string Topic { get; set; }

    [JsonPropertyName("privacy_level")]
    public int PrivacyLevel { get; set; }

    [JsonPropertyName("discoverable_disabled")]
    public bool DiscoverableDisabled { get; set; }
}

public class JsonSticker
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("pack_id")]
    public ulong? PackId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; }

    [JsonPropertyName("format_type")]
    public int Format { get; set; }

    [JsonPropertyName("available")]
    public bool? Available { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("user")]
    public JsonUser? Creator { get; set; }

    [JsonPropertyName("sort_value")]
    public int? SortValue { get; set; }
}

public class JsonGuildScheduledEventMetadata
{
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}

public class JsonGuildScheduledEventRecurrenceRuleNWeekday
{
    [JsonPropertyName("n")]
    public int N { get; set; }

    [JsonPropertyName("day")]
    public byte Day { get; set; }
}

public class JsonGuildScheduledEventRecurrenceRule
{
    [JsonPropertyName("start")]
    public DateTimeOffset? StartAt { get; set; }

    [JsonPropertyName("end")]
    public DateTimeOffset? EndAt { get; set; }

    [JsonPropertyName("frequency")]
    public byte Frequency { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("by_weekday")]
    public byte[]? ByWeekday { get; set; }

    [JsonPropertyName("by_n_weekday")]
    public JsonGuildScheduledEventRecurrenceRuleNWeekday[]? ByNWeekday { get; set; }

    [JsonPropertyName("by_month")]
    public int[]? ByMonth { get; set; }

    [JsonPropertyName("by_month_day")]
    public int[]? ByMonthDay { get; set; }

    [JsonPropertyName("by_year_day")]
    public int[]? ByYearDay { get; set; }

    [JsonPropertyName("count")]
    public int? Count { get; set; }
}

public class JsonGuildScheduledEvent
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }

    [JsonPropertyName("creator_id")]
    public ulong? CreatorId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("scheduled_start_time")]
    public DateTimeOffset ScheduledStartTime { get; set; }

    [JsonPropertyName("scheduled_end_time")]
    public DateTimeOffset? ScheduledEndTime { get; set; }

    [JsonPropertyName("privacy_level")]
    public int PrivacyLevel { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("entity_type")]
    public int EntityType { get; set; }

    [JsonPropertyName("entity_id")]
    public ulong? EntityId { get; set; }

    [JsonPropertyName("entity_metadata")]
    public JsonGuildScheduledEventMetadata? EntityMetadata { get; set; }

    [JsonPropertyName("creator")]
    public JsonUser? Creator { get; set; }

    [JsonPropertyName("user_count")]
    public int? UserCount { get; set; }

    [JsonPropertyName("image")]
    public string? CoverImageHash { get; set; }

    [JsonPropertyName("recurrence_rule")]
    public JsonGuildScheduledEventRecurrenceRule? RecurrenceRule { get; set; }
}

public class JsonGuild
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("icon_hash")]
    public string? IconHashTemplate { get; set; }

    [JsonPropertyName("splash")]
    public string? SplashHash { get; set; }

    [JsonPropertyName("discovery_splash")]
    public string? DiscoverySplashHash { get; set; }

    [JsonPropertyName("owner")]
    public bool? IsOwner { get; set; }

    [JsonPropertyName("owner_id")]
    public ulong OwnerId { get; set; }

    [JsonPropertyName("permissions")]
    public ulong? Permissions { get; set; }

    [JsonPropertyName("afk_channel_id")]
    public ulong? AfkChannelId { get; set; }

    [JsonPropertyName("afk_timeout")]
    public int AfkTimeout { get; set; }

    [JsonPropertyName("widget_enabled")]
    public bool? WidgetEnabled { get; set; }

    [JsonPropertyName("widget_channel_id")]
    public ulong? WidgetChannelId { get; set; }

    [JsonPropertyName("verification_level")]
    public int VerificationLevel { get; set; }

    [JsonPropertyName("default_message_notifications")]
    public int DefaultMessageNotificationLevel { get; set; }

    [JsonPropertyName("explicit_content_filter")]
    public int ContentFilter { get; set; }

    [JsonPropertyName("roles")]
    public JsonRole[] Roles { get; set; }

    [JsonPropertyName("emojis")]
    public JsonEmoji[] Emojis { get; set; }

    [JsonPropertyName("features")]
    public string[] Features { get; set; }

    [JsonPropertyName("mfa_level")]
    public int MfaLevel { get; set; }

    [JsonPropertyName("application_id")]
    public ulong? ApplicationId { get; set; }

    [JsonPropertyName("system_channel_id")]
    public ulong? SystemChannelId { get; set; }

    [JsonPropertyName("system_channel_flags")]
    public int SystemChannelFlags { get; set; }

    [JsonPropertyName("rules_channel_id")]
    public ulong? RulesChannelId { get; set; }

    [JsonPropertyName("joined_at")]
    public DateTimeOffset JoinedAt { get; set; }

    [JsonPropertyName("large")]
    public bool IsLarge { get; set; }

    [JsonPropertyName("unavailable")]
    public bool? IsUnavailable { get; set; }

    [JsonPropertyName("member_count")]
    public int UserCount { get; set; }

    [JsonPropertyName("voice_states")]
    public JsonVoiceState[] VoiceStates { get; set; }

    [JsonPropertyName("members")]
    public JsonGuildUser[] Users { get; set; }

    [JsonPropertyName("channels")]
    public JsonChannel[] Channels { get; set; }

    [JsonPropertyName("threads")]
    public JsonChannel[] ActiveThreads { get; set; }

    [JsonPropertyName("presences")]
    public JsonPresence[] Presences { get; set; }

    [JsonPropertyName("max_presences")]
    public int? MaxPresences { get; set; }

    [JsonPropertyName("max_members")]
    public int? MaxUsers { get; set; }

    [JsonPropertyName("vanity_url_code")]
    public string? VanityUrlCode { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("banner")]
    public string? BannerHash { get; set; }

    [JsonPropertyName("premium_tier")]
    public int PremiumTier { get; set; }

    [JsonPropertyName("premium_subscription_count")]
    public int? PremiumSubscriptionCount { get; set; }

    [JsonPropertyName("preferred_locale")]
    public string PreferredLocale { get; set; }

    [JsonPropertyName("public_updates_channel_id")]
    public ulong? PublicUpdatesChannelId { get; set; }

    [JsonPropertyName("max_video_channel_users")]
    public int? MaxVideoChannelUsers { get; set; }

    [JsonPropertyName("max_stage_video_channel_users")]
    public int? MaxStageVideoChannelUsers { get; set; }

    [JsonPropertyName("approximate_member_count")]
    public int? ApproximateUserCount { get; set; }

    [JsonPropertyName("approximate_presence_count")]
    public int? ApproximatePresenceCount { get; set; }

    [JsonPropertyName("welcome_screen")]
    public JsonGuildWelcomeScreen? WelcomeScreen { get; set; }

    [JsonPropertyName("nsfw_level")]
    public int NsfwLevel { get; set; }

    [JsonPropertyName("stage_instances")]
    public JsonStageInstance[] StageInstances { get; set; }

    [JsonPropertyName("stickers")]
    public JsonSticker[] Stickers { get; set; }

    [JsonPropertyName("guild_scheduled_events")]
    public JsonGuildScheduledEvent[] ScheduledEvents { get; set; }

    [JsonPropertyName("premium_progress_bar_enabled")]
    public bool PremiumProgressBarEnabled { get; set; }

    [JsonPropertyName("safety_alerts_channel_id")]
    public ulong? SafetyAlertsChannelId { get; set; }
}
