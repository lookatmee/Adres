using System.Text;
using System.Text.Json;

namespace Adres.Web.Services;

public interface IApiService
{
    Task<T?> GetAsync<T>(string endpoint);
    Task<List<T>?> GetListAsync<T>(string endpoint);
    Task<T?> PostAsync<T>(string endpoint, object data);
    Task<T?> PutAsync<T>(string endpoint, object data);
    Task<bool> DeleteAsync(string endpoint);
    Task PatchAsync(string endpoint);
}

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService(IConfiguration configuration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, _jsonOptions);
    }

    public async Task<List<T>?> GetListAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<T>>(content, _jsonOptions);
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
    }

    public async Task<T?> PutAsync<T>(string endpoint, object data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            
            response.EnsureSuccessStatusCode();
            
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return default;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error en la petición HTTP: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception($"Error al procesar la respuesta JSON: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error inesperado: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        return response.IsSuccessStatusCode;
    }

    public async Task PatchAsync(string endpoint)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, endpoint);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error en la petición HTTP: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error inesperado: {ex.Message}", ex);
        }
    }
} 