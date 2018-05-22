Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports BitWatch.Form1

Public Module Modulebitstamp
    Public Sub bitstamp()
        ' Get current bitcoin exchange rates from bitstamp.net
        ' Free account limited to ? requests per month
        ' Create a request for the URL. 	
        Dim request As WebRequest = WebRequest.Create("https://www.bitstamp.net/api/v2/ticker/btcusd")
        ' Get the response.
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        ' Get the stream containing content returned by the server.
        Dim dataStream As Stream = response.GetResponseStream()
        ' Open the stream using a StreamReader for easy access.
        Dim reader As New StreamReader(dataStream)
        ' Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()
        ' Display the content.
        GlobalVariables.JsonReader = responseFromServer
        ' Cleanup the streams and the response.
        reader.Close()
        dataStream.Close()
        response.Close()
    End Sub

End Module
