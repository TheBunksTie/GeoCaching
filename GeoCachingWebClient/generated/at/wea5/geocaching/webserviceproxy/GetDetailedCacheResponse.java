
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
 *         &lt;element name="GetDetailedCacheResult" type="{http://GeoCaching.Services/}CacheDetails" minOccurs="0"/>
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
    "getDetailedCacheResult"
})
@XmlRootElement(name = "GetDetailedCacheResponse")
public class GetDetailedCacheResponse {

    @XmlElement(name = "GetDetailedCacheResult")
    protected CacheDetails getDetailedCacheResult;

    /**
     * Ruft den Wert der getDetailedCacheResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link CacheDetails }
     *     
     */
    public CacheDetails getGetDetailedCacheResult() {
        return getDetailedCacheResult;
    }

    /**
     * Legt den Wert der getDetailedCacheResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link CacheDetails }
     *     
     */
    public void setGetDetailedCacheResult(CacheDetails value) {
        this.getDetailedCacheResult = value;
    }

}
