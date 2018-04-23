Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Namespace Q234787
    Partial Public Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub dateEdit1_Properties_CustomWeekNumber(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Repository.CustomWeekNumberEventArgs) Handles dateEdit1.Properties.CustomWeekNumber
            Dim [date] As Date = e.Date
            Dim baseDate As Date = If([date].Month < 10, New Date([date].Year - 1, 10, 1), New Date([date].Year, 10, 1))
            e.WeekNumber = CInt((Math.Floor(CDbl((New Date([date].Ticks - baseDate.Ticks)).DayOfYear \ 7))))
        End Sub
    End Class
End Namespace