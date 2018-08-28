Imports System.Net
Imports System.Net.Http

Public Class GhostBinProvider
    Implements IDisposable
    Private ReadOnly _client As HttpClient
    Dim nCookies As New CookieContainer
    Public Sub New()
        Dim handler = New HttpClientHandler() With {
            .AllowAutoRedirect = False,
            .CookieContainer = nCookies,
           .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        }

        _client = New HttpClient(handler)
        _client.BaseAddress = New Uri("http://x-booter.xyz/")

        _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml")
        _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36")
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        _client.Dispose()
    End Sub

    Public Async Function Register() As Task(Of String)

        ' Set the authentication cookie so GhostBin can verify that we've been on the homepage once.
        Await _client.GetAsync(_client.BaseAddress)
        Dim rnd As New Random
        Dim passwords As String = rnd.Next(1000000, 99999999).ToString
        Dim request = New HttpRequestMessage(HttpMethod.Post, "/booter/register.php") With {
            .Content = New FormUrlEncodedContent(New Dictionary(Of String, String)() From {
                {"register-username", "Hacked" & rnd.Next(100000, 999999)},
                {"register-email", "Hacked/Scammer" & rnd.Next(100000, 999999).ToString & "@gmail.com"},
                {"register-password", passwords},
                {"register-password2", passwords},
                {"register-terms", "on"},
                {"doCreate", "create"}
            })
        }

        ' This method retrives the location header and redirects us to that page. 
        ' Since we've set AllowAutoRedirect to false on L18, it doesn't redirect 
        ' us and instead handles just the response. 
        Dim response = Await _client.SendAsync(request)
        Return Await response.Content.ReadAsStringAsync
    End Function

End Class