Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports BitWatch.Form1

Public Module Modulecexio
    Public Sub cexio()
        ' Get current bitcoin exchange rates from mybitx.com 
        ' Calls to the Market Data APIs are rate limited to 1 call per 10 seconds
        ' Create a request for the URL. 
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim request As WebRequest = WebRequest.Create("https://cex.io/api/ticker/BTC/USD")
        ' Get the response.
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        ' Get the stream containing content returned by the server.
        Dim dataStream As Stream = response.GetResponseStream()
        ' Open the stream using a StreamReader for easy access.
        Dim reader As New StreamReader(dataStream)
        ' Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()
        ' Display the content.
        ' MessageBox.Show(responseFromServer)
        GlobalVariables.JsonReader = responseFromServer
        ' Cleanup the streams and the response.
        reader.Close()
        dataStream.Close()
        response.Close()
    End Sub

End Module
