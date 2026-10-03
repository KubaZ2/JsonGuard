using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonGuard.Benchmarks.Discord.Required;

public class JsonRole
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("colors")]
    public required JsonRoleColors Colors { get; set; }

    [JsonPropertyName("hoist")]
    public required bool Hoist { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("unicode_emoji")]
    public string? UnicodeEmoji { get; set; }

    [JsonPropertyName("position")]
    public required int Position { get; set; }

    [JsonPropertyName("permissions")]
    public required ulong Permissions { get; set; }

    [JsonPropertyName("managed")]
    public required bool Managed { get; set; }

    [JsonPropertyName("mentionable")]
    public required bool Mentionable { get; set; }

    [JsonPropertyName("tags")]
    public JsonRoleTags? Tags { get; set; }

    [JsonPropertyName("flags")]
    public required int Flags { get; set; }
}

public class JsonRoleColors
{
    [JsonPropertyName("primary_color")]
    public required int PrimaryColor { get; set; }

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
    public required ulong Id { get; set; }

    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("discriminator")]
    public required ushort Discriminator { get; set; }

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
    public required string Hash { get; set; }

    [JsonPropertyName("sku_id")]
    public required ulong SkuId { get; set; }
}

public class JsonCollectibles
{
    [JsonPropertyName("nameplate")]
    public JsonNameplate? Nameplate { get; set; }
}

public class JsonNameplate
{
    [JsonPropertyName("sku_id")]
    public required ulong SkuId { get; set; }

    [JsonPropertyName("asset")]
    public required string Asset { get; set; }

    [JsonPropertyName("label")]
    public required string Label { get; set; }

    [JsonPropertyName("palette")]
    public required string Palette { get; set; }
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
    public required JsonUser User { get; set; }

    [JsonPropertyName("nick")]
    public string? Nickname { get; set; }

    [JsonPropertyName("avatar")]
    public string? GuildAvatarHash { get; set; }

    [JsonPropertyName("banner")]
    public string? GuildBannerHash { get; set; }

    [JsonPropertyName("roles")]
    public required ulong[] RoleIds { get; set; }

    [JsonPropertyName("joined_at")]
    public DateTimeOffset? JoinedAt { get; set; }

    [JsonPropertyName("premium_since")]
    public DateTimeOffset? GuildBoostStart { get; set; }

    [JsonPropertyName("deaf")]
    public required bool Deafened { get; set; }

    [JsonPropertyName("mute")]
    public required bool Muted { get; set; }

    [JsonPropertyName("flags")]
    public required int GuildFlags { get; set; }

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
    public required ulong UserId { get; set; }

    [JsonPropertyName("member")]
    public JsonGuildUser? User { get; set; }

    [JsonPropertyName("session_id")]
    public required string SessionId { get; set; }

    [JsonPropertyName("deaf")]
    public required bool IsDeafened { get; set; }

    [JsonPropertyName("mute")]
    public required bool IsMuted { get; set; }

    [JsonPropertyName("self_deaf")]
    public required bool IsSelfDeafened { get; set; }

    [JsonPropertyName("self_mute")]
    public required bool IsSelfMuted { get; set; }

    [JsonPropertyName("self_stream")]
    public bool? SelfStreamExists { get; set; }

    [JsonPropertyName("self_video")]
    public required bool SelfVideoExists { get; set; }

    [JsonPropertyName("suppress")]
    public required bool Suppressed { get; set; }

    [JsonPropertyName("request_to_speak_timestamp")]
    public DateTimeOffset? RequestToSpeakTimestamp { get; set; }
}

public class JsonPermissionOverwrite
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("type")]
    public required int Type { get; set; }

    [JsonPropertyName("allow")]
    public required ulong Allowed { get; set; }

    [JsonPropertyName("deny")]
    public required ulong Denied { get; set; }
}

public class JsonGuildThreadMetadata
{
    [JsonPropertyName("archived")]
    public required bool Archived { get; set; }

    [JsonPropertyName("auto_archive_duration")]
    public required int AutoArchiveDuration { get; set; }

    [JsonPropertyName("archive_timestamp")]
    public required DateTimeOffset ArchiveTimestamp { get; set; }

    [JsonPropertyName("locked")]
    public required bool Locked { get; set; }

    [JsonPropertyName("invitable")]
    public bool? Invitable { get; set; }

    [JsonPropertyName("create_timestamp")]
    public DateTimeOffset? CreatedAt { get; set; }
}

public class JsonThreadCurrentUser
{
    [JsonPropertyName("join_timestamp")]
    public required DateTimeOffset JoinTimestamp { get; set; }

    [JsonPropertyName("flags")]
    public required int Flags { get; set; }
}

public class JsonForumTag
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("moderated")]
    public required bool Moderated { get; set; }

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
    public required ulong Id { get; set; }

    [JsonPropertyName("guild_id")]
    public required ulong GuildId { get; set; }

    [JsonPropertyName("type")]
    public required int Type { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

public class JsonAttachment
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("filename")]
    public required string FileName { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("content_type")]
    public string? ContentType { get; set; }

    [JsonPropertyName("size")]
    public required long Size { get; set; }

    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("proxy_url")]
    public required string ProxyUrl { get; set; }

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
    public required int Flags { get; set; }

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
    public required int MembershipState { get; set; }

    [JsonPropertyName("team_id")]
    public required ulong TeamId { get; set; }

    [JsonPropertyName("user")]
    public required JsonUser User { get; set; }

    [JsonPropertyName("role")]
    public required string Role { get; set; }
}

public class JsonTeam
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("members")]
    public required JsonTeamUser[] Users { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("owner_user_id")]
    public required ulong OwnerId { get; set; }
}

public class JsonApplicationInstallParams
{
    [JsonPropertyName("scopes")]
    public required string[] Scopes { get; set; }

    [JsonPropertyName("permissions")]
    public required ulong Permissions { get; set; }
}

public class JsonApplicationIntegrationTypeConfiguration
{
    [JsonPropertyName("oauth2_install_params")]
    public JsonApplicationInstallParams? OAuth2InstallParams { get; set; }
}

public class JsonApplication
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("icon")]
    public string? IconHash { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("rpc_origins")]
    public required string[] RpcOrigins { get; set; }

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
    public required string VerifyKey { get; set; }

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
    public required int EventWebhooksStatus { get; set; }

    [JsonPropertyName("event_webhooks_types")]
    public string[]? EventWebhooksTypes { get; set; }

    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

    [JsonPropertyName("install_params")]
    public JsonApplicationInstallParams? InstallParams { get; set; }

    [JsonPropertyName("integration_types_config")]
    public required IReadOnlyDictionary<int, JsonApplicationIntegrationTypeConfiguration> IntegrationTypesConfiguration { get; set; }

    [JsonPropertyName("custom_install_url")]
    public string? CustomInstallUrl { get; set; }
}

public class JsonEmbedFooter
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }

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
    public required string Name { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }

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
    public required JsonEmbedField[] Fields { get; set; }
}

public class JsonMessageReactionCountDetails
{
    [JsonPropertyName("burst")]
    public required int Burst { get; set; }

    [JsonPropertyName("normal")]
    public required int Normal { get; set; }
}

public class JsonMessageReaction
{
    [JsonPropertyName("count")]
    public required int Count { get; set; }

    [JsonPropertyName("count_details")]
    public required JsonMessageReactionCountDetails CountDetails { get; set; }

    [JsonPropertyName("me")]
    public required bool Me { get; set; }

    [JsonPropertyName("me_burst")]
    public required bool MeBurst { get; set; }

    [JsonPropertyName("emoji")]
    public required JsonEmoji Emoji { get; set; }

    [JsonPropertyName("burst_colors")]
    public required int[] BurstColors { get; set; }
}

public class JsonMessageActivity
{
    [JsonPropertyName("type")]
    public required int Type { get; set; }

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
    public required JsonMessageSnapshotMessage Message { get; set; }
}

public class JsonMessageSnapshotMessage
{
    [JsonPropertyName("type")]
    public required int Type { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }

    [JsonPropertyName("embeds")]
    public required JsonEmbed[] Embeds { get; set; }

    [JsonPropertyName("attachments")]
    public required JsonAttachment[] Attachments { get; set; }

    [JsonPropertyName("timestamp")]
    public required DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("edited_timestamp")]
    public DateTimeOffset? EditedAt { get; set; }

    [JsonPropertyName("flags")]
    public uint? Flags { get; set; }

    [JsonPropertyName("mentions")]
    public required JsonUser[] MentionedUsers { get; set; }

    [JsonPropertyName("mention_roles")]
    public required ulong[] MentionedRoleIds { get; set; }

    [JsonPropertyName("sticker_items")]
    public JsonMessageSticker[]? Stickers { get; set; }
}

public class JsonMessageSticker
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("format_type")]
    public required int Format { get; set; }
}

public class JsonMessageInteractionMetadata
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("type")]
    public required int Type { get; set; }

    [JsonPropertyName("user")]
    public required JsonUser User { get; set; }

    [JsonPropertyName("authorizing_integration_owners")]
    public required IReadOnlyDictionary<int, ulong> AuthorizingIntegrationOwners { get; set; }

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
    public required ulong Id { get; set; }

    [JsonPropertyName("type")]
    public required int Type { get; set; }

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
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required int Type { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("created_at")]
    public required long CreatedAt { get; set; }

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
    public required JsonPresenceUser User { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("activities")]
    public JsonUserActivity[]? Activities { get; set; }

    [JsonPropertyName("client_status")]
    public required IReadOnlyDictionary<string, string> Platform { get; set; }
}

public class JsonPresenceUser
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

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
    public required ulong ChannelId { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

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
    public required JsonWelcomeScreenChannel[] WelcomeChannels { get; set; }
}

public class JsonStageInstance
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("guild_id")]
    public required ulong GuildId { get; set; }

    [JsonPropertyName("channel_id")]
    public required ulong ChannelId { get; set; }

    [JsonPropertyName("topic")]
    public required string Topic { get; set; }

    [JsonPropertyName("privacy_level")]
    public required int PrivacyLevel { get; set; }

    [JsonPropertyName("discoverable_disabled")]
    public required bool DiscoverableDisabled { get; set; }
}

public class JsonSticker
{
    [JsonPropertyName("id")]
    public required ulong Id { get; set; }

    [JsonPropertyName("pack_id")]
    public ulong? PackId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("tags")]
    public required string Tags { get; set; }

    [JsonPropertyName("format_type")]
    public required int Format { get; set; }

    [JsonPropertyName("available")]
    public bool? Available { get; set; }

    [JsonPropertyName("guild_id")]
    public required ulong GuildId { get; set; }

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
    public required int N { get; set; }

    [JsonPropertyName("day")]
    public required byte Day { get; set; }
}

public class JsonGuildScheduledEventRecurrenceRule
{
    [JsonPropertyName("start")]
    public DateTimeOffset? StartAt { get; set; }

    [JsonPropertyName("end")]
    public DateTimeOffset? EndAt { get; set; }

    [JsonPropertyName("frequency")]
    public required byte Frequency { get; set; }

    [JsonPropertyName("interval")]
    public required int Interval { get; set; }

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
    public required ulong Id { get; set; }

    [JsonPropertyName("guild_id")]
    public required ulong GuildId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }

    [JsonPropertyName("creator_id")]
    public ulong? CreatorId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("scheduled_start_time")]
    public required DateTimeOffset ScheduledStartTime { get; set; }

    [JsonPropertyName("scheduled_end_time")]
    public DateTimeOffset? ScheduledEndTime { get; set; }

    [JsonPropertyName("privacy_level")]
    public required int PrivacyLevel { get; set; }

    [JsonPropertyName("status")]
    public required int Status { get; set; }

    [JsonPropertyName("entity_type")]
    public required int EntityType { get; set; }

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
    public required ulong Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

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
    public required ulong OwnerId { get; set; }

    [JsonPropertyName("permissions")]
    public ulong? Permissions { get; set; }

    [JsonPropertyName("afk_channel_id")]
    public ulong? AfkChannelId { get; set; }

    [JsonPropertyName("afk_timeout")]
    public required int AfkTimeout { get; set; }

    [JsonPropertyName("widget_enabled")]
    public bool? WidgetEnabled { get; set; }

    [JsonPropertyName("widget_channel_id")]
    public ulong? WidgetChannelId { get; set; }

    [JsonPropertyName("verification_level")]
    public required int VerificationLevel { get; set; }

    [JsonPropertyName("default_message_notifications")]
    public required int DefaultMessageNotificationLevel { get; set; }

    [JsonPropertyName("explicit_content_filter")]
    public required int ContentFilter { get; set; }

    [JsonPropertyName("roles")]
    public required JsonRole[] Roles { get; set; }

    [JsonPropertyName("emojis")]
    public required JsonEmoji[] Emojis { get; set; }

    [JsonPropertyName("features")]
    public required string[] Features { get; set; }

    [JsonPropertyName("mfa_level")]
    public required int MfaLevel { get; set; }

    [JsonPropertyName("application_id")]
    public ulong? ApplicationId { get; set; }

    [JsonPropertyName("system_channel_id")]
    public ulong? SystemChannelId { get; set; }

    [JsonPropertyName("system_channel_flags")]
    public required int SystemChannelFlags { get; set; }

    [JsonPropertyName("rules_channel_id")]
    public ulong? RulesChannelId { get; set; }

    [JsonPropertyName("joined_at")]
    public required DateTimeOffset JoinedAt { get; set; }

    [JsonPropertyName("large")]
    public required bool IsLarge { get; set; }

    [JsonPropertyName("unavailable")]
    public bool? IsUnavailable { get; set; }

    [JsonPropertyName("member_count")]
    public required int UserCount { get; set; }

    [JsonPropertyName("voice_states")]
    public required JsonVoiceState[] VoiceStates { get; set; }

    [JsonPropertyName("members")]
    public required JsonGuildUser[] Users { get; set; }

    [JsonPropertyName("channels")]
    public required JsonChannel[] Channels { get; set; }

    [JsonPropertyName("threads")]
    public required JsonChannel[] ActiveThreads { get; set; }

    [JsonPropertyName("presences")]
    public required JsonPresence[] Presences { get; set; }

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
    public required int PremiumTier { get; set; }

    [JsonPropertyName("premium_subscription_count")]
    public int? PremiumSubscriptionCount { get; set; }

    [JsonPropertyName("preferred_locale")]
    public required string PreferredLocale { get; set; }

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
    public required int NsfwLevel { get; set; }

    [JsonPropertyName("stage_instances")]
    public required JsonStageInstance[] StageInstances { get; set; }

    [JsonPropertyName("stickers")]
    public required JsonSticker[] Stickers { get; set; }

    [JsonPropertyName("guild_scheduled_events")]
    public required JsonGuildScheduledEvent[] ScheduledEvents { get; set; }

    [JsonPropertyName("premium_progress_bar_enabled")]
    public required bool PremiumProgressBarEnabled { get; set; }

    [JsonPropertyName("safety_alerts_channel_id")]
    public ulong? SafetyAlertsChannelId { get; set; }
}
