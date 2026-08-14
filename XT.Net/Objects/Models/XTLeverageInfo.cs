using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models;

/// <summary>
/// Leverage info
/// </summary>
public record XTLeverageInfo
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>accountId</c>"] Account id
    /// </summary>
    [JsonPropertyName("accountId")]
    public string? AccountId { get; set; }
    /// <summary>
    /// ["<c>positionType</c>"] Position type
    /// </summary>
    [JsonPropertyName("positionType")]
    public PositionType PositionType { get; set; }
    /// <summary>
    /// ["<c>positionSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("positionSide")]
    public PositionSide PositionSide { get; set; }
    /// <summary>
    /// ["<c>contractType</c>"] Product type
    /// </summary>
    [JsonPropertyName("contractType")]
    public ProductType ProductType { get; set; }
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
}

