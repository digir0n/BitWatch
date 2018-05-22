Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports BitWatch.Form1

Public Module ModuleExcRate
    Public Sub UpdateExcRate()

        ' Get multi curency exchange rates form open exchange rates online using free account
        ' Free account limited to 1000 requests per month
        ' ronald.de.villiers@gmail.com
        ' R0n@ld@deV
        ' App Id credentials. https://openexchangerates.org/account/app-ids
        ' Create a request for the URL. 		
        Dim request As WebRequest = WebRequest.Create("https://openexchangerates.org/api/latest.json?app_id=e3ed15fe54834c638cf69832e88ef4b3")
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
