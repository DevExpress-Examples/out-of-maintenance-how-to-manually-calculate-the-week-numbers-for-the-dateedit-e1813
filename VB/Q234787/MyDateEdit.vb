Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Calendar
Imports System

Namespace DXSample
	Public Class MyDateEdit
		Inherits DateEdit
		Shared Sub New()
			RepositoryItemMyDateEdit.RegisterMyDateEdit()
		End Sub
		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemMyDateEdit.MyDateEditName
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemMyDateEdit
			Get
				Return CType(MyBase.Properties, RepositoryItemMyDateEdit)
			End Get
		End Property

		Protected Overrides Function CreatePopupForm() As PopupBaseForm
			If (Not Properties.IsVistaDisplayModeInternal) Then
				Return New MyPopupDateEditForm(Me)
			End If
			Return MyBase.CreatePopupForm()
		End Function
	End Class

	<UserRepositoryItem("RegisterMyDateEdit")> _
	Public Class RepositoryItemMyDateEdit
		Inherits RepositoryItemDateEdit
		Shared Sub New()
			RegisterMyDateEdit()
		End Sub
		Public Sub New()
			MyBase.New()
		End Sub

		Friend Const MyDateEditName As String = "MyDateEdit"

		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return MyDateEditName
			End Get
		End Property

		Public Shared Sub RegisterMyDateEdit()
			EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(MyDateEditName, GetType(MyDateEdit), GetType(RepositoryItemMyDateEdit), GetType(DateEditViewInfo), New ButtonEditPainter(), True))
		End Sub

		Friend ReadOnly Property IsVistaDisplayModeInternal() As Boolean
			Get
				Return IsVistaDisplayMode()
			End Get
		End Property
	End Class

	Public Class MyPopupDateEditForm
		Inherits PopupDateEditForm
		Public Sub New(ByVal ownerEdit As DateEdit)
			MyBase.New(ownerEdit)
		End Sub

		Protected Overrides Function CreateCalendar() As DateEditCalendar
			Return New MyDateEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue)
		End Function
	End Class

	Public Class MyDateEditCalendar
		Inherits DateEditCalendar
		Public Sub New(ByVal item As RepositoryItemDateEdit, ByVal editDate As Object)
			MyBase.New(item, editDate)
		End Sub

		Protected Overrides Function CreateInfoArgs() As DateEditInfoArgs
			Return New MyDateEditInfoArgs(Me)
		End Function
	End Class

	Public Class MyDateEditInfoArgs
		Inherits DateEditInfoArgs
		Public Sub New(ByVal calendar As MyDateEditCalendar)
			MyBase.New(calendar)
		End Sub

		Protected Overrides Function GetWeekNumber(ByVal [date] As DateTime) As Integer
			Dim baseDate As DateTime
			If [date].Month < 10 Then
				baseDate = New DateTime([date].Year - 1, 10, 1)
			Else
				baseDate = New DateTime([date].Year, 10, 1)
			End If
			Return CInt(Fix(Math.Floor(CDbl(New DateTime([date].Ticks - baseDate.Ticks).DayOfYear / 7))))
		End Function
	End Class
End Namespace