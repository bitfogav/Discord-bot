﻿using DiscordPlayerCountBot.Http.QueryParams.Base;
using DiscordPlayerCountBot.Json;
using System.Net.Http.Headers;

namespace DiscordPlayerCountBot.Http;

public class HttpExecuter : IHttpExecuter, IDisposable
{
    public readonly HttpClient HttpClient = new();

    public void Dispose()
    {
        HttpClient?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<TResponse?> GET<TRequest, TResponse>(string endPoint, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null, Dictionary<string, string>? additionalHeaders = null)
    {
        return await ExecuteHttpRequest<TRequest, TResponse>(endPoint, HttpMethod.Get, queryBuilder, body, authToken, additionalHeaders);
    }

    public async Task<TResponse?> POST<TRequest, TResponse>(string endPoint, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null)
    {
        return await ExecuteHttpRequest<TRequest, TResponse>(endPoint, HttpMethod.Post, queryBuilder, body, authToken);
    }

    public async Task<TResponse?> PATCH<TRequest, TResponse>(string endPoint, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null)
    {
        return await ExecuteHttpRequest<TRequest, TResponse>(endPoint, HttpMethod.Patch, queryBuilder, body, authToken);
    }

    public async Task<TResponse?> PUT<TRequest, TResponse>(string endPoint, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null)
    {
        return await ExecuteHttpRequest<TRequest, TResponse>(endPoint, HttpMethod.Put, queryBuilder, body, authToken);
    }

    public async Task<TResponse?> DELETE<TRequest, TResponse>(string endPoint, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null)
    {
        return await ExecuteHttpRequest<TRequest, TResponse>(endPoint, HttpMethod.Delete, queryBuilder, body, authToken);
    }

    private async Task<TResponse?> ExecuteHttpRequest<TRequest, TResponse>(string endPoint, HttpMethod method, IQueryParameterBuilder? queryBuilder = null, TRequest? body = default, Tuple<string, string>? authToken = null, Dictionary<string, string>? additionalHeaders = null)
    {
        if (HttpClient == null) return default;

        var queryParams = queryBuilder?.CreateQueryParameterString();
        var fullPath = $"{endPoint}{queryParams}";

        using var request = new HttpRequestMessage(method, fullPath);

        if (authToken != null)
            request.Headers.Add(authToken?.Item1!, authToken?.Item2);

        if (additionalHeaders != null)
        {
            foreach (var item in additionalHeaders)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        if (body != null)
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await HttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonHelper.DeserializeObject<TResponse>(jsonString);
    }
}