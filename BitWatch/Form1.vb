
Public Class Form1

    'Initialize Global variables
    Public Class GlobalVariables
        Public Shared JsonReader As String
        Public Shared mybitxZARask As Double

    End Class
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateExcRate()
        Dim read = Newtonsoft.Json.Linq.JObject.Parse(GlobalVariables.JsonReader)
        Label5.Text = read.Item("rates")("ZAR").ToString
        updateBitstampvalues()
        updateMybitxvalues()
        updatecexiovalues()
        Label16.Text = DateTime.Now
        Dim moment = DateTime.Now
        Dim second As Integer = moment.Second
        Do Until second = 0 Or
            second = 10 Or
            second = 20 Or
            second = 30 Or
            second = 40 Or
            second = 50
            moment = DateTime.Now
            second = moment.Second
        Loop

        Timer1.Enabled = True
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Bought Price Input
        Dim MoneyString As String = InputBox("Enter ZAR Value", "How much did you buy?", "Please Enter Value")
        Dim boughtZAR As Double
        boughtZAR = Format(Val(MoneyString))
        Label10.Text = boughtZAR.ToString("N2")
        ' How much Bitcoin did you get for this purchase?
        Dim BitString As String = InputBox("Enter BTC Value", "How much Bitcoin did you get?", "Please Enter Value")
        Dim boughtBTC As Double
        boughtBTC = Format(Val(BitString))
        ' How much bitcoin is left after your 1% trade fee on LUNO?
        boughtBTC = boughtBTC - (boughtBTC * 1 / 100)
        MessageBox.Show("You have " & boughtBTC & "BTC Bitcoin left after 1% fee")
        ' What is 3% target price?
        Dim targetZAR As Double
        targetZAR = boughtZAR + (boughtZAR * 3 / 100)
        Label11.Text = targetZAR.ToString("N2")
        ' How much ZAR can you get now?
        Dim curZAR As Double
        curZAR = GlobalVariables.mybitxZARask
        curZAR = boughtBTC * curZAR
        Label12.Text = curZAR.ToString("N2")
    End Sub
    Private Sub InitializeTimer()
        ' Run this procedure in an appropriate event.  
        ' Set to 1 second.  
        'Timer1.Interval = 1000
        ' Enable timer.  

    End Sub

    Private Sub Timer1_Tick(ByVal Sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        ' Set the caption to the current time.
        updateBitstampvalues()
        updateMybitxvalues()
        updatecexiovalues()
        Label16.Text = DateTime.Now
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'If Button1.Text = "Stop" Then
        'Button1.Text = "Start"
        'Timer1.Enabled = False
        'Else
        'Button1.Text = "Stop"
        'Timer1.Enabled = True
        'End If
    End Sub

    Public Sub updateBitstampvalues()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Start of bitstamp.net calculations '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '
        bitstamp()
        Dim read_bitstamp = Newtonsoft.Json.Linq.JObject.Parse(GlobalVariables.JsonReader)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Get current bid price of BTC/USD pair 
        ' 
        Label3.Text = read_bitstamp.Item("bid").ToString
        ' Calculate dollar value of bitcoin
        ' Convert exchange rate text string to value for calculation
        ' Convert Rand rate text string to value for calculation
        Dim randValue As Double
        Dim dollarValue As Double
        randValue = Format(Val(Label3.Text)) * Format(Val(Label5.Text))
        GlobalVariables.mybitxZARask = randValue
        dollarValue = Format(Val(Label3.Text))
        ' Round to 2 decimal places
        Label3.Text = dollarValue.ToString("N2")
        Label4.Text = randValue.ToString("N2")
        '
        ' End of bid price calculations
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Get current ask price of ZAR/USD pair 
        ' 
        Label8.Text = read_bitstamp.Item("ask").ToString
        ' Calculate dollar value of bitcoin
        ' Convert exchange rate text string to value for calculation
        ' Convert Rand rate text string to value for calculation
        randValue = Format(Val(Label8.Text)) * Format(Val(Label5.Text))
        dollarValue = Format(Val(Label8.Text))
        ' Round to 2 decimal places
        Label8.Text = dollarValue.ToString("N2")
        Label9.Text = randValue.ToString("N2")
        '
        ' End of ask price calculations
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' 
        'End Of bitstamp.net calculations '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub
    Public Sub updateMybitxvalues()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '
        ' Start of mybitx.com calculations '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '        '
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '
        mybitx()
        Dim read_mybitx = Newtonsoft.Json.Linq.JObject.Parse(GlobalVariables.JsonReader)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Get current bid price of ZAR/BTC pair 
        ' 
        Label2.Text = read_mybitx.Item("bid").ToString
        ' Calculate dollar value of bitcoin
        ' Convert exchange rate text string to value for calculation
        ' Convert Rand rate text string to value for calculation
        Dim randValue As Double
        Dim dollarValue As Double
        randValue = Format(Val(Label2.Text))
        dollarValue = Format(Val(Label2.Text)) / Format(Val(Label5.Text))
        ' Round to 2 decimal places
        Label2.Text = randValue.ToString("N2")
        Label1.Text = dollarValue.ToString("N2")
        '
        ' End of bid price calculations
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Get current ask price of ZAR/BTC pair 
        ' 
        Label7.Text = read_mybitx.Item("ask").ToString
        ' Calculate dollar value of bitcoin
        ' Convert exchange rate text string to value for calculation
        ' Convert Rand rate text string to value for calculation
        randValue = Format(Val(Label7.Text))
        dollarValue = Format(Val(Label7.Text)) / Format(Val(Label5.Text))
        ' Round to 2 decimal places
        Label7.Text = randValue.ToString("N2")
        Label6.Text = dollarValue.ToString("N2")
        '
        ' End of ask price calculations
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' End of mybitx.com calculations''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub

    Public Sub updatecexiovalues()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '
        ' Start of cexio calculations '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '        '
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '
        cexio()
        Dim read_cexio = Newtonsoft.Json.Linq.JObject.Parse(GlobalVariables.JsonReader)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Get current bid price of BTC/USD pair 
        ' 
        Label17.Text = read_cexio.Item("bid").ToString
        ' Calculate dollar value of bitcoin
        ' Convert exchange rate text string to value for calculation
        ' Convert Rand rate text string to value for calculation
        Dim randValue As Double
        Dim dollarValue As Double
        randValue = Format(Val(Label17.Text)) * Format(Val(Label5.Text))
        GlobalVariables.mybitxZARask = randValue
        dollarValue = Format(Val(Label17.Text))
        ' Round to 2 decimal places
        Label17.Text = dollarValue.ToString("N2")
        Label18.Text = randValue.ToString("N2")
        '
        ' End of bid price calculations
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Get current ask price of ZAR/USD pair 
        ' 
        Label19.Text = read_cexio.Item("ask").ToString
        ' Calculate dollar value of bitcoin
        ' Convert exchange rate text string to value for calculation
        ' Convert Rand rate text string to value for calculation
        randValue = Format(Val(Label19.Text)) * Format(Val(Label5.Text))
        dollarValue = Format(Val(Label19.Text))
        ' Round to 2 decimal places
        Label19.Text = dollarValue.ToString("N2")
        Label20.Text = randValue.ToString("N2")
        '
        ' End of ask price calculations
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' End of mybitx.com calculations''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub

End Class



