
package at.wea5.geocaching.webserviceproxy;

import java.net.MalformedURLException;
import java.net.URL;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;
import javax.xml.ws.WebEndpoint;
import javax.xml.ws.WebServiceClient;
import javax.xml.ws.WebServiceException;
import javax.xml.ws.WebServiceFeature;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.2.8
 * Generated source version: 2.2
 * 
 */
@WebServiceClient(name = "GeoCachingService", targetNamespace = "http://GeoCaching.Services/", wsdlLocation = "http://localhost:2037/GeoCachingService.asmx?wsdl")
public class GeoCachingService
    extends Service
{

    private final static URL GEOCACHINGSERVICE_WSDL_LOCATION;
    private final static WebServiceException GEOCACHINGSERVICE_EXCEPTION;
    private final static QName GEOCACHINGSERVICE_QNAME = new QName("http://GeoCaching.Services/", "GeoCachingService");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://localhost:2037/GeoCachingService.asmx?wsdl");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        GEOCACHINGSERVICE_WSDL_LOCATION = url;
        GEOCACHINGSERVICE_EXCEPTION = e;
    }

    public GeoCachingService() {
        super(__getWsdlLocation(), GEOCACHINGSERVICE_QNAME);
    }

    public GeoCachingService(WebServiceFeature... features) {
        super(__getWsdlLocation(), GEOCACHINGSERVICE_QNAME, features);
    }

    public GeoCachingService(URL wsdlLocation) {
        super(wsdlLocation, GEOCACHINGSERVICE_QNAME);
    }

    public GeoCachingService(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, GEOCACHINGSERVICE_QNAME, features);
    }

    public GeoCachingService(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public GeoCachingService(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    /**
     * 
     * @return
     *     returns GeoCachingServiceSoap
     */
    @WebEndpoint(name = "GeoCachingServiceSoap")
    public GeoCachingServiceSoap getGeoCachingServiceSoap() {
        return super.getPort(new QName("http://GeoCaching.Services/", "GeoCachingServiceSoap"), GeoCachingServiceSoap.class);
    }

    /**
     * 
     * @param features
     *     A list of {@link javax.xml.ws.WebServiceFeature} to configure on the proxy.  Supported features not in the <code>features</code> parameter will have their default values.
     * @return
     *     returns GeoCachingServiceSoap
     */
    @WebEndpoint(name = "GeoCachingServiceSoap")
    public GeoCachingServiceSoap getGeoCachingServiceSoap(WebServiceFeature... features) {
        return super.getPort(new QName("http://GeoCaching.Services/", "GeoCachingServiceSoap"), GeoCachingServiceSoap.class, features);
    }

    private static URL __getWsdlLocation() {
        if (GEOCACHINGSERVICE_EXCEPTION!= null) {
            throw GEOCACHINGSERVICE_EXCEPTION;
        }
        return GEOCACHINGSERVICE_WSDL_LOCATION;
    }

}
