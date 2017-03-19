
namespace MyLibrary {
    public class AllNewsPM : BasicWindowPM, IAllNewsPM {
        public const string NEWS_DIMISSED_EVENT = "AllNewsDismissed";

        readonly IMessageService mMessenger;

        public AllNewsPM( IMessageService i_messenger ) {
            mMessenger = i_messenger;

            SetVisibleProperty( false );
        }

        protected override void OnHidden() {
            mMessenger.Send( NEWS_DIMISSED_EVENT );
        }
    }
}
