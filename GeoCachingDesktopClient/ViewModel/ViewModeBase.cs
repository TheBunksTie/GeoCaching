using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Swk5.GeoCaching.Desktop.ViewModel {
    public abstract class AbstractViewModelBase<TViewModel> : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent<TProperty>(Expression<Func<TViewModel, TProperty>> propertySelector) {
            if (PropertyChanged != null) {
                var propertyExpression = propertySelector.Body as MemberExpression;
                string propertyName = propertyExpression.Member.Name;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        protected void Set<TProperty>(Expression<Func<TViewModel, TProperty>> propertySelector,
            ref TProperty currentValue,
            TProperty newValue) {
            if (!currentValue.Equals(newValue)) {
                currentValue = newValue;
                RaisePropertyChangedEvent(propertySelector);
            }
        }
    }
}