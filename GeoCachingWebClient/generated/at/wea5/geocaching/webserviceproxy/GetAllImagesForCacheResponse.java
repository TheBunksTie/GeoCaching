
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
 *         &lt;element name="GetAllImagesForCacheResult" type="{http://GeoCaching.Services/}ArrayOfImage" minOccurs="0"/>
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
    "getAllImagesForCacheResult"
})
@XmlRootElement(name = "GetAllImagesForCacheResponse")
public class GetAllImagesForCacheResponse {

    @XmlElement(name = "GetAllImagesForCacheResult")
    protected ArrayOfImage getAllImagesForCacheResult;

    /**
     * Ruft den Wert der getAllImagesForCacheResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfImage }
     *     
     */
    public ArrayOfImage getGetAllImagesForCacheResult() {
        return getAllImagesForCacheResult;
    }

    /**
     * Legt den Wert der getAllImagesForCacheResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfImage }
     *     
     */
    public void setGetAllImagesForCacheResult(ArrayOfImage value) {
        this.getAllImagesForCacheResult = value;
    }

}
