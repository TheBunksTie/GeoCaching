using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Statistic {
    public class StatisticVM {
        private readonly StatisticalOperation operation;

        public StatisticVM(StatisticalOperation operation) {
            this.operation = operation;
        }

        public StatisticalOperation Operation {
            get { return operation; }
        }

        public string Caption {
            get { return operation.UiCaption(); }
        }
    }
}