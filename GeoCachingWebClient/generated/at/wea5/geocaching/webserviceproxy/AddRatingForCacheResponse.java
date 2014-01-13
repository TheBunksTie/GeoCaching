
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
 *         &lt;element name="AddRatingForCacheResult" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
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
    "addRatingForCacheResult"
})
@XmlRootElement(name = "AddRatingForCacheResponse")
public class AddRatingForCacheResponse {

    @XmlElement(name = "AddRatingForCacheResult")
    protected boolean addRatingForCacheResult;

    /**
     * Ruft den Wert der addRatingForCacheResult-Eigenschaft ab.
     * 
     */
    public boolean isAddRatingForCacheResult() {
        return addRatingForCacheResult;
    }

    /**
     * Legt den Wert der addRatingForCacheResult-Eigenschaft fest.
     * 
     */
    public void setAddRatingForCacheResult(boolean value) {
        this.addRatingForCacheResult = value;
    }

}
