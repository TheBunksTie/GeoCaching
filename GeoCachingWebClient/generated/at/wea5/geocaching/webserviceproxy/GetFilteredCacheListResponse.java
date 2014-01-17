
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse für anonymous complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="GetFilteredCacheListResult" type="{http://GeoCaching.Services/}ArrayOfCache" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "getFilteredCacheListResult"
})
@XmlRootElement(name = "GetFilteredCacheListResponse")
public class GetFilteredCacheListResponse {

    @XmlElement(name = "GetFilteredCacheListResult")
    protected ArrayOfCache getFilteredCacheListResult;

    /**
     * Ruft den Wert der getFilteredCacheListResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfCache }
     *     
     */
    public ArrayOfCache getGetFilteredCacheListResult() {
        return getFilteredCacheListResult;
    }

    /**
     * Legt den Wert der getFilteredCacheListResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfCache }
     *     
     */
    public void setGetFilteredCacheListResult(ArrayOfCache value) {
        this.getFilteredCacheListResult = value;
    }

}
