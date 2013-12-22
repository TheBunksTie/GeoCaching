using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache.Filter {
    public class FilterCriteriumVM {
        private readonly FilterCriterium criterium;

        public FilterCriteriumVM(FilterCriterium criterium) {
            this.criterium = criterium;
        }

        public FilterCriterium Criterium {
            get { return criterium; }
        }

        public string Caption {
            get { return criterium.UiCaption(); }
        }
    }
}