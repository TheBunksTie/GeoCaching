using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.BusinessLogic.FilterManager {
    public interface IFilterManager {

        // returns a filter, which "filters" the whole cache-datataset  
        DataFilter GetDefaultFilter();
    }
}