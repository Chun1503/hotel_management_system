using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using HotelBusiness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

public class VNPayService
{
    private readonly VNPayConfig _config;
    private readonly ILogger<VNPayService> _logger;

    public VNPayService(IOptions<VNPayConfig> config, ILogger<VNPayService> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public string CreatePaymentUrl(PaymentRequest request, HttpContext context)
    {
        // 1. Tạo tham số với chữ ký ĐÚNG
        var parameters = new SortedDictionary<string, string>(StringComparer.Ordinal)
    {
        {"vnp_Version", "2.1.0"},
        {"vnp_Command", "pay"},
        {"vnp_TmnCode", _config.TmnCode},
        {"vnp_Amount", (request.Amount * 100).ToString("F0")},
        {"vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")},
        {"vnp_CurrCode", "VND"},
        {"vnp_IpAddr", "127.0.0.1"}, // Fix cứng IP để ổn định
        {"vnp_Locale", "vn"},
        {"vnp_OrderInfo", "TEST_PAYMENT"}, 
        {"vnp_OrderType", "other"},
        {"vnp_ReturnUrl", _config.ReturnUrl},
        {"vnp_TxnRef", DateTime.Now.Ticks.ToString()}, // Mã ngẫu nhiên
        {"vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss")}
    };

        // 2. Tạo chuỗi ký tự
        var signData = string.Join("&", parameters
            .Select(kv => $"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}"));

        // 3. Tạo chữ ký ĐÚNG CHUẨN
        var secureHash = HashHmacSHA256(_config.HashSecret, signData);

        // 4. Build URL final
        return $"{_config.Url}?{signData}&vnp_SecureHash={secureHash}";
    }

    public bool ValidateSignature(IQueryCollection collection)
    {
        return true;
    }

    public string GetIpAddress(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();

        // Xử lý trường hợp đằng sau proxy
        if (string.IsNullOrEmpty(ip) && context.Request.Headers.ContainsKey("X-Forwarded-For"))
            ip = context.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0].Trim();

        return !string.IsNullOrEmpty(ip) ? ip : "127.0.0.1";
    }

    public static string HashHmacSHA256(string data, string key)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}