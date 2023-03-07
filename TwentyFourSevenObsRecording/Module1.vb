Imports System.IO
Imports System.Diagnostics
Imports System.Timers

Module Module1
    Const RECORDING_TIME_BOX = 3600 * 1000
    Const OVERLAP_PERIOD = 60 * 1000

    Sub startProcess(Timeout As Long)
        Dim Process As New Process()
        Process.StartInfo.FileName = "obs64.exe"
        Process.StartInfo.Arguments = "--startrecording -m"
        Process.Start()

        System.Threading.Thread.Sleep(Timeout)
        Process.Kill()
    End Sub

    Function MillisecondsToNextHour() As Long
        Dim CurrentTime As DateTime = DateTime.Now
        Dim NextHour As DateTime = CurrentTime.AddHours(1).Date.AddHours(CurrentTime.Hour + 1)
        Dim Timeout As Long = (NextHour - CurrentTime).TotalMilliseconds
        Return Timeout
    End Function

    Sub Main(args As String())
        Dim Timeout As Long = MillisecondsToNextHour()

        Do
            Dim Thread As New System.Threading.Thread(Sub() startProcess(Timeout))
            Thread.Start()

            Timeout = RECORDING_TIME_BOX
            System.Threading.Thread.Sleep(Timeout - OVERLAP_PERIOD)
        Loop

    End Sub
End Module
