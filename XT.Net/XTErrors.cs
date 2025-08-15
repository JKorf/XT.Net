using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT.Net
{
    internal static class XTErrors
    {
        public static ErrorCollection SpotErrors { get; } = new ErrorCollection([
            
            new ErrorInfo(ErrorType.Unauthorized, false, "API key does not exist", "AUTH_101"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key not active", "AUTH_102"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "AUTH_104"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "AUTH_106"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Symbol trading not allowed based on region", "SYMBOL_004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Symbol trading not allowed via API", "SYMBOL_005"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Account security too low", "DEPOSIT_002", "WITHDRAW_003"),

            new ErrorInfo(ErrorType.InvalidSignature, false, "Signature error", "AUTH_103"),

            new ErrorInfo(ErrorType.SystemError, true, "System busy", "COMMON_002"),
            new ErrorInfo(ErrorType.SystemError, true, "Operation failed", "COMMON_003"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Timestamp expired", "AUTH_105"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Order price or quantity decimal price invalid", "ORDER_008"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Withdraw quantity precision invalid", "WITHDRAW_010"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Address invalid", "DEPOSIT_003", "WITHDRAW_002"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Memo must be a number", "WITHDRAW_018"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Memo invalid", "WITHDRAW_019"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "FUND_017"),

            new ErrorInfo(ErrorType.MissingParameter, false, "Withdrawal address can't be empty", "WITHDRAW_005"),
            new ErrorInfo(ErrorType.MissingParameter, false, "Memo can't be empty", "WITHDRAW_006"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price too low", "ORDER_F0101"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price too high", "ORDER_F0102"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price deviates too much from current price", "ORDER_F0501"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price step invalid", "ORDER_F0103"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too low", "ORDER_F0201", "WITHDRAW_022"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too high", "ORDER_F0202", "WITHDRAW_023"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity step invalid", "ORDER_F0203"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order value too low", "ORDER_F0301"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol does not exist", "SYMBOL_001"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "ORDER_005"),

            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is offline", "SYMBOL_002"),
            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol trading is suspended", "SYMBOL_003", "ORDER_003"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "ORDER_002", "WITHDRAW_011", "FUND_002", "TRANSFER_002"),

            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open orders", "ORDER_006"),

            ]);

        public static ErrorCollection FuturesErrors { get; } = new ErrorCollection([

            new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized", "400"),

            new ErrorInfo(ErrorType.InvalidSignature, false, "Invalid signature", "403"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol does not exist", "invalid_symbol"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "insufficient_balance"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Invalid quantity", "invalid_quantity"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity too low", "open_order_min_nominal_value_limit"),


            ]);
    }
}
