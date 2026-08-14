using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models;

/// <summary>
/// User trade
/// </summary>
public record XTUserTradeDetails
{
    /// <summary>
    /// ["<c>orderId</c>"] Order id
    /// </summary>
    [JsonPropertyName("orderId")]
    public long OrderId { get; set; }
    /// <summary>
    /// ["<c>execId</c>"] Trade id
    /// </summary>
    [JsonPropertyName("execId")]
    public long TradeId { get; set; }
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>contractSize</c>"] Contract size
    /// </summary>
    [JsonPropertyName("contractSize")]
    public decimal ContractSize { get; set; }
    /// <summary>
    /// ["<c>quantity</c>"] Trade quantity
    /// </summary>
    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>price</c>"] Price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    /// <summary>
    /// ["<c>couponDeductFee</c>"] Coupon deduct fee
    /// </summary>
    [JsonPropertyName("couponDeductFee")]
    public decimal CouponDeductFee { get; set; }
    /// <summary>
    /// ["<c>bonusDeductFee</c>"] Bonus deduct fee
    /// </summary>
    [JsonPropertyName("bonusDeductFee")]
    public decimal BonusDeductFee { get; set; }
    /// <summary>
    /// ["<c>feeCoin</c>"] Fee asset
    /// </summary>
    [JsonPropertyName("feeCoin")]
    public string FeeAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>timestamp</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>takerMaker</c>"] Taker maker
    /// </summary>
    [JsonPropertyName("takerMaker")]
    public TradeRole TakerMaker { get; set; }
    /// <summary>
    /// ["<c>orderSide</c>"] Order side
    /// </summary>
    [JsonPropertyName("orderSide")]
    public OrderSide OrderSide { get; set; }
    /// <summary>
    /// ["<c>positionSide</c>"] Position side
    /// </summary>
    [JsonPropertyName("positionSide")]
    public PositionSide PositionSide { get; set; }
}

