
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse f�r anonymous complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="GetRatingForCacheResult" type="{http://www.w3.org/2001/XMLSchema}double"/>
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
    "getRatingForCacheResult"
})
@XmlRootElement(name = "GetRatingForCacheResponse")
public class GetRatingForCacheResponse {

    @XmlElement(name = "GetRatingForCacheResult")
    protected double getRatingForCacheResult;

    /**
     * Ruft den Wert der getRatingForCacheResult-Eigenschaft ab.
     * 
     */
    public double getGetRatingForCacheResult() {
        return getRatingForCacheResult;
    }

    /**
     * Legt den Wert der getRatingForCacheResult-Eigenschaft fest.
     * 
     */
    public void setGetRatingForCacheResult(double value) {
        this.getRatingForCacheResult = value;
    }

}
