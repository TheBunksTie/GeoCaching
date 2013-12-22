using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache.Filter {
    public class FilterOperationVM {
        private readonly FilterOperation operation;

        public FilterOperationVM ( FilterOperation operation) {
            this.operation = operation;
        }

        public FilterOperation Operation{
            get { return operation; }
        }

        public string Caption {
            get { return operation.UiCaption(); }
        }
    }
}