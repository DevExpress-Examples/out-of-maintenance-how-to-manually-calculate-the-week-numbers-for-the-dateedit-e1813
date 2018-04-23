using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.ComponentModel;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System;

namespace DXSample {
    public class MyDateEdit : DateEdit {
        static MyDateEdit() { RepositoryItemMyDateEdit.RegisterMyDateEdit(); }
        public MyDateEdit() : base() { }

        public override string EditorTypeName { get { return RepositoryItemMyDateEdit.MyDateEditName; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMyDateEdit Properties { get { return (RepositoryItemMyDateEdit)base.Properties; } }

        protected override PopupBaseForm CreatePopupForm() {
            if (!Properties.IsVistaDisplayModeInternal)
                return new MyPopupDateEditForm(this);
            return base.CreatePopupForm();
        }
    }

    [UserRepositoryItem("RegisterMyDateEdit")]
    public class RepositoryItemMyDateEdit : RepositoryItemDateEdit {
        static RepositoryItemMyDateEdit() { RegisterMyDateEdit(); }
        public RepositoryItemMyDateEdit() : base() { }

        internal const string MyDateEditName = "MyDateEdit";

        public override string EditorTypeName { get { return MyDateEditName; } }

        public static void RegisterMyDateEdit() {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(MyDateEditName, typeof(MyDateEdit),
                typeof(RepositoryItemMyDateEdit), typeof(DateEditViewInfo), new ButtonEditPainter(), true));
        }

        internal bool IsVistaDisplayModeInternal { get { return IsVistaDisplayMode(); } }
    }

    public class MyPopupDateEditForm : PopupDateEditForm {
        public MyPopupDateEditForm(DateEdit ownerEdit) : base(ownerEdit) { }

        protected override DateEditCalendar CreateCalendar() {
            return new MyDateEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue);
        }
    }

    public class MyDateEditCalendar : DateEditCalendar {
        public MyDateEditCalendar(RepositoryItemDateEdit item, object editDate) : base(item, editDate) { }

        protected override DateEditInfoArgs CreateInfoArgs() {
            return new MyDateEditInfoArgs(this);
        }
    }

    public class MyDateEditInfoArgs : DateEditInfoArgs {
        public MyDateEditInfoArgs(MyDateEditCalendar calendar) : base(calendar) { }

        protected override int GetWeekNumber(DateTime date) {
            DateTime baseDate = date.Month < 10 ? new DateTime(date.Year - 1, 10, 1) : new DateTime(date.Year, 10, 1);
            return (int)Math.Floor((double)(new DateTime(date.Ticks - baseDate.Ticks).DayOfYear / 7));
        }
    }
}