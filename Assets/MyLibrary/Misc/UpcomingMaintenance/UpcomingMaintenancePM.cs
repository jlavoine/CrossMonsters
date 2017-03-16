using System;

namespace MyLibrary {
    public class UpcomingMaintenancePM : PresentationModel, IUpcomingMaintenancePM {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string CAN_DISMISS_PROPERTY = "CanDismiss";
        public const string BODY_TEXT_PROPERTY = "Body";
        public const string TITLE_TEXT_PROPERTY = "Title";

        public const string DISMISSED_EVENT = "MaintenancePM_Dismissed";

        public const string TITLE_KEY = "UpcomingMaintenance_Title";
        public const string BODY_KEY = "UpcomingMaintenance_Body";

        readonly IMessageService Messenger;
        readonly IUpcomingMaintenanceManager MaintenanceManager;
        readonly IStringTableManager StringTable;

        public UpcomingMaintenancePM( IUpcomingMaintenanceManager i_maintenanceManager, IMessageService i_messenger, IStringTableManager i_stringTable ) {
            Messenger = i_messenger;
            MaintenanceManager = i_maintenanceManager;
            StringTable = i_stringTable;

            ListenForMessages( true );
            SetVisibleProperty( false );
            SetCanDismissProperty( false );
        }

        public void Dismiss() {
            SendDismissEvent();
            SetVisibleProperty( false );
        }

        protected override void _Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                Messenger.AddListener<bool>( UpcomingMaintenanceManager.TRIGGER_MAINTENANCE_POPUP, OnTrigger );
            } else {
                Messenger.RemoveListener<bool>( UpcomingMaintenanceManager.TRIGGER_MAINTENANCE_POPUP, OnTrigger );
            }
        }

        public void OnTrigger( bool i_canDismiss ) {
            SetVisibleProperty( true );
            SetCanDismissProperty( i_canDismiss );
            SetTitleTextProperty();
            SetBodyTextProperty();
        }

        private void SendDismissEvent() {
            Messenger.Send( DISMISSED_EVENT );
        }

        private void SetBodyTextProperty() {
            string text = StringTable.Get( BODY_KEY );
            DateTime startTime = MaintenanceManager.GetMaintenanceStartTime();
            DateTime endTime = MaintenanceManager.GetMaintenanceEndTime();

            text = DrsStringUtils.Replace( text, "START", startTime.ToLocalTime().ToString() );
            text = DrsStringUtils.Replace( text, "END", endTime.ToLocalTime().ToString() );

            ViewModel.SetProperty( BODY_TEXT_PROPERTY, text );
        }

        private void SetTitleTextProperty() {
            string text = StringTable.Get( TITLE_KEY );
            ViewModel.SetProperty( TITLE_TEXT_PROPERTY, text );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }

        private void SetCanDismissProperty( bool i_canDismiss ) {
            ViewModel.SetProperty( CAN_DISMISS_PROPERTY, i_canDismiss );
        }
    }
}
